using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SpaceWalker.Ipc;
using UnityEngine;
using UnityEngine.Networking;
using Viture.Ipc.Pubsub;
using Viture.Ipc.Rpc;
using Viture.Ipc.Transport;
using VitureCommonLibrary;
using uWindowCapture;

public class MainCamera : MonoBehaviour
{
	private enum RecenterMode
	{
		Recenter,
		Calibrate,
		Mouse
	}

	private sealed class RecenterRequest
	{
		public RecenterMode Mode;

		public TaskCompletionSource<RecenterResult> Tcs;
	}

	private sealed class RecenterServiceImpl : RecenterService
	{
		private readonly MainCamera _owner;

		public RecenterServiceImpl(MainCamera owner)
		{
			_owner = owner;
		}

		public override Task<RecenterResult> RecenterAsync(CallContext ctx, RecenterSource source, CancellationToken ct)
		{
			return _owner.EnqueueRecenter(RecenterMode.Recenter);
		}

		public override Task<RecenterResult> RecenterMouseAsync(CallContext ctx, CancellationToken ct)
		{
			return _owner.EnqueueRecenter(RecenterMode.Mouse);
		}
	}

	private Channel _channel;

	private RpcServer _rpcServer;

	private bool rayHitRequest;

	private readonly ConcurrentQueue<RecenterRequest> _recenterQueue = new ConcurrentQueue<RecenterRequest>();

	private RecenterRequest _inFlightRecenter;

	[SerializeField]
	public GameObject Screens;

	private GameObject wideDisplayObj;

	private UwcWindowTexture wideDisplay;

	private GameObject wide24_9DisplayObj;

	private UwcWindowTexture wide24_9Display;

	[SerializeField]
	private DeviceManager deviceManager;

	private string _glassesModel = "N6";

	public const float DefaultScreenDistance = 1f;

	public float ScreenDistance = 1f;

	private volatile string setSkyboxFile;

	public string SkyboxFile;

	private readonly List<Subscription> _typedSubs = new List<Subscription>();

	private bool _handResizing;

	private float _baseScreenDistance = 1f;

	private ScreenDistanceHUD _screenDistanceHUD;

	public Channel Channel => _channel;

	private void Awake()
	{
		InitParam();
		FindVdd();
		InitIpc();
		Application.targetFrameRate = (_glassesModel.Contains("Native3Dof") ? 60 : 120);
		HandJointsVisualizer.HandPoseChanged += OnHandResize;
		_screenDistanceHUD = base.gameObject.AddComponent<ScreenDistanceHUD>();
	}

	private void InitParam()
	{
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		for (int i = 0; i < commandLineArgs.Length; i++)
		{
			if (commandLineArgs[i] == "-glassesModel" && i + 1 < commandLineArgs.Length)
			{
				string glassesModel = commandLineArgs[i + 1];
				_glassesModel = glassesModel;
				VitureCommonLibrary.Logger.Info("_glassesModel: " + _glassesModel);
				if (_glassesModel.Contains("Native3Dof"))
				{
					base.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
				}
			}
			if (!_glassesModel.Contains("Native3Dof") && commandLineArgs[i] == "-skybox" && i + 1 < commandLineArgs.Length)
			{
				string skybox = commandLineArgs[i + 1];
				SetSkybox(skybox);
			}
		}
	}

	public async Task SetSkybox(string imageFile = "")
	{
		SkyboxFile = imageFile;
		string text = (File.Exists(imageFile) ? imageFile : Path.Combine(Application.streamingAssetsPath, "Skybox", imageFile));
		if (string.IsNullOrWhiteSpace(imageFile) || !File.Exists(text))
		{
			RenderSettings.skybox = null;
			Camera.main.backgroundColor = Color.black;
			Camera.main.clearFlags = CameraClearFlags.Color;
			return;
		}
		using UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file://" + text);
		await uwr.SendWebRequest();
		if (uwr.result == UnityWebRequest.Result.Success)
		{
			Texture2D content = DownloadHandlerTexture.GetContent(uwr);
			ApplySkybox(content);
		}
	}

	private void ApplySkybox(Texture2D texture)
	{
		Material material = new Material(Shader.Find("Skybox/Panoramic"));
		material.SetTexture("_MainTex", texture);
		Camera.main.clearFlags = CameraClearFlags.Skybox;
		RenderSettings.skybox = material;
		DynamicGI.UpdateEnvironment();
	}

