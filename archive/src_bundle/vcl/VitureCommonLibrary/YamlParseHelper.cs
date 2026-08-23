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
			YamlStream yamlStream = new YamlStream();
			yamlStream.Load(new StringReader(yamlConfig));
			YamlMappingNode obj = (YamlMappingNode)yamlStream.Documents[0].RootNode;
			if (obj.Children.TryGetValue("cam0", out YamlNode value) && value is YamlMappingNode mappingNode)
			{
				result.ExtrinsicsL = Transpose4x4Matrix(QuaryFloatArrayNode(mappingNode, "T_cam_imu"));
				result.IntrinsicsL = QuaryFloatArrayNode(mappingNode, "intrinsics");
				result.DistL = QuaryFloatArrayNode(mappingNode, "distortion_coeffs", 4);
			}
			else
			{
				Logger.Warning("cam0 node not found in YAML");
			}
			if (obj.Children.TryGetValue("cam1", out YamlNode value2) && value2 is YamlMappingNode mappingNode2)
			{
				result.ExtrinsicsR = Transpose4x4Matrix(QuaryFloatArrayNode(mappingNode2, "T_cam_imu"));
				result.IntrinsicsR = QuaryFloatArrayNode(mappingNode2, "intrinsics");
				result.DistR = QuaryFloatArrayNode(mappingNode2, "distortion_coeffs", 4);
				result.ExtrinsicsLR = Transpose4x4Matrix(QuaryFloatArrayNode(mappingNode2, "T_cn_cnm1"));
			}
			else
			{
				Logger.Warning("cam1 node not found in YAML");
			}
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
		if (mappingNode.Children.TryGetValue(key, out YamlNode value))
		{
			if (value is YamlSequenceNode yamlSequenceNode)
			{
				float[] array = yamlSequenceNode.Children.Select((YamlNode node) => Convert.ToSingle(node.ToString(), CultureInfo.InvariantCulture)).ToArray();
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
			YamlStream yamlStream = new YamlStream();
			yamlStream.Load(new StringReader(yamlConfig));
			if (((YamlMappingNode)yamlStream.Documents[0].RootNode).Children.TryGetValue("OptData", out YamlNode value) && value is YamlMappingNode yamlMappingNode)
			{
				if (yamlMappingNode.Children.TryGetValue("display", out YamlNode value2) && value2 is YamlMappingNode yamlMappingNode2)
				{
					if (yamlMappingNode2.Children.TryGetValue("T_left_imu", out YamlNode value3))
					{
						if (value3 is YamlSequenceNode yamlSequenceNode)
						{
							float[] array3 = yamlSequenceNode.Children.Select((YamlNode node) => Convert.ToSingle(node.ToString(), CultureInfo.InvariantCulture)).ToArray();
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
				}
				else
				{
					Logger.Warning("display node not found under OptData");
				}
			}
			else
			{
				Logger.Warning("OptData node not found in YAML");
			}
		}
		catch (Exception ex)
		{
			Logger.Warning(ex.Message);
		}
		return result;
	}

	public static List<Vector3D> GetBiasData(string yamlConfig)
	{
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
			YamlStream yamlStream = new YamlStream();
			yamlStream.Load(new StringReader(yamlConfig));
			YamlMappingNode obj = (YamlMappingNode)yamlStream.Documents[0].RootNode;
			if (obj.Children.TryGetValue("Imu.acc_bias", out YamlNode value) && value is YamlSequenceNode yamlSequenceNode)
			{
				float[] array3 = yamlSequenceNode.Children.Select((YamlNode x) => Convert.ToSingle(x.ToString(), CultureInfo.InvariantCulture)).ToArray();
				list.Add(new Vector3D
				{
					X = array3[0],
					Y = array3[1],
					Z = array3[2]
				});
			}
			else
			{
				Logger.Warning("Imu.acc_bias not found in YAML");
			}
			if (obj.Children.TryGetValue("Imu.acc_scale", out YamlNode value2) && value2 is YamlSequenceNode yamlSequenceNode2)
			{
				float[] array4 = yamlSequenceNode2.Children.Select((YamlNode x) => Convert.ToSingle(x.ToString(), CultureInfo.InvariantCulture)).ToArray();
				list.Add(new Vector3D
				{
					X = array4[0],
					Y = array4[1],
					Z = array4[2]
				});
			}
			else
			{
				Logger.Warning("Imu.acc_scale not found in YAML");
			}
			if (obj.Children.TryGetValue("Imu.gyro_bias", out YamlNode value3) && value3 is YamlSequenceNode yamlSequenceNode3)
			{
				float[] array5 = yamlSequenceNode3.Children.Select((YamlNode x) => Convert.ToSingle(x.ToString(), CultureInfo.InvariantCulture)).ToArray();
				list.Add(new Vector3D
				{
					X = array5[0],
					Y = array5[1],
					Z = array5[2]
				});
			}
			else
			{
				Logger.Warning("Imu.gyro_bias not found in YAML");
			}
		}
		catch (Exception ex)
		{
			Logger.Warning(ex.Message);
		}
		return list;
	}
}
