using System;
using System.Reflection;
using System.Runtime.CompilerServices;

class VerifyLoad
{
    static int Main(string[] args)
    {
        string path = args[0];
        Assembly asm = Assembly.LoadFrom(path);
        bool bad = false;
        foreach (Type t in asm.GetTypes())
        {
            foreach (MethodInfo mi in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
            {
                try { RuntimeHelpers.PrepareMethod(mi.MethodHandle); }
                catch (Exception ex) { Console.WriteLine("FAIL " + t.FullName + "." + mi.Name + ": " + ex.GetType().Name + " " + ex.Message); bad = true; }
            }
        }
        // also verify the two patched methods specifically
        Type svd = asm.GetType("VitureCommonLibrary.SudoVirtualDisplay");
        Type gdm = asm.GetType("VitureCommonLibrary.GlassesDeviceManager");
        Console.WriteLine("SudoVirtualDisplay=" + (svd != null) + " GlassesDeviceManager=" + (gdm != null));
        Console.WriteLine(bad ? "RESULT: FAIL" : "RESULT: ALL IL OK");
        return bad ? 1 : 0;
    }
}