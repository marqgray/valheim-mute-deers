using System;

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
            if (character.m_name != "$enemy_wolf" && character.m_name != "$enemy_wolfcub")
                throw new NotImplementedException();
            return new Wolf(character);
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
        }

        public static void MuteTamedWolf(Character character)
        {
            throw new NotImplementedException();
        }
    }
}