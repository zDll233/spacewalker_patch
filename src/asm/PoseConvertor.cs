using System;
using System.Linq;
using SpaceWalker.Ipc;
using UnityEngine;
using VitureCommonLibrary;

internal static class PoseConvertor
{
	private static Matrix4x4 originImuMatrix = Matrix4x4.identity;

	private static Pose handlePose = Pose.identity;

	internal static Quaternion CheckLockAxis(Quaternion rotation, LockAxisState lockAxis, Quaternion reference)
	{
		if (lockAxis == LockAxisState.Unlock)
		{
			return rotation;
		}
		Quaternion rotation2 = Quaternion.Inverse(reference) * rotation;
		rotation2 = CheckLockAxis(rotation2, lockAxis);
		return reference * rotation2;
	}

	internal static Quaternion CheckLockAxis(Quaternion rotation, LockAxisState lockAxis = LockAxisState.Unlock)
	{
		switch (lockAxis)
		{
		case LockAxisState.Unlock:
			return rotation;
		case LockAxisState.LockX:
			rotation = RemoveTwist(rotation, Vector3.up);
			break;
		case LockAxisState.LockY:
			rotation = RemoveTwist(rotation, Vector3.right);
			break;
		case LockAxisState.LockZ:
			rotation = RemoveTwist(rotation, Vector3.forward);
			break;
		case LockAxisState.LockXY:
			rotation = RemoveTwist(rotation, Vector3.up);
			rotation = RemoveTwist(rotation, Vector3.right);
			break;
		case LockAxisState.LockXZ:
			rotation = RemoveTwist(rotation, Vector3.up);
			rotation = RemoveTwist(rotation, Vector3.forward);
			break;
		case LockAxisState.LockYZ:
			rotation = RemoveTwist(rotation, Vector3.right);
			rotation = RemoveTwist(rotation, Vector3.forward);
			break;
		case LockAxisState.LockXYZ:
			rotation = Quaternion.identity;
			break;
		}
		return rotation;
	}

	private static Quaternion RemoveTwist(Quaternion q, Vector3 axis)
	{
		axis = axis.normalized;
		float num = Vector3.Dot(new Vector3(q.x, q.y, q.z), axis);
		Quaternion quaternion = new Quaternion(axis.x * num, axis.y * num, axis.z * num, q.w);
		float num2 = Mathf.Sqrt(quaternion.x * quaternion.x + quaternion.y * quaternion.y + quaternion.z * quaternion.z + quaternion.w * quaternion.w);
		if (num2 < 0.001f)
		{
			return q;
		}
		float num3 = 1f / num2;
		quaternion = new Quaternion(quaternion.x * num3, quaternion.y * num3, quaternion.z * num3, quaternion.w * num3);
		if (num2 < 0.05f)
		{
			float t = Mathf.SmoothStep(0f, 1f, (num2 - 0.001f) / 0.049000002f);
			quaternion = Quaternion.Slerp(Quaternion.identity, quaternion, t);
		}
		return q * Quaternion.Inverse(quaternion);
	}

	internal static Quaternion GetRotation(float pitch, float yaw, float roll)
	{
		Quaternion quaternion = Quaternion.AngleAxis(yaw, Vector3.up);
		Quaternion quaternion2 = Quaternion.AngleAxis(pitch, Vector3.right);
		Quaternion quaternion3 = Quaternion.AngleAxis(roll, Vector3.forward);
		return Quaternion.identity * quaternion * quaternion2 * quaternion3;
	}

	internal static Matrix4x4 GetOriginImuMatrix(float[] pose, bool isP6s = false, bool isS6 = false)
	{
		if (!pose.Any((float x) => Math.Abs(x) > 1E-06f))
		{
			return originImuMatrix;
		}
		Matrix4x4 identity = Matrix4x4.identity;
		identity.SetColumn(0, new Vector4(pose[0], pose[1], pose[2], pose[3]));
		identity.SetColumn(1, new Vector4(pose[4], pose[5], pose[6], pose[7]));
		identity.SetColumn(2, new Vector4(pose[8], pose[9], pose[10], pose[11]));
		identity.SetColumn(3, new Vector4(pose[12], pose[13], pose[14], pose[15]));
		if (!identity.ValidTRS())
		{
			VitureCommonLibrary.Logger.Warning("GetOriginImuMatrix: pose matrix is invalid TRS");
			return originImuMatrix;
		}
		Quaternion identity2 = Quaternion.identity;
		Vector3 zero = Vector3.zero;
		try
		{
			if (isS6)
			{
				identity2 = identity.rotation;
				zero = new Vector3(pose[12], pose[13], pose[14]);
			}
			else if (isP6s)
			{
				identity2 = new Quaternion(identity.rotation.x, 0f - identity.rotation.z, identity.rotation.y, identity.rotation.w);
				zero = new Vector3(0f - pose[12], pose[14], 0f - pose[13]);
			}
			else
			{
				identity2 = new Quaternion(0f - identity.rotation.x, 0f - identity.rotation.y, identity.rotation.z, identity.rotation.w);
				zero = new Vector3(pose[12], pose[13], 0f - pose[14]);
			}
			originImuMatrix = Matrix4x4.TRS(zero, identity2, Vector3.one);
		}
		catch (Exception ex)
		{
			VitureCommonLibrary.Logger.Warning(ex.Message);
		}
		return originImuMatrix;
	}

	internal static Pose MatrixToPose(float[] pose)
	{
		Matrix4x4 identity = Matrix4x4.identity;
		identity.SetColumn(0, new Vector4(pose[0], pose[1], pose[2], pose[3]));
		identity.SetColumn(1, new Vector4(pose[4], pose[5], pose[6], pose[7]));
		identity.SetColumn(2, new Vector4(pose[8], pose[9], pose[10], pose[11]));
		identity.SetColumn(3, new Vector4(pose[12], pose[13], pose[14], pose[15]));
		if (identity.ValidTRS())
		{
			Matrix4x4 matrix4x = Matrix4x4.TRS(q: new Quaternion(identity.rotation.x, 0f - identity.rotation.z, identity.rotation.y, identity.rotation.w), pos: new Vector3(0f - pose[12], pose[14], 0f - pose[13]), s: Vector3.one) * originImuMatrix.inverse;
			handlePose.rotation = matrix4x.rotation;
			handlePose.position = new Vector3(matrix4x[12], matrix4x[13], matrix4x[14]);
		}
		else
		{
			VitureCommonLibrary.Logger.Warning("Pose matrix is invalid");
		}
		return handlePose;
	}

	internal static Quaternion WorldRotation(Quaternion rotation)
	{
		return rotation * Quaternion.Inverse(originImuMatrix.rotation);
	}
}
