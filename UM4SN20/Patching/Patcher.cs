using Harmony;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace UM4SN
{
    public static class Patcher
    {
        private static readonly HarmonyInstance harmony = HarmonyInstance.Create("me.nesrak1.um4sn");
        private static readonly List<PatchProcessor> loadedPatches = new List<PatchProcessor>();

        public static void PatchAll(SubPatchType patchType)
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                object[] attribs = type.GetCustomAttributes(typeof(SubPatchAttribute), true);
                if (attribs.Length > 2)
                {
                    //fix later to support different orders
                    SubPatchAttribute idVal = attribs[2] as SubPatchAttribute;
                    SubPatchAttribute typeVal = attribs[1] as SubPatchAttribute;
                    SubPatchAttribute infoVal = attribs[0] as SubPatchAttribute;
                    if ((int)patchType == idVal.id)
                    {
                        MethodInfo info;
                        if (infoVal.args != null)
                            info = typeVal.type.GetMethod(infoVal.method, allAttrs, null, infoVal.args, null);
                        else
                            info = typeVal.type.GetMethod(infoVal.method, allAttrs);

                        MethodInfo prefixInfo = type.GetMethod("Prefix", BindingFlags.Public | BindingFlags.Static);
                        MethodInfo postfixInfo = type.GetMethod("Postfix", BindingFlags.Public | BindingFlags.Static);
                        MethodInfo transpilerInfo = type.GetMethod("Transpiler", BindingFlags.Public | BindingFlags.Static);
                        HarmonyMethod prefixMethod = TryHarmonyMethod(prefixInfo);
                        HarmonyMethod postfixMethod = TryHarmonyMethod(postfixInfo);
                        HarmonyMethod transpilerMethod = TryHarmonyMethod(transpilerInfo);
                        loadedPatches.Add(harmony.Patch(info, prefixMethod, postfixMethod, transpilerMethod));
                    }
                }
            }
        }

        private static HarmonyMethod TryHarmonyMethod(MethodInfo info)
        {
            if (info == null) return null;
            return new HarmonyMethod(info);
        }

        private static BindingFlags allAttrs = BindingFlags.Public
           | BindingFlags.NonPublic
           | BindingFlags.Instance
           | BindingFlags.Static
           | BindingFlags.GetField
           | BindingFlags.SetField
           | BindingFlags.GetProperty
           | BindingFlags.SetProperty;
    }
}
