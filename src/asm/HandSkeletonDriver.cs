using System;
using UnityEngine;
using VitureCommonLibrary;

public class HandSkeletonDriver : MonoBehaviour
{
	[Serializable]
	private struct BoneBindingDefinition
	{
		public string suffix;

		public int jointIndex;

		public BoneBindingDefinition(string suffix, int jointIndex)
		{
			this.suffix = suffix;
			this.jointIndex = jointIndex;
		}
	}

	private sealed class BoneBinding
	{
		public string suffix;

		public int jointIndex;

		public int rotationTargetJointIndex;

		public Transform bone;

		public Vector3 initialLocalPosition;

		public Quaternion initialLocalRotation;

		public Vector3 initialLocalChildDirection;

		public bool hasInitialLocalChildDirection;

		public bool isWrist;
	}

	private const int PoseLength = 374;

	private const int JointCount = 26;

	private const int DrivenJointCount = 21;

	private const int LeftHandJointOffset = 1;

	private const int RightHandValidIndex = 187;

	private const int RightHandJointOffset = 188;

	private static readonly BoneBindingDefinition[] DefaultBindingDefinitions = new BoneBindingDefinition[21]
	{
		new BoneBindingDefinition("Wrist", 5),
		new BoneBindingDefinition("Palm", 20),
		new BoneBindingDefinition("ThumbProximal", 6),
		new BoneBindingDefinition("ThumbDistal", 7),
		new BoneBindingDefinition("ThumbTip", 0),
		new BoneBindingDefinition("IndexProximal", 8),
		new BoneBindingDefinition("IndexIntermediate", 9),
		new BoneBindingDefinition("IndexDistal", 10),
		new BoneBindingDefinition("IndexTip", 1),
		new BoneBindingDefinition("MiddleProximal", 11),
		new BoneBindingDefinition("MiddleIntermediate", 12),
		new BoneBindingDefinition("MiddleDistal", 13),
		new BoneBindingDefinition("MiddleTip", 2),
		new BoneBindingDefinition("RingProximal", 14),
		new BoneBindingDefinition("RingIntermediate", 15),
		new BoneBindingDefinition("RingDistal", 16),
		new BoneBindingDefinition("RingTip", 3),
		new BoneBindingDefinition("LittleProximal", 17),
		new BoneBindingDefinition("LittleIntermediate", 18),
		new BoneBindingDefinition("LittleDistal", 19),
		new BoneBindingDefinition("LittleTip", 4)
	};

	[SerializeField]
	private DeviceManager deviceManager;

	[SerializeField]
	private Transform leftHandRoot;

	[SerializeField]
	private Transform rightHandRoot;

	[SerializeField]
	private bool autoResolveReferences = true;

	[SerializeField]
	private bool keepLastPoseWhenHandInvalid = true;

	[SerializeField]
	private bool hideHandWhenInvalid = true;

	[SerializeField]
	private bool driveWristRotation = true;

	[SerializeField]
	private bool driveFingerRotation = true;

	[SerializeField]
	private bool driveFingerBonePosition;

	private readonly object _poseLock = new object();

	private readonly Vector3[] _leftJointPositions = new Vector3[26];

	private readonly Vector3[] _rightJointPositions = new Vector3[26];

	private readonly Vector3[] _leftFrameJointPositions = new Vector3[26];

	private readonly Vector3[] _rightFrameJointPositions = new Vector3[26];

	private BoneBinding[] _leftBindings;

	private BoneBinding[] _rightBindings;

	private bool _leftHandValid;

	private bool _rightHandValid;

	private bool _hasLoggedMissingReferences;

	private void Reset()
	{
		ResolveReferences();
	}

	private void Awake()
	{
		ResolveReferences();
		CacheBindings();
		if (hideHandWhenInvalid)
		{
			SetHandVisibility(leftHandRoot, visible: false);
			SetHandVisibility(rightHandRoot, visible: false);
		}
	}

	private void OnEnable()
	{
		GestureManager.GesturePoseUpdate += OnGesturePoseUpdate;
	}

	private void OnDisable()
	{
		GestureManager.GesturePoseUpdate -= OnGesturePoseUpdate;
	}

