<#
============================================================================
 VDDBoost.ps1 - 一次性助推器(替代原 VDDBoost.exe,免编译)

 功能:
   check   体检模式:补丁字节是否在位 + 各 VDD 当前刷新率
   默认    助推模式:SpaceWalker 未在运行则启动(已在运行则跳过)→ 轮询
           VDD 集合(每 0.5s 一次,连续 10 次无变化 = 创建完成)→ 把每个
           VDD 切到当前分辨率下的最高刷新率(120Hz)→ 退出。

 本脚本只处理 SpaceWalker(读 swpath.txt),自包含运行。

 用法:
   boost.bat / check.bat 内部调用本脚本,无需手动执行。
   直接运行:powershell -NoProfile -ExecutionPolicy Bypass -File VDDBoost.ps1 [check]

 原理:VDD 驱动只能以 60Hz 创建(120Hz 创建会崩溃),创建后运行时
       切换刷新率是安全的,等价于在系统设置里手动选 120Hz。
============================================================================
#>
param([string]$Mode = "")

# ---------- 内嵌的 P/Invoke 代码(与原 VDDBoost.cs 相同) ----------
$NativeCs = @'
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public static class VddNative
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct DISPLAY_DEVICE
    {
        public int cb;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string DeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string DeviceString;
        public int StateFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string DeviceID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string DeviceKey;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct DEVMODE
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public int dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string dmFormName;
        public short dmLogPixels;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    }

    [DllImport("user32.dll", CharSet = CharSet.Ansi)]
    static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

    [DllImport("user32.dll", CharSet = CharSet.Ansi)]
    static extern bool EnumDisplaySettings(string lpszDeviceName, uint iModeNum, ref DEVMODE lpDevMode);

    [DllImport("user32.dll", CharSet = CharSet.Ansi)]
    static extern int ChangeDisplaySettingsEx(string lpszDeviceName, ref DEVMODE lpDevMode, IntPtr hwnd, int dwflags, IntPtr lParam);

    public static string[] GetVddDevices()
    {
        List<string> list = new List<string>();
        DISPLAY_DEVICE d = new DISPLAY_DEVICE();
        d.cb = Marshal.SizeOf(typeof(DISPLAY_DEVICE));
        uint dev = 0;
        while (EnumDisplayDevices(null, dev, ref d, 0))
        {
            if ((d.StateFlags & 0x1) != 0 && d.DeviceString != null && d.DeviceString.IndexOf("VITURE Virtual Display", StringComparison.OrdinalIgnoreCase) >= 0)
                list.Add(d.DeviceName);
            dev++;
        }
        return list.ToArray();
    }

    public static string GetModeText(string dev)
    {
        DEVMODE cur = new DEVMODE();
        cur.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
        if (!EnumDisplaySettings(dev, 0xFFFFFFFF, ref cur)) return null;
        return cur.dmPelsWidth + "x" + cur.dmPelsHeight + " @" + cur.dmDisplayFrequency + "Hz";
    }

    // 把设备切到当前分辨率下的最高刷新率。
    // 返回:0=切换成功,1=已是最佳,2=切换失败,-1=读不到当前模式
    public static int SetBestRefresh(string dev, out string resultText, out int bestRate)
    {
        bestRate = 0;
        DEVMODE cur = new DEVMODE();
        cur.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
        if (!EnumDisplaySettings(dev, 0xFFFFFFFF, ref cur)) { resultText = dev + ": cannot read current mode"; return -1; }
        DEVMODE best = new DEVMODE();
        best.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
        int bestRate2 = 0;
        DEVMODE cand = new DEVMODE();
        cand.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
        uint m = 0;
        while (EnumDisplaySettings(dev, m, ref cand))
        {
            if (cand.dmPelsWidth == cur.dmPelsWidth && cand.dmPelsHeight == cur.dmPelsHeight && cand.dmDisplayFrequency > bestRate2)
            {
                bestRate2 = cand.dmDisplayFrequency;
                best = cand;
            }
            m++;
        }
        bestRate = bestRate2;
        resultText = dev + ": " + cur.dmPelsWidth + "x" + cur.dmPelsHeight + " @" + cur.dmDisplayFrequency + "Hz (max at res: " + bestRate2 + "Hz)";
        if (bestRate2 > cur.dmDisplayFrequency)
        {
            int r = ChangeDisplaySettingsEx(dev, ref best, IntPtr.Zero, 0, IntPtr.Zero);
            resultText += (r == 0 ? "\n  -> switching to " + bestRate2 + "Hz: OK" : "\n  -> switching to " + bestRate2 + "Hz: failed 0x" + r.ToString("X"));
            return r == 0 ? 0 : 2;
        }
        return 1;
    }
}
'@

if (-not ('VddNative' -as [type])) { Add-Type -TypeDefinition $NativeCs }

# ---------- 路径解析:swpath.txt(脚本旁 → 当前目录)→ 默认安装路径 ----------
function Resolve-SwExe {
    $cands = @((Join-Path $PSScriptRoot 'swpath.txt'), (Join-Path (Get-Location) 'swpath.txt'))
    foreach ($c in $cands) {
        if (Test-Path -LiteralPath $c) {
            foreach ($l in (Get-Content -LiteralPath $c)) {
                $p = $l.Trim()
                if ($p.Length -gt 0 -and -not $p.StartsWith(';') -and -not $p.StartsWith('#')) {
                    if (Test-Path -LiteralPath $p) { return $p }
                }
            }
        }
    }
    $def = 'C:\Program Files\VITURE\SpaceWalker\SpaceWalker.exe'
    if (Test-Path -LiteralPath $def) { return $def }
    return $null
}

