namespace SilentDeers
{
    public static class AnimalConditionals
    {
        public static bool IsCharacterADeer(Character character)
        {
            return AnimalIdentifiers.Deers.ContainsValue(character.m_name);
        }

        public static bool IsCharacterADeer(BaseAI baseAI)
        {
            var baseAICharacter = baseAI.GetComponent<Character>();
            return IsCharacterADeer(baseAICharacter);
        }
    }
}