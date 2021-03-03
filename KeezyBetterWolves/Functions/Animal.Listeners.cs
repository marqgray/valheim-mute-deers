using System;
using System.Linq;
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
            try
            {
                if (shouldMute) return;
                if (!sfx.name.Contains(SfxIdentifiers.WolfHowl)) return;
                var configVolume = KeezyBetterWolves.ConfigMuteTamedWolvesHowlVolumePercentage.Value;
                var volumeMultiplier = configVolume / 100f;
                sfx.m_maxVol *= volumeMultiplier;
                sfx.m_minVol *= volumeMultiplier;
            }
            catch (Exception exception)
            {
                throw new Exception(ExceptionMessages.AdjustTamedWolfVolume);
            }
        }

        public static void PlayerTamedWolfDamageReduction(Character targetCharacter, ref HitData hit,
            ref bool showDamageText, ref bool triggerEffects, ref HitData.DamageModifier damageMod)
        {
            try
            {
                if (!AnimalConditionals.IsCharacterAWolf(targetCharacter)) return;
                var wolf = new Wolf(targetCharacter);
                if (!wolf.IsTamed()) return;
                if (hit.GetAttacker().m_name
                    .Contains(PlayerIdentifiers.Player[PlayerIdentifiers.PlayerType.PlayerNormal]))
                    hit.m_damage.Modify(1f - KeezyBetterWolves.ConfigTamedWolfPlayerDamageReduction.Value / 100f);
                else
                    hit.m_damage.Modify(1f - KeezyBetterWolves.ConfigTamedWolfDamageReduction.Value / 100f);
            }
            catch (Exception exception)
            {
                throw new Exception(ExceptionMessages.TamedWolfDamageReduce);
            }
        }
    }
}