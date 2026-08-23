using System;
using JetBrains.Annotations;
using UnityEngine;
using VitureCommonLibrary;

public class HandJointsVisualizer : MonoBehaviour
{
	[Serializable]
	private class JointTextureOverride
	{
		public bool isLeftHand = true;

		[Range(0f, 25f)]
		public int jointIndex;

		public string texturePath;
	}

	[SerializeField]
	private DeviceManager deviceManager;

	[SerializeField]
	private GameObject jointSpritePrefab;

	[SerializeField]
	private JointTextureOverride[] jointTextureOverrides;

	[SerializeField]
	private float jointSpriteScale = 0.03f;

	[SerializeField]
	private bool renderJointSprites;

	[SerializeField]
	private Camera _camera;

	public int JointCount = 26;

	private readonly object _poseLock = new object();

	private GameObject[] leftPoints;

	private GameObject[] rightPoints;

	private Vector3[] lPos;

	private Vector3[] rPos;

	private Vector3[] lFramePos;

	private Vector3[] rFramePos;

	public static bool leftValid;

	public static bool rightValid;

	private string _glassesModel = string.Empty;

	private Vector3 leftHandPos = Vector3.zero;

	private Vector3 rightHandPos = Vector3.zero;

	private bool leftHandHolding;

	private bool rightHandHolding;

	private float _latestLeftAction;

	private float _latestLeftActionParam;

	private float _latestRightAction;

	private float _latestRightActionParam;

	private bool _leftFrameValid;

	private bool _rightFrameValid;

	private float _leftFrameAction;

	private float _leftFrameActionParam;

	private float _rightFrameAction;

	private float _rightFrameActionParam;

	private bool m_IsResizing;

	[CanBeNull]
	public static event Action<bool, float> HandPoseChanged;

	[CanBeNull]
	public static event Action HandRecenterTriggered;

	private void Start()
	{
		InitParam();
		lPos = new Vector3[JointCount];
		rPos = new Vector3[JointCount];
		lFramePos = new Vector3[JointCount];
		rFramePos = new Vector3[JointCount];
		GestureManager.GesturePoseUpdate += OnGesturePoseUpdate;
		if (renderJointSprites && (Application.isEditor || _glassesModel.Contains("P6S")))
		{
			InitHandJoints();
		}
	}

	private void OnDestroy()
	{
		GestureManager.GesturePoseUpdate -= OnGesturePoseUpdate;
		leftValid = false;
		rightValid = false;
		HandJointsVisualizer.HandPoseChanged = null;
		HandJointsVisualizer.HandRecenterTriggered = null;
	}

	private void ParseHandPose(float[] pose)
	{
		float latestLeftAction = 0f;
		float latestLeftActionParam = 0f;
		float latestRightAction = 0f;
		float latestRightActionParam = 0f;
		if (pose == null || pose.Length != 374)
		{
			lock (_poseLock)
			{
				leftValid = false;
				rightValid = false;
				_latestLeftAction = 0f;
				_latestLeftActionParam = 0f;
				_latestRightAction = 0f;
				_latestRightActionParam = 0f;
				return;
			}
		}
		bool flag = pose[0] != 0f;
		bool flag2 = pose[187] != 0f;
		lock (_poseLock)
		{
			leftValid = flag;
			rightValid = flag2;
			if (leftValid)
			{
				int num = 1;
				for (int i = 0; i < 26; i++)
				{
					float num2 = pose[num + i * 7];
					float num3 = pose[num + i * 7 + 1];
					float y = pose[num + i * 7 + 2];
					lPos[i] = new Vector3(0f - num2, y, 0f - num3);
					if (i == 0)
					{
						VitureCommonLibrary.Logger.Debug($"Left Thumb: {lPos[i]}");
					}
					if (i == 5)
					{
						VitureCommonLibrary.Logger.Debug($"Left Hand Root: {lPos[i]}");
						leftHandPos = lPos[i];
					}
				}
				latestLeftAction = pose[183];
				latestLeftActionParam = pose[184];
			}
			if (rightValid)
			{
				int num4 = 188;
				for (int j = 0; j < 26; j++)
				{
					float num5 = pose[num4 + j * 7];
					float num6 = pose[num4 + j * 7 + 1];
					float y2 = pose[num4 + j * 7 + 2];
					rPos[j] = new Vector3(0f - num5, y2, 0f - num6);
					if (j == 0)
					{
						VitureCommonLibrary.Logger.Debug($"Right Thumb: {rPos[j]}");
					}
					if (j == 5)
					{
						VitureCommonLibrary.Logger.Debug($"Right Hand Root: {rPos[j]}");
						rightHandPos = rPos[j];
					}
				}
				latestRightAction = pose[370];
				latestRightActionParam = pose[371];
			}
			_latestLeftAction = latestLeftAction;
			_latestLeftActionParam = latestLeftActionParam;
			_latestRightAction = latestRightAction;
			_latestRightActionParam = latestRightActionParam;
		}
		if (leftValid)
		{
			float num7 = pose[185];
			float num8 = pose[186];
			if (Math.Abs(num7 - 1f) < 1E-06f && Math.Abs(num8 - 2f) < 1E-06f)
			{
				if (!leftHandHolding)
				{
					leftHandHolding = true;
					deviceManager?.Reset();
					HandJointsVisualizer.HandRecenterTriggered?.Invoke();
				}
			}
			else
			{
				leftHandHolding = false;
			}
		}
		if (!rightValid)
		{
			return;
		}
		float num9 = pose[372];
		float num10 = pose[373];
		if (Math.Abs(num9 - 1f) < 1E-06f && Math.Abs(num10 - 2f) < 1E-06f)
		{
			if (!rightHandHolding)
			{
				rightHandHolding = true;
				deviceManager?.Reset();
				HandJointsVisualizer.HandRecenterTriggered?.Invoke();
			}
		}
		else
		{
			rightHandHolding = false;
		}
	}

