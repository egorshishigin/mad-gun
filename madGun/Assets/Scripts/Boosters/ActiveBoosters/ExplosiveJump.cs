using Zenject;

using Player;

using UnityEngine;

namespace Boosters
{
    public class ExplosiveJump : ActiveBoosterBase
    {
        [SerializeField] private PlayerMovement _playerMovement;

        [SerializeField] private float _jumpSpeed;

        [SerializeField] private float _forwardSpeed;

        private PlayerJumpExplosionPool _explosionPool;

        [Inject]
        private void Construct(PlayerJumpExplosionPool explosionPool)
        {
            _explosionPool = explosionPool;
        }

        protected override void OnActivated()
        {
            _playerMovement.FireJump(_jumpSpeed, _forwardSpeed);

            _explosionPool.AddExplosion(transform.position);
        }

        protected override void OnDectivated()
        {
            _explosionPool.RemoveExplosion();
        }
    }
}