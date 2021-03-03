using System;
using System.Linq;
using KeezyBetterWolves.Constants;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KeezyBetterWolves
{
    public static class AnimalListeners
    {
        public static void MuteTamedWolfListener(ZSFX sfx, ref bool shouldMute)
        {
            try
            {
                if (shouldMute) return;
                if (!sfx.name.Contains(SfxIdentifiers.WolfHowl)) return;
                if (Random.Range(0, 100) > KeezyBetterWolves.ConfigMuteTamedWolvesHowlPercentage.Value) return;
                foreach (var wolf in from wolfCharacter in Character.GetAllCharacters()
                        .FindAll(AnimalConditionals.IsCharacterAWolf)
                    where Vector3.Distance(wolfCharacter.GetTransform().position, sfx.transform.position) < 30
                    select new Wolf(wolfCharacter)
                    into wolf
                    where wolf.IsTamed()
                    select wolf) shouldMute = true;
            }
            catch (Exception exception)
            {
                throw new Exception(ExceptionMessages.MuteWolfAttempt);
            }
        }

        public static void AdjustTamedWolfVolume(ZSFX sfx, ref bool shouldMute)
        {
            if (shouldMute) return;
            if (!sfx.name.Contains(SfxIdentifiers.WolfHowl)) return;
            var configVolume = KeezyBetterWolves.ConfigMuteTamedWolvesHowlVolumePercentage.Value;
            var volumeMultiplier = configVolume / 100f;
            sfx.m_maxVol *= volumeMultiplier;
            sfx.m_minVol *= volumeMultiplier;
        }

        public static void PlayerTamedWolfDamageReduction(ref BaseAI victim, ref float damage, ref Character attacker)
        {
            if (AnimalConditionals.IsCharacterAWolf(victim))
            {
            }
        }
    }
}