using Zenject;

using Score;

using UnityEngine;

namespace Boosters
{
    public class CoinsBooster : BoosterBase
    {
        [SerializeField] private int _coinsCount;

        private GameScore _gameScore;

        [Inject]
        private void Construct(GameScore gameScore)
        {
            _gameScore = gameScore;
        }

        protected override void ActivateBooster()
        {
            _gameScore.IncreaseCoins(_coinsCount);
        }
    }
}