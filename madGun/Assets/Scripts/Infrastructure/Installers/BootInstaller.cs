using Zenject;

using Data;

using WeaponsShop;

using PowerUp;

using GamePause;

using Localization;

using UnityEngine;

namespace Infrastructure
{
    public class BootInstaller : MonoInstaller
    {
        [SerializeField] private WeaponsConfig _weaponsConfig;

        [SerializeField] private PowerUpConfig _powerUpConfig;

        [SerializeField] private LocalizationSetting _localizationSetting;

        public override void InstallBindings()
        {
            BindGamePause();

            BindWeaponsConfig();

            BindPowerUpConfig();

            BindGameData();

            BindLocalizationSetting();
        }

        private void BindGameData()
        {
            Container
                .Bind<GameDataIO>()
                .AsSingle();
        }

        private void BindPowerUpConfig()
        {
            Container
                .Bind<PowerUpConfig>()
                .FromScriptableObject(_powerUpConfig)
                .AsSingle();
        }

        private void BindWeaponsConfig()
        {
            Container
                .Bind<WeaponsConfig>()
                .FromScriptableObject(_weaponsConfig)
                .AsSingle();
        }

        private void BindGamePause()
        {
            Container
                .Bind<Pause>()
                .AsSingle();
        }

        private void BindLocalizationSetting()
        {
            Container
                .Bind<LocalizationSetting>()
                .FromComponentInNewPrefab(_localizationSetting)
                .AsSingle();
        }
    }
}