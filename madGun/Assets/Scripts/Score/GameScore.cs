using System;

using UnityEngine;

namespace Score
{
    public class GameScore
    {
        private int _currentScore;

        private int _coins;

        public GameScore(int currentScore, int coins)
        {
            _currentScore = currentScore;

            _coins = coins;

            ScoreChanged.Invoke(_currentScore);

            CoinsChanged.Invoke(_coins);
        }

        public event Action<int> ScoreChanged = delegate { };

        public event Action<int> CoinsChanged = delegate { };

        public void IncreaseScore(int amount)
        {
            _currentScore += amount;

            ScoreChanged.Invoke(_currentScore);
        }

        public void IncreaseCoins(int amount)
        {
            _coins += amount;

            CoinsChanged.Invoke(_coins);
        }
    }
}