	private void Update()
	{
		if (autoResolveReferences && (deviceManager == null || leftHandRoot == null || rightHandRoot == null || _leftBindings == null || _rightBindings == null))
		{
			ResolveReferences();
			CacheBindings();
		}
		if (!CanDriveHands())
		{
			if (hideHandWhenInvalid)
			{
				SetHandVisibility(leftHandRoot, visible: false);
				SetHandVisibility(rightHandRoot, visible: false);
			}
			return;
		}
		bool leftHandValid;
		bool rightHandValid;
		lock (_poseLock)
		{
			Array.Copy(_leftJointPositions, _leftFrameJointPositions, 26);
			Array.Copy(_rightJointPositions, _rightFrameJointPositions, 26);
			leftHandValid = _leftHandValid;
			rightHandValid = _rightHandValid;
		}
		if (hideHandWhenInvalid)
		{
			SetHandVisibility(leftHandRoot, leftHandValid);
			SetHandVisibility(rightHandRoot, rightHandValid);
		}
		if (leftHandValid || !keepLastPoseWhenHandInvalid)
		{
			ApplyHandPose(_leftBindings, _leftFrameJointPositions, leftHandValid, driveWristRotation, driveFingerRotation, driveFingerBonePosition, isLeftHand: true);
		}
		if (rightHandValid || !keepLastPoseWhenHandInvalid)
		{
			ApplyHandPose(_rightBindings, _rightFrameJointPositions, rightHandValid, driveWristRotation, driveFingerRotation, driveFingerBonePosition, isLeftHand: false);
		}
	}

	private bool CanDriveHands()
	{
		if (deviceManager == null)
		{
			if (!_hasLoggedMissingReferences)
			{
				VitureCommonLibrary.Logger.Warning("HandSkeletonDriver could not find DeviceManager. Driver will wait until references are resolved.");
				_hasLoggedMissingReferences = true;
			}
			return false;
		}
		if (leftHandRoot == null || rightHandRoot == null)
		{
			if (!_hasLoggedMissingReferences)
			{
				VitureCommonLibrary.Logger.Warning("HandSkeletonDriver is missing Left Hand Tracking or Right Hand Tracking root.");
				_hasLoggedMissingReferences = true;
			}
			return false;
		}
		_hasLoggedMissingReferences = false;
		return deviceManager.handTrack;
	}

	private void ResolveReferences()
	{
		if (deviceManager == null)
		{
			deviceManager = UnityEngine.Object.FindObjectOfType<DeviceManager>();
		}
		if (leftHandRoot == null)
		{
			leftHandRoot = FindChildRecursive(base.transform, "Left Hand Tracking");
		}
		if (rightHandRoot == null)
		{
			rightHandRoot = FindChildRecursive(base.transform, "Right Hand Tracking");
		}
	}

	private void CacheBindings()
	{
		_leftBindings = BuildBindings(leftHandRoot, "L_");
		_rightBindings = BuildBindings(rightHandRoot, "R_");
	}

	private static BoneBinding[] BuildBindings(Transform handRoot, string prefix)
	{
		if (handRoot == null)
		{
			return null;
		}
		BoneBinding[] array = new BoneBinding[DefaultBindingDefinitions.Length];
		for (int i = 0; i < DefaultBindingDefinitions.Length; i++)
		{
			BoneBindingDefinition boneBindingDefinition = DefaultBindingDefinitions[i];
			string text = prefix + boneBindingDefinition.suffix;
			Transform transform = FindChildRecursive(handRoot, text);
			if (transform == null)
			{
				VitureCommonLibrary.Logger.Warning("HandSkeletonDriver missing bone: " + text);
			}
			array[i] = new BoneBinding
			{
				suffix = boneBindingDefinition.suffix,
				jointIndex = boneBindingDefinition.jointIndex,
				rotationTargetJointIndex = GetRotationTargetJointIndex(boneBindingDefinition.suffix),
				bone = transform,
				initialLocalPosition = ((transform != null) ? transform.localPosition : Vector3.zero),
				initialLocalRotation = ((transform != null) ? transform.localRotation : Quaternion.identity),
				initialLocalChildDirection = GetInitialLocalChildDirection(transform, boneBindingDefinition.suffix, prefix),
				hasInitialLocalChildDirection = TryGetInitialChildDirectionState(transform, boneBindingDefinition.suffix, prefix),
				isWrist = (boneBindingDefinition.suffix == "Wrist")
			};
		}
		return array;
	}