	private void InitIpc()
	{
		TeardownIpc();
		Channel channel2 = new Channel($"spacewalker.unity.{Process.GetCurrentProcess().Id}");
		channel2.FrameError += delegate(string channel, string reason)
		{
			VitureCommonLibrary.Logger.Warning("IPC framer: channel=" + channel + " reason=" + reason);
		};
		_typedSubs.Add(Topics.LockAxis.Subscribe(channel2, OnLockAxis));
		_typedSubs.Add(Topics.Film.Subscribe(channel2, delegate(FilmAngleValue v)
		{
			deviceManager?.UpdateFilmAngle(v.Value);
		}));
		_typedSubs.Add(Topics.Duty.Subscribe(channel2, delegate(DutyValue v)
		{
			deviceManager?.SetDutyCmd(v.Value);
		}));
		_typedSubs.Add(Topics.Zoom.Subscribe(channel2, OnZoomStep));
		_typedSubs.Add(Topics.SetZoom.Subscribe(channel2, OnSetZoom));
		_typedSubs.Add(Topics.SetBrightness.Subscribe(channel2, delegate(BrightnessValue v)
		{
			GlassesDeviceManager.Instance.SetBrightness(v.Value);
		}));
		_typedSubs.Add(Topics.SetVolume.Subscribe(channel2, delegate(VolumeValue v)
		{
			GlassesDeviceManager.Instance.SetVolume(v.Value);
		}));
		_typedSubs.Add(Topics.Skybox.Subscribe(channel2, delegate(SkyboxPath p)
		{
			setSkyboxFile = p.Path;
		}));
		_typedSubs.Add(Topics.HandTrack.Subscribe(channel2, OnHandTrack));
		_typedSubs.Add(Topics.SmoothFollow.Subscribe(channel2, delegate(SmoothFollowValue v)
		{
			deviceManager?.SetSmoothFollow(v.Value);
		}));
		_rpcServer = new RpcServer(channel2);
		new RecenterServiceImpl(this).Bind(_rpcServer);
		_channel = channel2;
	}

	private void TeardownIpc()
	{
		foreach (Subscription typedSub in _typedSubs)
		{
			typedSub.Dispose();
		}
		_typedSubs.Clear();
		if (_rpcServer != null)
		{
			_rpcServer.Dispose();
			_rpcServer = null;
		}
		if (_channel != null)
		{
			_channel.Dispose();
			_channel = null;
		}
		FailPendingRecenters();
	}

	private void FailPendingRecenters()
	{
		RecenterRequest inFlightRecenter = _inFlightRecenter;
		_inFlightRecenter = null;
		inFlightRecenter?.Tcs.TrySetResult(new RecenterResult(string.Empty, 0.5f, 0.5f));
		RecenterRequest result;
		while (_recenterQueue.TryDequeue(out result))
		{
			result.Tcs.TrySetResult(new RecenterResult(string.Empty, 0.5f, 0.5f));
		}
	}

	private void OnLockAxis(LockAxisValue v)
	{
		deviceManager?.UpdateLockAxis(v.Value);
	}

	private void OnZoomStep(ZoomStep step)
	{
		if (step.Direction == ZoomDirection.ZoomIn)
		{
			if (ScreenDistance > 0.5f)
			{
				ScreenDistance -= 0.1f;
			}
		}
		else if (ScreenDistance < 2f)
		{
			ScreenDistance += 0.1f;
		}
		deviceManager?.UpdateDistance();
		PublishZoomEvent(ScreenDistance);
	}

	private void OnSetZoom(ZoomValue v)
	{
		ScreenDistance = (float)v.Zoom;
		deviceManager?.UpdateDistance();
	}

	private Task<RecenterResult> EnqueueRecenter(RecenterMode mode)
	{
		RecenterRequest recenterRequest = new RecenterRequest
		{
			Mode = mode,
			Tcs = new TaskCompletionSource<RecenterResult>(TaskCreationOptions.RunContinuationsAsynchronously)
		};
		_recenterQueue.Enqueue(recenterRequest);
		return recenterRequest.Tcs.Task;
	}

	private void PumpRecenterQueue()
	{
		if (_inFlightRecenter == null && _recenterQueue.TryDequeue(out var result))
		{
			_inFlightRecenter = result;
			switch (result.Mode)
			{
			case RecenterMode.Recenter:
				ScreenDistance = 1f;
				deviceManager?.ResetScreens();
				PublishZoomEvent(ScreenDistance);
				break;
			case RecenterMode.Calibrate:
				deviceManager?.ResetAndCalibration();
				break;
			}
			rayHitRequest = true;
		}
	}

