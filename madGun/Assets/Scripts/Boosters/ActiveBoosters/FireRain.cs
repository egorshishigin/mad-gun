using System.Collections;
using System.Collections.Generic;

using Zenject;

using Projectiles;

using UnityEngine;

namespace Boosters
{
    public class FireRain : ActiveBoosterBase
    {
        [SerializeField] private BoosterFireBall _rocketPrefab;

        [SerializeField] private float _fireRate;

        [SerializeField] private Transform[] _shootPoints;

        private ActiveBoostersState _boostersState;

        private float _nextTimeToShoot;

        [Inject]
        private void Construct(ActiveBoostersState activeBoostersState)
        {
            _boostersState = activeBoostersState;
        }

        protected override void OnActivated()
        {
            _boostersState.FireRain = true;
        }

        protected override void OnDectivated()
        {
            _boostersState.FireRain = false;
        }

        private void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            if (Time.time >= _nextTimeToShoot)
            {
                _nextTimeToShoot = Time.time + 1f / _fireRate;

                int randomPointIdex = Random.Range(0, _shootPoints.Length);

                BoosterFireBall rocket = Instantiate(_rocketPrefab, _shootPoints[randomPointIdex]);

                rocket.Launch(_shootPoints[randomPointIdex].forward);
            }
        }
    }
}