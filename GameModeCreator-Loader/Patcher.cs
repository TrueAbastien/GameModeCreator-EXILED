using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using System.Reflection;
using System.Diagnostics;

namespace GameModeCreator.Loader
{
    public static class Patcher
    {
        private static int incrementalCounter;
        public static void OnLoad(out Harmony harmony)
        {
            harmony = new Harmony($"empire.gamemodecreator.{ ++incrementalCounter }");
        }

        public static void UnPatch(Harmony harmony)
        {
            foreach (MethodBase original in harmony.GetPatchedMethods().ToList())
            {
                bool num = original.HasMethodBody();
                Patches patchInfo2 = Harmony.GetPatchInfo(original);
                if (num)
                {
                    patchInfo2.Postfixes.DoIf(IDCheck, delegate (Patch patchInfo)
                    {
                        harmony.Unpatch(original, patchInfo.PatchMethod);
                    });
                    patchInfo2.Prefixes.DoIf(IDCheck, delegate (Patch patchInfo)
                    {
                        harmony.Unpatch(original, patchInfo.PatchMethod);
                    });
                }
                patchInfo2.Transpilers.DoIf(IDCheck, delegate (Patch patchInfo)
                {
                    harmony.Unpatch(original, patchInfo.PatchMethod);
                });
                if (num)
                {
                    patchInfo2.Finalizers.DoIf(IDCheck, delegate (Patch patchInfo)
                    {
                        harmony.Unpatch(original, patchInfo.PatchMethod);
                    });
                }
            }
            bool IDCheck(Patch patchInfo)
            {
                if (harmony.Id != null)
                {
                    return patchInfo.owner == harmony.Id;
                }
                return true;
            }
        }

        public static void PatchOver(Harmony harmony, Type gamemode)
        {
            if (gamemode == null)
                return;

            // Patch all current Gamemode Patches
            try
            {
                Assembly assembly = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Assembly;
                foreach (Type type in assembly.GetTypes())
                {
                    var attribute = type.GetCustomAttribute<GameModeCreator.API.Patch.Attributes.PatchOf>();
                    if (attribute != null)
                    {
                        if (attribute.gamemode == gamemode)
                        {
                            harmony.CreateClassProcessor(type).Patch();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Exiled.API.Features.Log.Error($"Patching failed! {exception}");
            }
        }
    }
}
