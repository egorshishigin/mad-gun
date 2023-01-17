using Zenject;

using PlayerInput;

using Projectiles;

using Player;

using UnityEngine;

namespace Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerControl _playerControl;

        [SerializeField] private PlayerProjectile _playerProjectilePrefab;

        [SerializeField] private EnemyProjectile _enemyProjectilePrefab;

        [SerializeField] private Transform _playerProjectilesHolder;

        [SerializeField] private Transform _enemyProjectilesHolder;

        [SerializeField] private PlayerHitBox _player;

        public override void InstallBindings()
        {
            BindPlayerInput();

            BindPlayerProjectile();

            BindEnemyShooting();

            BindPlayer();
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
                .FromComponentInNewPrefab(_playerProjectilePrefab)
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