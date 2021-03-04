using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace SilentDeers
{
    public delegate void ZSFXPlayListener(ZSFX sfx, ref bool shouldMute);

    [BepInPlugin("SilentDeers", ModInfo.Name, ModInfo.Version)]
    public class SilentDeer : BaseUnityPlugin
    {
        public static ConfigEntry<bool> ConfigMuteDeerBleat;
        public static ConfigEntry<int> ConfigMuteDeerBleatPercentage;
        public static ConfigEntry<int> ConfigMuteDeerBleatVolumePercentage;

        private void Awake()
        {

            ConfigMuteDeerBleat = Config.Bind("Deers.Sound", "DeerBleatMute", true,
                "Set this key to true or false to mute all deer bleats entirely.");

            ConfigMuteDeerBleatPercentage = Config.Bind("Deers.Sound", "DeerBleatMutePercent", 100,
                "Instead of muting all deer bleats entirely, you could also specify a range from 0 to 100 to allow only some bleats of the deers to go through. Example: 20 = 80% howl rate per deer.");

            ConfigMuteDeerBleatVolumePercentage = Config.Bind("Deers.Sound", "DeerBleatVolumePercent", 100,
                "Adjust the volume of all deer bleats by a range of 0 to 100.");

            if (ConfigMuteDeerBleat.Value || ConfigMuteDeerBleatPercentage.Value < 100)
                ZSFXPlayEvent += AnimalListeners.MuteDeerListener;

            if (ConfigMuteDeerBleatVolumePercentage.Value < 100)
                ZSFXPlayEvent += AnimalListeners.AdjustDeerVolume;

            new Harmony("SilentDeers.Harmony").PatchAll();
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