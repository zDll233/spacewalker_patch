using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using SpaceWalker.Ipc;
using UnityEngine;
using Viture.Ipc.Pubsub;
using Viture.Ipc.Transport;
using VitureCommonLibrary;
using VitureCommonLibrary.SlamV3;

public class DeviceManager : MonoBehaviour
{
	[SerializeField]
	private UnityEngine.Camera mainCamera;

	[SerializeField]
	private GameObject vddManager;

	[SerializeField]
	private bool resetAndCalibFlag;

	[SerializeField]
	private bool resetFlag;

	[SerializeField]
	private bool use307;

	public bool handTrack;

	public bool smoothFollow = true;

	private MainCamera _mainCamera;

	private string _glassesModel = "P6Series";

	private bool currentFilmOpen = true;

	private int _filmAngle = 30;

	private LockAxisState _lockAxis;

	private LayoutMode _layoutMode;

	private int _duty;

	private bool hasAck;

	private int retryCount;

	private System.Timers.Timer _timer = new System.Timers.Timer
	{
		Interval = 3000.0
	};

	private ulong _imuDataCount;

	private ulong _frameCount;

	private ulong _lastResetTime;

	private ulong _lastImuTime;

	private List<ulong> _imuTimestamps = new List<ulong>();

	private const int MAX_IMU_TS_COUNT = 10000;

	private const int DEFAULT_IMU_RATE = 240;

	private const int P6_IMU_RATE = 500;

	private long _s6FrameArrivedCount;

	private UnityEngine.Quaternion? currentRotation;

	private UnityEngine.Quaternion? screenRotation;

	private UnityEngine.Vector3? screenPos;

	private UnityEngine.Vector3? recenterPos;

	private UnityEngine.Quaternion? lockReferenceRotation;

	private UnityEngine.Vector3? currentPos;

	private Tuple<DateTime, UnityEngine.Vector3?> baseCalibEuler;

	private Tuple<DateTime, UnityEngine.Vector3?> currentCalibEuler;

	private UnityEngine.Vector3 calibSpeed = UnityEngine.Vector3.zero;

	private UnityEngine.Vector3 deltaEuler = UnityEngine.Vector3.zero;

	private int resetCount;

	private int toastCount;

	private float firstResetTime;

	private float currentRunTime;

	private bool publishStaticToastFlag;

	private bool carinaYamlUpdate;

	private string carinaYamlContent = string.Empty;

	private ManualResetEventSlim getSNSlim = new ManualResetEventSlim(initialState: false);

	private ManualResetEventSlim getBiasSlim = new ManualResetEventSlim(initialState: false);

	private readonly ConcurrentQueue<byte[]> _receivedDataQueue = new ConcurrentQueue<byte[]>();

	private int _mainThreadId;

	private float _smoothFlowYawOffset;

	private float _smoothFlowYawVelocity;

	private float _smoothFlowLastGazeYaw;

	private bool _smoothFlowHasLastGazeYaw;

	private int _smoothFlowLatchedSide;

	private float _smoothFlowLatchedYawOffset;

	private const float SMOOTH_FLOW_FOLLOW_SMOOTH_TIME = 0.05f;

	private const float SMOOTH_FLOW_RELEASE_SMOOTH_TIME = 0.08f;

	private const float SMOOTH_FLOW_HEAD_STILL_YAW_SPEED = 6f;

	private const float SMOOTH_FLOW_EDGE_HYSTERESIS = 0.2f;

	private const float SMOOTH_FLOW_OFFSET_THRESHOLD = 0.05f;

	private const float SMOOTH_FLOW_VELOCITY_THRESHOLD = 0.5f;

	private const float SMOOTH_FLOW_DIRECTION_THRESHOLD = 0.5f;

	private string backSkybox = string.Empty;

	private bool LateLatchEnabled
	{
		get
		{
			if (!string.IsNullOrEmpty(_glassesModel))
			{
				return !_glassesModel.Contains("Native3Dof");
			}
			return false;
		}
	}

	private void Start()
	{
		_mainThreadId = Thread.CurrentThread.ManagedThreadId;
		InitParam();
		_mainCamera = mainCamera.GetComponent<MainCamera>();
		if (!_glassesModel.Contains("Native3Dof"))
		{
			_timer.Elapsed += RetryTimer_Elapsed;
			GlassesDeviceManager instance = GlassesDeviceManager.Instance;
			instance.DeviceConnectChanged = (Action<bool>)Delegate.Combine(instance.DeviceConnectChanged, new Action<bool>(OnVitureDeviceConnectChanged));
			GlassesDeviceManager.Instance.ReceivedGlassesData += OnReceivedGlassesData;
			OnVitureDeviceConnectChanged(GlassesDeviceManager.Instance.IsConnected);
		}
	}

	private void InitParam()
	{
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		string text = string.Join(' ', commandLineArgs);
		VitureCommonLibrary.Logger.Info("CMD Parameter: " + text);
		for (int i = 0; i < commandLineArgs.Length; i++)
		{
			if (commandLineArgs[i] == "-layoutMode" && i + 1 < commandLineArgs.Length)
			{
				string text2 = commandLineArgs[i + 1];
				VitureCommonLibrary.Logger.Info("Parameter -layoutMode: " + text2);
				if (Enum.TryParse<LayoutMode>(text2, out var result))
				{
					_layoutMode = result;
				}
			}
			if (commandLineArgs[i] == "-filmAngle" && i + 1 < commandLineArgs.Length)
			{
				string text3 = commandLineArgs[i + 1];
				VitureCommonLibrary.Logger.Info("Parameter -filmAngle: " + text3);
				if (int.TryParse(text3, out var result2))
				{
					_filmAngle = result2;
				}
			}
			if (commandLineArgs[i] == "-glassesModel" && i + 1 < commandLineArgs.Length)
			{
				string glassesModel = commandLineArgs[i + 1];
				_glassesModel = glassesModel;
				VitureCommonLibrary.Logger.Info("_glassesModel: " + _glassesModel);
			}
			if (commandLineArgs[i] == "-lockAxis" && i + 1 < commandLineArgs.Length)
			{
				string text4 = commandLineArgs[i + 1];
				VitureCommonLibrary.Logger.Info("Parameter -lockAxis: " + text4);
				if (int.TryParse(text4, out var result3))
				{
					_lockAxis = (LockAxisState)result3;
				}
			}
			if (commandLineArgs[i] == "-duty" && i + 1 < commandLineArgs.Length)
			{
				string text5 = commandLineArgs[i + 1];
				VitureCommonLibrary.Logger.Info("Parameter -duty: " + text5);
				if (int.TryParse(text5, out var result4))
				{
					_duty = result4;
				}
			}
			if (commandLineArgs[i] == "-handTrack" && i + 1 < commandLineArgs.Length)
			{
				string text6 = commandLineArgs[i + 1];
				VitureCommonLibrary.Logger.Info("Parameter -handTrack: " + text6);
				if (bool.TryParse(text6, out var result5))
				{
					handTrack = result5;
				}
			}
			if (commandLineArgs[i] == "-smoothFollow" && i + 1 < commandLineArgs.Length)
			{
				string text7 = commandLineArgs[i + 1];
				VitureCommonLibrary.Logger.Info("Parameter -smoothFollow: " + text7);
				if (bool.TryParse(text7, out var result6))
				{
					smoothFollow = result6;
				}
			}
			if (Application.isEditor)
			{
				handTrack = true;
			}
		}
	}

