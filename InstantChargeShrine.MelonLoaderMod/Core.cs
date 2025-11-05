using HarmonyLib;
using Il2Cpp;
using MelonLoader;

[assembly: MelonInfo(typeof(InstantChargeShrine.MelonLoaderMod.Core), "InstantChargeShrine", "1.0.3", "Slimaeus", null)]
[assembly: MelonGame("Ved", "Megabonk")]

namespace InstantChargeShrine.MelonLoaderMod;

public class Core : MelonMod
{
    [HarmonyPatch(typeof(ChargeShrine))]
    public static class ChargeShrinePatches
    {
        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        public static void Awake_Postfix(ChargeShrine __instance)
        {
            __instance.chargeTime = 0f;
            __instance.currentChargeTime = 0f;
        }
    }
}