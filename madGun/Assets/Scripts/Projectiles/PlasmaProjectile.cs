using Weapons;

using UnityEngine;

namespace Projectiles
{
    public class PlasmaProjectile : MonoBehaviour
    {
        [SerializeField] private Explosion _explosion;

        [SerializeField] private ParticleSystem _effect;

        [SerializeField] private AudioSource _audio;

        private bool _explode;

        private void OnDisable()
        {
            _explode = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!_explode)
            {
                _explosion.ExplosionDamage();

                _effect.Play();

                _audio.PlayOneShot(_audio.clip);

                _explode = true;
            }
        }
    }
}