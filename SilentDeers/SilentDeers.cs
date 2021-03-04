using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace SilentDeers
{
    public delegate void ZSFXPlayListener(ZSFX sfx, ref bool shouldMute);

    public delegate void CharacterApplyDamageListener(Character targetCharacter, ref HitData hit,
        ref bool showDamageText, ref bool triggerEffects, ref HitData.DamageModifier damageMod);

    [BepInPlugin("SilentDeers", ModInfo.Name, ModInfo.Version)]
    public class KeezyBetterWolves : BaseUnityPlugin
    {
        public static ConfigEntry<int> ConfigTamedWolfDamageReduction;
        public static ConfigEntry<int> ConfigTamedWolfPlayerDamageReduction;
        public static ConfigEntry<bool> ConfigMuteTamedWolvesHowl;
        public static ConfigEntry<int> ConfigMuteTamedWolvesHowlPercentage;
        public static ConfigEntry<int> ConfigMuteTamedWolvesHowlVolumePercentage;

        private void Awake()
        {
            ConfigTamedWolfDamageReduction = Config.Bind("Wolves.Damage", "TamedWolfDamageReduction", 70,
                "Reduce damage on tamed wolves from all non-player sources from a percentage range of 0 to 100.");

            ConfigTamedWolfPlayerDamageReduction = Config.Bind("Wolves.Damage", "TamedWolfPlayerDamageReduction", 95,
                "Reduce damage on tamed wolves by players from a percentage range of 0 to 100.");

            ConfigMuteTamedWolvesHowl = Config.Bind("Wolves.Sound", "TamedHowlMute", true,
                "Set this key to true or false to mute all tamed wolves howl entirely.");

            ConfigMuteTamedWolvesHowlPercentage = Config.Bind("Wolves.Sound", "TamedHowlMutePercent", 100,
                "Instead of muting all tamed wolves howl entirely, you could also specify a range from 0 to 100 to allow only some howls of the wolves to go through. Example: 20 = 80% howl rate per wolf.");

            ConfigMuteTamedWolvesHowlVolumePercentage = Config.Bind("Wolves.Sound", "TamedHowlVolumePercent", 100,
                "Adjust the volume of all tamed wolf howls by a range of 0 to 100.");

            if (ConfigMuteTamedWolvesHowl.Value || ConfigMuteTamedWolvesHowlPercentage.Value < 100)
                ZSFXPlayEvent += AnimalListeners.MuteTamedWolfListener;

            if (ConfigMuteTamedWolvesHowlVolumePercentage.Value < 100)
                ZSFXPlayEvent += AnimalListeners.AdjustTamedWolfVolume;

            if (ConfigTamedWolfDamageReduction.Value > 0 || ConfigTamedWolfPlayerDamageReduction.Value > 0)
                CharacterApplyDamageEvent += AnimalListeners.PlayerTamedWolfDamageReduction;

            new Harmony("SilentDeers.Harmony").PatchAll();
        }

        public static event ZSFXPlayListener ZSFXPlayEvent;
        public static event CharacterApplyDamageListener CharacterApplyDamageEvent;

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

        [HarmonyPatch(typeof(Character), "ApplyDamage")]
        private class CharacterApplyDamagePatch
        {
            [HarmonyPrefix]
            private static bool Prefix(ref Character __instance, ref HitData hit, ref bool showDamageText,
                ref bool triggerEffects, ref HitData.DamageModifier mod)
            {
                CharacterApplyDamageEvent?.Invoke(__instance, ref hit, ref showDamageText, ref triggerEffects, ref mod);
                return true;
            }
        }
    }
}