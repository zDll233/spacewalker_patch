using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

class SWPatch
{
    static byte[] exeBytes;
    static int changes = 0;
    static long dllBaseInExe;

    static int Main(string[] args)
    {
        string exePath = args.Length > 0 ? args[0] : @"C:\Program Files\VITURE\SpaceWalker\SpaceWalker.exe";
        string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string outPath = args.Length > 1 ? args[1] : Path.Combine(home, "zdlRepository", "viture_patch", "spacewalker_patch", "SpaceWalker.exe.patched");
                string tmp = args.Length > 2 ? args[2] : Path.Combine(home, "zdlRepository", "viture_patch", "spacewalker_patch", "bundle_extracted");
        exeBytes = File.ReadAllBytes(exePath);
        Console.WriteLine("exe size: 0x" + exeBytes.Length.ToString("X"));

        // bundle VitureCommonLibrary.dll @ 0x3A25000
        dllBaseInExe = 0x3A25000;
        PatchInDll(Path.Combine(tmp, "VitureCommonLibrary.dll"), "VitureCommonLibrary", (asm) =>
        {
            // NOTE: only the firmware path (HID command to glasses) is patched - SAFE.
            // DO NOT patch BuildVddsAsync to 120: the VDA driver (UMDF) crashes on
            // 120Hz monitor creation (WDF_VIOLATION 0x10d / device Code 43).
            var gdm = FindType(asm, "GlassesDeviceManager");
            var pick = gdm.Methods.FirstOrDefault(m => m.Name == "PickNativeDisplayMode");
            if (pick == null) { Console.WriteLine("ERR: PickNativeDisplayMode not found"); return; }
            int[] fromVals = { 49, 52, 61, 64 };
            int[] toVals = { 51, 54, 63, 66 };
            foreach (var ins in pick.Body.Instructions)
            {
                if (ins.OpCode == OpCodes.Ldc_I4_S || ins.OpCode == OpCodes.Ldc_I4)
                {
                    int v = ins.OpCode == OpCodes.Ldc_I4_S ? (sbyte)ins.Operand : (int)ins.Operand;
                    int idx = Array.IndexOf(fromVals, v);
                    if (idx >= 0) WriteLdc(pick, ins, toVals[idx], "PickNativeDisplayMode " + v + "->" + toVals[idx]);
                }
            }
        });

        File.WriteAllBytes(outPath, exeBytes);
        Console.WriteLine("total byte changes: " + changes);
        Console.WriteLine("written: " + outPath);
        return 0;
    }

    static void PatchInDll(string dllPath, string tag, Action<AssemblyDefinition> patch)
    {
        Console.WriteLine("== " + tag + " ==");
        var asm = AssemblyDefinition.ReadAssembly(dllPath);
        patch(asm);
    }

    static TypeDefinition FindType(AssemblyDefinition asm, string name)
    {
        foreach (var t in asm.MainModule.Types)
            if (t.Name == name) return t;
        return null;
    }

    static bool IsLdc(Instruction i) => i.OpCode == OpCodes.Ldc_I4_S || i.OpCode == OpCodes.Ldc_I4;
    static int GetLdcValue(Instruction i) => i.OpCode == OpCodes.Ldc_I4_S ? (sbyte)i.Operand : (int)i.Operand;

    static void WriteLdc(MethodDefinition method, Instruction ins, int newVal, string who)
    {
        long methodBodyFile = RvaToFileOff(method.Module, (long)method.RVA);
        byte[] dllBytes = File.ReadAllBytes(method.Module.FileName);
        byte first = dllBytes[methodBodyFile];
        int headerSize = (first & 0x3) == 0x2 ? 1 : 12;
        long fileOff = methodBodyFile + headerSize + ins.Offset;
        long exeOff = dllBaseInExe + fileOff;
        int oldVal = GetLdcValue(ins);
        if (ins.OpCode == OpCodes.Ldc_I4_S)
        {
            if (exeBytes[exeOff] != 0x1F || exeBytes[exeOff + 1] != (byte)oldVal)
            {
                Console.WriteLine("  CHECK FAIL (want 1F " + oldVal.ToString("X2") + ") got " + exeBytes[exeOff].ToString("X2") + " " + exeBytes[exeOff + 1].ToString("X2") + " @ exe 0x" + exeOff.ToString("X") + " methodRva=0x" + method.RVA.ToString("X") + " hdr=" + headerSize + " insOff=0x" + ins.Offset.ToString("X"));
                return;
            }
            exeBytes[exeOff + 1] = (byte)newVal;
            Console.WriteLine("  " + who + ": " + oldVal + "->" + newVal + " @ exe 0x" + exeOff.ToString("X"));
            changes++;
        }
        else if (ins.OpCode == OpCodes.Ldc_I4)
        {
            if (exeBytes[exeOff] != 0x20 || BitConverter.ToInt32(exeBytes, (int)exeOff + 1) != oldVal)
            {
                Console.WriteLine("  CHECK FAIL (want 20) @" + exeOff.ToString("X"));
                return;
            }
            byte[] nb = BitConverter.GetBytes(newVal);
            for (int j = 0; j < 4; j++) exeBytes[exeOff + 1 + j] = nb[j];
            Console.WriteLine("  " + who + ": " + oldVal + "->" + newVal + " @ exe 0x" + exeOff.ToString("X"));
            changes++;
        }
    }

    static void PatchLdcBeforeCall(MethodDefinition method, string callName, int oldVal, int newVal, string who)
    {
        var il = method.Body.Instructions;
        for (int idx = 0; idx < il.Count; idx++)
        {
            var ins = il[idx];
            if ((ins.OpCode == OpCodes.Call || ins.OpCode == OpCodes.Callvirt) && ins.Operand is MethodReference mr && mr.Name == callName)
            {
                for (int k = idx - 1; k >= 0 && k >= idx - 12; k--)
                {
                    var c = il[k];
                    if (IsLdc(c))
                    {
                        if (GetLdcValue(c) == oldVal)
                            WriteLdc(method, c, newVal, who + " (before call " + callName + ")");
                        break;
                    }
                }
            }
        }
    }

    static long RvaToFileOff(ModuleDefinition module, long rva)
    {
        byte[] hdr = File.ReadAllBytes(module.FileName);
        int pe = BitConverter.ToInt32(hdr, 0x3C);
        int opt = pe + 24;
        int numSec = BitConverter.ToUInt16(hdr, pe + 6);
        int sec = opt + 240;
        for (int s = 0; s < numSec; s++)
        {
            int o = sec + s * 40;
            uint va = BitConverter.ToUInt32(hdr, o + 12);
            uint vs = BitConverter.ToUInt32(hdr, o + 8);
            uint raw = BitConverter.ToUInt32(hdr, o + 20);
            if (rva >= va && rva < va + vs) return raw + (rva - va);
        }
        return -1;
    }
}