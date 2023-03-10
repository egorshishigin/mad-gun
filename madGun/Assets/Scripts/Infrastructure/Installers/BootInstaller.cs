using Zenject;

using Data;

using WeaponsShop;

using PowerUp;

using GamePause;

using UnityEngine;

namespace Infrastructure
{
    public class BootInstaller : MonoInstaller
    {
        [SerializeField] private WeaponsConfig _weaponsConfig;

        [SerializeField] private PowerUpConfig _powerUpConfig;

        [SerializeField] private YandexAD _yandexAD;

        public override void InstallBindings()
        {
            Container.Bind<Pause>().AsSingle();

            Container.Bind<WeaponsConfig>().FromScriptableObject(_weaponsConfig).AsSingle();

            Container.Bind<PowerUpConfig>().FromScriptableObject(_powerUpConfig).AsSingle();

            Container.Bind<GameDataIO>().AsSingle();

            Container.Bind<YandexAD>().FromComponentInNewPrefab(_yandexAD).AsSingle();
        }
    }
}