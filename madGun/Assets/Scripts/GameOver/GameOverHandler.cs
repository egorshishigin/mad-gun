using System.Runtime.InteropServices;

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
        [DllImport("__Internal")]
        private static extern void SetLeaderboard(int value);

        [SerializeField] private GameOverView _view;

        [SerializeField] private GameObject _pauseMenu;

        [SerializeField] private GameObject _reviveButton;

        [SerializeField] private Health _playerHealth;

        [SerializeField] private AudioSource _coinsRewardSound;

        private GameDataIO _gameData;

        private Pause _pause;

        private GameScore _gameScore;

        private GameTimer _gameTimer;

        private YandexAD _yandexAD;

        private bool _reviveUsed;

        [Inject]
        private void Construct(GameDataIO gameData, Pause pause, GameScore gameScore, GameTimer timer, YandexAD yandexAD)
        {
            _gameData = gameData;

            _pause = pause;

            _gameScore = gameScore;

            _gameTimer = timer;

            _yandexAD = yandexAD;
        }

        private void OnEnable()
        {
            _playerHealth.Died += GameOver;

            _yandexAD.GoldADRewarded += DoubleGold;

            _yandexAD.ReviveADRewarded += RevivePlayer;
        }

        private void OnDisable()
        {
            _playerHealth.Died -= GameOver;

            _yandexAD.GoldADRewarded -= DoubleGold;

            _yandexAD.ReviveADRewarded -= RevivePlayer;
        }

        private void GameOver()
        {
            _view.gameObject.SetActive(true);

            _pauseMenu.SetActive(false);

            _reviveButton.SetActive(_reviveUsed ? false : true);

            int score = _gameScore.Coins * _gameScore.Kills;

            _view.SetGameOverValues(_gameScore.Coins, _gameTimer.GameTime, _gameScore.Kills, score);

            _view.PanelFadeAnimation();

            if (score > _gameData.GameData.HighScore)
            {
                _gameData.GameData.SetHighScore(score);

                SetLeaderboard(score);
            }

            _pause.SetPause(true);

            _gameData.GameData.GainCoins(_gameScore.Coins);

            _gameData.SaveGameData();
        }

        private void RevivePlayer()
        {
            _reviveUsed = true;

            _playerHealth.HealUp(100);

            _playerHealth.Revive();

            _view.PanelFadeOutAnimation();

            _pause.SetPause(false);

            _pauseMenu.gameObject.SetActive(true);
        }

        private void DoubleGold()
        {
            _coinsRewardSound.PlayOneShot(_coinsRewardSound.clip);

            int doubleCoins = _gameScore.Coins * 2;

            _gameData.GameData.GainCoins(doubleCoins);

            _view.SetDoubleCoinsText(doubleCoins);

            _gameData.SaveGameData();
        }
    }
}