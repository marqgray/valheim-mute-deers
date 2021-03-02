using System;
using UnityEngine;

namespace KeezyBetterWolves
{
    public class Wolf
    {
        private const float MutedFloatValue = 0f;
        private readonly float _originalIdleSoundChance;
        private readonly float _originalIdleSoundInterval;

        private Wolf(Character character)
        {
            Character = character;
            _originalIdleSoundChance = Character.GetBaseAI().m_idleSoundChance;
            _originalIdleSoundInterval = Character.GetBaseAI().m_idleSoundInterval;
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

        public void SetMuteState(bool isMuted)
        {
            try
            {
                if (isMuted)
                {
                    Character.GetBaseAI().m_idleSoundChance = MutedFloatValue;
                    Character.GetBaseAI().m_idleSoundInterval = MutedFloatValue;
                }
                else
                {
                    Character.GetBaseAI().m_idleSoundChance = _originalIdleSoundChance;
                    Character.GetBaseAI().m_idleSoundInterval = _originalIdleSoundInterval;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error trying to mute wolf for an unknown reason");
            }
        }

        public bool GetMuteState()
        {
            try
            {
                return Math.Abs(Character.GetBaseAI().m_idleSoundChance - MutedFloatValue) < 0.1f &&
                       Math.Abs(Character.GetBaseAI().m_idleSoundInterval - MutedFloatValue) < 0.1f;
            }
            catch (Exception e)
            {
                throw new Exception("Error trying to retrieve wolf mute state for an unknown reason");
            }
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
            if (!sfx.name.Contains("sfx_wolf_haul")) return;
            foreach (var wolfCharacter in Character.GetAllCharacters().FindAll(IsCharacterAWolf))
                if (Vector3.Distance(wolfCharacter.GetTransform().position, sfx.transform.position) < 30)
                {
                    var wolf = Instantiate(wolfCharacter);
                    if (wolf.IsTamed()) shouldMute = true;
                }
        }
    }
}