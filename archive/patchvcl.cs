using System;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

class PatchVcl
{
    static int Main(string[] args)
    {
        string src = args.Length > 0 ? args[0] : @"C:\Program Files\VITURE\SpaceWalker\VitureCommonLibrary.dll";
        string outp = args.Length > 1 ? args[1] : @"C:\Users\<user>\zdlRepository\viture_patch\spacewalker_patch\archive\VitureCommonLibrary.dll.patched";

        Directory.CreateDirectory(Path.GetDirectoryName(outp));
        var asm = AssemblyDefinition.ReadAssembly(src);

        int patchCount = 0;

        // ---- Patch A: SudoVirtualDisplay.AddVirtualDisplay(width,height,refreshRate,...) : force refreshRate=120 ----
        var svd = asm.MainModule.Types.FirstOrDefault(t => t.Name == "SudoVirtualDisplay");
        if (svd == null) { Console.WriteLine("ERROR: SudoVirtualDisplay not found"); return 2; }
        var addVd = svd.Methods.FirstOrDefault(m => m.Name == "AddVirtualDisplay" && m.Parameters.Count == 7);
        if (addVd == null) { Console.WriteLine("ERROR: AddVirtualDisplay(7-param) not found"); return 2; }
        Console.WriteLine("AddVirtualDisplay found; refreshRate param index: " + (Array.FindIndex(addVd.Parameters.ToArray(), p => p.Name == "refreshRate")));
        {
            int refreshIdx = Array.FindIndex(addVd.Parameters.ToArray(), p => p.Name == "refreshRate");
            var il = addVd.Body.GetILProcessor();
            var first = addVd.Body.Instructions[0];
            il.InsertBefore(first, il.Create(OpCodes.Ldc_I4, 120));
            il.InsertBefore(first, il.Create(OpCodes.Starg, addVd.Parameters[refreshIdx]));
            Console.WriteLine("PATCHED AddVirtualDisplay: refreshRate forced to 120 (param IL index " + (refreshIdx + 1) + ")");
            patchCount++;
        }

        // ---- Patch B: GlassesDeviceManager.PickNativeDisplayMode : 60Hz enum values -> 120Hz ----
        var gdm = asm.MainModule.Types.FirstOrDefault(t => t.Name == "GlassesDeviceManager");
        if (gdm == null) { Console.WriteLine("ERROR: GlassesDeviceManager not found"); return 2; }
        var pick = gdm.Methods.FirstOrDefault(m => m.Name == "PickNativeDisplayMode");
        if (pick == null) { Console.WriteLine("ERROR: PickNativeDisplayMode not found"); return 2; }
        int[] fromVals = { 49, 52, 61, 64 }; // 1920_1080_60, 1920_1200_60, ULTRAWIDE_3840_1080_60, ULTRAWIDE_3840_1200_60
        int[] toVals   = { 51, 54, 63, 66 }; // 1920_1080_120, 1920_1200_120, ULTRAWIDE_3840_1080_120, ULTRAWIDE_3840_1200_120
        foreach (var instr in pick.Body.Instructions)
        {
            if (instr.OpCode == OpCodes.Ldc_I4 || instr.OpCode == OpCodes.Ldc_I4_S)
            {
                int v = instr.OpCode == OpCodes.Ldc_I4_S ? (sbyte)instr.Operand : (int)instr.Operand;
                int idx = Array.IndexOf(fromVals, v);
                if (idx >= 0)
                {
                    instr.OpCode = OpCodes.Ldc_I4;
                    instr.Operand = toVals[idx];
                    Console.WriteLine("PATCHED PickNativeDisplayMode: " + v + " -> " + toVals[idx]);
                    patchCount++;
                }
            }
        }

        asm.Write(outp);
        Console.WriteLine("Saved: " + outp + " (patched items: " + patchCount + ")");
        return patchCount > 0 ? 0 : 3;
    }
}