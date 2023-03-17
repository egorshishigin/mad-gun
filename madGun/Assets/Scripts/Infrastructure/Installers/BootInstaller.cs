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
            Container.Bind<Pause>().AsSingle();

            Container.Bind<WeaponsConfig>().FromScriptableObject(_weaponsConfig).AsSingle();

            Container.Bind<PowerUpConfig>().FromScriptableObject(_powerUpConfig).AsSingle();

            Container.Bind<GameDataIO>().AsSingle();

            BindLocalizationSetting();
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