# 两个 VDD 名列表是否完全相同
function Test-SameSet([string[]]$a, [string[]]$b) {
    if ($null -eq $a -or $null -eq $b) { return $false }
    if ($a.Count -ne $b.Count) { return $false }
    for ($i = 0; $i -lt $a.Count; $i++) { if ($a[$i] -ne $b[$i]) { return $false } }
    return $true
}

# ================= check:体检模式 =================
# PS 会把 "--check"/"-check" 当作命名参数留在 $args 里,裸 "check" 才会
# 绑到 $Mode;两种写法都接受,统一进体检模式。
$isCheck = $Mode -match 'check' -or (@($args) -match 'check').Count -gt 0
if ($isCheck) {
    $exe = Resolve-SwExe
    if ($null -eq $exe) { Write-Host '[check] SpaceWalker.exe not found (no swpath.txt, default path missing).'; exit 4 }
    Write-Host "[check] SpaceWalker.exe: $exe"
    # 固件 120Hz 补丁签名(bundle 内 VitureCommonLibrary.dll 的 4 个字节)
    $expect = @(0x3F, 0x42, 0x33, 0x36)   # 61->63, 64->66, 49->51, 52->54
    $offs   = @(0x3AA031C, 0x3AA031F, 0x3AA0325, 0x3AA0328)
    $good = 0
    try {
        $b = [System.IO.File]::ReadAllBytes($exe)
        for ($i = 0; $i -lt $offs.Count; $i++) { if ($b[$offs[$i]] -eq $expect[$i]) { $good++ } }
        if ($good -eq 4) { Write-Host '[check] firmware 120Hz patch: 4/4 bytes  -> installed' }
        else { Write-Host "[check] firmware 120Hz patch: $good/4 bytes  -> NOT installed (run install_sw.bat)" }
    } catch { Write-Host "[check] read error: $($_.Exception.Message)" }
    $vdds = [VddNative]::GetVddDevices()
    if ($vdds.Count -eq 0) { Write-Host '[check] no VITURE Virtual Display present.'; if ($good -eq 4) { exit 0 } else { exit 3 } }
    foreach ($dev in $vdds) {
        $t = [VddNative]::GetModeText($dev)
        if ($null -ne $t) { Write-Host "[check] VDD $($dev): $t" }
    }
    if ($good -eq 4) { exit 0 } else { exit 3 }
}

# ================= 默认:助推模式 =================
# SpaceWalker 专用:未在运行则启动(读 swpath.txt / 默认路径),
# 已在运行则跳过;然后等待 VDD 集合稳定并升到最高刷新率。
$appExe = Resolve-SwExe
if (-not (Get-Process -Name 'SpaceWalker' -ErrorAction SilentlyContinue)) {
    if ($null -eq $appExe) {
        Write-Host '[VDDBoost] cannot find SpaceWalker. Run install_sw.bat first'
        Write-Host '          (it records the path), or pass it here. Exiting.'
        exit 5
    }
    Write-Host "[VDDBoost] SpaceWalker is not running - starting it from $appExe ..."
    try {
        Start-Process -FilePath $appExe -WorkingDirectory (Split-Path -Parent $appExe)
    } catch {
        Write-Host "[VDDBoost] failed to start SpaceWalker: $($_.Exception.Message)"
        exit 4
    }
}
Write-Host '[VDDBoost] SpaceWalker is up. Waiting for its virtual displays to be created...'

# 等待 VDD 集合稳定:0.5s 轮询一次,连续 10 次无变化(=5s 不变)才算创建完成
$deadline = (Get-Date).AddSeconds(120)
$prev = $null
$stable = 0
$waited = 0
while ((Get-Date) -lt $deadline) {
    $cur = [VddNative]::GetVddDevices()
    if ($cur.Count -gt 0 -and (Test-SameSet $prev $cur)) { $stable++ } else { $stable = 0 }
    $prev = $cur
    if ($stable -ge 10) { break }
    Start-Sleep -Milliseconds 500
    $waited += 500
    if ($waited % 5000 -eq 0) { Write-Host "[VDDBoost] waiting for virtual displays... ($($waited / 1000)s, stable=$stable)" }
}
if ($null -eq $prev -or $prev.Count -eq 0) {
    Write-Host '[VDDBoost] no VITURE Virtual Display appeared within 120s.'
    Write-Host '    Check: glasses connected over USB-C? A virtual-screen layout activated'
    Write-Host '    inside the app? (Any layout that creates the virtual displays)'
    exit 3
}
Write-Host "[VDDBoost] stable virtual display set: $($prev.Count) device(s)."

$switched = 0
$failed = 0
foreach ($dev in $prev) {
    $txt = ''; $best = 0
    $rc = [VddNative]::SetBestRefresh($dev, [ref]$txt, [ref]$best)
    Write-Host $txt
    if ($rc -eq 0) { $switched++; Start-Sleep -Milliseconds 800 }
    elseif ($rc -eq 2 -or $rc -eq -1) { $failed++ }
}
Write-Host "[VDDBoost] done. upgraded=$switched failed=$failed (tool exits now)"
if ($failed -gt 0) { exit 2 } else { exit 0 }