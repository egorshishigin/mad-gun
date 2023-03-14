using System.Runtime.InteropServices;

using Zenject;

using Newtonsoft.Json;

using WeaponsShop;

using PowerUp;

namespace Data
{
    public class GameDataIO
    {
        [DllImport("__Internal")]
        private static extern void SaveDataExtern(string data);

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

            SaveDataExtern(json);
        }

        public void LoadGameData(string data)
        {
            string loadedData = data;

            if (loadedData.Length <= 3)
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
    }
}