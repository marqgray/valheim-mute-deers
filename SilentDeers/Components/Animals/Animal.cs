using System;

namespace SilentDeers
{
    public abstract class Animal
    {
        protected Character Character { get; set; }

        public abstract Character GetCharacter();
    }
}