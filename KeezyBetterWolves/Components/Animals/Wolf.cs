using System;
using KeezyBetterWolves.Constants;

namespace KeezyBetterWolves
{
    public class Wolf : Animal
    {
        public Wolf(Character character)
        {
            if (!AnimalConditionals.IsCharacterAWolf(character))
                throw new Exception(ExceptionMessages.InvalidWolfCharacterObject);
            Character = character;
        }

        public override Character GetCharacter()
        {
            return Character;
        }
    }
}