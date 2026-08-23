using System;
using System.IO;
using System.Text;
using System.Threading;

namespace VitureCommonLibrary;

public static class P6BiasReader
{
	private static int yamlConfigSize = 0;

	private static int yamlConfigVersion = 0;

	private static int recvYAMLConfigByteCount = 0;

	private static int currentYamlReadCount = 0;

	private static byte[]? yamlConfigBytes = null;

	private static string _yamlContent = string.Empty;

	private static ManualResetEventSlim RecvYAMLConfigSlim = new ManualResetEventSlim(initialState: false);

	public static string GetYamlConfig(string sn, bool runInEditor = false)
	{
		if (runInEditor)
		{
			return File.ReadAllText("config.yaml", Encoding.UTF8);
		}
		if (string.IsNullOrWhiteSpace(sn))
		{
			return string.Empty;
		}
		if (!string.IsNullOrWhiteSpace(_yamlContent))
		{
			return _yamlContent;
		}
		string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "VITURE", "SpaceWalker");
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string path = Path.Combine(text, "factory_bias_" + sn + ".yaml");
		if (File.Exists(path))
		{
			_yamlContent = File.ReadAllText(path, Encoding.UTF8);
		}
		if (!string.IsNullOrWhiteSpace(_yamlContent))
		{
			return _yamlContent;
		}
		if (GlassesDeviceManager.Instance.UseHidDevice)
		{
			GlassesMsgSemaphore.SendMsgAndAwaitAck(DeviceMsgId.MSG_R_DOF6_PARA_INFO);
			try
			{
				while (recvYAMLConfigByteCount < yamlConfigSize)
				{
					RecvYAMLConfigSlim.Reset();
					int num = yamlConfigSize - recvYAMLConfigByteCount;
					currentYamlReadCount = ((num < 44) ? num : 44);
					GetYAMLConfigCmd getYAMLConfigCmd = default(GetYAMLConfigCmd);
					getYAMLConfigCmd.Version = (ushort)yamlConfigVersion;
					getYAMLConfigCmd.Offset = (uint)recvYAMLConfigByteCount;
					getYAMLConfigCmd.Len = (ushort)currentYamlReadCount;
					getYAMLConfigCmd.TotalLen = (uint)yamlConfigSize;
					GetYAMLConfigCmd getYAMLConfigCmd2 = getYAMLConfigCmd;
					GlassesMsgSemaphore.SendMsgAndAwaitAck(DeviceMsgId.MSG_R_READ_DOF6_PARA, getYAMLConfigCmd2.ToBytes());
					RecvYAMLConfigSlim.Wait(3000);
				}
			}
			catch (Exception ex)
			{
				Logger.Warning(ex.Message);
			}
			_yamlContent = ((yamlConfigBytes == null) ? string.Empty : Encoding.UTF8.GetString(yamlConfigBytes));
		}
		else
		{
			_yamlContent = CarinaNative.GetConfig();
		}
		if (!string.IsNullOrWhiteSpace(_yamlContent))
		{
			Logger.Info(_yamlContent);
			File.WriteAllText(path, _yamlContent, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
		}
		return _yamlContent;
	}

	public static void ProcessMsg(HidMessage hidMsg)
	{
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_DOF6_PARA_INFO))
		{
			try
			{
				yamlConfigVersion = (hidMsg.Data.Payload[1] << 8) + hidMsg.Data.Payload[0];
				yamlConfigSize = (hidMsg.Data.Payload[3] << 24) + (hidMsg.Data.Payload[4] << 16) + (hidMsg.Data.Payload[5] << 8) + hidMsg.Data.Payload[6];
				if (yamlConfigSize > 0)
				{
					yamlConfigBytes = new byte[yamlConfigSize];
				}
			}
			catch (Exception ex)
			{
				Logger.Warning(ex.Message);
			}
			GlassesMsgSemaphore.ReleaseSemaphore(107);
		}
		if (!hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_READ_DOF6_PARA))
		{
			return;
		}
		try
		{
			if (yamlConfigBytes != null && currentYamlReadCount > 0 && currentYamlReadCount < hidMsg.Data.Payload.Length - 1 && recvYAMLConfigByteCount + currentYamlReadCount <= yamlConfigBytes.Length)
			{
				Array.Copy(hidMsg.Data.Payload, 1, yamlConfigBytes, recvYAMLConfigByteCount, currentYamlReadCount);
				recvYAMLConfigByteCount += currentYamlReadCount;
				RecvYAMLConfigSlim.Set();
			}
		}
		catch (Exception ex2)
		{
			Logger.Warning(ex2.Message);
		}
		GlassesMsgSemaphore.ReleaseSemaphore(108);
	}

	public static void Clear()
	{
		yamlConfigBytes = null;
		_yamlContent = string.Empty;
	}

	public static void ClearFile(string sn)
	{
		string path = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "VITURE", "SpaceWalker"), "factory_bias_" + sn + ".yaml");
		if (File.Exists(path))
		{
			File.Delete(path);
		}
	}
}
