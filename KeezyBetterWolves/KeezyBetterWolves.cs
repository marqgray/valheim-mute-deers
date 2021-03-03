using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using KeezyBetterWolves.Constants;

namespace KeezyBetterWolves
{
    public delegate void ZSFXPlayListener(ZSFX sfx, ref bool shouldMute);

    [BepInPlugin("KeezyBetterWolves", ModInfo.Name, ModInfo.Version)]
    public class KeezyBetterWolves : BaseUnityPlugin
    {
        public static ConfigEntry<int> ConfigMuteTamedWolvesHowlPercentage;
        public static ConfigEntry<bool> ConfigMuteTamedWolvesHowl;

        private void Awake()
        {
            ConfigMuteTamedWolvesHowl = Config.Bind("Wolves.Sound", "MuteTamedHowl", true,
                "Set this key to true or false to mute all tamed wolves howl entirely.");

            ConfigMuteTamedWolvesHowlPercentage = Config.Bind("Wolves.Sound", "MuteTamedHowlPercent", 100,
                "Instead of muting all tamed wolves howl entirely, you could also specify a range from 0 to 100 to allow only some howls of the wolves to go through. Example: 20 = 80% howl rate per wolf.");

            if (ConfigMuteTamedWolvesHowl.Value) ZSFXPlayEvent += AnimalListeners.MuteTamedWolfListener;

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