using Zenject;

using PlayerInput;

using Projectiles;

using Player;

using Timer;

using Spawner;

using Score;

using Boosters;

using UnityEngine;
using Weapons;

namespace Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerControl _playerControl;

        [SerializeField] private PlayerProjectile _playerProjectilePrefab;

        [SerializeField] private EnemyProjectile _enemyProjectilePrefab;

        [SerializeField] private Transform _playerProjectilesHolder;

        [SerializeField] private Transform _enemyProjectilesHolder;

        [SerializeField] private Transform _boostersHolder;

        [SerializeField] private BoostersHolder _boostersHolderPrefab;

        [SerializeField] private PlayerHitBox _player;

        [SerializeField] private WaveSpawner _waveSpawner;

        [SerializeField] private WavesConfig _wavesConfig;

        [SerializeField] private FireRain _fireRainBooster;

        [SerializeField] private BulletTime _bulletTime;

        [SerializeField] private Weapon _playerWeapon;

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

            BindActiveBoosters();
        }

        private void BindActiveBoosters()
        {
            Container.Bind<ActiveBoostersState>().AsSingle();

            Container.Bind<FireRain>().FromInstance(_fireRainBooster).AsSingle();

            Container.Bind<BulletTime>().FromInstance(_bulletTime).AsSingle();
        }

        private void BindBoostersPool()
        {
            Container.Bind<BoostersPool>().AsSingle();

            Container
               .BindMemoryPool<BoostersHolder, BoostersHolder.Pool>()
               .WithInitialSize(5)
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
            Container.Bind<ProjectilesPool>().AsSingle();

            Container
                .BindMemoryPool<PlayerProjectile, PlayerProjectile.Pool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(_playerWeapon.Projectile)
                .UnderTransform(_playerProjectilesHolder);
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