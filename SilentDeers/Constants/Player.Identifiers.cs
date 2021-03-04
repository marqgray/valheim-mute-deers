using System.Collections.Generic;

namespace SilentDeers
{
    public static class PlayerIdentifiers
    {
        public enum PlayerType
        {
            PlayerNormal = 0
        }

        public static readonly Dictionary<PlayerType, string> Player = new Dictionary<PlayerType, string>
        {
            {PlayerType.PlayerNormal, "Human"}
        };
    }
}