	private void OnHandTrack(HandTrackValue v)
	{
		deviceManager?.SetHandTrack(v.Value);
		if (!v.Value)
		{
			HandJointsVisualizer.leftValid = false;
			HandJointsVisualizer.rightValid = false;
		}
	}

	private void FindVdd()
	{
		if (wideDisplayObj == null)
		{
			wideDisplayObj = GameObject.Find("WideScreen_32_9");
		}
		if (wide24_9DisplayObj == null)
		{
			wide24_9DisplayObj = GameObject.Find("WideScreen_24_9");
		}
		if (wideDisplayObj != null && wideDisplay == null)
		{
			UwcWindowTexture component = wideDisplayObj.GetComponent<UwcWindowTexture>();
			if (component != null)
			{
				wideDisplay = component;
				Material material = wideDisplay.GetComponent<Renderer>().material;
				if (_glassesModel.Contains("P6") && material.HasProperty("Curve Radius"))
				{
					material.SetFloat("Curve Radius", 2f);
				}
			}
		}
		if (!(wide24_9DisplayObj != null) || !(wide24_9Display == null))
		{
			return;
		}
		UwcWindowTexture component2 = wide24_9DisplayObj.GetComponent<UwcWindowTexture>();
		if (component2 != null)
		{
			wide24_9Display = component2;
			Material material2 = wide24_9Display.GetComponent<Renderer>().material;
			if (_glassesModel.Contains("P6") && material2.HasProperty("Curve Radius"))
			{
				material2.SetFloat("Curve Radius", 2f);
			}
		}
	}

	private void Update()
	{
		if (setSkyboxFile != null)
		{
			SetSkybox(setSkyboxFile);
			setSkyboxFile = null;
		}
		RayHitTest();
		PumpRecenterQueue();
	}

	private void RayHitTest()
	{
		if (rayHitRequest)
		{
			rayHitRequest = false;
			string displayName = ResolveLookedAtDisplayName();
			CompleteInFlightRecenter(displayName);
		}
	}

	private string ResolveLookedAtDisplayName()
	{
		if (wideDisplayObj != null && wideDisplayObj.activeInHierarchy && wideDisplay != null)
		{
			return wideDisplay.desktopName ?? string.Empty;
		}
		if (wide24_9DisplayObj != null && wide24_9DisplayObj.activeInHierarchy && wide24_9Display != null)
		{
			return wide24_9Display.desktopName ?? string.Empty;
		}
		if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out var hitInfo) && hitInfo.collider.gameObject != null)
		{
			GameObject gameObject = hitInfo.collider.gameObject;
			VitureCommonLibrary.Logger.Info("RaycastHit gameObj Name: " + gameObject.name);
			UwcWindowTexture component = gameObject.GetComponent<UwcWindowTexture>();
			if (component != null)
			{
				return component.desktopName ?? string.Empty;
			}
		}
		return string.Empty;
	}

	private void CompleteInFlightRecenter(string displayName)
	{
		RecenterRequest inFlightRecenter = _inFlightRecenter;
		_inFlightRecenter = null;
		inFlightRecenter?.Tcs.TrySetResult(new RecenterResult(displayName ?? string.Empty, 0.5f, 0.5f));
	}

	private void OnDestroy()
	{
		HandJointsVisualizer.HandPoseChanged -= OnHandResize;
		TeardownIpc();
	}

	public void OnHandResize(bool resizing, float ratio)
	{
		if (resizing && !_handResizing)
		{
			_baseScreenDistance = ScreenDistance;
		}
		_handResizing = resizing;
		if (_handResizing)
		{
			float f = Mathf.Log(Mathf.Clamp(ratio, 0.001f, 10f));
			float num = Mathf.Exp(Mathf.Sign(f) * Mathf.Sqrt(Mathf.Abs(f)) * 0.5f);
			float num2 = Mathf.Clamp(_baseScreenDistance / num, 0.5f, 2f);
			ScreenDistance = num2 * 0.15f + ScreenDistance * 0.85f;
			deviceManager?.UpdateDistance();
			PublishZoomEvent(ScreenDistance);
		}
	}

	private void PublishZoomEvent(double screenDistance)
	{
		if (_channel != null)
		{
			TypedTopic<ZoomValue> zoomEvent = Topics.ZoomEvent;
			Channel channel = _channel;
			ZoomValue value = new ZoomValue(screenDistance);
			zoomEvent.Publish(channel, in value);
		}
	}
}