	private void OnPoseUpdate(float[] data, double timestamp)
	{
		if (handTrack)
		{
			GestureManager.UpdatePose(data, timestamp);
		}
		if (CarinaNative.GetPose(timestamp + 0.02, data) && data != null)
		{
			Pose pose = PoseConvertor.MatrixToPose(data);
			currentPos = ((_lockAxis == LockAxisState.Unlock) ? pose.position : UnityEngine.Vector3.zero);
			currentRotation = pose.rotation;
		}
		if (resetFlag)
		{
			screenRotation = null;
			screenPos = null;
			recenterPos = null;
			lockReferenceRotation = null;
			ResetSmoothFlowState();
			resetFlag = false;
		}
	}

	private void GetSNCmd()
	{
		getSNSlim.Reset();
		if (GlassesDeviceManager.Instance.UseHidDevice)
		{
			HidMessage hidMessage = new HidMessage();
			hidMessage.Data.MsgID = 16;
			GlassesDeviceManager.Instance.SendMsg(hidMessage);
		}
		else
		{
			UsbMessage usbMessage = new UsbMessage();
			usbMessage.Data.MsgID = 16;
			GlassesDeviceManager.Instance.SendMsg(usbMessage);
		}
		getSNSlim.Wait();
	}

	private void GetBiasCmd()
	{
		if (GlassesDeviceManager.Instance.UseHidDevice && GlassesDeviceManager.Instance.P6Series)
		{
			GlassesMsgSemaphore.SendMsgAndAwaitAck(DeviceMsgId.MSG_ID_IMU_CALI_PARAM_R);
		}
	}

	private void OpenImuCmd()
	{
		if (GlassesDeviceManager.Instance.UseHidDevice)
		{
			GlassesMsgSemaphore.SendMsgAndAwaitAck(DeviceMsgId.MSG_W_CONTROL_IMUOPEN, (byte)((!GlassesDeviceManager.Instance.P6Series) ? 1 : (use307 ? 3 : 2)));
		}
	}

	private async Task SetFilmCmd(bool open = true)
	{
		currentFilmOpen = open;
		VitureCommonLibrary.Logger.Info($"SetSkybox: {open} Begin");
		MainCamera component = mainCamera.GetComponent<MainCamera>();
		if (!open)
		{
			backSkybox = component.SkyboxFile;
			if (!string.IsNullOrWhiteSpace(backSkybox))
			{
				await component.SetSkybox();
			}
		}
		else if (!string.IsNullOrWhiteSpace(backSkybox))
		{
			await component.SetSkybox(backSkybox);
		}
		await Task.Run(delegate
		{
			VitureCommonLibrary.Logger.Info($"SetFilmCmd: {open} Begin");
			if (GlassesDeviceManager.Instance.UseHidDevice)
			{
				HidMessage hidMessage = new HidMessage();
				hidMessage.Data.MsgID = 14;
				hidMessage.Data.PutValue((byte)(open ? 1 : 0));
				GlassesDeviceManager.Instance.SendMsg(hidMessage);
			}
			else
			{
				UsbMessage usbMessage = new UsbMessage();
				usbMessage.Data.MsgID = 14;
				usbMessage.Data.PutValue((byte)(open ? 1 : 0));
				GlassesDeviceManager.Instance.SendMsg(usbMessage);
			}
		});
	}

	public void SetDutyCmd(int duty)
	{
		GlassesDeviceManager.Instance.SetDuty(duty);
	}

	private void SendByteToDevice(ushort msgId, byte value)
	{
		if (GlassesDeviceManager.Instance.UseHidDevice)
		{
			HidMessage hidMessage = new HidMessage();
			hidMessage.Data.MsgID = msgId;
			hidMessage.Data.PutValue(value);
			GlassesDeviceManager.Instance.SendMsg(hidMessage);
		}
		else
		{
			UsbMessage usbMessage = new UsbMessage();
			usbMessage.Data.MsgID = msgId;
			usbMessage.Data.PutValue(value);
			GlassesDeviceManager.Instance.SendMsg(usbMessage);
		}
	}

	private void SetImuFrameCmd(int rate = 240)
	{
		if (GlassesDeviceManager.Instance.UseHidDevice)
		{
			byte b = 0;
			b = rate switch
			{
				90 => 1, 
				120 => 2, 
				240 => 3, 
				500 => 4, 
				_ => 0, 
			};
			VitureCommonLibrary.Logger.Info($"set ImuFrameRate: {rate} cmd: {b}");
			GlassesMsgSemaphore.SendMsgAndAwaitAck(DeviceMsgId.MSG_W_CONTROL_IMU_REPORT_FQ, b);
		}
	}

