using System;
using KeezyBetterWolves.Constants;

namespace KeezyBetterWolves
{
    public abstract class Animal
    {
        protected Character Character { get; set; }

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

        public abstract Character GetCharacter();
    }
}