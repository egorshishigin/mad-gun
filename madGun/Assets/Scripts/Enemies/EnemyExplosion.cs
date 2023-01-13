using HealthSystem;

using Weapons;

using UnityEngine;

namespace Enemies
{
    public class EnemyExplosion : MonoBehaviour
    {
        [SerializeField] private Health _health;

        [SerializeField] private ParticleSystem _particleSystem;

        [SerializeField] private Explosion _explosion;

        private void OnEnable()
        {
            _health.Died += DiedExplosion;
        }

        private void OnDisable()
        {
            _health.Died -= DiedExplosion;
        }

        private void DiedExplosion()
        {
            _health.gameObject.SetActive(false);

            _particleSystem.Play();

            _explosion.ExplosionDamage();
        }
    }
}