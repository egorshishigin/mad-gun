using Zenject;

using PlayerInput;

using Projectiles;

using Player;

using Timer;

using Spawner;

using Score;

using Boosters;

using GamePause;

using Weapons;

using UnityEngine;

namespace Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerControl _playerControl;

        [SerializeField] private EnemyProjectile _enemyProjectilePrefab;

        [SerializeField] private Transform _playerProjectilesHolder;

        [SerializeField] private Transform _enemyProjectilesHolder;

        [SerializeField] private Transform _boostersHolder;

        [SerializeField] private BoostersHolder _boostersHolderPrefab;

        [SerializeField] private PlayerHitBox _player;

        [SerializeField] private WaveSpawner _waveSpawner;

        [SerializeField] private WavesConfig _wavesConfig;

        [SerializeField] private KeyBoardPauseButton _pauseButton;

        [SerializeField] private SceneLoader _sceneLoader;

        [SerializeField] private AmmoConfig _ammoConfig;

        [SerializeField] private WeaponSwitch _weaponSwitch;

        [SerializeField] private BoostersAudio _boostersAudio;

        public override void InstallBindings()
        {
            BindPlayerInput();

            BindPlayerProjectile();

            BindEnemyShooting();

            BindPlayer();

            BindGameTimer();

            BindSpawner();

            BindGameScore();

            BindBoostersPool();

            BindPause();

            BindSceneLoader();

            BindAmmo();
        }

        private void BindAmmo()
        {
            Container.Bind<WeaponSwitch>().FromInstance(_weaponSwitch).AsSingle();

            Container.Bind<AmmoConfig>().FromScriptableObject(_ammoConfig).AsSingle();

            Container.Bind<Ammo>().AsSingle();
        }

        private void BindSceneLoader()
        {
            Container.Bind<SceneLoader>().FromInstance(_sceneLoader).AsSingle();
        }

        private void BindPause()
        {
            Container.Bind<KeyBoardPauseButton>().FromInstance(_pauseButton).AsSingle();
        }

        private void BindBoostersPool()
        {

            Container.Bind<BoostersAudio>().FromInstance(_boostersAudio).AsSingle();

            Container.Bind<BoostersPool>().AsSingle();

            Container
               .BindMemoryPool<BoostersHolder, BoostersHolder.Pool>()
               .WithInitialSize(25)
               .FromComponentInNewPrefab(_boostersHolderPrefab)
               .UnderTransform(_boostersHolder);
        }

        private void BindGameScore()
        {
            GameScore gameScore = new GameScore(0, 0);

            Container.Bind<GameScore>().FromInstance(gameScore).AsSingle();
        }

        private void BindSpawner()
        {
            Container.Bind<WavesConfig>().FromScriptableObject(_wavesConfig).AsSingle();

            Container.BindInterfacesTo<WaveSpawner>().FromInstance(_waveSpawner).AsSingle();

            Container.BindFactory<IWave, WaveFactory>().FromFactory<WavesFactory>();
        }

        private void BindGameTimer()
        {
            Container.BindInterfacesAndSelfTo<GameTimer>().AsSingle();
        }

        private void BindPlayer()
        {
            Container
                .Bind<PlayerHitBox>()
                .FromInstance(_player)
                .AsSingle();
        }

        private void BindEnemyShooting()
        {
            Container.Bind<EnemyProjectilePool>().AsSingle();

            Container
                .BindMemoryPool<EnemyProjectile, EnemyProjectile.Pool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(_enemyProjectilePrefab)
                .UnderTransform(_enemyProjectilesHolder);
        }

        private void BindPlayerProjectile()
        {
            //Container.Bind<ProjectilesPool>().AsSingle();

            //Container
            //    .BindMemoryPool<PlayerProjectile, PlayerProjectile.Pool>()
            //    .WithInitialSize(10)
            //    .FromComponentInNewPrefab(_weaponSettings.PlayerProjectile)
            //    .UnderTransform(_playerProjectilesHolder);
        }

        private void BindPlayerInput()
        {
            Container
                .Bind<PlayerControl>()
                .FromInstance(_playerControl)
                .AsSingle();
        }
    }
}