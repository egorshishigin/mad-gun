using System.IO;

using Zenject;

using Newtonsoft.Json;

using WeaponsShop;

using PowerUp;

using UnityEngine;

namespace Data
{
    public class GameDataIO
    {
        private const string FileName = "/GameData.json";

        private GameData _gameData;

        private WeaponsConfig _weaponsConfig;

        private PowerUpConfig _powerUpConfig;

        [Inject]
        public GameDataIO(WeaponsConfig weaponsConfig, PowerUpConfig powerUpConfig)
        {
            _weaponsConfig = weaponsConfig;

            _powerUpConfig = powerUpConfig;
        }

        public GameData GameData => _gameData;

        public void SaveGameData()
        {
            string json = JsonConvert.SerializeObject(_gameData);

            File.WriteAllText(Application.persistentDataPath + FileName, json);
        }

        public void LoadGameData()
        {
            if (File.Exists(Application.persistentDataPath + FileName))
            {
                string json = File.ReadAllText(Application.persistentDataPath + FileName);

                _gameData = JsonConvert.DeserializeObject<GameData>(json);
            }
            else
            {
                _gameData = new GameData(100000, 0, _weaponsConfig, _powerUpConfig);

                _gameData.InitializeWeapons();

                _gameData.InitializePowerUps();
            }
        }
    }
}