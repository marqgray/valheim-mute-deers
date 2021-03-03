using System;
using KeezyBetterWolves.Constants;
using KeezyBetterWolves.Functions;

namespace KeezyBetterWolves.Components.Animals
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