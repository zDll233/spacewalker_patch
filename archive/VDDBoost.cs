using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

class VDDBoost
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

    static List<string> GetVddDevices()
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
        return list;
    }

    static bool SameSet(List<string> a, List<string> b)
    {
        if (a == null || b == null || a.Count != b.Count) return false;
        for (int i = 0; i < a.Count; i++)
            if (a[i] != b[i]) return false;
        return true;
    }

    static string ResolveSwExe()
    {
        // config preferred next to this exe, then cwd
        string[] cfgCandidates = new string[]
        {
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "swpath.txt"),
            Path.Combine(Directory.GetCurrentDirectory(), "swpath.txt")
        };
        foreach (string cfg in cfgCandidates)
        {
            if (!File.Exists(cfg)) continue;
            foreach (string l in File.ReadAllLines(cfg))
            {
                string p = l.Trim();
                if (p.Length == 0 || p.StartsWith(";") || p.StartsWith("#")) continue;
                if (File.Exists(p)) return p;
            }
        }
        // usual install path
        string def = @"C:\Program Files\VITURE\SpaceWalker\SpaceWalker.exe";
        return File.Exists(def) ? def : null;
    }

    static int CheckMode()
    {
        string exe = ResolveSwExe();
        if (exe == null) { Console.WriteLine("[check] SpaceWalker.exe not found (no swpath.txt, default path missing)."); return 4; }
        Console.WriteLine("[check] SpaceWalker.exe: " + exe);
        // firmware 120Hz patch signature (PickNativeDisplayMode values) inside bundle VCL
        byte[] expect = { 0x3F, 0x42, 0x33, 0x36 }; // 61->63, 64->66, 49->51, 52->54
        long[] offs = { 0x3AA031C, 0x3AA031F, 0x3AA0325, 0x3AA0328 };
        int good = 0;
        try
        {
            byte[] b = File.ReadAllBytes(exe);
            for (int i = 0; i < offs.Length; i++)
                if (b[offs[i]] == expect[i]) good++;
            Console.WriteLine("[check] firmware 120Hz patch: " + good + "/4 bytes" + (good == 4 ? "  -> installed" : "  -> NOT installed (run install_sw.bat)"));
        }
        catch (Exception ex) { Console.WriteLine("[check] read error: " + ex.Message); }
        // list VDDs with current refresh
        List<string> vdds = GetVddDevices();
        if (vdds.Count == 0) { Console.WriteLine("[check] no VITURE Virtual Display present."); return good == 4 ? 0 : 3; }
        foreach (string dev in vdds)
        {
            DEVMODE cur = new DEVMODE();
            cur.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
            if (EnumDisplaySettings(dev, 0xFFFFFFFF, ref cur))
                Console.WriteLine("[check] VDD " + dev + ": " + cur.dmPelsWidth + "x" + cur.dmPelsHeight + " @" + cur.dmDisplayFrequency + "Hz");
        }
        return good == 4 ? 0 : 3;
    }

    static int Main(string[] args)
    {
        bool quiet = args.Length > 0 && args[0] == "--quiet";
        if (args.Length > 0 && args[0] == "--check") return CheckMode();
        string swExe = ResolveSwExe();
        Console.WriteLine("[VDDBoost] checking SpaceWalker ...");
        if (Process.GetProcessesByName("SpaceWalker").Length == 0)
        {
            if (swExe == null)
            {
                Console.WriteLine("[VDDBoost] cannot find SpaceWalker. Run boost.bat first (it records the path),");
                Console.WriteLine("          or pass it here. Exiting.");
                return 5;
            }
            Console.WriteLine("[VDDBoost] SpaceWalker is not running - starting it from " + swExe + " ...");
            try
            {
                Process.Start(new ProcessStartInfo(swExe) { WorkingDirectory = Path.GetDirectoryName(swExe) });
            }
            catch (Exception ex)
            {
                Console.WriteLine("[VDDBoost] failed to start SpaceWalker: " + ex.Message);
                return 4;
            }
        }
        Console.WriteLine("[VDDBoost] SpaceWalker is up. Waiting for its virtual displays to be created...");

        // wait until the VDD set is stable: poll every 0.5s, require
        // 10 consecutive polls with no change (i.e. ~5s unchanged)
        DateTime deadline = DateTime.Now.AddSeconds(120);
        List<string> prev = null;
        int stable = 0;
        long waited = 0;
        while (DateTime.Now < deadline)
        {
            List<string> cur = GetVddDevices();
            if (cur.Count > 0 && SameSet(prev, cur)) stable++; else stable = 0;
            prev = cur;
            if (stable >= 10) break;
            Thread.Sleep(500);
            waited += 500;
            if (waited % 5000 == 0)
                Console.WriteLine("[VDDBoost] waiting for virtual displays... ({0}s, stable={1})", waited / 1000, stable);
        }
        if (prev == null || prev.Count == 0)
        {
            Console.WriteLine("[VDDBoost] no VITURE Virtual Display appeared within 120s.");
            Console.WriteLine("    Check: glasses connected over USB-C? A virtual-screen layout activated");
            Console.WriteLine("    inside SpaceWalker? (Any layout that creates the virtual displays)");
            return 3;
        }
        Console.WriteLine("[VDDBoost] stable virtual display set: " + prev.Count + " device(s).");

        int switched = 0;
        foreach (string dev in prev)
        {
            DEVMODE cur = new DEVMODE();
            cur.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
            if (!EnumDisplaySettings(dev, 0xFFFFFFFF, ref cur))
            {
                Console.WriteLine("  " + dev + ": cannot read current mode");
                continue;
            }
            // find highest refresh rate at the same resolution
            DEVMODE best = new DEVMODE();
            best.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
            int bestRate = 0;
            DEVMODE cand = new DEVMODE();
            cand.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
            uint m = 0;
            while (EnumDisplaySettings(dev, m, ref cand))
            {
                if (cand.dmPelsWidth == cur.dmPelsWidth && cand.dmPelsHeight == cur.dmPelsHeight && cand.dmDisplayFrequency > bestRate)
                {
                    bestRate = cand.dmDisplayFrequency;
                    best = cand;
                }
                m++;
            }
            Console.WriteLine("  {0}: {1}x{2} @ {3}Hz (max at res: {4}Hz)", dev, cur.dmPelsWidth, cur.dmPelsHeight, cur.dmDisplayFrequency, bestRate);
            if (bestRate > cur.dmDisplayFrequency)
            {
                int r = ChangeDisplaySettingsEx(dev, ref best, IntPtr.Zero, 0, IntPtr.Zero);
                Console.WriteLine("    -> switching to " + bestRate + "Hz: " + (r == 0 ? "OK" : "failed 0x" + r.ToString("X")));
                if (r == 0) switched++;
                Thread.Sleep(800); // let it settle before touching the next one
            }
        }
        Console.WriteLine("[VDDBoost] done. upgraded=" + switched + " (tool exits now)");
        return 0;
    }
}