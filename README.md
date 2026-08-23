# SpaceWalker 120Hz 解锁项目

VITURE SpaceWalker 的 120Hz 解锁方案(Windows):眼镜 **2D 视窗 120Hz** + **虚拟显示器(VDD)120Hz**。

**结论**:本项目的目标路径全部可用——视窗(2D)120 ✅、VDD 120 ✅(60Hz 创建 + 运行时升频,驱动创建 120Hz 会崩溃,因此创建路径永不改)。逆向细节、字节改动、固件限制见 **[Agent.md](Agent.md)**。

## 文件清单

```
spacewalker_patch/
├─ install_sw.bat       安装补丁(路径配置 + 备份 .orig + 字节校验;需管理员)
├─ restore_sw.bat       回滚到原版
├─ boost.bat            日常:SW 未运行则启动 → VDD 升 120 → 退出
├─ check.bat            体检:补丁状态 + VDD 刷新率
├─ VDDBoost.ps1         助推/体检逻辑本体(纯脚本,自包含)
├─ SpaceWalker.exe.patched  补丁版(与 .orig 仅 4 字节差异)
├─ SpaceWalker.exe.orig     原版备份
├─ swpath.txt           install_sw.bat 自动生成(路径配置,所有脚本共用)
├─ bundle_extracted/    bundle 提取(补丁输入源,更新软件后重新提取)
├─ tools/               项目内工具(ilspycmd、bundleextract3、Mono.Cecil)
├─ dotnet/              项目内 .NET SDK(构建补丁用;也可用系统 SDK)
├─ patch/ src/ archive/ 开发与逆向资料(见 Agent.md)
```

## 用法

```
install_sw.bat            ← 安装补丁 + 路径设置(右键 → 以管理员身份运行;先退出 SpaceWalker)
boost.bat                 ← 日常:SW 未运行则自动开 SW → VDD 升 120 → 退出(已在运行则跳过启动)
boost.bat forget          ← 清除记录的路径
check.bat                 ← 体检:补丁是否在位、VDD 刷新率
restore_sw.bat            ← 回滚补丁(需管理员)
```

**路径设置(首次)**:`install_sw.bat` 按下列顺序确定 SpaceWalker.exe 路径,并保存到 `swpath.txt` 供所有脚本共用:

1. 已有 `swpath.txt` 且路径有效 → 直接沿用
2. 默认安装路径 `C:\Program Files\VITURE\SpaceWalker\SpaceWalker.exe` 存在 → 自动采用
3. 都没有 → 提示手动输入完整路径(校验存在后保存)

之后 `boost.bat`/`restore_sw.bat`/`check.bat` 都只读 `swpath.txt`,无需再配置。

## 其他

- 本项目自包含(SDK/工具/bundle 均在项目内);配套的 Immersive3D 项目在 `..\re_dump\02_immersive3d`
- 残留虚拟显示器清理(通用):管理员运行 `..\re_dump\02_immersive3d\cleanup_vdd.bat`