	private void OnVitureDeviceConnectChanged(bool connect)
	{
		if (Thread.CurrentThread.ManagedThreadId == _mainThreadId)
		{
			Task.Run(delegate
			{
				OnVitureDeviceConnectChanged(connect);
			});
			return;
		}
		if (connect)
		{
			Thread.Sleep(500);
			if (!GlassesDeviceManager.Instance.UseHidDevice)
			{
				string config_file = Path.Combine(Application.streamingAssetsPath, "Configs", "custom_config.yaml");
				GlassesDeviceManager.Instance.Initialize(config_file);
				string carinaYamlConfig = CarinaNative.GetConfig();
				VitureCommonLibrary.Logger.Info("carinaYamlConfig: " + carinaYamlConfig);
				if (carinaYamlConfig.Split(new string[2] { "\r\n", "\n" }, StringSplitOptions.None).Length < 10)
				{
					VitureCommonLibrary.Logger.Error("carinaYamlConfig content is error.");
					return;
				}
				carinaYamlContent = carinaYamlConfig;
				carinaYamlUpdate = true;
				PoseConvertor.GetOriginImuMatrix(YamlParseHelper.GetImuOriginPose(carinaYamlConfig), isP6s: true);
				if (!GestureManager.HasInit)
				{
					Task.Run(delegate
					{
						GestureManager.Init(carinaYamlConfig);
						GestureManager.Start();
					});
				}
				CarinaNative.PoseUpdate += OnPoseUpdate;
				CarinaNative.CameraImageUpdate += CarinaNative_CameraImageUpdate;
				bool flag = CarinaNative.Start();
				VitureCommonLibrary.Logger.Info($"CarinaNative.Start ret: {flag}");
			}
			else
			{
				GlassesDeviceManager.Instance.Initialize();
				if (GlassesDeviceManager.Instance.S6NeedsHostSlam)
				{
					GlassesDeviceManager.Instance.S6ImuFrameReceived -= OnS6ImuFrame;
					GlassesDeviceManager.Instance.S6ImuFrameReceived += OnS6ImuFrame;
				}
			}
			hasAck = false;
			retryCount = 0;
			SendInitCmd();
			if (GlassesDeviceManager.Instance.UseHidDevice)
			{
				_timer.Start();
			}
			return;
		}
		hasAck = false;
		retryCount = 0;
		if (GlassesDeviceManager.Instance.UseHidDevice)
		{
			_timer.Stop();
			if (GlassesDeviceManager.Instance.P6Series)
			{
				P6BiasReader.Clear();
				VitureSlam.SaveOnlineBaisData(GlassesDeviceManager.Instance.GlassesSN);
				VitureSlam.Stop();
			}
			if (GlassesDeviceManager.Instance.S6NeedsHostSlam)
			{
				GlassesDeviceManager.Instance.S6ImuFrameReceived -= OnS6ImuFrame;
				Interlocked.Exchange(ref _s6FrameArrivedCount, 0L);
				VitureSlamV3.OnPoseUpdate -= OnSlamV3Pose;
				VitureSlamV3.StopForS6();
				S6CalibrationManager.Instance.ClearMemory();
			}
		}
		else
		{
			CarinaNative.PoseUpdate -= OnPoseUpdate;
			CarinaNative.Stop();
		}
		GlassesDeviceManager.Instance.Dispose();
	}

	private void CarinaNative_CameraImageUpdate(IntPtr left, IntPtr right, IntPtr center, IntPtr invalid, double timestamp, int width, int height)
	{
		if (handTrack)
		{
			GestureManager.UpdateImage(left, right, (uint)(width * height * 2), timestamp);
		}
	}

	private void SendInitCmd()
	{
		if (GlassesDeviceManager.Instance.UseHidDevice && GlassesDeviceManager.Instance.R6NewerModel && !GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			EnsureR6NewerSnAndFwLoaded();
			if (GlassesDeviceManager.Instance.S6NeedsHostSlam)
			{
				InitS6Calibration();
			}
			SendR6ImuReportOn(500);
			Thread.Sleep(200);
			SetDutyCmd(_duty);
			hasAck = true;
			_timer.Stop();
			VitureCommonLibrary.Logger.Info($"SendInitCmd End (R6Newer non-Native3Dof path, S6Slam={GlassesDeviceManager.Instance.S6NeedsHostSlam})");
			return;
		}
		GetSNCmd();
		if (GlassesDeviceManager.Instance.P6Series && GlassesDeviceManager.Instance.UseHidDevice)
		{
			GetBiasCmd();
			string yamlConfig = (carinaYamlContent = P6BiasReader.GetYamlConfig(GlassesDeviceManager.Instance.GlassesSN));
			carinaYamlUpdate = true;
			PoseConvertor.GetOriginImuMatrix(YamlParseHelper.GetImuOriginPose(yamlConfig));
			if (YamlParseHelper.GetBiasData(yamlConfig).Count == 3)
			{
				VitureSlam.CarinaYamlContent = carinaYamlContent;
			}
			VitureSlam.LoadOnlineBaisData(GlassesDeviceManager.Instance.GlassesSN);
			VitureSlam.Start(Path.Combine(Application.streamingAssetsPath, "Configs/slam_config_P6.yaml"));
		}
		OpenImuCmd();
		Thread.Sleep(200);
		SetImuFrameCmd(GlassesDeviceManager.Instance.P6Series ? 500 : 240);
		Thread.Sleep(200);
		SetDutyCmd(_duty);
		VitureCommonLibrary.Logger.Info("SendInitCmd End");
	}

	private void EnsureR6NewerSnAndFwLoaded()
	{
		if (string.IsNullOrEmpty(GlassesDeviceManager.Instance.GlassesSN))
		{
			SendR6ShortReadAndPoll(R6NewerMsgId.TF_REQ_ID_BOARD_SN_R, () => GlassesDeviceManager.Instance.GlassesSN, "SN");
		}
		if (string.IsNullOrEmpty(GlassesDeviceManager.Instance.FirmwareVersion))
		{
			SendR6ShortReadAndPoll(R6NewerMsgId.TF_REQ_ID_APP_FW_VERSION_R, () => GlassesDeviceManager.Instance.FirmwareVersion, "FW");
		}
	}

	private void SendR6ShortReadAndPoll(R6NewerMsgId msgId, Func<string> readPopulated, string label)
	{
		R6NewerHidMessage msg = new R6NewerHidMessage
		{
			MsgID = (ushort)msgId,
			DataLen = 0
		};
		GlassesDeviceManager.Instance.SendMsg(msg);
		for (int i = 0; i < 40; i++)
		{
			if (!string.IsNullOrEmpty(readPopulated()))
			{
				VitureCommonLibrary.Logger.Info($"SendR6ShortReadAndPoll[{label}]: populated after {(i + 1) * 50}ms");
				return;
			}
			Thread.Sleep(50);
		}
		VitureCommonLibrary.Logger.Warning($"SendR6ShortReadAndPoll[{label}]: timeout (2s) — {label} still empty after 0x{(ushort)msgId:X4}");
	}

