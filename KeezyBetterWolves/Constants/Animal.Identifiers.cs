using System.Collections.Generic;

namespace KeezyBetterWolves
{
    public static class AnimalIdentifiers
    {
        public static readonly Dictionary<AnimalType, string> Animals = new Dictionary<AnimalType, string>
        {
            {AnimalType.EnemyAdultWolf, "$enemy_wolf"},
            {AnimalType.EnemyAdultWolfCub, "$enemy_wolfcub"}
        };

        public static readonly Dictionary<AnimalType, string> Wolves = new Dictionary<AnimalType, string>
        {
            {AnimalType.EnemyAdultWolf, Animals[AnimalType.EnemyAdultWolf]},
            {AnimalType.EnemyAdultWolfCub, Animals[AnimalType.EnemyAdultWolfCub]}
        };

        public static readonly Dictionary<AnimalType, string> EnemyWolves = new Dictionary<AnimalType, string>
        {
            {AnimalType.EnemyAdultWolf, Animals[AnimalType.EnemyAdultWolf]},
            {AnimalType.EnemyAdultWolfCub, Animals[AnimalType.EnemyAdultWolfCub]}
        };
    }

    public enum AnimalType
    {
        EnemyAdultWolf = 0,
        EnemyAdultWolfCub = 1
    }
}