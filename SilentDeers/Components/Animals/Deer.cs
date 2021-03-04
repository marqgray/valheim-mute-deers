using System;

namespace SilentDeers
{
    public class Deer : Animal
    {
        public Deer(Character character)
        {
            if (!AnimalConditionals.IsCharacterADeer(character))
                throw new Exception(ExceptionMessages.InvalidDeerCharacterObject);
            Character = character;
        }

        public override Character GetCharacter()
        {
            return Character;
        }
    }
}