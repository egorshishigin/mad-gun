using System;

namespace Score
{
    public class GameScore
    {
        private int _kills;

        private int _coins;

        public GameScore(int kills, int coins)
        {
            _kills = kills;

            _coins = coins;

            KillsChanged.Invoke(_kills);

            CoinsChanged.Invoke(_coins);
        }

        public int Coins => _coins;

        public int Kills => _kills;

        public event Action<int> KillsChanged = delegate { };

        public event Action<int> CoinsChanged = delegate { };

        public void IncreaseScore(int amount)
        {
            _kills += amount;

            KillsChanged.Invoke(_kills);
        }

        public void IncreaseCoins(int amount)
        {
            _coins += amount;

            CoinsChanged.Invoke(_coins);
        }
    }
}