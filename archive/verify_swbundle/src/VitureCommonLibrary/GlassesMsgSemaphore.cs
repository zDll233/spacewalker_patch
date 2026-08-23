using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace VitureCommonLibrary;

public static class GlassesMsgSemaphore
{
	private static readonly Dictionary<ushort, ManualResetEventSlim> messageAcks = new Dictionary<ushort, ManualResetEventSlim>();

	private static readonly Dictionary<byte, ManualResetEventSlim> otaMessageAcks = new Dictionary<byte, ManualResetEventSlim>();

	private static HidMessage? hidSendCacheMsg = null;

	private static R6NewerHidMessage? r6SendCacheMsg = null;

	private static UsbMessage? usbSendCacheMsg = null;

	private static UsbOtaMessage? otaSendCacheMsg = null;

	private static volatile bool p6sMcuUpdating = false;

	private static int retryCount = 0;

	private static int MaxRetryCount = 0;

	private static readonly object retryLock = new object();

	private static Type? GetMsgType()
	{
		Type type = null;
		if (!GlassesDeviceManager.Instance.UseHidDevice)
		{
			return typeof(UsbMessage);
		}
		if (GlassesDeviceManager.Instance.R6NewerModel)
		{
			return typeof(R6NewerHidMessage);
		}
		return typeof(HidMessage);
	}

	private static bool CheckAndConvertMsgIdType(DeviceMsgId msgId, Type? msgType, byte[]? bytes, int waitSec)
	{
		if (GlassesDeviceManager.Instance.R6NewerModel && DeviceMsgIdConverter.TryConvertToR6MsgId(msgId, out var r6MsgId))
		{
			return SendMsgAndAwaitAck(r6MsgId, msgType, bytes, waitSec);
		}
		return SendMsgAndAwaitAck(msgId, msgType, bytes, waitSec);
	}

	public static bool SendMsgAndAwaitAck(DeviceMsgId msgId, byte val, Type? msgType = null, int waitSec = 4)
	{
		byte[] bytes = new byte[1] { val };
		msgType = msgType ?? GetMsgType();
		return CheckAndConvertMsgIdType(msgId, msgType, bytes, waitSec);
	}

	public static bool SendMsgAndAwaitAck(DeviceMsgId msgId, ushort val, Type? msgType = null, int waitSec = 4)
	{
		byte[] bytes = BitConverter.GetBytes(val);
		msgType = msgType ?? GetMsgType();
		return CheckAndConvertMsgIdType(msgId, msgType, bytes, waitSec);
	}

	public static bool SendMsgAndAwaitAck(DeviceMsgId msgId, byte[]? bytes = null, int waitSec = 4)
	{
		Type msgType = GetMsgType();
		return CheckAndConvertMsgIdType(msgId, msgType, bytes, waitSec);
	}

	public static bool SendMsgAndAwaitAck(R6NewerMsgId msgId, byte[]? bytes = null, int waitSec = 4)
	{
		Type msgType = GetMsgType();
		return SendMsgAndAwaitAck(msgId, msgType, bytes, waitSec);
	}

	public static Task<bool> SendMsgAndAwaitAckAsync(DeviceMsgId msgId, byte val, Type? msgType = null, int waitSec = 4)
	{
		Type msgType2 = msgType;
		return Task.Run(() => SendMsgAndAwaitAck(msgId, val, msgType2, waitSec));
	}

	public static Task<bool> SendMsgAndAwaitAckAsync(DeviceMsgId msgId, ushort val, Type? msgType = null, int waitSec = 4)
	{
		Type msgType2 = msgType;
		return Task.Run(() => SendMsgAndAwaitAck(msgId, val, msgType2, waitSec));
	}

	public static Task<bool> SendMsgAndAwaitAckAsync(DeviceMsgId msgId, byte[]? bytes = null, int waitSec = 4)
	{
		byte[] bytes2 = bytes;
		return Task.Run(() => SendMsgAndAwaitAck(msgId, bytes2, waitSec));
	}

	public static Task<bool> SendMsgAndAwaitAckAsync(R6NewerMsgId msgId, byte[]? bytes = null, int waitSec = 4)
	{
		byte[] bytes2 = bytes;
		return Task.Run(() => SendMsgAndAwaitAck(msgId, bytes2, waitSec));
	}

	public static Task<bool> SendMsgAndAwaitAckAsync<TMsgIdType>(TMsgIdType msgId, Type? msgType, byte[]? bytes = null, int waitSec = 4) where TMsgIdType : Enum
	{
		TMsgIdType msgId2 = msgId;
		Type msgType2 = msgType;
		byte[] bytes2 = bytes;
		return Task.Run(() => SendMsgAndAwaitAck(msgId2, msgType2, bytes2, waitSec));
	}

