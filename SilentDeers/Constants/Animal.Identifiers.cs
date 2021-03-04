using System.Collections.Generic;

namespace SilentDeers
{
    public static class AnimalIdentifiers
    {
        public static readonly Dictionary<AnimalType, string> Animals = new Dictionary<AnimalType, string>
        {
            {AnimalType.Deer, "$enemy_deer"}
        };

        public static readonly Dictionary<AnimalType, string> Deers = new Dictionary<AnimalType, string>
        {
            {AnimalType.Deer, Animals[AnimalType.Deer]}
        };

        public static readonly Dictionary<AnimalType, string> EnemyWolves = new Dictionary<AnimalType, string>
        {
            {AnimalType.Deer, Animals[AnimalType.Deer]}
        };
    }

    public enum AnimalType
    {
        Deer = 0
    }
}