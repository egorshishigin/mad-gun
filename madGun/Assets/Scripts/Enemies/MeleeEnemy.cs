using Player;

using UnityEngine;

namespace Enemies
{
    public class MeleeEnemy : EnemyBase
    {
        [SerializeField] private int _damage;

        [SerializeField] private Transform _attackPoint;

        [SerializeField] private float _attackRadius;

        protected override void Attack()
        {
            Collider[] colliders = Physics.OverlapSphere(_attackPoint.position, _attackRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out PlayerHitBox playerHitBox))
                {
                    playerHitBox.HitHandler(_damage);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_attackRadius <= 0 || _attackPoint == null)
                return;

            Gizmos.color = Color.green;

            Gizmos.DrawSphere(_attackPoint.position, _attackRadius);
        }
    }
}