	public static bool SendMsgAndAwaitAck<TMsgIdType>(TMsgIdType msgId, Type? msgType, byte[]? bytes = null, int waitSec = 4) where TMsgIdType : Enum
	{
		if (!(msgId is DeviceMsgId) && !(msgId is R6NewerMsgId) && !(msgId is UsbOtaCmd))
		{
			return false;
		}
		if (GlassesDeviceManager.Instance.Muted)
		{
			Logger.Info($"SendMsgAndAwaitAck: skipped — HID channel muted, msgId={msgId}");
			return false;
		}
		bool result = false;
		if (GlassesDeviceManager.Instance.UseHidDevice && msgType == typeof(HidMessage) && msgId is DeviceMsgId)
		{
			object obj = msgId;
			DeviceMsgId msgId2 = (DeviceMsgId)((obj is DeviceMsgId) ? obj : null);
			result = SendHidMessage(msgId2, bytes, waitSec);
		}
		else if (GlassesDeviceManager.Instance.UseHidDevice && msgType == typeof(R6NewerHidMessage) && msgId is R6NewerMsgId)
		{
			object obj2 = msgId;
			R6NewerMsgId msgId3 = (R6NewerMsgId)((obj2 is R6NewerMsgId) ? obj2 : null);
			result = SendR6HidMessage(msgId3, bytes, waitSec);
		}
		else if (!GlassesDeviceManager.Instance.UseHidDevice && msgType == typeof(UsbMessage) && msgId is DeviceMsgId)
		{
			object obj3 = msgId;
			DeviceMsgId msgId4 = (DeviceMsgId)((obj3 is DeviceMsgId) ? obj3 : null);
			result = SendUsbMessage(msgId4, bytes, waitSec);
		}
		else if (!GlassesDeviceManager.Instance.UseHidDevice && msgType == typeof(UsbOtaMessage) && msgId is UsbOtaCmd)
		{
			object obj4 = msgId;
			UsbOtaCmd otaCmd = (UsbOtaCmd)((obj4 is UsbOtaCmd) ? obj4 : null);
			result = SendUsbOtaMessage(otaCmd, bytes, waitSec);
		}
		return result;
	}

	private static bool SendHidMessage(DeviceMsgId msgId, byte[]? bytes, int waitSec = 4)
	{
		p6sMcuUpdating = false;
		HidMessage hidMessage = new HidMessage();
		hidMessage.Data.MsgID = (ushort)msgId;
		if (bytes != null)
		{
			Array.Copy(bytes, 0, hidMessage.Data.Payload, 0, bytes.Length);
			hidMessage.DataLength = ((bytes != null) ? bytes.Length : 0);
		}
		ManualResetEventSlim manualResetEventSlim;
		lock (messageAcks)
		{
			if (!messageAcks.ContainsKey(hidMessage.Data.MsgID))
			{
				messageAcks.Add(hidMessage.Data.MsgID, new ManualResetEventSlim(initialState: false));
			}
			manualResetEventSlim = messageAcks[hidMessage.Data.MsgID];
		}
		manualResetEventSlim.Reset();
		GlassesDeviceManager.Instance.SendMsg(hidMessage);
		hidSendCacheMsg = hidMessage;
		bool result = SendMessageAndHandleTimeout(manualResetEventSlim, (ushort)msgId, isOtaMessage: false, isR6MsgId: false, waitSec);
		lock (messageAcks)
		{
			if (messageAcks.ContainsKey(hidMessage.Data.MsgID))
			{
				messageAcks.Remove(hidMessage.Data.MsgID);
			}
		}
		hidSendCacheMsg = null;
		return result;
	}

	private static bool SendR6HidMessage(R6NewerMsgId msgId, byte[]? bytes, int waitSec = 4)
	{
		p6sMcuUpdating = false;
		R6NewerHidMessage r6NewerHidMessage = new R6NewerHidMessage();
		r6NewerHidMessage.MsgID = (ushort)msgId;
		if (bytes != null)
		{
			Array.Copy(bytes, 0, r6NewerHidMessage.Payload, 0, bytes.Length);
			r6NewerHidMessage.DataLen = (ushort)((bytes != null) ? ((uint)bytes.Length) : 0u);
		}
		ushort num = (ushort)(msgId & (R6NewerMsgId)255);
		ManualResetEventSlim manualResetEventSlim;
		lock (messageAcks)
		{
			if (!messageAcks.ContainsKey(num))
			{
				messageAcks.Add(num, new ManualResetEventSlim(initialState: false));
			}
			manualResetEventSlim = messageAcks[num];
		}
		manualResetEventSlim.Reset();
		Logger.Info($"[ACK-SEND] R6 MsgID=0x{(ushort)msgId:X4} waitKey=0x{num:X4} ts={DateTime.UtcNow:HH:mm:ss.fff}");
		GlassesDeviceManager.Instance.SendMsg(r6NewerHidMessage);
		r6SendCacheMsg = r6NewerHidMessage;
		bool flag = SendMessageAndHandleTimeout(manualResetEventSlim, num, isOtaMessage: false, isR6MsgId: true, waitSec);
		Logger.Info($"[ACK-RESULT] R6 MsgID=0x{(ushort)msgId:X4} success={flag} ts={DateTime.UtcNow:HH:mm:ss.fff}");
		lock (messageAcks)
		{
			if (messageAcks.ContainsKey(num))
			{
				messageAcks.Remove(num);
			}
		}
		r6SendCacheMsg = null;
		return flag;
	}

