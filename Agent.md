# SpaceWalker 逆向工程笔记(Agent.md)

面向后续维护者/AI 代理。README.md 只面向最终用户;本文件记录**改了什么、为什么、怎么改、哪些改不了**。

## 一、改了什么(完整改动清单)

### 1. `SpaceWalker.exe` —— 4 字节(安装补丁,字节级替换)

| 文件偏移 | 原值 | 新值 | 所在方法 | 含义 |
|---|---|---|---|---|
| `0x3AA031C` | 0x3D(61) | 0x3F(63) | `GlassesDeviceManager.PickNativeDisplayMode` | 超宽视窗 ULTRAWIDE_3840_1080_60 → _120 |
| `0x3AA031F` | 0x40(64) | 0x42(66) | 同上 | 超宽视窗 ULTRAWIDE_3840_1200_60 → _120 |
| `0x3AA0325` | 0x31(49) | 0x33(51) | 同上 | 原生 2D NATIVE_1920_1080_60 → _120 |
| `0x3AA0328` | 0x34(52) | 0x36(54) | 同上 | 原生 2D NATIVE_1920_1200_60 → _120 |

- 都是 `ldc.i4.s`(0x1F)指令的操作数字节,位于单文件 bundle 内 **VitureCommonLibrary.dll**(1.17MB 版;安装目录那份 608KB 旧副本不是运行时实际使用的)
- 均走**固件 HID 通道**(发给眼镜的模式命令),不触碰 VDD 创建路径
- 生效状态:2D 两处 ✅(固件 VIT0030 EDID 含 120 档);**超宽两处 ⚠️ 不生效**(固件无超宽 120 EDID,命令 ACK OK 但 EDID 不变)——保留,等固件更新

### 2. `VDDBoost.ps1` —— 运行时升频(非字节改动)

- 等待 VDD 集合稳定(0.5s 轮询,连续 10 次无变化)→ 对每个 `VITURE Virtual Display` 调 `ChangeDisplaySettingsEx` 升到当前分辨率最高刷新率(120)
- `check` 模式:校验上述 4 字节 + 列出 VDD 刷新率

## 二、为什么这样改(原理)

### 固件 HID 模式协议

- 命令:`TF_CMD_NATIVE_DISPLAY_MODE_W = 322`,payload = `R6NewerDisplayMode` 枚举值(1 字节)
- 前置命令:`TF_CMD_NATIVE_TRACKING_MODE_W = 323,{1}`;i3d 侧还有 `TF_CMD_NATIVE_XR_MODE_W = 320,{1}`
- 消息格式:R6NewerHidMessage,64 字节:`ProtoVer(16) Seq MsgID(LE) DataLen(LE) CRC(sum) Payload[56]`
- 枚举 `R6NewerDisplayMode`(49..66):每 3 个一组 60/90/120 —— 49/50/51=1920_1080,52/53/54=1920_1200,55/56/57=3D_SBS_3840_1080,58/59/60=3D_SBS_3840_1200,61/62/63=ULTRAWIDE_3840_1080,64/65/66=ULTRAWIDE_3840_1200

### 固件 EDID 表(实测解析)

| EDID 型号 | 分辨率 | 时序 | 120? |
|---|---|---|---|
| VIT0030 | 1920×1080 | 60Hz + 120Hz 双 DTD(逐行) | ✅(Windows 展开 60/90/120/59/119) |
| CVT3132 / CVT3133 | 3840×1080 | 单 DTD 60Hz | ❌ |

- 固件收到模式命令后切换 EDID;超宽/3D 命令(61..66、55..60)固件 ACK 成功但 EDID 不变——**固件根本没有那些 EDID**

### VDD(VitureVDA 驱动)

- 基于开源 SudoVDA 的 UMDF 间接显示驱动;`IOCTL_ADD_VIRTUAL_DISPLAY(0x222000)` 参数 `(Width, Height, RefreshRate, MonitorGuid, DeviceName, SerialNumber, ClientId)`
- **创建路径只有 60Hz 稳定**:120Hz 创建会崩溃(设备 Code 43;重启可能 WDF_VIOLATION 0x10d 蓝屏)——曾实测触发,已永久禁用该方向
- **切换路径安全**:创建后 `ChangeDisplaySettingsEx` 升频与手动设置等价;驱动模式表按请求分辨率提供 60/90/120(3840×1080 实测含 120)
- 删除必须精确 MonitorGuid(创建时随机生成,系统不暴露);驱动无枚举/按名删除接口;watchdog 只清理注册 client 的显示 → 孤儿 VDD 只能重启驱动清理(`pnputil /restart-device "ROOT\DISPLAY\0001"`,需管理员,见 cleanup_vdd.bat)

## 三、改不了的东西(限制清单)

| 目标 | 原因 | 出路 |
|---|---|---|
| 超宽直连(3840×1080)120Hz | 固件无超宽 120 EDID | 等固件更新 |
| VDD 创建 120Hz | 驱动缺陷(崩溃) | 永远不要碰;60 创建 + 运行时升频 |
| 老(BYPASS)协议 120Hz | `R6OlderDisplayMode` 枚举无 120 档 | 无 |
| 孤儿 VDD 用户态删除 | 驱动协议无枚举/按名删除 | 管理员重启驱动 |

## 四、更新软件后重新生成补丁

1. 提取新 bundle:`tools\bundleextract3.exe <新的 SpaceWalker.exe> <outdir> 58D96B4`(header 偏移若变化需重新定位,见 swpatch.cs 注释)
2. 确认 `bundle_extracted\VitureCommonLibrary.dll` 中 `PickNativeDisplayMode` 的 4 个 ldc 还在(ilspycmd 反编译核对)
3. `dotnet build patch/swpatch.csproj && dotnet patch/bin/Release/net8.0/swpatch.dll <exe> <out.patched>`(用项目内 SDK `dotnet\dotnet.exe` 或系统 SDK)
4. 用 `install_sw.bat` 安装

## 五、关键文件地图

| 文件 | 作用 |
|---|---|
| `patch/swpatch.cs` | Cecil 补丁器:定位 PickNativeDisplayMode → 写 4 字节(只改固件路径!) |
| `patch/swpatch.csproj` | 引用项目内 Mono.Cecil(`tools\.store\...`) |
| `VDDBoost.ps1` | 运行时升频 + 体检(自包含,不依赖 i3d 项目) |
| `bundle_extracted/` | 单文件 bundle 提取(补丁输入源) |
| `src/` | 反编译参考源码(SpaceWalker 程序集 + VCL) |
| `archive/` | 废弃中间产物(勿用) |

## 六、调查工具(archive 或共享)

- `tools\bundleextract3.exe <exe> <outdir> <headerHex>`:bundle 提取(manifest: 12B 头 + varint id + 40B + 每条 25B 固定 + varint 路径)
- 调查工具参考:`..\re_dump\02_immersive3d\archive\experiments\`(hidmode2=直接发 HID 模式命令、vddtest3840=创建 3840 VDD 实测、tryset120=强制升频)——跨项目通用
