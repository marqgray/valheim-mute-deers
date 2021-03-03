using System.Collections.Generic;

namespace KeezyBetterWolves.Constants
{
    public static class AnimalIdentifiers
    {
        public static readonly Dictionary<Animal, string> Animals = new Dictionary<Animal, string>
        {
            {Animal.EnemyAdultWolf, "$enemy_wolf"},
            {Animal.EnemyAdultWolfCub, "$enemy_wolfcub"}
        };

        public static readonly Dictionary<Animal, string> Wolves = new Dictionary<Animal, string>
        {
            {Animal.EnemyAdultWolf, Animals[Animal.EnemyAdultWolf]},
            {Animal.EnemyAdultWolfCub, Animals[Animal.EnemyAdultWolfCub]}
        };

        public static readonly Dictionary<Animal, string> EnemyWolves = new Dictionary<Animal, string>
        {
            {Animal.EnemyAdultWolf, Animals[Animal.EnemyAdultWolf]},
            {Animal.EnemyAdultWolfCub, Animals[Animal.EnemyAdultWolfCub]}
        };
    }

    public enum Animal
    {
        EnemyAdultWolf = 0,
        EnemyAdultWolfCub = 1
    }
}