	private void SendR6ImuReportOn(int rate)
	{
		byte b = rate switch
		{
			90 => 1, 
			120 => 2, 
			240 => 3, 
			500 => 4, 
			_ => 0, 
		};
		VitureCommonLibrary.Logger.Info($"SendR6ImuReportOn: rate={rate} freqCmd=0x{b:X2} (fire-and-forget; will verify by frame arrival)");
		long num = Interlocked.Read(ref _s6FrameArrivedCount);
		R6NewerHidMessage r6NewerHidMessage = new R6NewerHidMessage
		{
			MsgID = 769,
			DataLen = 2
		};
		r6NewerHidMessage.Payload[0] = 2;
		r6NewerHidMessage.Payload[1] = b;
		GlassesDeviceManager.Instance.SendMsg(r6NewerHidMessage);
		for (int i = 0; i < 15; i++)
		{
			long num2 = Interlocked.Read(ref _s6FrameArrivedCount);
			if (num2 - num >= 5)
			{
				VitureCommonLibrary.Logger.Info($"SendR6ImuReportOn: 0x7309 stream OK — {num2 - num} 帧已到达 (within {(i + 1) * 100}ms)");
				return;
			}
			Thread.Sleep(100);
		}
		long num3 = Interlocked.Read(ref _s6FrameArrivedCount);
		VitureCommonLibrary.Logger.Warning($"SendR6ImuReportOn: 0x7309 stream 未在 1.5s 内启动 (received {num3 - num} 帧)。" + "可能 firmware 未实装 0x0301 / 该 section 待烧录 / USB 异常。S6 SLAM 后续步骤仍会执行，但 IMU 输入空。");
	}

	private void InitS6Calibration()
	{
		try
		{
			string glassesSN = GlassesDeviceManager.Instance.GlassesSN;
			string firmwareVersion = GlassesDeviceManager.Instance.FirmwareVersion ?? string.Empty;
			if (string.IsNullOrWhiteSpace(glassesSN))
			{
				VitureCommonLibrary.Logger.Warning("InitS6Calibration: SN not yet available, skipping calibration load.");
				return;
			}
			Task<bool> task = S6CalibrationManager.Instance.EnsureCalibrationLoaded(glassesSN, firmwareVersion);
			if (!task.Wait(30000))
			{
				VitureCommonLibrary.Logger.Warning("InitS6Calibration: EnsureCalibrationLoaded timed out (30s).");
				return;
			}
			if (!task.Result)
			{
				VitureCommonLibrary.Logger.Warning("InitS6Calibration: calibration incomplete; SLAM startup will be skipped.");
				return;
			}
			S6DisplayOpticalParam displayOptical = S6CalibrationManager.Instance.GetDisplayOptical();
			if (displayOptical != null)
			{
				PoseConvertor.GetOriginImuMatrix(displayOptical.ToImuOriginMatrix(useRightEye: false), isP6s: false, isS6: true);
				carinaYamlUpdate = true;
			}
			VitureSlamV3.EnsureHandlerCreated(Path.Combine(Application.streamingAssetsPath, "Configs", "slam_config_S6.yaml"));
			VitureSlamV3.OnPoseUpdate -= OnSlamV3Pose;
			VitureSlamV3.OnPoseUpdate += OnSlamV3Pose;
			if (!VitureSlamV3.StartForS6(glassesSN))
			{
				VitureCommonLibrary.Logger.Warning("InitS6Calibration: VitureSlamV3.StartForS6 failed for sn=" + glassesSN);
			}
		}
		catch (Exception ex)
		{
			VitureCommonLibrary.Logger.Error("InitS6Calibration exception: " + ex.Message, ex.StackTrace);
		}
	}

	private void OnSlamV3Pose(SlamPose pose)
	{
		try
		{
			UnityEngine.Quaternion rotation = new UnityEngine.Quaternion(pose.orientation.x, 0f - pose.orientation.y, 0f - pose.orientation.z, pose.orientation.w) * UnityEngine.Quaternion.AngleAxis(90f, UnityEngine.Vector3.right);
			ulong timestamp = pose.timestamp / 1000;
			ProcessImuMsg(rotation, timestamp);
		}
		catch (Exception ex)
		{
			VitureCommonLibrary.Logger.Warning("OnSlamV3Pose exception: " + ex.Message);
		}
	}

	private void RetryTimer_Elapsed(object sender, ElapsedEventArgs e)
	{
		if (!hasAck && retryCount < 10)
		{
			retryCount++;
			VitureCommonLibrary.Logger.Info($"Retry OpenIMU retryCount: {retryCount}");
			SendInitCmd();
		}
		else
		{
			_timer.Stop();
		}
	}

	private void OnReceivedGlassesData(byte[] bytes)
	{
		if (bytes == null)
		{
			return;
		}
		if (GlassesDeviceManager.Instance.UseHidDevice)
		{
			HidMessage hidMessage = HidMessage.FromBytes(bytes);
			if (hidMessage.Data.Header1 != byte.MaxValue)
			{
				return;
			}
			if (IsImuMessage(hidMessage))
			{
				try
				{
					ProcessHidImuMsg(hidMessage);
					return;
				}
				catch (Exception ex)
				{
					VitureCommonLibrary.Logger.Error("ProcessHidImuMsg error: " + ex.Message, ex.StackTrace);
					return;
				}
			}
		}
		_receivedDataQueue.Enqueue(bytes);
	}

	private static bool IsImuMessage(HidMessage hidMsg)
	{
		if (!hidMsg.Data.MsgID.Equal(DeviceEventId.RawIMUEventReport) && !hidMsg.Data.MsgID.Equal(DeviceEventId.VsyncHIDEventReport))
		{
			return hidMsg.Data.MsgID.Equal(DeviceEventId.IMUEventReport);
		}
		return true;
	}

	private void ProcessReceivedGlassesData(byte[] bytes)
	{
		if (GlassesDeviceManager.Instance.UseHidDevice)
		{
			HidMessage hidMessage = HidMessage.FromBytes(bytes);
			if (hidMessage.Data.Header1 == byte.MaxValue)
			{
				ProcessHidMsg(hidMessage);
			}
		}
		else
		{
			UsbMessage usbMsg = UsbMessage.FromBytes(bytes);
			ProcessUsbMsg(usbMsg);
		}
	}

