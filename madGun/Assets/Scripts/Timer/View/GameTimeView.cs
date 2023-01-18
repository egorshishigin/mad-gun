using Zenject;

using TMPro;

using UnityEngine;

namespace Timer
{
    public class GameTimeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _gameTime;

        private GameTimer _gameTimer;

        [Inject]
        private void Construct(GameTimer gameTimer)
        {
            _gameTimer = gameTimer;
        }

        private void OnEnable()
        {
            _gameTimer.TimeChanged += TimeChangedHandler;
        }

        private void OnDisable()
        {
            _gameTimer.TimeChanged -= TimeChangedHandler;
        }

        private void TimeChangedHandler(float gameTime)
        {
            float minutes = Mathf.FloorToInt(gameTime / 60);

            float seconds = Mathf.FloorToInt(gameTime % 60);

            string time = $"{minutes:00}:{seconds:00}";

            _gameTime.text = time;
        }
    }
}