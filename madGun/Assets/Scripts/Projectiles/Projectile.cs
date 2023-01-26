using System.Collections;

using UnityEngine;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private MeshRenderer _meshRenderer;

        [SerializeField] private Collider _collider;

        [SerializeField] private TrailRenderer _trail;

        [SerializeField] private ForceMode _forceMode;

        [SerializeField] private float _activeTime;

        [SerializeField] private LayerMask _metalLayer;

        [SerializeField] private LayerMask _stoneLayer;

        [SerializeField] private LayerMask _fleshLayer;

        [SerializeField] private ParticleSystem _fleshHit;

        [SerializeField] private ParticleSystem _metalHit;

        [SerializeField] private ParticleSystem _stoneHit;

        [SerializeField] private AudioSource _fleshSound;

        [SerializeField] private AudioSource _metalSound;

        [SerializeField] private int _damage;

        [SerializeField] private bool _disableOnCollision;

        private IShootable _shootable;

        private void OnCollisionEnter(Collision collision)
        {
            if ((_fleshLayer & (1 << collision.gameObject.layer)) != 0)
            {
                _fleshHit.Play();

                _fleshSound.PlayOneShot(_fleshSound.clip);
            }
            else if ((_metalLayer & (1 << collision.gameObject.layer)) != 0)
            {
                _metalHit.Play();

                _metalSound.PlayOneShot(_metalSound.clip);
            }
            else if ((_stoneLayer & (1 << collision.gameObject.layer)) != 0)
            {
                _stoneHit.Play();
            }

            if (collision.transform.TryGetComponent(out _shootable))
            {
                _shootable.HitHandler(_damage);
            }

            if (_disableOnCollision)
            {
                _meshRenderer.enabled = false;

                _collider.enabled = false;

                _rigidbody.isKinematic = true;
            }

            _trail.gameObject.SetActive(false);

            _trail.Clear();
        }

        public void Launch(Vector3 direction, float speed)
        {
            _rigidbody.AddForce(direction * speed, _forceMode);

            StartCoroutine(Deactivate());
        }

        public void EnableTrail()
        {
            _trail.gameObject.SetActive(true);
        }

        public void ResetProjectile()
        {
            _rigidbody.velocity = Vector3.zero;

            _rigidbody.angularVelocity = Vector3.zero;

            transform.rotation = Quaternion.identity;

            _meshRenderer.enabled = true;

            _collider.enabled = true;

            _rigidbody.isKinematic = false;
        }

        public abstract void ReturnToPool();

        private IEnumerator Deactivate()
        {
            yield return new WaitForSeconds(_activeTime);

            ReturnToPool();
        }
    }
}