	private void OnS6ImuFrame(R6NewerHidMessage r6Msg)
	{
		Interlocked.Increment(ref _s6FrameArrivedCount);
		try
		{
			VitureSlamV3.ProcessImuFrame(r6Msg);
		}
		catch (Exception ex)
		{
			VitureCommonLibrary.Logger.Warning("OnS6ImuFrame exception: " + ex.Message);
		}
	}

	private void ProcessHidImuMsg(HidMessage hidMsg)
	{
		if (hidMsg.Data.MsgID.Equal(DeviceEventId.RawIMUEventReport) || hidMsg.Data.MsgID.Equal(DeviceEventId.VsyncHIDEventReport))
		{
			if (!hasAck)
			{
				return;
			}
			currentPos = UnityEngine.Vector3.zero;
			PoseState poseState = VitureSlam.Track(hidMsg, hidMsg.Data.MsgID == 775);
			UnityEngine.Quaternion quaternion = PoseConvertor.WorldRotation(new UnityEngine.Quaternion((float)poseState.rx, (float)poseState.ry, (float)poseState.rz, (float)poseState.rw));
			quaternion = new UnityEngine.Quaternion(0f - quaternion.x, 0f - quaternion.y, quaternion.z, quaternion.w);
			ProcessImuMsg(quaternion, hidMsg.DeviceTimestamp);
		}
		if (hidMsg.Data.MsgID.Equal(DeviceEventId.IMUEventReport))
		{
			currentPos = UnityEngine.Vector3.zero;
			UnityEngine.Vector3 euler = new UnityEngine.Vector3(hidMsg.EulerAngle.Y, 0f - hidMsg.EulerAngle.Z, 0f - hidMsg.EulerAngle.X);
			ProcessImuMsg(euler, hidMsg.DeviceTimestamp);
		}
	}

