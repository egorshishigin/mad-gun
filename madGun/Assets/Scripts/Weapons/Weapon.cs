using Zenject;

using PlayerInput;

using Projectiles;

using UnityEngine;
using UnityEngine.Pool;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;

        [SerializeField] private Transform _weaponModel;

        [SerializeField] private float _fireRate;

        [SerializeField] private float _speed;

        [SerializeField] private float _lookTime;

        [SerializeField] private ParticleSystem _particleSystem;

        private PlayerControl _playerControl;

        private ProjectilesPool _projectilesPool;

        private float _nextTimeToShoot;

        [Inject]
        private void Construct(PlayerControl playerControl, ProjectilesPool projectilesPool)
        {
            _playerControl = playerControl;

            _projectilesPool = projectilesPool;
        }

        private void OnEnable()
        {
            _playerControl.ScreenHold += Aim;
        }

        private void OnDisable()
        {
            _playerControl.ScreenHold -= Aim;
        }

        private void Aim(Vector3 target)
        {
            RotateShootPointToTarget(target);

            Shoot();
        }

        private void RotateShootPointToTarget(Vector3 target)
        {
            Vector3 lookPositon = target - _shootPoint.position;

            _shootPoint.rotation = Quaternion.LookRotation(lookPositon);

            Quaternion rotation = Quaternion.LookRotation(lookPositon);

            _weaponModel.rotation = Quaternion.Slerp(_weaponModel.rotation, rotation, Time.deltaTime * _lookTime);
        }

        private void Shoot()
        {
            if (Time.time >= _nextTimeToShoot)
            {
                _nextTimeToShoot = Time.time + 1f / _fireRate;

                _projectilesPool.AddProjectile(_shootPoint.position, _shootPoint.forward, _speed);

                _particleSystem.Play();
            }
            else return;
        }
    }
}