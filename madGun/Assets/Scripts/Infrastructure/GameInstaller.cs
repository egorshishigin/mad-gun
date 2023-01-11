using Zenject;

using PlayerInput;

using Projectiles;

using UnityEngine;

namespace Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerControl _playerControl;

        [SerializeField] private Projectile _playerProjectilePrefab;

        [SerializeField] private Transform _playerProjectilesHolder;

        public override void InstallBindings()
        {
            BindPlayerInput();

            BindPlayerProjectilePool();
        }

        private void BindPlayerProjectilePool()
        {
            Container
                .Bind<ProjectilesPool>()
                .AsSingle();

            Container
                .BindMemoryPool<Projectile, Projectile.Pool>()
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