	private static Transform FindChildRecursive(Transform parent, string targetName)
	{
		if (parent == null)
		{
			return null;
		}
		Transform[] componentsInChildren = parent.GetComponentsInChildren<Transform>(includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].name == targetName)
			{
				return componentsInChildren[i];
			}
		}
		return null;
	}

	private void OnGesturePoseUpdate(float[] pose)
	{
		lock (_poseLock)
		{
			if (pose == null || pose.Length != 374)
			{
				_leftHandValid = false;
				_rightHandValid = false;
				return;
			}
			_leftHandValid = pose[0] != 0f;
			_rightHandValid = pose[187] != 0f;
			if (_leftHandValid)
			{
				ParseHandPose(pose, 1, _leftJointPositions);
			}
			if (_rightHandValid)
			{
				ParseHandPose(pose, 188, _rightJointPositions);
			}
		}
	}

	private static void ParseHandPose(float[] pose, int jointOffset, Vector3[] output)
	{
		for (int i = 0; i < 21; i++)
		{
			int num = jointOffset + i * 7;
			float num2 = pose[num];
			float num3 = pose[num + 1];
			float y = pose[num + 2];
			output[i] = new Vector3(0f - num2, y, 0f - num3);
		}
	}

	private static void ApplyHandPose(BoneBinding[] bindings, Vector3[] joints, bool handValid, bool driveWristRotation, bool driveFingerRotation, bool driveFingerBonePosition, bool isLeftHand)
	{
		if (bindings == null)
		{
			return;
		}
		BoneBinding boneBinding = null;
		for (int i = 0; i < bindings.Length; i++)
		{
			if (bindings[i] != null && bindings[i].isWrist)
			{
				boneBinding = bindings[i];
				break;
			}
		}
		if (boneBinding != null && boneBinding.bone != null)
		{
			Quaternion wristWorldRotation;
			if (!handValid)
			{
				boneBinding.bone.localRotation = boneBinding.initialLocalRotation;
			}
			else if (driveWristRotation && TryComputeWristWorldRotation(joints, isLeftHand, out wristWorldRotation))
			{
				Transform parent = boneBinding.bone.parent;
				boneBinding.bone.localRotation = ((parent != null) ? (Quaternion.Inverse(parent.rotation) * wristWorldRotation) : wristWorldRotation);
			}
		}
		foreach (BoneBinding boneBinding2 in bindings)
		{
			if (boneBinding2 == null || boneBinding2.bone == null)
			{
				continue;
			}
			if (!handValid)
			{
				boneBinding2.bone.localPosition = boneBinding2.initialLocalPosition;
				boneBinding2.bone.localRotation = boneBinding2.initialLocalRotation;
				continue;
			}
			Vector3 vector = joints[boneBinding2.jointIndex];
			Transform parent2 = boneBinding2.bone.parent;
			if (parent2 == null)
			{
				boneBinding2.bone.position = vector;
				continue;
			}
			if (!driveFingerBonePosition && ShouldKeepInitialFingerLocalPosition(boneBinding2.suffix))
			{
				boneBinding2.bone.localPosition = boneBinding2.initialLocalPosition;
			}
			else
			{
				boneBinding2.bone.localPosition = parent2.InverseTransformPoint(vector);
			}
			if (driveFingerRotation && !boneBinding2.isWrist && boneBinding2.rotationTargetJointIndex >= 0 && boneBinding2.hasInitialLocalChildDirection)
			{
				Vector3 direction = joints[boneBinding2.rotationTargetJointIndex] - vector;
				if (!(direction.sqrMagnitude < 1E-08f))
				{
					Vector3 normalized = parent2.InverseTransformDirection(direction).normalized;
					Quaternion quaternion = Quaternion.FromToRotation((boneBinding2.initialLocalRotation * boneBinding2.initialLocalChildDirection).normalized, normalized);
					boneBinding2.bone.localRotation = quaternion * boneBinding2.initialLocalRotation;
				}
			}
		}
	}

	private static bool ShouldKeepInitialFingerLocalPosition(string suffix)
	{
		switch (suffix)
		{
		case "IndexProximal":
		case "IndexIntermediate":
		case "IndexDistal":
		case "MiddleProximal":
		case "MiddleIntermediate":
		case "MiddleDistal":
		case "RingProximal":
		case "RingIntermediate":
		case "RingDistal":
		case "LittleProximal":
		case "LittleIntermediate":
		case "LittleDistal":
			return true;
		default:
			return false;
		}
	}

	private static void SetHandVisibility(Transform handRoot, bool visible)
	{
		if (!(handRoot == null) && handRoot.gameObject.activeSelf != visible)
		{
			handRoot.gameObject.SetActive(visible);
		}
	}

	private static bool TryComputeWristWorldRotation(Vector3[] joints, bool isLeftHand, out Quaternion wristWorldRotation)
	{
		Vector3 vector = joints[5];
		Vector3 vector2 = joints[8];
		Vector3 vector3 = joints[11];
		Vector3 vector4 = joints[17];
		Vector3 vector5 = vector3 - vector;
		Vector3 vector6 = vector2 - vector4;
		if (vector5.sqrMagnitude < 1E-08f || vector6.sqrMagnitude < 1E-08f)
		{
			wristWorldRotation = Quaternion.identity;
			return false;
		}
		vector5.Normalize();
		vector6.Normalize();
		Vector3 upwards = (isLeftHand ? Vector3.Cross(vector5, vector6) : Vector3.Cross(vector6, vector5));
		if (upwards.sqrMagnitude < 1E-08f)
		{
			wristWorldRotation = Quaternion.identity;
			return false;
		}
		upwards.Normalize();
		wristWorldRotation = Quaternion.LookRotation(vector5, upwards);
		return true;
	}

	private static int GetRotationTargetJointIndex(string suffix)
	{
		return suffix switch
		{
			"ThumbProximal" => 7, 
			"ThumbDistal" => 0, 
			"IndexProximal" => 9, 
			"IndexIntermediate" => 10, 
			"IndexDistal" => 1, 
			"MiddleProximal" => 12, 
			"MiddleIntermediate" => 13, 
			"MiddleDistal" => 2, 
			"RingProximal" => 15, 
			"RingIntermediate" => 16, 
			"RingDistal" => 3, 
			"LittleProximal" => 18, 
			"LittleIntermediate" => 19, 
			"LittleDistal" => 4, 
			_ => -1, 
		};
	}

	private static Vector3 GetInitialLocalChildDirection(Transform bone, string suffix, string prefix)
	{
		if (bone == null)
		{
			return Vector3.forward;
		}
		Transform transform = FindChildRecursive(bone, prefix + GetChildSuffix(suffix));
		if (transform == null)
		{
			return Vector3.forward;
		}
		Vector3 vector = bone.InverseTransformDirection(transform.position - bone.position);
		if (vector.sqrMagnitude < 1E-08f)
		{
			return Vector3.forward;
		}
		return vector.normalized;
	}

	private static bool TryGetInitialChildDirectionState(Transform bone, string suffix, string prefix)
	{
		if (bone == null)
		{
			return false;
		}
		string childSuffix = GetChildSuffix(suffix);
		if (string.IsNullOrEmpty(childSuffix))
		{
			return false;
		}
		return FindChildRecursive(bone, prefix + childSuffix) != null;
	}

	private static string GetChildSuffix(string suffix)
	{
		return suffix switch
		{
			"ThumbProximal" => "ThumbDistal", 
			"ThumbDistal" => "ThumbTip", 
			"IndexProximal" => "IndexIntermediate", 
			"IndexIntermediate" => "IndexDistal", 
			"IndexDistal" => "IndexTip", 
			"MiddleProximal" => "MiddleIntermediate", 
			"MiddleIntermediate" => "MiddleDistal", 
			"MiddleDistal" => "MiddleTip", 
			"RingProximal" => "RingIntermediate", 
			"RingIntermediate" => "RingDistal", 
			"RingDistal" => "RingTip", 
			"LittleProximal" => "LittleIntermediate", 
			"LittleIntermediate" => "LittleDistal", 
			"LittleDistal" => "LittleTip", 
			_ => string.Empty, 
		};
	}
}
