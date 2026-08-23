using System;
using System.Runtime.InteropServices;
using System.Text;

class Vdd120
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

    static int Main(string[] args)
    {
        bool watch = args.Length > 0 && args[0] == "--watch";
        int interval = args.Length > 1 && args[1] == "--watch" ? 0 : 3000;
        if (watch) interval = args.Length > 1 ? int.Parse(args[1]) : 3000;

        do
        {
            int switched = 0;
            DISPLAY_DEVICE d = new DISPLAY_DEVICE();
            d.cb = Marshal.SizeOf(typeof(DISPLAY_DEVICE));
            uint dev = 0;
            while (EnumDisplayDevices(null, dev, ref d, 0))
            {
                string devName = d.DeviceName;
                string devString = d.DeviceString;
                bool attached = (d.StateFlags & 0x1) != 0;
                if (attached && devString != null && devString.IndexOf("VITURE Virtual Display", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    DEVMODE cur = new DEVMODE();
                    cur.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                    if (EnumDisplaySettings(devName, 0xFFFFFFFF, ref cur))
                    {
                        Console.WriteLine("[{0:HH:mm:ss}] {1} '{2}' {3}x{4}@{5}Hz", DateTime.Now, devName, devString, cur.dmPelsWidth, cur.dmPelsHeight, cur.dmDisplayFrequency);
                        if (cur.dmDisplayFrequency < 120)
                        {
                            // find 120Hz mode with same resolution
                            DEVMODE best = new DEVMODE();
                            best.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                            DEVMODE cand = new DEVMODE();
                            cand.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                            uint m = 0;
                            bool found = false;
                            while (EnumDisplaySettings(devName, m, ref cand))
                            {
                                if (cand.dmPelsWidth == cur.dmPelsWidth && cand.dmPelsHeight == cur.dmPelsHeight && cand.dmDisplayFrequency == 120)
                                {
                                    best = cand;
                                    found = true;
                                    break;
                                }
                                m++;
                            }
                            if (found)
                            {
                                int r = ChangeDisplaySettingsEx(devName, ref best, IntPtr.Zero, 0, IntPtr.Zero);
                                Console.WriteLine("    -> switching to 120Hz: ChangeDisplaySettingsEx=" + r + (r == 0 ? " OK" : " (0x" + r.ToString("X") + ")"));
                                if (r == 0) switched++;
                            }
                            else
                            {
                                Console.WriteLine("    -> 120Hz mode not (yet) listed for this resolution");
                            }
                        }
                    }
                }
                dev++;
            }
            if (!watch) { Console.WriteLine("done. switched=" + switched); return switched > 0 ? 0 : 2; }
            System.Threading.Thread.Sleep(interval);
        } while (watch);
        return 0;
    }
}