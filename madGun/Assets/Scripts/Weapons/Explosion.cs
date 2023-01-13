using UnityEngine;

namespace Weapons
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private int _damage;

        [SerializeField] private float _radius;

        private IShootable _shootable;

        public void ExplosionDamage()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out _shootable))
                {
                    _shootable.HitHandler(_damage);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_radius <= 0)
                return;

            Gizmos.color = Color.yellow;

            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}