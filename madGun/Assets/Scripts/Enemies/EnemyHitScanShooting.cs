using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class EnemyHitScanShooting : MonoBehaviour
    {
        [SerializeField] private Vector3 _shootSpread;

        [SerializeField] private float _maxDistanse;

        [SerializeField] private ParticleSystem _muzzleFlash;

        [SerializeField] private Transform _shootPoint;

        [SerializeField] private float _shootDelay;

        [SerializeField] private LayerMask _layerMask;

        private BulletTrailPool _trailPool;

        private float _lastShootTime;

        [Inject]
        private void Construct(BulletTrailPool trailPool)
        {
            _trailPool = trailPool;
        }

        private void Update()
        {
            Shoot();
        }

        public void Shoot()
        {
            if (_lastShootTime + _shootDelay < Time.time)
            {
                _muzzleFlash.Play();

                Vector3 direction = GetDirection();

                if (Physics.Raycast(_shootPoint.position, direction, out RaycastHit raycastHit, _maxDistanse, _layerMask))
                {
                    _trailPool.ActivateTrail(_shootPoint.position, raycastHit);

                    _lastShootTime = Time.time;
                }
            }
        }

        private Vector3 GetDirection()
        {
            Vector3 direction = transform.forward;

            direction += new Vector3(
                Random.Range(-_shootSpread.x, _shootSpread.x),
                Random.Range(-_shootSpread.y, _shootSpread.y),
                Random.Range(-_shootSpread.z, _shootSpread.z)
                );

            direction.Normalize();

            return direction;
        }
    }
}