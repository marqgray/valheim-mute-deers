namespace KeezyBetterWolves
{
    public static class AnimalConditionals
    {
        public static bool IsCharacterAWolf(Character character)
        {
            return AnimalIdentifiers.Wolves.ContainsValue(character.m_name);
        }
    }
}