using System.Collections;

using Zenject;

using UnityEngine;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private MeshRenderer _meshRenderer;

        [SerializeField] private ForceMode _forceMode;

        [SerializeField] private float _activeTime;

        [SerializeField] private LayerMask _metalLayer;

        [SerializeField] private LayerMask _stoneLayer;

        [SerializeField] private LayerMask _fleshLayer;

        [SerializeField] private ParticleSystem _fleshHit;

        [SerializeField] private ParticleSystem _metalHit;

        [SerializeField] private ParticleSystem _stoneHit;

        [SerializeField] private int _damage;

        private ProjectilesPool _projectilesPool;

        private IShootable _shootable;

        [Inject]
        private void Construct(ProjectilesPool projectilesPool)
        {
            _projectilesPool = projectilesPool;
        }

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
        }

        private void Launch(Vector3 direction, float speed)
        {
            _rigidbody.AddForce(direction * speed, _forceMode);

            StartCoroutine(Deactivate());
        }

        private void ResetProjectile()
        {
            _rigidbody.velocity = Vector3.zero;

            _rigidbody.angularVelocity = Vector3.zero;

            transform.rotation = Quaternion.identity;

            _meshRenderer.enabled = true;
        }

        private void ReturnToPool()
        {
            _projectilesPool.RemoveProjectile();
        }

        private IEnumerator Deactivate()
        {
            yield return new WaitForSeconds(_activeTime);

            ReturnToPool();
        }

        public class Pool : MonoMemoryPool<Vector3, Vector3, float, Projectile>
        {
            protected override void OnCreated(Projectile item)
            {
                item.gameObject.SetActive(false);
            }

            protected override void Reinitialize(Vector3 startPosition, Vector3 direction, float speed, Projectile item)
            {
                item.transform.position = startPosition;

                item.gameObject.SetActive(true);

                item.Launch(direction, speed);
            }

            protected override void OnDespawned(Projectile item)
            {
                item.gameObject.SetActive(false);

                item.ResetProjectile();
            }
        }
    }
}