using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace VitureCommonLibrary;

public static class YamlParseHelper
{
	public static CameraParam GetParamFromYaml(string yamlConfig)
	{
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		CameraParam cameraParam = default(CameraParam);
		cameraParam.ExtrinsicsL = new float[16];
		cameraParam.IntrinsicsL = new float[4];
		cameraParam.DistL = new float[4];
		cameraParam.ExtrinsicsR = new float[16];
		cameraParam.IntrinsicsR = new float[4];
		cameraParam.DistR = new float[4];
		cameraParam.ExtrinsicsLR = new float[16];
		CameraParam result = cameraParam;
		if (string.IsNullOrWhiteSpace(yamlConfig))
		{
			return result;
		}
		string[] array = yamlConfig.Split(new string[2] { "\r\n", "\n" }, StringSplitOptions.None);
		int num = 0;
		string[] array2 = array;
		for (int i = 0; i < array2.Length && !array2[i].StartsWith("Imu.gyro_bias:"); i++)
		{
			num++;
		}
		try
		{
			yamlConfig = string.Join(Environment.NewLine, array.Skip(num + 1).Take(33));
			YamlStream val = new YamlStream();
			val.Load((TextReader)new StringReader(yamlConfig));
			YamlMappingNode val2 = (YamlMappingNode)val.Documents[0].RootNode;
			if (((IDictionary<YamlNode, YamlNode>)val2.Children).TryGetValue(YamlNode.op_Implicit("cam0"), out YamlNode value))
			{
				YamlMappingNode val3 = (YamlMappingNode)(object)((value is YamlMappingNode) ? value : null);
				if (val3 != null)
				{
					result.ExtrinsicsL = Transpose4x4Matrix(QuaryFloatArrayNode(val3, "T_cam_imu"));
					result.IntrinsicsL = QuaryFloatArrayNode(val3, "intrinsics");
					result.DistL = QuaryFloatArrayNode(val3, "distortion_coeffs", 4);
					goto IL_0173;
				}
			}
			Logger.Warning("cam0 node not found in YAML");
			goto IL_0173;
			IL_0173:
			if (!((IDictionary<YamlNode, YamlNode>)val2.Children).TryGetValue(YamlNode.op_Implicit("cam1"), out YamlNode value2))
			{
				goto IL_01f4;
			}
			YamlMappingNode val4 = (YamlMappingNode)(object)((value2 is YamlMappingNode) ? value2 : null);
			if (val4 == null)
			{
				goto IL_01f4;
			}
			result.ExtrinsicsR = Transpose4x4Matrix(QuaryFloatArrayNode(val4, "T_cam_imu"));
			result.IntrinsicsR = QuaryFloatArrayNode(val4, "intrinsics");
			result.DistR = QuaryFloatArrayNode(val4, "distortion_coeffs", 4);
			result.ExtrinsicsLR = Transpose4x4Matrix(QuaryFloatArrayNode(val4, "T_cn_cnm1"));
			goto end_IL_00be;
			IL_01f4:
			Logger.Warning("cam1 node not found in YAML");
			end_IL_00be:;
		}
		catch (Exception ex)
		{
			Logger.Warning(ex.Message);
		}
		return result;
	}

	public static float[] Transpose4x4Matrix(float[] matrix)
	{
		if (matrix == null || matrix.Length != 16)
		{
			Logger.Error("Input must be a non-null float array of length 16.");
			return Array.Empty<float>();
		}
		for (int i = 0; i < 4; i++)
		{
			for (int j = i + 1; j < 4; j++)
			{
				int num = i * 4 + j;
				int num2 = j * 4 + i;
				float num3 = matrix[num];
				matrix[num] = matrix[num2];
				matrix[num2] = num3;
			}
		}
		return matrix;
	}

	public static float[] QuaryFloatArrayNode(YamlMappingNode mappingNode, string key, int size = 0)
	{
		if (((IDictionary<YamlNode, YamlNode>)mappingNode.Children).TryGetValue(YamlNode.op_Implicit(key), out YamlNode value))
		{
			YamlSequenceNode val = (YamlSequenceNode)(object)((value is YamlSequenceNode) ? value : null);
			if (val != null)
			{
				float[] array = val.Children.Select((YamlNode node) => Convert.ToSingle(((object)node).ToString(), CultureInfo.InvariantCulture)).ToArray();
				if (size > 0 && array.Length < size)
				{
					Array.Resize(ref array, size);
				}
				return array;
			}
		}
		else
		{
			Logger.Warning("QuaryFloatArrayNode Not found " + key + " node in YAML");
		}
		return new float[size];
	}

