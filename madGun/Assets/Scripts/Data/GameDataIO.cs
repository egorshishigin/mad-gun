using System.Runtime.InteropServices;

using Zenject;

using Newtonsoft.Json;

using WeaponsShop;

using PowerUp;
using System.IO;
using UnityEngine;

namespace Data
{
    public class GameDataIO
    {
        [DllImport("__Internal")]
        private static extern void SaveDataExtern(string data);

        private const string FileName = "GameData";

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

#if UNITY_STANDALONE || UNITY_EDITOR
            File.WriteAllText(Application.persistentDataPath + FileName, json);
#else
            SaveDataExtern(json);
#endif
        }

        public void LoadGameData(string data)
        {
            string loadedData = data;

            if (loadedData.Length <= 3 ||  string.IsNullOrEmpty(loadedData))
            {
                _gameData = new GameData(0, 0, _weaponsConfig, _powerUpConfig);

                _gameData.InitializeWeapons();

                _gameData.InitializePowerUps();
            }
            else
            {
                _gameData = JsonConvert.DeserializeObject<GameData>(loadedData);
            }
        }

        public void LoadLocalData()
        {
            string json;

            if (File.Exists(Application.persistentDataPath + FileName))
            {
                json = File.ReadAllText(Application.persistentDataPath + FileName);

                _gameData = JsonConvert.DeserializeObject<GameData>(json);
            }
            else
            {
                _gameData = new GameData(10000, 0, _weaponsConfig, _powerUpConfig);

                _gameData.InitializeWeapons();

                _gameData.InitializePowerUps();
            }
        }
    }
}