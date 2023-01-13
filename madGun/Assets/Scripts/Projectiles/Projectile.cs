using System.Collections;

using UnityEngine;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private MeshRenderer _meshRenderer;

        [SerializeField] private Collider _collider;

        [SerializeField] private GameObject _trail;

        [SerializeField] private ForceMode _forceMode;

        [SerializeField] private float _activeTime;

        [SerializeField] private LayerMask _metalLayer;

        [SerializeField] private LayerMask _stoneLayer;

        [SerializeField] private LayerMask _fleshLayer;

        [SerializeField] private ParticleSystem _fleshHit;

        [SerializeField] private ParticleSystem _metalHit;

        [SerializeField] private ParticleSystem _stoneHit;

        [SerializeField] private int _damage;

        private IShootable _shootable;

        private void OnCollisionEnter(Collision collision)
        {
            if ((_fleshLayer & (1 << collision.gameObject.layer)) != 0)
            {
                _fleshHit.Play();
            }
            else if ((_metalLayer & (1 << collision.gameObject.layer)) != 0)
            {
                _metalHit.Play();
            }
            else if ((_stoneLayer & (1 << collision.gameObject.layer)) != 0)
            {
                _stoneHit.Play();
            }

            if (collision.transform.TryGetComponent(out _shootable))
            {
                _shootable.HitHandler(_damage);
            }

            _meshRenderer.enabled = false;

            _collider.enabled = false;

            _trail.SetActive(false);

            _rigidbody.isKinematic = true;
        }

        public void Launch(Vector3 direction, float speed)
        {
            _rigidbody.AddForce(direction * speed, _forceMode);

            StartCoroutine(Deactivate());
        }

        public void EnableTrail()
        {
            _trail.SetActive(true);
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