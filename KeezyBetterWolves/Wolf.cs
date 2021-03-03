using System;
using System.Linq;
using UnityEngine;

namespace KeezyBetterWolves
{
    public class Wolf
    {
        private Wolf(Character character)
        {
            Character = character;
        }

        public Character Character { get; }

        public static Wolf Instantiate(Character character)
        {
            if (!IsCharacterAWolf(character))
                throw new Exception("Character object is not a wolf");
            return new Wolf(character);
        }

        public static bool IsCharacterAWolf(Character character)
        {
            return character.m_name == "$enemy_wolf" || character.m_name == "$enemy_wolfcub";
        }

        public bool IsTamed()
        {
            try
            {
                return Character.IsTamed();
            }
            catch (Exception exception)
            {
                throw new Exception("Error trying to retrieve wolf tame status for an unknown reason");
            }
        }

        public static void MuteTamedWolfListener(ZSFX sfx, ref bool shouldMute)
        {
            try
            {
                if (!sfx.name.Contains("sfx_wolf_haul")) return;
                foreach (var wolf in from wolfCharacter in Character.GetAllCharacters().FindAll(IsCharacterAWolf)
                    where Vector3.Distance(wolfCharacter.GetTransform().position, sfx.transform.position) < 30
                    select Instantiate(wolfCharacter)
                    into wolf
                    where wolf.IsTamed()
                    select wolf) shouldMute = true;
            }
            catch (Exception exception)
            {
                throw new Exception("Error trying to determine to mute a wolf for an unknown reason");
            }
        }
    }
}