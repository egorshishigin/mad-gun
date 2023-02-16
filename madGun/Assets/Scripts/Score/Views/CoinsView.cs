using Zenject;

using TMPro;

using UnityEngine;

namespace Score
{
    public class CoinsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coins;

        private GameScore _gameScore;

        [Inject]
        private void Construct(GameScore gameScore)
        {
            _gameScore = gameScore;
        }

        private void OnEnable()
        {
            _gameScore.CoinsChanged += UpdateCoinsText;
        }

        private void OnDisable()
        {
            _gameScore.CoinsChanged -= UpdateCoinsText;
        }

        private void UpdateCoinsText(int value)
        {
            _coins.text = value.ToString();
        }
    }
}