	private void ProcessHidMsg(HidMessage hidMsg)
	{
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_GLASSID))
		{
			string glassesSN = hidMsg.GetGlassesSN();
			VitureCommonLibrary.Logger.Info("SN: " + glassesSN);
			getSNSlim.Set();
		}
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_ID_IMU_CALI_PARAM_R) && hidMsg.GetAckSuceess())
		{
			Vector3D gyroOffset = hidMsg.GyroOffset;
			VitureCommonLibrary.Logger.Info("gyroOffset: " + gyroOffset.ToString());
			VitureSlam.GyroBias = gyroOffset.ToBytes();
			GlassesMsgSemaphore.ReleaseSemaphore(hidMsg.Data.MsgID);
		}
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_W_CONTROL_IMUOPEN))
		{
			VitureCommonLibrary.Logger.Info($"OpenIMU ACK: {hidMsg.GetAckSuceess()}");
			hasAck = true;
			_timer.Stop();
			GlassesMsgSemaphore.ReleaseSemaphore(hidMsg.Data.MsgID);
		}
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_W_CONTROL_IMU_REPORT_FQ))
		{
			ParseImuRate(hidMsg);
		}
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_W_DISPLAY_DUTY) || hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_DISPLAY_DUTY))
		{
			byte b = hidMsg.Data.Payload[1];
			VitureCommonLibrary.Logger.Info($"duty: {b}");
		}
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_DOF6_PARA_INFO) || hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_READ_DOF6_PARA))
		{
			P6BiasReader.ProcessMsg(hidMsg);
		}
		if (hidMsg.Data.MsgID == 5 || hidMsg.Data.MsgID == 6 || hidMsg.Data.MsgID == 769)
		{
			byte value = ((hidMsg.Data.MsgID == 769) ? hidMsg.Data.Payload[0] : hidMsg.Data.Payload[1]);
			PublishBrightnessEvent(value);
		}
		if (hidMsg.Data.MsgID == 50 || hidMsg.Data.MsgID == 51 || hidMsg.Data.MsgID == 772)
		{
			byte value2 = ((hidMsg.Data.MsgID == 772) ? hidMsg.Data.Payload[0] : hidMsg.Data.Payload[1]);
			PublishVolumeEvent(value2);
		}
	}

	private void ProcessUsbMsg(UsbMessage usbMsg)
	{
		if (usbMsg.Data.Header == UsbMessageData.HEADER_TYPE.DOWN || usbMsg.Data.Header == UsbMessageData.HEADER_TYPE.UP)
		{
			if (usbMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_GLASSID))
			{
				string glassesSN = usbMsg.GetGlassesSN();
				VitureCommonLibrary.Logger.Info("SN: " + glassesSN);
				getSNSlim.Set();
			}
			if (usbMsg.Data.MsgID == 5 || usbMsg.Data.MsgID == 6 || usbMsg.Data.MsgID == 769)
			{
				byte value = ((usbMsg.Data.MsgID == 769) ? usbMsg.Data.Payload[0] : usbMsg.Data.Payload[1]);
				PublishBrightnessEvent(value);
			}
			if (usbMsg.Data.MsgID == 50 || usbMsg.Data.MsgID == 51 || usbMsg.Data.MsgID == 772)
			{
				byte value2 = ((usbMsg.Data.MsgID == 772) ? usbMsg.Data.Payload[0] : usbMsg.Data.Payload[1]);
				PublishVolumeEvent(value2);
			}
		}
	}

	private void PublishBrightnessEvent(int value)
	{
		if (!Application.isEditor)
		{
			Channel channel = _mainCamera?.Channel;
			if (channel != null)
			{
				TypedTopic<BrightnessValue> brightnessEvent = Topics.BrightnessEvent;
				BrightnessValue value2 = new BrightnessValue(value);
				brightnessEvent.Publish(channel, in value2);
			}
		}
	}

	private void PublishVolumeEvent(int value)
	{
		if (!Application.isEditor)
		{
			Channel channel = _mainCamera?.Channel;
			if (channel != null)
			{
				TypedTopic<VolumeValue> volumeEvent = Topics.VolumeEvent;
				VolumeValue value2 = new VolumeValue(value);
				volumeEvent.Publish(channel, in value2);
			}
		}
	}

	private static void ParseImuRate(HidMessage hidMsg)
	{
		byte b = hidMsg.Data.Payload[1];
		int num = 60;
		num = b switch
		{
			1 => 90, 
			2 => 120, 
			3 => 240, 
			4 => 500, 
			_ => 60, 
		};
		GlassesMsgSemaphore.ReleaseSemaphore(hidMsg.Data.MsgID);
		VitureCommonLibrary.Logger.Info($"IMU FrameRate: {num} {b}");
	}

	private void ProcessImuMsg(UnityEngine.Quaternion rotation, ulong timestamp)
	{
		_imuDataCount++;
		currentPos = UnityEngine.Vector3.zero;
		_ = rotation.eulerAngles;
		if (resetFlag)
		{
			if (_lastResetTime == 0L)
			{
				_lastResetTime = timestamp;
			}
			float duration = (float)((double)(timestamp - _lastResetTime) / 1000000.0);
			_lastResetTime = timestamp;
			Statistics.ProcessStatistics(duration, ref _imuDataCount, ref _frameCount, _imuTimestamps);
			Reset();
		}
		currentRotation = rotation;
		if (_lastImuTime != 0)
		{
			_imuTimestamps.Add(timestamp - _lastImuTime);
			if (_imuTimestamps.Count > 10000)
			{
				_imuTimestamps.RemoveAt(0);
			}
		}
		_lastImuTime = timestamp;
	}

	private void ProcessImuMsg(UnityEngine.Vector3 euler, ulong timestamp)
	{
		_imuDataCount++;
		currentPos = UnityEngine.Vector3.zero;
		if (resetAndCalibFlag)
		{
			resetAndCalibFlag = false;
			ResetAndCalib(euler);
		}
		euler = FixImuDraft(euler);
		if (resetFlag)
		{
			if (_lastResetTime == 0L)
			{
				_lastResetTime = timestamp;
			}
			float duration = (float)(timestamp - _lastResetTime) * 0.001f;
			_lastResetTime = timestamp;
			Statistics.ProcessStatistics(duration, ref _imuDataCount, ref _frameCount, _imuTimestamps);
			Reset();
		}
		currentRotation = PoseConvertor.GetRotation(euler.x, euler.y, euler.z);
		if (_lastImuTime != 0)
		{
			_imuTimestamps.Add(timestamp - _lastImuTime);
			if (_imuTimestamps.Count > 10000)
			{
				_imuTimestamps.RemoveAt(0);
			}
		}
		_lastImuTime = timestamp;
	}

	public void Reset()
	{
		resetFlag = false;
		baseCalibEuler = null;
		calibSpeed = UnityEngine.Vector3.zero;
		screenPos = null;
		recenterPos = null;
		screenRotation = null;
		lockReferenceRotation = null;
		ResetSmoothFlowState();
		RollingShutter.ResetMotion();
	}

	private UnityEngine.Vector3 FixImuDraft(UnityEngine.Vector3 euler)
	{
		if ((double)Math.Abs(calibSpeed.y) >= 1E-06)
		{
			double totalSeconds = (DateTime.Now - baseCalibEuler.Item1).TotalSeconds;
			deltaEuler = new UnityEngine.Vector3((float)(totalSeconds * (double)calibSpeed.x), (float)(totalSeconds * (double)calibSpeed.y), (float)(totalSeconds * (double)calibSpeed.z));
			VitureCommonLibrary.Logger.Debug($"runTimeSec: {totalSeconds,10:000.000000} deltaEuler: {deltaEuler.x,10:000.000000} {deltaEuler.y,10:000.000000} {deltaEuler.z,10:000.000000}");
			euler.y -= deltaEuler.y;
		}
		return euler;
	}

	private void ResetAndCalib(UnityEngine.Vector3 euler)
	{
		resetAndCalibFlag = false;
		screenRotation = null;
		screenPos = null;
		recenterPos = null;
		lockReferenceRotation = null;
		calibSpeed = UnityEngine.Vector3.zero;
		ResetSmoothFlowState();
		if (baseCalibEuler == null)
		{
			baseCalibEuler = new Tuple<DateTime, UnityEngine.Vector3?>(DateTime.Now, euler);
		}
		currentCalibEuler = new Tuple<DateTime, UnityEngine.Vector3?>(DateTime.Now, euler);
		calibSpeed = CalculateSpeed();
		VitureCommonLibrary.Logger.Info($"baseCalibEuler: {baseCalibEuler.Item2?.x,10:000.000000} {baseCalibEuler.Item2?.y,10:000.000000} {baseCalibEuler.Item2?.z,10:000.000000}");
		VitureCommonLibrary.Logger.Info($"currentCalibEuler: {currentCalibEuler.Item2?.x,10:000.000000} {currentCalibEuler.Item2?.y,10:000.000000} {currentCalibEuler.Item2?.z,10:000.000000}");
		VitureCommonLibrary.Logger.Info($"calibSpeed: {calibSpeed.x,10:000.000000} {calibSpeed.y,10:000.000000} {calibSpeed.z,10:000.000000}");
	}

	private UnityEngine.Vector3 CalculateSpeed()
	{
		UnityEngine.Vector3? vector = currentCalibEuler.Item2 - baseCalibEuler.Item2;
		double totalSeconds = (currentCalibEuler.Item1 - baseCalibEuler.Item1).TotalSeconds;
		if (vector.HasValue && totalSeconds > 1.0)
		{
			return new UnityEngine.Vector3((float)((double)vector.Value.x / totalSeconds), (float)((double)vector.Value.y / totalSeconds), (float)((double)vector.Value.z / totalSeconds));
		}
		return UnityEngine.Vector3.zero;
	}

	private IEnumerator DelayReset()
	{
		yield return new WaitForSeconds(1f);
		ResetScreens();
	}

	private void Update()
	{
		byte[] result;
		while (_receivedDataQueue.TryDequeue(out result))
		{
			try
			{
				ProcessReceivedGlassesData(result);
			}
			catch (Exception ex)
			{
				VitureCommonLibrary.Logger.Error("ProcessReceivedGlassesData error: " + ex.Message, ex.StackTrace);
			}
		}
		currentRunTime = Time.realtimeSinceStartup;
		if (carinaYamlUpdate)
		{
			carinaYamlUpdate = false;
			if (UnityEngine.Camera.main != null)
			{
				UnityEngine.Camera.main.fieldOfView = GlassesDeviceManager.GetGlassesFov(carinaYamlContent);
				if (vddManager != null)
				{
					vddManager.GetComponent<VddManager>().AutoLayoutDisplays();
				}
				StartCoroutine(DelayReset());
			}
		}
		if (publishStaticToastFlag)
		{
			Channel channel = _mainCamera?.Channel;
			if (channel != null)
			{
				Topics.StaticCalib.Publish(channel);
			}
			publishStaticToastFlag = false;
		}
		if (mainCamera == null || !currentRotation.HasValue || !currentPos.HasValue)
		{
			_frameCount++;
			return;
		}
		float num = 0f;
		num = currentRotation.Value.eulerAngles.x;
		if (!lockReferenceRotation.HasValue)
		{
			lockReferenceRotation = currentRotation.Value;
		}
		UnityEngine.Quaternion value = PoseConvertor.CheckLockAxis(currentRotation.Value, _lockAxis, lockReferenceRotation.Value);
		if (!screenRotation.HasValue)
		{
			screenRotation = value;
		}
		if (!recenterPos.HasValue)
		{
			recenterPos = currentPos;
		}
		if (!screenPos.HasValue)
		{
			float num2 = 1f - _mainCamera.ScreenDistance;
			screenPos = recenterPos.Value - screenRotation.Value * UnityEngine.Vector3.forward * num2;
			VitureCommonLibrary.Logger.Info($"Set screenPos ScreenDistance: {_mainCamera.ScreenDistance}");
		}
		ApplySmoothFlowToScreens(_smoothFlowYawOffset, currentPos.Value);
		if (_filmAngle > 0)
		{
			if (num > 180f)
			{
				num -= 360f;
			}
			if (num >= (float)_filmAngle && currentFilmOpen)
			{
				SetFilmCmd(open: false);
			}
			else if (num < (float)_filmAngle && !currentFilmOpen)
			{
				SetFilmCmd();
			}
		}
		_frameCount++;
	}

	private void OnEnable()
	{
		Application.onBeforeRender += OnBeforeRenderApplyPose;
	}

	private void OnDisable()
	{
		Application.onBeforeRender -= OnBeforeRenderApplyPose;
	}

	private void OnBeforeRenderApplyPose()
	{
		if (LateLatchEnabled)
		{
			ApplyPoseToCamera();
		}
	}

	private void LateUpdate()
	{
		if (!LateLatchEnabled)
		{
			ApplyPoseToCamera();
		}
	}

	private void ApplyPoseToCamera()
	{
		if (!(mainCamera == null) && currentRotation.HasValue && currentPos.HasValue)
		{
			if (!lockReferenceRotation.HasValue)
			{
				lockReferenceRotation = currentRotation.Value;
			}
			UnityEngine.Quaternion quaternion = PoseConvertor.CheckLockAxis(currentRotation.Value, _lockAxis, lockReferenceRotation.Value);
			float yawOffset = UpdateSmoothFlow(quaternion);
			mainCamera.transform.rotation = quaternion;
			mainCamera.transform.position = currentPos.Value;
			ApplySmoothFlowToScreens(yawOffset, currentPos.Value);
			if (LateLatchEnabled)
			{
				RollingShutter.Feed(quaternion, mainCamera);
			}
		}
	}

	private void ResetSmoothFlowState()
	{
		_smoothFlowYawOffset = 0f;
		_smoothFlowYawVelocity = 0f;
		_smoothFlowLastGazeYaw = 0f;
		_smoothFlowHasLastGazeYaw = false;
		_smoothFlowLatchedSide = 0;
		_smoothFlowLatchedYawOffset = 0f;
	}

	private float UpdateSmoothFlow(UnityEngine.Quaternion rawRotation)
	{
		if (!smoothFollow)
		{
			if (_smoothFlowYawOffset != 0f || _smoothFlowLatchedSide != 0 || _smoothFlowHasLastGazeYaw)
			{
				ResetSmoothFlowState();
			}
			return 0f;
		}
		if (!screenRotation.HasValue || _mainCamera == null)
		{
			return 0f;
		}
		if (!CalculateActiveScreenYawBounds(out var leftBound, out var rightBound))
		{
			return 0f;
		}
		UnityEngine.Vector3 vector = UnityEngine.Quaternion.Inverse(screenRotation.Value) * rawRotation * UnityEngine.Vector3.forward;
		float num = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
		float num2 = num - _smoothFlowYawOffset;
		float deltaTime = Time.deltaTime;
		if (deltaTime <= 0f)
		{
			return _smoothFlowYawOffset;
		}
		float num3 = 0f;
		if (_smoothFlowHasLastGazeYaw)
		{
			num3 = Mathf.DeltaAngle(_smoothFlowLastGazeYaw, num2) / deltaTime;
		}
		_smoothFlowLastGazeYaw = num2;
		_smoothFlowHasLastGazeYaw = true;
		float num4 = num2 - rightBound;
		float num5 = num2 - leftBound;
		float num6 = num - rightBound;
		float num7 = num - leftBound;
		bool flag = num4 > 0.2f;
		bool flag2 = num5 < -0.2f;
		bool flag3 = num3 > 0.5f;
		bool flag4 = num3 < -0.5f;
		float num8 = ((_smoothFlowLatchedSide == 0) ? 0f : _smoothFlowLatchedYawOffset);
		if (_smoothFlowLatchedSide == 0)
		{
			if (flag && flag3)
			{
				_smoothFlowLatchedSide = 1;
				_smoothFlowLatchedYawOffset = Mathf.Max(0f, num6);
				num8 = _smoothFlowLatchedYawOffset;
			}
			else if (flag2 && flag4)
			{
				_smoothFlowLatchedSide = -1;
				_smoothFlowLatchedYawOffset = Mathf.Min(0f, num7);
				num8 = _smoothFlowLatchedYawOffset;
			}
		}
		else if (_smoothFlowLatchedSide > 0)
		{
			if (flag2 && flag4)
			{
				_smoothFlowLatchedSide = -1;
				_smoothFlowLatchedYawOffset = num7;
			}
			else if (flag)
			{
				float num9 = ((_smoothFlowLatchedYawOffset < 0f) ? num6 : Mathf.Max(0f, num6));
				if (flag3 || num9 > _smoothFlowLatchedYawOffset + 0.05f)
				{
					_smoothFlowLatchedYawOffset = Mathf.Max(_smoothFlowLatchedYawOffset, num9);
				}
			}
			num8 = _smoothFlowLatchedYawOffset;
		}
		else
		{
			if (flag && flag3)
			{
				_smoothFlowLatchedSide = 1;
				_smoothFlowLatchedYawOffset = num6;
			}
			else if (flag2)
			{
				float num10 = ((_smoothFlowLatchedYawOffset > 0f) ? num7 : Mathf.Min(0f, num7));
				if (flag4 || num10 < _smoothFlowLatchedYawOffset - 0.05f)
				{
					_smoothFlowLatchedYawOffset = Mathf.Min(_smoothFlowLatchedYawOffset, num10);
				}
			}
			num8 = _smoothFlowLatchedYawOffset;
		}
		bool flag5 = _smoothFlowLatchedSide == 0 && Mathf.Abs(num8) <= 0.05f;
		float num11 = (flag5 ? 0.08f : 0.05f);
		if (flag5 && Mathf.Abs(num3) <= 6f)
		{
			num11 *= 0.5f;
		}
		_smoothFlowYawOffset = Mathf.SmoothDamp(_smoothFlowYawOffset, num8, ref _smoothFlowYawVelocity, num11, float.PositiveInfinity, deltaTime);
		float num12 = Mathf.Abs(num8 - _smoothFlowYawOffset);
		bool flag6 = Mathf.Abs(num3) <= 6f;
		bool num13 = num12 <= 0.05f;
		bool flag7 = Mathf.Abs(_smoothFlowYawVelocity) <= 0.5f;
		if (num13 && flag7 && (flag5 || flag6))
		{
			_smoothFlowYawOffset = num8;
			_smoothFlowYawVelocity = 0f;
		}
		return _smoothFlowYawOffset;
	}

	private void ApplySmoothFlowToScreens(float yawOffset, UnityEngine.Vector3 cameraPivot)
	{
		if (!(_mainCamera == null) && !(_mainCamera.Screens == null) && screenRotation.HasValue && screenPos.HasValue)
		{
			Transform obj = _mainCamera.Screens.transform;
			UnityEngine.Quaternion quaternion = UnityEngine.Quaternion.Euler(0f, yawOffset, 0f);
			obj.rotation = quaternion * screenRotation.Value;
			obj.position = cameraPivot + quaternion * (screenPos.Value - cameraPivot);
		}
	}

	private bool CalculateActiveScreenYawBounds(out float leftBound, out float rightBound)
	{
		leftBound = 0f;
		rightBound = 0f;
		Transform transform = _mainCamera.Screens.transform;
		float num = float.MaxValue;
		float num2 = float.MinValue;
		bool flag = false;
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (child.gameObject.activeSelf && !(child.localScale.x < 0.01f))
			{
				flag = true;
				UnityEngine.Vector3 localPosition = child.localPosition;
				float num3 = child.localRotation.eulerAngles.y;
				if (num3 > 180f)
				{
					num3 -= 360f;
				}
				float f = num3 * (MathF.PI / 180f);
				float num4 = child.localScale.x * 0.5f;
				float num5 = Mathf.Cos(f);
				float num6 = Mathf.Sin(f);
				float y = localPosition.x - num4 * num5;
				float x = localPosition.z + num4 * num6;
				float y2 = localPosition.x + num4 * num5;
				float x2 = localPosition.z - num4 * num6;
				float a = Mathf.Atan2(y, x) * 57.29578f;
				float b = Mathf.Atan2(y2, x2) * 57.29578f;
				num = Mathf.Min(num, Mathf.Min(a, b));
				num2 = Mathf.Max(num2, Mathf.Max(a, b));
			}
		}
		if (!flag)
		{
			return false;
		}
		leftBound = num;
		rightBound = num2;
		return true;
	}

	public void ResetScreens()
	{
		if (!GlassesDeviceManager.Instance.UseHidDevice)
		{
			Reset();
			return;
		}
		if (GlassesDeviceManager.Instance.P6Series && currentRunTime > 60f)
		{
			if (firstResetTime == 0f || currentRunTime - firstResetTime >= 600f)
			{
				firstResetTime = currentRunTime;
				resetCount = 0;
			}
			resetCount++;
			if (currentRunTime - firstResetTime < 600f && resetCount == 4 && toastCount < 3)
			{
				toastCount++;
				resetCount = 0;
				firstResetTime = 0f;
				publishStaticToastFlag = true;
			}
		}
		resetFlag = true;
	}

	public void UpdateDistance()
	{
		screenPos = null;
	}

	public void ResetAndCalibration()
	{
		resetAndCalibFlag = true;
	}

	private void OnDestroy()
	{
		if (!_glassesModel.Contains("Native3Dof"))
		{
			_timer.Stop();
			_timer.Dispose();
			if (!GlassesDeviceManager.Instance.UseHidDevice)
			{
				CarinaNative.PoseUpdate -= OnPoseUpdate;
				CarinaNative.CameraImageUpdate -= CarinaNative_CameraImageUpdate;
				CarinaNative.Stop();
			}
			else if (GlassesDeviceManager.Instance.P6Series)
			{
				VitureSlam.Stop();
			}
			VitureSlamV3.OnPoseUpdate -= OnSlamV3Pose;
			VitureSlamV3.DestroyHandlerIfAny();
			GestureManager.Uninit();
			GlassesDeviceManager.Instance.ReceivedGlassesData -= OnReceivedGlassesData;
			GlassesDeviceManager instance = GlassesDeviceManager.Instance;
			instance.DeviceConnectChanged = (Action<bool>)Delegate.Remove(instance.DeviceConnectChanged, new Action<bool>(OnVitureDeviceConnectChanged));
			GlassesDeviceManager.Instance.Dispose();
		}
	}

	private void OnApplicationQuit()
	{
		try
		{
			VitureSlamV3.OnPoseUpdate -= OnSlamV3Pose;
			VitureSlamV3.DestroyHandlerIfAny();
		}
		catch (Exception ex)
		{
			VitureCommonLibrary.Logger.Warning("OnApplicationQuit VitureSlamV3 teardown exception: " + ex.Message);
		}
	}

	internal void UpdateFilmAngle(int filmAngle)
	{
		_filmAngle = filmAngle;
	}

	internal void UpdateLockAxis(LockAxisState lockAxis)
	{
		_lockAxis = lockAxis;
		resetFlag = true;
	}

	internal void SetHandTrack(bool val)
	{
		handTrack = val;
	}

	internal void SetSmoothFollow(bool val)
	{
		smoothFollow = val;
	}
}