	private void CopyFrameState()
	{
		if (lFramePos == null || rFramePos == null || lPos == null || rPos == null)
		{
			return;
		}
		lock (_poseLock)
		{
			Array.Copy(lPos, lFramePos, JointCount);
			Array.Copy(rPos, rFramePos, JointCount);
			_leftFrameValid = leftValid;
			_rightFrameValid = rightValid;
			_leftFrameAction = _latestLeftAction;
			_leftFrameActionParam = _latestLeftActionParam;
			_rightFrameAction = _latestRightAction;
			_rightFrameActionParam = _latestRightActionParam;
		}
	}

	private void TryProcessResize(float lAction, float lActionParam, float rAction, float rActionParam)
	{
		float num = 0f;
		float param = 0f;
		bool flag = false;
		if (leftHandPos.y < _camera.transform.position.y - 0.25f || rightHandPos.y < _camera.transform.position.y - 0.25f)
		{
			CancelActiveResize("hand too low");
			return;
		}
		if (Math.Abs(lAction - 5f) < 1E-06f || Math.Abs(lAction - 6f) < 1E-06f)
		{
			num = lAction;
			param = lActionParam;
			flag = true;
		}
		else if (Math.Abs(rAction - 5f) < 1E-06f || Math.Abs(rAction - 6f) < 1E-06f)
		{
			num = rAction;
			param = rActionParam;
			flag = true;
		}
		if (!flag)
		{
			CancelActiveResize("resize action lost");
			return;
		}
		bool flag2 = Math.Abs(num - 5f) < 1E-06f;
		bool flag3 = Math.Abs(num - 6f) < 1E-06f;
		if (flag2)
		{
			if (!m_IsResizing)
			{
				m_IsResizing = true;
			}
			ProcessHandResize(num, param);
		}
		else if (flag3)
		{
			CompleteResize(param);
		}
	}

	private void CancelActiveResize(string reason)
	{
		if (m_IsResizing)
		{
			VitureCommonLibrary.Logger.Info("Resize cancelled: " + reason);
			CompleteResize(0f);
		}
	}

	private void CompleteResize(float param)
	{
		if (m_IsResizing)
		{
			m_IsResizing = false;
			ProcessHandResize(6f, param);
		}
	}

	private static void ProcessHandResize(float action, float param)
	{
		VitureCommonLibrary.Logger.Info($"action: {action} param: {param}");
		if (Math.Abs(action - 5f) < 1E-06f)
		{
			VitureCommonLibrary.Logger.Info($"Hand Resizing: {param}");
			HandJointsVisualizer.HandPoseChanged?.Invoke(arg1: true, param);
		}
		if (Math.Abs(action - 6f) < 1E-06f)
		{
			VitureCommonLibrary.Logger.Info($"Hand Resized: {param}");
			HandJointsVisualizer.HandPoseChanged?.Invoke(arg1: false, param);
		}
	}

	private void OnGesturePoseUpdate(float[] pose)
	{
		ParseHandPose(pose);
	}

