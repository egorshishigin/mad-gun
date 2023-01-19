using Zenject;

using TMPro;

using UnityEngine;

namespace Score
{
    public class GameScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;

        private GameScore _gameScore;

        [Inject]
        private void Construct(GameScore gameScore)
        {
            _gameScore = gameScore;
        }

        private void OnEnable()
        {
            _gameScore.ScoreChanged += UpdateGameScoreText;
        }

        private void OnDisable()
        {
            _gameScore.ScoreChanged += UpdateGameScoreText;
        }

        private void UpdateGameScoreText(int value)
        {
            _score.text = value.ToString();
        }
    }
}