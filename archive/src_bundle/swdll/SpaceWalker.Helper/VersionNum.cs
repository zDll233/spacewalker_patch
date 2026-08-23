using System;
using System.Linq;

namespace SpaceWalker.Helper;

public class VersionNum
{
	private int[] _versionNum = Array.Empty<int>();

	private string _version = string.Empty;

	public string Version
	{
		get
		{
			return _version;
		}
		set
		{
			_version = value;
			_versionNum = (from _ in _version.Split('.')
				select int.Parse(_)).ToArray();
		}
	}

	public int CompareTo(VersionNum other)
	{
		for (int i = 0; i < _versionNum.Length; i++)
		{
			if (_versionNum[i] != other._versionNum[i])
			{
				return _versionNum[i].CompareTo(other._versionNum[i]);
			}
		}
		return 0;
	}

	public static bool operator ==(VersionNum left, VersionNum right)
	{
		if ((object)left == null && (object)right == null)
		{
			return true;
		}
		if ((object)left == null || (object)right == null)
		{
			return false;
		}
		return left.CompareTo(right) == 0;
	}

	public static bool operator !=(VersionNum left, VersionNum right)
	{
		return !(left == right);
	}

	public static bool operator <(VersionNum left, VersionNum right)
	{
		return left.CompareTo(right) < 0;
	}

	public static bool operator >(VersionNum left, VersionNum right)
	{
		return left.CompareTo(right) > 0;
	}

	public static bool operator <=(VersionNum left, VersionNum right)
	{
		return left.CompareTo(right) <= 0;
	}

	public static bool operator >=(VersionNum left, VersionNum right)
	{
		return left.CompareTo(right) >= 0;
	}

	public override bool Equals(object? obj)
	{
		if (obj is VersionNum versionNum)
		{
			return this == versionNum;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return _versionNum.Aggregate(0, (int current, int c) => current ^ c.GetHashCode());
	}

	public override string ToString()
	{
		return string.Join('.', _versionNum);
	}
}
