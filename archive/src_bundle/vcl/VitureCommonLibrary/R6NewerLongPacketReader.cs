using System;
using System.Buffers.Binary;
using System.Threading;

namespace VitureCommonLibrary;

public sealed class R6NewerLongPacketReader
{
	private static readonly Lazy<R6NewerLongPacketReader> _lazy = new Lazy<R6NewerLongPacketReader>(() => new R6NewerLongPacketReader());

	public const int LongResponseHeaderSize = 5;

	public const int MaxPayloadPerSegment = 51;

	public const int LengthCrcHeaderSize = 6;

	private readonly object _readLock = new object();

	private readonly ManualResetEventSlim _segmentReady = new ManualResetEventSlim(initialState: false);

	private readonly object _stateLock = new object();

	private ushort _pendingResponseMsgId;

	private R6NewerHidMessage? _lastResponse;

	public static R6NewerLongPacketReader Instance => _lazy.Value;

	public string? LastFailReason { get; private set; }

	private R6NewerLongPacketReader()
	{
	}

	public byte[]? ReadLongPacket(ushort readMsgId, int perSegmentTimeoutMs = 3000, int overallTimeoutMs = 30000)
	{
		LastFailReason = null;
		if ((readMsgId & 0xF000) != 12288)
		{
			LastFailReason = $"invalid readMsgId 0x{readMsgId:X4} (not 0x3xxx)";
			Logger.Error("R6NewerLongPacketReader: " + LastFailReason + "; aborting.");
			return null;
		}
		ushort num = (ushort)((readMsgId & 0xFFFu) | 0xA000u);
		lock (_readLock)
		{
			DateTime dateTime = DateTime.UtcNow.AddMilliseconds(overallTimeoutMs);
			if (!TryRequestSegment(readMsgId, num, 0, perSegmentTimeoutMs, out R6NewerHidMessage response) || response == null)
			{
				LastFailReason = $"seg0 timeout ({perSegmentTimeoutMs}ms) — 设备未响应 0x{num:X4}，检查 HID 连接和 OnLongPacketResponse 回调";
				Logger.Warning($"R6 long-packet read 0x{readMsgId:X4}: {LastFailReason}");
				return null;
			}
			if (!TryParseFirstSegment(response, out int totalSegNum, out uint length, out ushort crc, out byte[] accumulator, out int writeOffset))
			{
				if (LastFailReason == null)
				{
					LastFailReason = "first segment parse failed (unknown)";
				}
				return null;
			}
			Logger.Info($"R6 long-packet 0x{readMsgId:X4}: totalSeg={totalSegNum}, length={length}, crc=0x{crc:X4}");
			for (int i = 1; i < totalSegNum; i++)
			{
				if (DateTime.UtcNow > dateTime)
				{
					LastFailReason = $"overall timeout ({overallTimeoutMs}ms) @ seg {i}/{totalSegNum}";
					Logger.Warning($"R6 long-packet read 0x{readMsgId:X4}: {LastFailReason}");
					return null;
				}
				if (!TryRequestSegment(readMsgId, num, (ushort)i, perSegmentTimeoutMs, out R6NewerHidMessage response2) || response2 == null)
				{
					LastFailReason = $"seg {i}/{totalSegNum} timeout ({perSegmentTimeoutMs}ms)";
					Logger.Warning($"R6 long-packet read 0x{readMsgId:X4}: {LastFailReason}");
					return null;
				}
				if (!AppendSegment(response2, i, accumulator, ref writeOffset, totalSegNum))
				{
					LastFailReason = $"seg {i}/{totalSegNum} append failed (data corruption)";
					Logger.Warning($"R6 long-packet read 0x{readMsgId:X4}: {LastFailReason}");
					return null;
				}
			}
			int num2 = (int)(6 + length);
			if (accumulator.Length > num2)
			{
				byte[] array = new byte[num2];
				Buffer.BlockCopy(accumulator, 0, array, 0, num2);
				accumulator = array;
			}
			else if (accumulator.Length < num2)
			{
				LastFailReason = $"assembled {accumulator.Length}B < expected {num2}B (数据不完整)";
				Logger.Warning($"R6 long-packet read 0x{readMsgId:X4}: {LastFailReason}");
				return null;
			}
			if (!VerifyChecksum(accumulator, length, crc))
			{
				LastFailReason = $"CRC mismatch (length={length})";
				Logger.Warning($"R6 long-packet read 0x{readMsgId:X4}: {LastFailReason}");
				int num3 = Math.Min(32, accumulator.Length);
				int num4 = Math.Min(32, accumulator.Length);
				int num5 = Math.Max(0, accumulator.Length - num4);
				Logger.Info($"[longpkt.crcfail] assembled={accumulator.Length}B head[0..{num3}]={HexDump(accumulator, 0, num3)} tail[{num5}..{accumulator.Length}]={HexDump(accumulator, num5, num4)}");
				return null;
			}
			Logger.Info($"R6 long-packet read 0x{readMsgId:X4}: success, total {accumulator.Length}B.");
			return accumulator;
		}
	}

