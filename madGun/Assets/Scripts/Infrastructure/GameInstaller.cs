using Zenject;

using PlayerInput;

using Projectiles;

using Enemies;

using Player;

using UnityEngine;

namespace Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerControl _playerControl;

        [SerializeField] private Projectile _playerProjectilePrefab;

        [SerializeField] private BulletTrail _enemyBulletTrailPrefab;

        [SerializeField] private Transform _playerProjectilesHolder;

        [SerializeField] private Transform _enemyBulletsHolder;

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
            Container.Bind<BulletTrailPool>().AsSingle();

            Container
                .BindMemoryPool<BulletTrail, BulletTrail.Pool>()
                .WithInitialSize(10).FromComponentInNewPrefab(_enemyBulletTrailPrefab)
                .UnderTransform(_enemyBulletsHolder);
        }

        private void BindPlayerProjectile()
        {
            Container.Bind<ProjectilesPool>().AsSingle();

            Container
                .BindMemoryPool<PlayerProjectile, PlayerProjectile.Pool>()
                .WithInitialSize(10).FromComponentInNewPrefab(_playerProjectilePrefab)
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