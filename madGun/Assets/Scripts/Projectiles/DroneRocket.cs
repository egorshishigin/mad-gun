using Weapons;

using UnityEngine;

namespace Projectiles
{
    public class DroneRocket : MonoBehaviour
    {
        [SerializeField] private GameObject _rocketModel;

        [SerializeField] private Explosion _explosion;

        [SerializeField] private ParticleSystem _explosionEffect;

        [SerializeField] private float _speed;

        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private AudioSource _explosionSound;

        private void OnCollisionEnter(Collision collision)
        {
            _explosionEffect.Play();

            _explosion.ExplosionDamage();

            _rocketModel.SetActive(false);

            _explosionSound.PlayOneShot(_explosionSound.clip);
        }

        public void Launch(Vector3 direction)
        {
            _rigidbody.AddForce(direction * _speed, ForceMode.VelocityChange);
        }
    }
}