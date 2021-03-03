using BepInEx;
using HarmonyLib;
using KeezyBetterWolves.Animals;
using KeezyBetterWolves.Constants;

namespace KeezyBetterWolves
{
    public delegate void ZSFXPlayListener(ZSFX sfx, ref bool shouldMute);

    [BepInPlugin("KeezyBetterWolves", ModInfo.Name, ModInfo.Version)]
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