	public bool ProbeFirstSegment(ushort readMsgId, int timeoutMs, out R6NewerHidMessage? resp)
	{
		resp = null;
		if ((readMsgId & 0xF000) != 12288)
		{
			Logger.Error($"ProbeFirstSegment: readMsgId 0x{readMsgId:X4} not 0x3xxx");
			return false;
		}
		ushort responseMsgId = (ushort)((readMsgId & 0xFFFu) | 0xA000u);
		lock (_readLock)
		{
			return TryRequestSegment(readMsgId, responseMsgId, 0, timeoutMs, out resp);
		}
	}

	public void OnLongPacketResponse(R6NewerHidMessage r6Msg)
	{
		if (r6Msg == null)
		{
			return;
		}
		object[] obj = new object[4] { r6Msg.MsgID, r6Msg.DataLen, _pendingResponseMsgId, null };
		byte[] payload = r6Msg.Payload;
		byte[] payload2 = r6Msg.Payload;
		obj[3] = HexDump(payload, 0, (payload2 != null) ? payload2.Length : 0);
		Logger.Info(string.Format("[longpkt.rx] msgId=0x{0:X4} dataLen={1} pendingMsgId=0x{2:X4} payload[56]={3}", obj));
		lock (_stateLock)
		{
			if (_pendingResponseMsgId == 0)
			{
				Logger.Info($"R6 long-packet drop: no pending read, got 0x{r6Msg.MsgID:X4}.");
				return;
			}
			if (r6Msg.MsgID != _pendingResponseMsgId)
			{
				Logger.Info($"R6 long-packet drop: expected 0x{_pendingResponseMsgId:X4} got 0x{r6Msg.MsgID:X4}.");
				return;
			}
			_lastResponse = r6Msg;
		}
		_segmentReady.Set();
	}

	private bool TryRequestSegment(ushort readMsgId, ushort responseMsgId, ushort segNum, int timeoutMs, out R6NewerHidMessage? response)
	{
		response = null;
		lock (_stateLock)
		{
			_pendingResponseMsgId = responseMsgId;
			_lastResponse = null;
		}
		_segmentReady.Reset();
		R6NewerHidMessage r6NewerHidMessage = new R6NewerHidMessage
		{
			MsgID = readMsgId,
			DataLen = 2
		};
		BinaryPrimitives.WriteUInt16LittleEndian(r6NewerHidMessage.Payload.AsSpan(0, 2), segNum);
		Logger.Info($"[longpkt.tx] msgId=0x{r6NewerHidMessage.MsgID:X4} seg={segNum} dataLen={r6NewerHidMessage.DataLen} payload[56]={HexDump(r6NewerHidMessage.Payload, 0, r6NewerHidMessage.Payload.Length)}");
		try
		{
			GlassesDeviceManager.Instance.SendMsg(r6NewerHidMessage);
		}
		catch (Exception ex)
		{
			Logger.Warning($"R6 long-packet send 0x{readMsgId:X4} seg {segNum} failed: {ex.Message}");
			return false;
		}
		bool flag = _segmentReady.Wait(timeoutMs);
		lock (_stateLock)
		{
			response = _lastResponse;
			_pendingResponseMsgId = 0;
			_lastResponse = null;
		}
		if (flag)
		{
			return response != null;
		}
		return false;
	}

