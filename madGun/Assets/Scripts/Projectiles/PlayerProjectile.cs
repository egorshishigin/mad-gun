using Zenject;

using UnityEngine;
using UnityEngine.Pool;

namespace Projectiles
{
    public class PlayerProjectile : Projectile
    {
        private ObjectPool<PlayerProjectile> _pool;

        public void SetPool(ObjectPool<PlayerProjectile> pool)
        {
            _pool = pool;
        }

        public override void ReturnToPool()
        {
            _pool.Release(this);
        }
    }
}