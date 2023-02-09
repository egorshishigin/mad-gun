using System.Collections.Generic;

using UnityEngine;

namespace Player
{
    public class PlayerJumpExplosionPool
    {
        private readonly PlayerJumpExplosion.Pool _pool;

        private readonly List<PlayerJumpExplosion> _explosions = new List<PlayerJumpExplosion>();

        public PlayerJumpExplosionPool(PlayerJumpExplosion.Pool pool)
        {
            _pool = pool;
        }

        public void AddExplosion(Vector3 startPosition)
        {
            _explosions.Add(_pool.Spawn(startPosition));
        }

        public void RemoveExplosion()
        {
            var explosion = _explosions[0];

            _pool.Despawn(explosion);

            _explosions.Remove(explosion);
        }
    }
}