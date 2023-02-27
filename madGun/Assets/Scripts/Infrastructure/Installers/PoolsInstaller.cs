using Zenject;

using Boosters;

using Projectiles;

using UnityEngine;

public class PoolsInstaller : MonoInstaller
{
    [SerializeField] private EnemyProjectile _enemyProjectilePrefab;

    [SerializeField] private Transform _enemyProjectilesHolder;

    [SerializeField] private Transform _boostersHolder;

    [SerializeField] private BoostersHolder _boostersHolderPrefab;

    public override void InstallBindings()
    {
        BindBoostersPool();

        BindEnemyProjectilePool();
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
}