	private bool TryParseFirstSegment(R6NewerHidMessage firstResp, out int totalSegNum, out uint length, out ushort crc, out byte[] accumulator, out int writeOffset)
	{
		totalSegNum = 0;
		length = 0u;
		crc = 0;
		accumulator = Array.Empty<byte>();
		writeOffset = 0;
		if (firstResp.Payload == null || firstResp.Payload.Length < 11)
		{
			byte[] payload = firstResp.Payload;
			LastFailReason = $"first segment payload too short ({((payload != null) ? payload.Length : 0)}B < {11}B)";
			return false;
		}
		R6LongResponse r6LongResponse = new R6LongResponse(firstResp.Payload);
		totalSegNum = r6LongResponse.TOTAL_SEG_NUM;
		if (totalSegNum == 0)
		{
			LastFailReason = "设备不支持该标定项 (total_seg_num=0, 正常)";
			Logger.Info("R6 long-packet first seg: " + LastFailReason);
			return false;
		}
		if (totalSegNum == 65535)
		{
			LastFailReason = "该 section 未烧录 (total_seg_num=0xFFFF, flash 默认值)";
			Logger.Info("R6 long-packet first seg: " + LastFailReason);
			return false;
		}
		if (totalSegNum > 4096)
		{
			LastFailReason = $"bogus total_seg_num={totalSegNum} (expected 1~4096)";
			Logger.Warning("R6 long-packet first seg: " + LastFailReason);
			return false;
		}
		ushort cURRENT_SEG_NUM = r6LongResponse.CURRENT_SEG_NUM;
		if (cURRENT_SEG_NUM != 0)
		{
			LastFailReason = $"first segment current_seg_num={cURRENT_SEG_NUM} (expected 0)";
			Logger.Warning("R6 long-packet first seg: " + LastFailReason);
			return false;
		}
		Span<byte> span = firstResp.Payload.AsSpan(5, 6);
		length = BinaryPrimitives.ReadUInt32LittleEndian(span.Slice(0, 4));
		crc = BinaryPrimitives.ReadUInt16LittleEndian(span.Slice(4, 2));
		Logger.Info($"[longpkt.parse] seg0 head[5..10] raw={HexDump(firstResp.Payload, 5, 6)} parsedLength={length}(0x{length:X8}) parsedCrc=0x{crc:X4} appSeq={r6LongResponse.APP_SEQ} totalSeg={totalSegNum} curSeg={r6LongResponse.CURRENT_SEG_NUM}");
		int num = totalSegNum * 51;
		accumulator = new byte[num];
		int num2 = firstResp.DataLen - 5;
		if (num2 < 0 || num2 > firstResp.Payload.Length - 5)
		{
			LastFailReason = $"first segment DataLen={firstResp.DataLen} out of range";
			Logger.Warning("R6 long-packet first seg: " + LastFailReason);
			return false;
		}
		if (num2 > 0)
		{
			Buffer.BlockCopy(firstResp.Payload, 5, accumulator, 0, num2);
			writeOffset = num2;
		}
		return true;
	}

	private bool AppendSegment(R6NewerHidMessage segResp, int expectedSegIdx, byte[] accumulator, ref int writeOffset, int totalSegNum)
	{
		if (segResp.Payload == null || segResp.Payload.Length < 5)
		{
			return false;
		}
		R6LongResponse r6LongResponse = new R6LongResponse(segResp.Payload);
		if (r6LongResponse.CURRENT_SEG_NUM != expectedSegIdx)
		{
			Logger.Warning($"R6 long-packet seg index mismatch: expected {expectedSegIdx}, got {r6LongResponse.CURRENT_SEG_NUM}.");
			return false;
		}
		if (r6LongResponse.TOTAL_SEG_NUM != totalSegNum)
		{
			Logger.Warning($"R6 long-packet total_seg_num changed mid-stream: {totalSegNum} → {r6LongResponse.TOTAL_SEG_NUM}.");
			return false;
		}
		int num = segResp.DataLen - 5;
		if (num < 0 || num > segResp.Payload.Length - 5)
		{
			Logger.Warning($"R6 long-packet seg {expectedSegIdx}: DataLen={segResp.DataLen} out of range");
			return false;
		}
		if (writeOffset + num > accumulator.Length)
		{
			byte[] array = new byte[Math.Max(accumulator.Length * 2, writeOffset + num)];
			Buffer.BlockCopy(accumulator, 0, array, 0, writeOffset);
			accumulator = array;
		}
		Buffer.BlockCopy(segResp.Payload, 5, accumulator, writeOffset, num);
		writeOffset += num;
		return true;
	}

	private static bool VerifyChecksum(byte[] buffer, uint length, ushort crcExpected)
	{
		if (buffer.Length < 6 + length)
		{
			return false;
		}
		ushort num = Crc16Xmodem(buffer, 6, (int)length);
		if (num != crcExpected)
		{
			Logger.Info($"R6 long-packet CRC: expected 0x{crcExpected:X4}, actual 0x{num:X4} (length={length}).");
			return false;
		}
		return true;
	}

	private static ushort Crc16Xmodem(byte[] data, int offset, int count)
	{
		ushort num = 0;
		int num2 = offset + count;
		for (int i = offset; i < num2; i++)
		{
			num ^= (ushort)(data[i] << 8);
			for (int j = 0; j < 8; j++)
			{
				num = (((num & 0x8000) == 0) ? ((ushort)(num << 1)) : ((ushort)((uint)(num << 1) ^ 0x1021u)));
			}
		}
		return num;
	}

	private static string HexDump(byte[]? data, int offset, int count)
	{
		if (data == null || data.Length == 0)
		{
			return "(null)";
		}
		if (offset < 0)
		{
			offset = 0;
		}
		if (offset + count > data.Length)
		{
			count = data.Length - offset;
		}
		if (count <= 0)
		{
			return "(empty)";
		}
		return BitConverter.ToString(data, offset, count).Replace('-', ' ');
	}
}
