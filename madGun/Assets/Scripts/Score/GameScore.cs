using System;

namespace Score
{
    public class GameScore
    {
        private int _currentScore;

        public GameScore(int currentScore)
        {
            _currentScore = currentScore;
        }

        public event Action<int> ScoreChanged = delegate { };

        public void IncreaseScore(int amount)
        {
            _currentScore += amount;

            ScoreChanged.Invoke(_currentScore);
        }
    }
}