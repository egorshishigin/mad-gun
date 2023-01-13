using System.Collections.Generic;

using UnityEngine;

namespace Enemies
{
    public class BulletTrailPool
    {
        private readonly BulletTrail.Pool _pool;

        private readonly List<BulletTrail> _bulletTrails = new List<BulletTrail>();

        public BulletTrailPool(BulletTrail.Pool pool)
        {
            _pool = pool;
        }

        public void ActivateTrail(Vector3 startPosition, RaycastHit hit)
        {
            _bulletTrails.Add(_pool.Spawn(startPosition, hit));
        }

        public void DeactivateTrail()
        {
            var trail = _bulletTrails[0];

            _pool.Despawn(trail);

            _bulletTrails.Remove(trail);
        }
    }
}