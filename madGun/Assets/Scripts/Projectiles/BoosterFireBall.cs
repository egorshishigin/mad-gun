using Weapons;

using UnityEngine;

namespace Projectiles
{
    public class BoosterFireBall : MonoBehaviour
    {
        [SerializeField] private Explosion _explosion;

        [SerializeField] private ParticleSystem _explosionEffect;

        [SerializeField] private float _speed;

        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private AudioSource _explosionSound;

        [SerializeField] private Collider _collider;

        [SerializeField] private GameObject _model;

        private void OnCollisionEnter(Collision collision)
        {
            _collider.enabled = false;

            _model.SetActive(false);

            _explosionEffect.Play();

            _explosion.ExplosionDamage();

            _explosionSound.PlayOneShot(_explosionSound.clip);
        }

        public void Launch(Vector3 direction)
        {
            _rigidbody.AddForce(direction * _speed, ForceMode.VelocityChange);
        }
    }
}