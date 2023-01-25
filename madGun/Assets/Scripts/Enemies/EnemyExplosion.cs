using HealthSystem;

using Weapons;

using Projectiles;

using UnityEngine;

namespace Enemies
{
    public class EnemyExplosion : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        [SerializeField] private Explosion _explosion;

        private PlayerProjectile _playerProjectile;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent<PlayerProjectile>(out _playerProjectile))
            {
                DiedExplosion();
            }
            else return;

        }

        private void DiedExplosion()
        {
            _particleSystem.Play();

            _explosion.ExplosionDamage();

            gameObject.SetActive(false);
        }
    }
}