	private static bool SendUsbMessage(DeviceMsgId msgId, byte[]? bytes, int waitSec = 4)
	{
		p6sMcuUpdating = false;
		UsbMessage usbMessage = new UsbMessage();
		usbMessage.Data.MsgID = (ushort)msgId;
		if (bytes != null)
		{
			Array.Copy(bytes, 0, usbMessage.Data.Payload, 0, bytes.Length);
			usbMessage.Data.DataLength = bytes.Length;
		}
		ManualResetEventSlim manualResetEventSlim;
		lock (messageAcks)
		{
			if (!messageAcks.ContainsKey(usbMessage.Data.MsgID))
			{
				messageAcks.Add(usbMessage.Data.MsgID, new ManualResetEventSlim(initialState: false));
			}
			manualResetEventSlim = messageAcks[usbMessage.Data.MsgID];
		}
		manualResetEventSlim.Reset();
		GlassesDeviceManager.Instance.SendMsg(usbMessage);
		usbSendCacheMsg = usbMessage;
		bool result = SendMessageAndHandleTimeout(manualResetEventSlim, (ushort)msgId, isOtaMessage: false, isR6MsgId: false, waitSec);
		lock (messageAcks)
		{
			if (messageAcks.ContainsKey(usbMessage.Data.MsgID))
			{
				messageAcks.Remove(usbMessage.Data.MsgID);
			}
		}
		usbSendCacheMsg = null;
		return result;
	}

	private static bool SendUsbOtaMessage(UsbOtaCmd otaCmd, byte[]? bytes, int waitSec = 4)
	{
		p6sMcuUpdating = true;
		UsbOtaMessage usbOtaMessage = new UsbOtaMessage();
		usbOtaMessage.Cmd = (byte)otaCmd;
		if (bytes != null)
		{
			usbOtaMessage.Len = (ushort)usbOtaMessage.Data.Length;
			usbOtaMessage.Data = new byte[bytes.Length];
			Array.Copy(bytes, 0, usbOtaMessage.Data, 0, bytes.Length);
		}
		ManualResetEventSlim manualResetEventSlim;
		lock (otaMessageAcks)
		{
			if (!otaMessageAcks.ContainsKey(usbOtaMessage.Cmd))
			{
				otaMessageAcks.Add(usbOtaMessage.Cmd, new ManualResetEventSlim(initialState: false));
			}
			manualResetEventSlim = otaMessageAcks[usbOtaMessage.Cmd];
		}
		manualResetEventSlim.Reset();
		usbOtaMessage.Len = (ushort)((bytes != null) ? ((uint)bytes.Length) : 0u);
		GlassesDeviceManager.Instance.SendMsg(usbOtaMessage);
		otaSendCacheMsg = usbOtaMessage;
		bool result = SendMessageAndHandleTimeout(manualResetEventSlim, (ushort)otaCmd, isOtaMessage: true, isR6MsgId: false, waitSec);
		lock (otaMessageAcks)
		{
			if (otaMessageAcks.ContainsKey(usbOtaMessage.Cmd))
			{
				otaMessageAcks.Remove(usbOtaMessage.Cmd);
			}
		}
		otaSendCacheMsg = null;
		return result;
	}

