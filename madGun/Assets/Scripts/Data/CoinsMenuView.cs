using Zenject;

using TMPro;

using UnityEngine;

namespace Data.Views
{
    public class CoinsMenuView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsText;

        private GameDataIO _dataIO;

        private GameData _gameData;

        [Inject]
        private void Construct(GameDataIO dataIO)
        {
            _dataIO = dataIO;

            _gameData = _dataIO.GameData;
        }

        private void OnEnable()
        {
            _gameData.CoinsSpent += CoinsSpentHandler;
        }

        private void OnDisable()
        {
            _gameData.CoinsSpent -= CoinsSpentHandler;
        }

        private void Start()
        {
            CoinsSpentHandler(_gameData.Coins);
        }

        private void CoinsSpentHandler(int value)
        {
            _coinsText.text = value.ToString();
        }
    }
}