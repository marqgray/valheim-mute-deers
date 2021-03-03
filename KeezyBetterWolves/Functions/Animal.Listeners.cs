using System;
using System.Linq;
using KeezyBetterWolves.Components.Animals;
using KeezyBetterWolves.Constants;
using UnityEngine;

namespace KeezyBetterWolves.Functions
{
    public static class AnimalListeners
    {
        public static void MuteTamedWolfListener(ZSFX sfx, ref bool shouldMute)
        {
            try
            {
                if (!sfx.name.Contains(SfxIdentifiers.WolfHowl)) return;
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
    }
}