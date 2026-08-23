using System;
using System.IO;
using System.Text;

class BundleExtract3
{
    static void Main(string[] args)
    {
        string path = args[0], outDir = args[1];
        long hOff = Convert.ToInt64(args[2], 16);
        byte[] b = File.ReadAllBytes(path);
        uint major = BitConverter.ToUInt32(b, (int)hOff);
        uint minor = BitConverter.ToUInt32(b, (int)hOff + 4);
        int numFiles = BitConverter.ToInt32(b, (int)hOff + 8);
        Console.WriteLine("header@{0:X}: major={1} minor={2} numFiles={3}", hOff, major, minor, numFiles);
        long p = hOff + 12;
        long idLen = b[p];
        if ((idLen & 0x80) != 0) { idLen = (b[p] & 0x7F) | (b[p + 1] << 7); p++; }
        p++;
        string id = Encoding.UTF8.GetString(b, (int)p, (int)idLen);
        Console.WriteLine("bundle_id: " + id);
        p += idLen;
        p += 40; // v2 header
        Directory.CreateDirectory(outDir);
        int ok = 0, skip = 0;
        for (int i = 0; i < numFiles; i++)
        {
            try
            {
                long off = BitConverter.ToInt64(b, (int)p);
                long size = BitConverter.ToInt64(b, (int)p + 8);
                long compSize = BitConverter.ToInt64(b, (int)p + 16);
                int type = b[p + 24];
                p += 25;
                long nLen = b[p];
                if ((nLen & 0x80) != 0) { nLen = (b[p] & 0x7F) | (b[p + 1] << 7); p++; }
                p++;
                string name = Encoding.UTF8.GetString(b, (int)p, (int)nLen).TrimEnd('\0');
                p += nLen;
                if (off > 0 && off + size <= b.Length)
                {
                    string fp = Path.Combine(outDir, name);
                    Directory.CreateDirectory(Path.GetDirectoryName(fp));
                    byte[] data = new byte[size];
                    Array.Copy(b, off, data, 0, size);
                    File.WriteAllBytes(fp, data);
                    if (name.EndsWith(".dll") || name.EndsWith(".json")) Console.WriteLine("  [{0}] {1} type={2} size={3}", i, name, type, size);
                    ok++;
                }
                else skip++;
            }
            catch (Exception ex)
            {
                Console.WriteLine("entry " + i + " parse error: " + ex.Message + " (p=0x" + p.ToString("X") + ")");
                return;
            }
        }
        Console.WriteLine("extracted={0} skipped={1}", ok, skip);
    }
}