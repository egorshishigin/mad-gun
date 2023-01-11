using System.Collections.Generic;

using UnityEngine;

namespace Projectiles
{
    public class ProjectilesPool
    {
        private readonly Projectile.Pool _projectilePool;

        private readonly List<Projectile> _projectiles = new List<Projectile>();

        public ProjectilesPool(Projectile.Pool pool)
        {
            _projectilePool = pool;
        }

        public void AddProjectile(Vector3 startPosition, Vector3 direction, float speed)
        {
            _projectiles.Add(_projectilePool.Spawn(startPosition, direction, speed));
        }

        public void RemoveProjectile()
        {
            var projectile = _projectiles[0];

            _projectilePool.Despawn(projectile);

            _projectiles.Remove(projectile);
        }
    }
}