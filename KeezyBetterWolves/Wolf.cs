using System;

namespace KeezyBetterWolves
{
    public class Wolf
    {
        public Character Character { get; }
        private readonly float _originalIdleSoundChance;
        private readonly float _originalIdleSoundInterval;
        private const float MutedFloatValue = 0f;

        public Wolf()
        {
            throw new NotImplementedException();
            this._originalIdleSoundChance = this.Character.GetBaseAI().m_idleSoundChance;
            this._originalIdleSoundInterval = this.Character.GetBaseAI().m_idleSoundInterval;
        }

        public void SetMuteState(bool isMuted)
        {
            try
            {
                if (isMuted)
                {
                    this.Character.GetBaseAI().m_idleSoundChance = MutedFloatValue;
                    this.Character.GetBaseAI().m_idleSoundInterval = MutedFloatValue;
                }
                else
                {
                    this.Character.GetBaseAI().m_idleSoundChance = this._originalIdleSoundChance;
                    this.Character.GetBaseAI().m_idleSoundInterval = this._originalIdleSoundInterval;
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
                return Math.Abs(this.Character.GetBaseAI().m_idleSoundChance - MutedFloatValue) < 0.1f &&
                       Math.Abs(this.Character.GetBaseAI().m_idleSoundInterval - MutedFloatValue) < 0.1f;
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
        }

        public bool IsTamed()
        {
            return this.Character.IsTamed();
        }
    }
}