	private static bool SendMessageAndHandleTimeout(ManualResetEventSlim semaphore, ushort msgId, bool isOtaMessage = false, bool isR6MsgId = false, int waitSec = 4)
	{
		MaxRetryCount = waitSec / 2 - 1;
		string text = (isR6MsgId ? ("R6MsgID: 0x" + msgId.ToString("X4")) : (isOtaMessage ? $"OtaCmd: {msgId}" : $"MsgID: {(DeviceMsgId)msgId}"));
		lock (retryLock)
		{
			retryCount = 0;
			while (retryCount <= MaxRetryCount)
			{
				if (semaphore.Wait(2000))
				{
					Logger.Info($"[ACK-WAIT] {text} got ACK, retry={retryCount} ts={DateTime.UtcNow:HH:mm:ss.fff}");
					return true;
				}
				Logger.Warning($"[ACK-WAIT] {text} TIMEOUT retry={retryCount}/{MaxRetryCount} ts={DateTime.UtcNow:HH:mm:ss.fff}");
				retryCount++;
				if (retryCount <= MaxRetryCount)
				{
					Monitor.Exit(retryLock);
					try
					{
						HandleErrorMsg();
					}
					finally
					{
						Monitor.Enter(retryLock);
					}
				}
			}
		}
		return false;
	}

	public static void ReleaseSemaphore(ushort msgId)
	{
		if (p6sMcuUpdating)
		{
			lock (otaMessageAcks)
			{
				if (otaMessageAcks.TryGetValue((byte)msgId, out ManualResetEventSlim value))
				{
					value.Set();
					lock (retryLock)
					{
						retryCount = 0;
						return;
					}
				}
				return;
			}
		}
		lock (messageAcks)
		{
			ushort num = msgId;
			if (GlassesDeviceManager.Instance.R6NewerModel)
			{
				msgId = (ushort)(msgId & 0xFFu);
			}
			if (messageAcks.TryGetValue(msgId, out ManualResetEventSlim value2))
			{
				Logger.Info($"[ACK-RELEASE] origMsgID=0x{num:X4} waitKey=0x{msgId:X4} → semaphore.Set() ts={DateTime.UtcNow:HH:mm:ss.fff}");
				value2.Set();
				lock (retryLock)
				{
					retryCount = 0;
					return;
				}
			}
			Logger.Info($"[ACK-RELEASE] origMsgID=0x{num:X4} waitKey=0x{msgId:X4} → no waiter found ts={DateTime.UtcNow:HH:mm:ss.fff}");
		}
	}

	public static void HandleErrorMsg()
	{
		if (GlassesDeviceManager.Instance.UseHidDevice)
		{
			DeviceMsgId result2;
			if (GlassesDeviceManager.Instance.R6NewerModel)
			{
				if (Enum.TryParse<R6NewerMsgId>($"{r6SendCacheMsg?.MsgID}", out var result))
				{
					Logger.Warning($"HandleErrorMsg: r6SendCacheMsg:{result} retryCount: {retryCount}");
					if (r6SendCacheMsg != null && GlassesDeviceManager.Instance.IsConnected)
					{
						GlassesDeviceManager.Instance.SendMsg(r6SendCacheMsg);
					}
					if (retryCount == MaxRetryCount)
					{
						Logger.Error($"Send R6HidMessage Failed: {result}");
					}
				}
			}
			else if (Enum.TryParse<DeviceMsgId>($"{hidSendCacheMsg?.Data.MsgID}", out result2))
			{
				Logger.Warning($"HandleErrorMsg: hidSendCacheMsg:{result2} retryCount: {retryCount}");
				if (hidSendCacheMsg != null && GlassesDeviceManager.Instance.IsConnected)
				{
					GlassesDeviceManager.Instance.SendMsg(hidSendCacheMsg);
				}
				if (retryCount == MaxRetryCount)
				{
					Logger.Error($"Send HidMessage Failed: {result2}");
				}
			}
		}
		else if (!p6sMcuUpdating)
		{
			if (Enum.TryParse<DeviceMsgId>($"{usbSendCacheMsg?.Data.MsgID}", out var result3))
			{
				Logger.Warning($"HandleErrorMsg: usbSendCacheMsg:{result3} retryCount: {retryCount}");
				if (usbSendCacheMsg != null && GlassesDeviceManager.Instance.IsConnected)
				{
					GlassesDeviceManager.Instance.SendMsg(usbSendCacheMsg);
				}
				if (retryCount == MaxRetryCount)
				{
					Logger.Error($"Send USBMessage Failed: {result3}");
				}
			}
		}
		else if (otaSendCacheMsg != null)
		{
			Logger.Warning($"HandleErrorMsg: otaSendCacheMsg:{otaSendCacheMsg?.Cmd} retryCount: {retryCount}");
			if (GlassesDeviceManager.Instance.IsConnected)
			{
				GlassesDeviceManager.Instance.SendMsg(otaSendCacheMsg);
			}
			if (retryCount == MaxRetryCount)
			{
				Logger.Error($"Send UsbOtaMessage Failed: {otaSendCacheMsg?.Cmd}");
			}
		}
	}

	public static void ResetRetryCount()
	{
		lock (retryLock)
		{
			retryCount = 0;
		}
	}
}
