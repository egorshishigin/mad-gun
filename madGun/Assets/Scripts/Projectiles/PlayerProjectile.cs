using Zenject;

using UnityEngine;

namespace Projectiles
{
    public class PlayerProjectile : Projectile
    {
        private ProjectilesPool _projectilesPool;

        [Inject]
        private void Construct(ProjectilesPool projectilesPool)
        {
            _projectilesPool = projectilesPool;
        }

        public override void ReturnToPool()
        {
            _projectilesPool.RemoveProjectile();
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