using Zenject;

using Data;

using HealthSystem;

using GamePause;

using Score;

using Timer;

using UnityEngine;

namespace GameOver
{
    public class GameOverHandler : MonoBehaviour
    {
        [SerializeField] private GameOverView _view;

        [SerializeField] private GameObject _pauseMenu;

        [SerializeField] private Health _playerHealth;

        private GameDataIO _gameData;

        private Pause _pause;

        private GameScore _gameScore;

        private GameTimer _gameTimer;

        [Inject]
        private void Construct(GameDataIO gameData, Pause pause, GameScore gameScore, GameTimer timer)
        {
            _gameData = gameData;

            _pause = pause;

            _gameScore = gameScore;

            _gameTimer = timer;
        }

        private void OnEnable()
        {
            _playerHealth.Died += GameOver;
        }

        private void OnDisable()
        {
            _playerHealth.Died -= GameOver;
        }

        private void GameOver()
        {
            _view.gameObject.SetActive(true);

            _pauseMenu.SetActive(false);

            int score = _gameScore.Coins * _gameScore.Kills;

            _view.SetGameOverValues(_gameScore.Coins, _gameTimer.GameTime, _gameScore.Kills, score);

            if (score > _gameData.GameData.HighScore)
            {
                _gameData.GameData.SetHighScore(score);
            }

            _pause.SetPause(true);

            _gameData.SaveGameData();
        }
    }
}