	private void Update()
	{
		CopyFrameState();
		TryProcessResize(_leftFrameAction, _leftFrameActionParam, _rightFrameAction, _rightFrameActionParam);
		if (renderJointSprites && (Application.isEditor || _glassesModel.Contains("P6S")))
		{
			UpdateJoints();
		}
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
			}
		}
	}

	private void InitHandJoints()
	{
		leftPoints = new GameObject[JointCount];
		rightPoints = new GameObject[JointCount];
		lPos = new Vector3[JointCount];
		rPos = new Vector3[JointCount];
		for (int i = 0; i < JointCount; i++)
		{
			if (ShouldSkipJoint(i))
			{
				leftPoints[i] = null;
				rightPoints[i] = null;
				continue;
			}
			string texturePathForJoint = GetTexturePathForJoint(isLeftHand: true, i);
			string texturePathForJoint2 = GetTexturePathForJoint(isLeftHand: false, i);
			leftPoints[i] = CreateJointSprite("LeftJoint_" + i, texturePathForJoint, GetScaleMultiplier(texturePathForJoint));
			rightPoints[i] = CreateJointSprite("RightJoint_" + i, texturePathForJoint2, GetScaleMultiplier(texturePathForJoint2));
		}
	}

	private static bool ShouldSkipJoint(int jointIndex)
	{
		if (jointIndex != 5)
		{
			return jointIndex >= 21;
		}
		return true;
	}

	private static float GetScaleMultiplier(string texturePath)
	{
		switch (texturePath)
		{
		case "hand_points/hand_1":
		case "hand_points/hand_2":
			return 0.5f;
		case "hand_points/hand_3":
			return 0.6f;
		default:
			return 1f;
		}
	}

	private string GetTexturePathForJoint(bool isLeftHand, int jointIndex)
	{
		if (jointTextureOverrides != null)
		{
			for (int i = 0; i < jointTextureOverrides.Length; i++)
			{
				JointTextureOverride jointTextureOverride = jointTextureOverrides[i];
				if (jointTextureOverride != null && jointTextureOverride.isLeftHand == isLeftHand && jointTextureOverride.jointIndex == jointIndex)
				{
					if (!string.IsNullOrWhiteSpace(jointTextureOverride.texturePath))
					{
						return jointTextureOverride.texturePath;
					}
					return null;
				}
			}
		}
		return GetDefaultTexturePathByJointIndex(jointIndex);
	}

	private static string GetDefaultTexturePathByJointIndex(int jointIndex)
	{
		switch (jointIndex)
		{
		case 20:
			return "hand_points/hand_0";
		case 6:
		case 8:
		case 11:
		case 14:
		case 17:
			return "hand_points/hand_1";
		case 7:
		case 9:
		case 10:
		case 12:
		case 13:
		case 15:
		case 16:
		case 18:
		case 19:
			return "hand_points/hand_2";
		case 0:
		case 1:
		case 2:
		case 3:
		case 4:
			return "hand_points/hand_3";
		default:
			return null;
		}
	}

	private GameObject CreateJointSprite(string jointName, string texturePathOverride, float scaleMultiplier)
	{
		GameObject gameObject;
		if (jointSpritePrefab != null)
		{
			gameObject = UnityEngine.Object.Instantiate(jointSpritePrefab, base.transform);
			gameObject.name = jointName;
			gameObject.transform.localScale = gameObject.transform.localScale * scaleMultiplier;
		}
		else
		{
			gameObject = new GameObject(jointName);
			gameObject.transform.SetParent(base.transform, worldPositionStays: false);
			gameObject.transform.localScale = Vector3.one * jointSpriteScale * scaleMultiplier;
			if (gameObject.GetComponent<SpriteRenderer>() == null)
			{
				gameObject.AddComponent<SpriteRenderer>();
			}
			if (gameObject.GetComponent<BillboardSprite>() == null)
			{
				gameObject.AddComponent<BillboardSprite>();
			}
		}
		BillboardSprite component = gameObject.GetComponent<BillboardSprite>();
		if (component != null && !string.IsNullOrWhiteSpace(texturePathOverride))
		{
			component.SetTexturePath(texturePathOverride);
		}
		return gameObject;
	}

	private void UpdateJoints()
	{
		if (lFramePos == null || lFramePos.Length != JointCount)
		{
			Debug.LogError("Invalid joint lPos array!");
			return;
		}
		if (rFramePos == null || rFramePos.Length != JointCount)
		{
			Debug.LogError("Invalid joint rPos array!");
			return;
		}
		bool flag = deviceManager?.handTrack ?? false;
		for (int i = 0; i < JointCount; i++)
		{
			if (leftPoints[i] != null && _leftFrameValid && flag)
			{
				leftPoints[i].SetActive(value: true);
				leftPoints[i].transform.position = lFramePos[i];
			}
			else if (leftPoints[i] != null)
			{
				leftPoints[i].SetActive(value: false);
			}
			if (rightPoints[i] != null && _rightFrameValid && flag)
			{
				rightPoints[i].SetActive(value: true);
				rightPoints[i].transform.position = rFramePos[i];
			}
			else if (rightPoints[i] != null)
			{
				rightPoints[i].SetActive(value: false);
			}
		}
	}
}