	public static float[] GetImuOriginPose(string yamlConfig)
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		float[] result = new float[16];
		if (string.IsNullOrWhiteSpace(yamlConfig))
		{
			return result;
		}
		string[] array = yamlConfig.Split(new string[2] { "\r\n", "\n" }, StringSplitOptions.None);
		int num = 0;
		string[] array2 = array;
		for (int i = 0; i < array2.Length && !array2[i].StartsWith("OptData:"); i++)
		{
			num++;
		}
		try
		{
			yamlConfig = string.Join(Environment.NewLine, array.Skip(num).Take(17));
			YamlStream val = new YamlStream();
			val.Load((TextReader)new StringReader(yamlConfig));
			if (!((IDictionary<YamlNode, YamlNode>)((YamlMappingNode)val.Documents[0].RootNode).Children).TryGetValue(YamlNode.op_Implicit("OptData"), out YamlNode value))
			{
				goto IL_016b;
			}
			YamlMappingNode val2 = (YamlMappingNode)(object)((value is YamlMappingNode) ? value : null);
			if (val2 == null)
			{
				goto IL_016b;
			}
			if (!((IDictionary<YamlNode, YamlNode>)val2.Children).TryGetValue(YamlNode.op_Implicit("display"), out YamlNode value2))
			{
				goto IL_015f;
			}
			YamlMappingNode val3 = (YamlMappingNode)(object)((value2 is YamlMappingNode) ? value2 : null);
			if (val3 == null)
			{
				goto IL_015f;
			}
			if (((IDictionary<YamlNode, YamlNode>)val3.Children).TryGetValue(YamlNode.op_Implicit("T_left_imu"), out YamlNode value3))
			{
				YamlSequenceNode val4 = (YamlSequenceNode)(object)((value3 is YamlSequenceNode) ? value3 : null);
				if (val4 != null)
				{
					float[] array3 = val4.Children.Select((YamlNode node) => Convert.ToSingle(((object)node).ToString(), CultureInfo.InvariantCulture)).ToArray();
					if (array3.Length == 16)
					{
						result = array3;
					}
				}
			}
			else
			{
				Logger.Warning("T_left_imu node not found under display");
			}
			goto end_IL_005b;
			IL_015f:
			Logger.Warning("display node not found under OptData");
			goto end_IL_005b;
			IL_016b:
			Logger.Warning("OptData node not found in YAML");
			end_IL_005b:;
		}
		catch (Exception ex)
		{
			Logger.Warning(ex.Message);
		}
		return result;
	}

	public static List<Vector3D> GetBiasData(string yamlConfig)
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		List<Vector3D> list = new List<Vector3D>();
		if (string.IsNullOrWhiteSpace(yamlConfig))
		{
			return list;
		}
		string[] array = yamlConfig.Split(new string[2] { "\r\n", "\n" }, StringSplitOptions.None);
		int num = 0;
		string[] array2 = array;
		for (int i = 0; i < array2.Length && !array2[i].StartsWith("Imu.acc_bias:"); i++)
		{
			num++;
		}
		try
		{
			yamlConfig = string.Join(Environment.NewLine, array.Skip(num).Take(3));
			YamlStream val = new YamlStream();
			val.Load((TextReader)new StringReader(yamlConfig));
			YamlMappingNode val2 = (YamlMappingNode)val.Documents[0].RootNode;
			if (((IDictionary<YamlNode, YamlNode>)val2.Children).TryGetValue(YamlNode.op_Implicit("Imu.acc_bias"), out YamlNode value))
			{
				YamlSequenceNode val3 = (YamlSequenceNode)(object)((value is YamlSequenceNode) ? value : null);
				if (val3 != null)
				{
					float[] array3 = val3.Children.Select((YamlNode x) => Convert.ToSingle(((object)x).ToString(), CultureInfo.InvariantCulture)).ToArray();
					list.Add(new Vector3D
					{
						X = array3[0],
						Y = array3[1],
						Z = array3[2]
					});
					goto IL_012c;
				}
			}
			Logger.Warning("Imu.acc_bias not found in YAML");
			goto IL_012c;
			IL_0249:
			Logger.Warning("Imu.gyro_bias not found in YAML");
			goto end_IL_0059;
			IL_01c0:
			if (!((IDictionary<YamlNode, YamlNode>)val2.Children).TryGetValue(YamlNode.op_Implicit("Imu.gyro_bias"), out YamlNode value2))
			{
				goto IL_0249;
			}
			YamlSequenceNode val4 = (YamlSequenceNode)(object)((value2 is YamlSequenceNode) ? value2 : null);
			if (val4 == null)
			{
				goto IL_0249;
			}
			float[] array4 = val4.Children.Select((YamlNode x) => Convert.ToSingle(((object)x).ToString(), CultureInfo.InvariantCulture)).ToArray();
			list.Add(new Vector3D
			{
				X = array4[0],
				Y = array4[1],
				Z = array4[2]
			});
			goto end_IL_0059;
			IL_012c:
			if (((IDictionary<YamlNode, YamlNode>)val2.Children).TryGetValue(YamlNode.op_Implicit("Imu.acc_scale"), out YamlNode value3))
			{
				YamlSequenceNode val5 = (YamlSequenceNode)(object)((value3 is YamlSequenceNode) ? value3 : null);
				if (val5 != null)
				{
					float[] array5 = val5.Children.Select((YamlNode x) => Convert.ToSingle(((object)x).ToString(), CultureInfo.InvariantCulture)).ToArray();
					list.Add(new Vector3D
					{
						X = array5[0],
						Y = array5[1],
						Z = array5[2]
					});
					goto IL_01c0;
				}
			}
			Logger.Warning("Imu.acc_scale not found in YAML");
			goto IL_01c0;
			end_IL_0059:;
		}
		catch (Exception ex)
		{
			Logger.Warning(ex.Message);
		}
		return list;
	}
}
