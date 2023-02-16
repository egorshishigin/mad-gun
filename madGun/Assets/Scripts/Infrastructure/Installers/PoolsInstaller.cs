using Zenject;

using Boosters;

using Player;

using Projectiles;

using UnityEngine;

public class PoolsInstaller : MonoInstaller
{
    [SerializeField] private EnemyProjectile _enemyProjectilePrefab;

    [SerializeField] private Transform _enemyProjectilesHolder;

    [SerializeField] private Transform _boostersHolder;

    [SerializeField] private BoostersHolder _boostersHolderPrefab;

    [SerializeField] private PlayerJumpExplosion _jumpExplosionPrefab;

    [SerializeField] private Transform _playerJumpExplosionPool;

    public override void InstallBindings()
    {
        BindBoostersPool();

        BindEnemyProjectilePool();

        BindPlayerJumpExplosionPool();
    }

    private void BindBoostersPool()
    {
        Container
            .Bind<BoostersPool>()
            .AsSingle();

        Container
           .BindMemoryPool<BoostersHolder, BoostersHolder.Pool>()
           .WithInitialSize(25)
           .FromComponentInNewPrefab(_boostersHolderPrefab)
           .UnderTransform(_boostersHolder);
    }

    private void BindEnemyProjectilePool()
    {
        Container
            .Bind<EnemyProjectilePool>()
            .AsSingle();

        Container
            .BindMemoryPool<EnemyProjectile, EnemyProjectile.Pool>()
            .WithInitialSize(10)
            .FromComponentInNewPrefab(_enemyProjectilePrefab)
            .UnderTransform(_enemyProjectilesHolder);
    }

    private void BindPlayerJumpExplosionPool()
    {
        Container
            .Bind<PlayerJumpExplosionPool>()
            .AsSingle();

        Container
            .BindMemoryPool<PlayerJumpExplosion, PlayerJumpExplosion.Pool>()
            .WithInitialSize(3)
            .FromComponentInNewPrefab(_jumpExplosionPrefab)
            .UnderTransform(_playerJumpExplosionPool);
    }
}