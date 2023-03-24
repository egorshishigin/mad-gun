using UnityEngine;

using Zenject;

namespace Projectiles
{
    public class EnemyProjectile : Projectile
    {
        private EnemyProjectilePool _projectilesPool;

        [Inject]
        private void Construct(EnemyProjectilePool projectilesPool)
        {
            _projectilesPool = projectilesPool;
        }

        public override void ReturnToPool()
        {
            _projectilesPool.RemoveEnemyProjectile();
        }

        public class Pool : MonoMemoryPool<Vector3, Vector3, float, Projectile>
        {
            protected override void OnCreated(Projectile item)
            {
                item.gameObject.SetActive(false);
            }

            protected override void Reinitialize(Vector3 startPosition, Vector3 direction, float speed, Projectile item)
            {
                item.transform.position = startPosition;

                item.gameObject.SetActive(true);

                item.EnableTrail();

                item.Launch(direction, speed);
            }

            protected override void OnDespawned(Projectile item)
            {
                item.gameObject.SetActive(false);

                item.ResetProjectile();
            }
        }
    }
}