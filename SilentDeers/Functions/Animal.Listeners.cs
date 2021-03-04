using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SilentDeers
{
    public static class AnimalListeners
    {
        public static void MuteDeerListener(ZSFX sfx, ref bool shouldMute)
        {
            try
            {
                if (shouldMute) return;
                if (!sfx.name.Contains(SfxIdentifiers.DeerBleat)) return;
                if (Random.Range(0, 100) > SilentDeer.ConfigMuteDeerBleatPercentage.Value) return;
                foreach (var deer in from deerCharacter in Character.GetAllCharacters()
                        .FindAll(AnimalConditionals.IsCharacterADeer)
                    where Vector3.Distance(deerCharacter.GetTransform().position, sfx.transform.position) < 30
                    select new Deer(deerCharacter)
                    into deer
                    select deer) shouldMute = true;
            }
            catch (Exception exception)
            {
                throw new Exception(ExceptionMessages.MuteDeerAttempt);
            }
        }

        public static void AdjustDeerVolume(ZSFX sfx, ref bool shouldMute)
        {
            try
            {
                if (shouldMute) return;
                if (!sfx.name.Contains(SfxIdentifiers.DeerBleat)) return;
                var configVolume = SilentDeer.ConfigMuteDeerBleatVolumePercentage.Value;
                var volumeMultiplier = configVolume / 100f;
                sfx.m_maxVol *= volumeMultiplier;
                sfx.m_minVol *= volumeMultiplier;
            }
            catch (Exception exception)
            {
                throw new Exception(ExceptionMessages.AdjustDeerBleatVolume);
            }
        }
    }
}