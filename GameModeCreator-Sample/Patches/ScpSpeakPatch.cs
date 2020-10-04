using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Assets._Scripts.Dissonance;

namespace GameModeCreator.Sample.Patches
{
    [HarmonyPatch(typeof(DissonanceUserSetup), nameof(DissonanceUserSetup.CallCmdAltIsActive))]
    [API.Patch.Attributes.PatchOf(typeof(GMSample))]
    public class ScpSpeakPatch
    {
        public static bool Prefix(DissonanceUserSetup __instance, bool value)
        {
            if (__instance.gameObject.TryGetComponent(out CharacterClassManager ccm))
            {
                if (ccm.IsAnyScp())
                {
                    __instance.MimicAs939 = value;
                }
            }
            return true;
        }
    }
}
