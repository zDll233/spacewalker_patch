using System;
using System.Reflection;
using System.Runtime.CompilerServices;

class VerifyPatched
{
    static int Main(string[] args)
    {
        Assembly asm = Assembly.LoadFrom(args[0]);
        int bad = 0;
        Type svd = asm.GetType("VitureCommonLibrary.SudoVirtualDisplay");
        foreach (MethodInfo mi in svd.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
        {
            if (mi.Name == "AddVirtualDisplay")
            {
                try { RuntimeHelpers.PrepareMethod(mi.MethodHandle); Console.WriteLine("OK  " + mi.Name); }
                catch (Exception ex) { bad++; Console.WriteLine("FAIL " + mi.Name + ": " + ex.GetType().FullName + " " + ex.Message); }
            }
        }
        Type gdm = asm.GetType("VitureCommonLibrary.GlassesDeviceManager");
        foreach (MethodInfo mi in gdm.GetMethods(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly))
        {
            if (mi.Name == "PickNativeDisplayMode")
            {
                try { RuntimeHelpers.PrepareMethod(mi.MethodHandle); Console.WriteLine("OK  " + mi.Name); }
                catch (Exception ex) { bad++; Console.WriteLine("FAIL " + mi.Name + ": " + ex.GetType().FullName + " " + ex.Message); }
            }
        }
        Console.WriteLine(bad == 0 ? "RESULT: patched methods IL valid" : "RESULT: FAIL");
        return bad;
    }
}