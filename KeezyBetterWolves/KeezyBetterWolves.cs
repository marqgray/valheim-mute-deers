using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using KeezyBetterWolves.Constants;

namespace KeezyBetterWolves
{
    public delegate void ZSFXPlayListener(ZSFX sfx, ref bool shouldMute);

    public delegate void BaseAIOnDamagedListener(ref BaseAI victim, ref float damage, ref Character attacker);

    [BepInPlugin("KeezyBetterWolves", ModInfo.Name, ModInfo.Version)]
    public class KeezyBetterWolves : BaseUnityPlugin
    {
        public static ConfigEntry<bool> ConfigMuteTamedWolvesHowl;
        public static ConfigEntry<int> ConfigMuteTamedWolvesHowlPercentage;
        public static ConfigEntry<int> ConfigMuteTamedWolvesHowlVolumePercentage;

        private void Awake()
        {
            ConfigMuteTamedWolvesHowl = Config.Bind("Wolves.Sound", "TamedHowlMute", true,
                "Set this key to true or false to mute all tamed wolves howl entirely.");

            ConfigMuteTamedWolvesHowlPercentage = Config.Bind("Wolves.Sound", "TamedHowlMutePercent", 100,
                "Instead of muting all tamed wolves howl entirely, you could also specify a range from 0 to 100 to allow only some howls of the wolves to go through. Example: 20 = 80% howl rate per wolf.");

            ConfigMuteTamedWolvesHowlVolumePercentage = Config.Bind("Wolves.Sound", "TamedHowlVolumePercent", 100,
                "Adjust the volume of all tamed wolf howls by a range of 0 to 100.");

            if (ConfigMuteTamedWolvesHowl.Value) ZSFXPlayEvent += AnimalListeners.MuteTamedWolfListener;

            if (ConfigMuteTamedWolvesHowlVolumePercentage.Value < 100)
                ZSFXPlayEvent += AnimalListeners.AdjustTamedWolfVolume;

            new Harmony("KeezyBetterWolves.Harmony").PatchAll();
        }

        public static event ZSFXPlayListener ZSFXPlayEvent;
        public static event BaseAIOnDamagedListener BaseAIOnDamagedEvent;

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

        [HarmonyPatch(typeof(BaseAI), "OnDamaged")]
        private class BaseAIOnDamagedPatch
        {
            [HarmonyPrefix]
            private static bool Prefix(ref BaseAI __instance, ref float damage, ref Character attacker)
            {
                BaseAIOnDamagedEvent?.Invoke(ref __instance, ref damage, ref attacker);
                return true;
            }
        }
    }
}