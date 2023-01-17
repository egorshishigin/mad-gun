using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    public class EnemyProjectilePool
    {
        private readonly EnemyProjectile.Pool _projectilePool;

        private readonly List<Projectile> _projectiles = new List<Projectile>();

        public EnemyProjectilePool(EnemyProjectile.Pool pool)
        {
            _projectilePool = pool;
        }

        public void AddEnemyProjectile(Vector3 startPosition, Vector3 direction, float speed)
        {
            _projectiles.Add(_projectilePool.Spawn(startPosition, direction, speed));
        }

        public void RemoveEnemyProjectile()
        {
            var projectile = _projectiles[0];

            _projectilePool.Despawn(projectile);

            _projectiles.Remove(projectile);
        }
    }
}