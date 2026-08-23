using System;
using System.Reflection;
using System.Runtime.CompilerServices;

class VerifyLoad2
{
    static int Main(string[] args)
    {
        string path = args[0];
        try
        {
            Assembly asm = Assembly.LoadFrom(path);
            Console.WriteLine("Loaded: " + asm.FullName);
            Type[] types;
            try { types = asm.GetTypes(); }
            catch (ReflectionTypeLoadException rtle)
            {
                Console.WriteLine("GetTypes partial: " + rtle.LoaderExceptions.Length + " errors");
                types = rtle.Types;
            }
            int bad = 0, ok = 0;
            foreach (Type t in types)
            {
                if (t == null) continue;
                foreach (MethodInfo mi in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
                {
                    try { RuntimeHelpers.PrepareMethod(mi.MethodHandle); ok++; }
                    catch (Exception ex) { Console.WriteLine("FAIL " + t.FullName + "." + mi.Name + ": " + ex.GetType().FullName + " / " + ex.Message); bad++; }
                }
            }
            Console.WriteLine("methods JIT-verified: ok=" + ok + " bad=" + bad);
            return bad > 0 ? 1 : 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("TOP-LEVEL ERROR: " + ex.GetType().FullName + " / " + ex.Message);
            BadImageFormatException bife = ex as BadImageFormatException;
            if (bife != null) { Console.WriteLine("file=" + bife.FileName); }
            return 2;
        }
    }
}