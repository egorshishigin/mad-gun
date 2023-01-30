using Zenject;

using Data;

using UnityEngine;

namespace PowerUp
{
    public class PowerUpBase : MonoBehaviour
    {
        [SerializeField] private int _id;

        [SerializeField] private int _timePowerUpAmount;

        [SerializeField] private int _countPowerUpAmount;

        private GameDataIO _gameData;

        private IUpgradetable _upgradetable;

        [Inject]
        private void Consruct(GameDataIO gameDataIO)
        {
            _gameData = gameDataIO;
        }

        private void Awake()
        {
            _upgradetable = transform.GetComponent<IUpgradetable>();

            ApplyPowerUp();
        }

        public void ApplyPowerUp()
        {
            var level = GetPowerUpLevel();

            _upgradetable.Upgrade(level * _timePowerUpAmount, level * _countPowerUpAmount);
        }

        private int GetPowerUpLevel()
        {
            int level = _gameData.GameData.PowerUpsLevels[_id];

            return level;
        }
    }
}