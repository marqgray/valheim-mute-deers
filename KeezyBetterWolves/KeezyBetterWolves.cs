using BepInEx;
using HarmonyLib;
using KeezyBetterWolves.Animals;

namespace KeezyBetterWolves
{
    public delegate void ZSFXPlayListener(ZSFX sfx, ref bool shouldMute);

    [BepInPlugin("KeezyBetterWolves", "Keezy's Better Wolves", "0.1.0.0")]
    public class KeezyBetterWolves : BaseUnityPlugin
    {
        private void Awake()
        {
            ZSFXPlayEvent += Wolf.MuteTamedWolfListener;
            new Harmony("KeezyBetterWolves.Harmony").PatchAll();
        }

        public static event ZSFXPlayListener ZSFXPlayEvent;

        [HarmonyPatch(typeof(ZSFX), "Play")]
        private class ZSFXPlayPatch
        {
            [HarmonyPrefix]
            private static bool Prefix(ref ZSFX __instance)
            {
                var shouldMute = false;
                ZSFXPlayEvent?.Invoke(__instance, ref shouldMute);
                return !shouldMute;
            }
        }
    }
}