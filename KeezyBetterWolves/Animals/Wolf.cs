using System;
using System.Linq;
using KeezyBetterWolves.Strings;
using UnityEngine;

namespace KeezyBetterWolves
{
    public class Wolf
    {
        public Wolf(Character character)
        {
            if (!IsCharacterAWolf(character))
                throw new Exception(ExceptionMessages.InvalidCharacterObject);
            Character = character;
        }

        public Character Character { get; }

        public static bool IsCharacterAWolf(Character character)
        {
            return AnimalIdentifiers.Wolves.ContainsValue(character.m_name);
        }

        public bool IsTamed()
        {
            try
            {
                return Character.IsTamed();
            }
            catch (Exception exception)
            {
                throw new Exception(ExceptionMessages.TameQuery);
            }
        }

        public static void MuteTamedWolfListener(ZSFX sfx, ref bool shouldMute)
        {
            try
            {
                if (!sfx.name.Contains(SfxIdentifiers.WolfHowl)) return;
                foreach (var wolf in from wolfCharacter in Character.GetAllCharacters().FindAll(IsCharacterAWolf)
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