using System.Collections;
using System.Collections.Generic;

using Zenject;

using Projectiles;

using UnityEngine;

namespace Boosters
{
    public class Drone : ActiveBoosterBase
    {
        [SerializeField] private DroneRocket _rocketPrefab;

        [SerializeField] private float _moveSpeed;

        [SerializeField] private float _fireRate;

        [SerializeField] private Transform _shootPoint;

        [SerializeField] private Transform _startPoint;

        private ActiveBoostersState _boostersState;

        private float _nextTimeToShoot;

        [Inject]
        private void Construct(ActiveBoostersState activeBoostersState)
        {
            _boostersState = activeBoostersState;
        }

        protected override void OnActivated()
        {
            _boostersState.Drone = true;
        }

        protected override void OnDectivated()
        {
            _boostersState.Drone = false;

            transform.position = _startPoint.position;
        }

        private void Update()
        {
            Move();

            Shoot();
        }

        private void Shoot()
        {
            if (Time.time >= _nextTimeToShoot)
            {
                _nextTimeToShoot = Time.time + 1f / _fireRate;

                DroneRocket rocket = Instantiate(_rocketPrefab, _shootPoint);

                rocket.Launch(_shootPoint.forward);
            }
        }

        private void Move()
        {
            transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        }
    }
}