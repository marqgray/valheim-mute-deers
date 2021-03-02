using BepInEx;
using HarmonyLib;

namespace KeezyBetterWolves
{
    public delegate void CharacterAwakeListener(Character character);

    public delegate void ZSFXPlayListener(ZSFX sfx, ref bool shouldMute);

    [BepInPlugin("KeezyBetterWolves", "Keezy's Better Wolves", "0.1.0.0")]
    public class KeezyBetterWolves : BaseUnityPlugin
    {
        private void Awake()
        {
            ZSFXPlayEvent += Wolf.MuteTamedWolfListener;
            new Harmony("KeezyBetterWolves.Harmony").PatchAll();
        }

        public static event CharacterAwakeListener CharacterAwakeEvent;
        public static event ZSFXPlayListener ZSFXPlayEvent;

        [HarmonyPatch(typeof(Character), "Awake")]
        private class CharacterAwakePatch
        {
            [HarmonyPostfix]
            private static void Postfix(ref Character __instance)
            {
                CharacterAwakeEvent?.Invoke(__instance);
            }
        }

        [HarmonyPatch(typeof(ZSFX), "Play")]
        private class ZSFXPlayPatch
        {
            [HarmonyPrefix]
            private static bool Prefix(ref ZSFX __instance)
            {
                var shouldMute = false;
                ZSFXPlayEvent.Invoke(__instance, ref shouldMute);
                return !shouldMute;
            }
        }
    }
}