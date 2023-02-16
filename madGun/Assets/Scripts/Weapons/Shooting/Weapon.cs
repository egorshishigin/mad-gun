using System;

using Zenject;

using PlayerInput;

using Projectiles;

using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int _id;

        [SerializeField] private WeaponType _type;

        [SerializeField] private Transform[] _shootPoints;

        [SerializeField] private Transform _shootPointsHolder;

        [SerializeField] private ProjectilesPool _projectilesPool;

        [Header("Fire settings")]

        [SerializeField] private int _damage;

        [SerializeField] private float _fireRate;

        [SerializeField] private float _speed;

        [Header("Spread")]

        [SerializeField] private float _xSpread;

        [SerializeField] private float _ySpread;

        [Header("Second gun options")]

        [SerializeField] private GameObject _secondGun;

        [SerializeField] private bool _doubleGun;

        private PlayerControl _playerControl;

        private float _nextTimeToShoot;

        private Ammo _ammo;

        private Vector3 _shootDirection;

        public event Action Shot = delegate { };

        public WeaponType Type => _type;

        public int ID => _id;

        [Inject]
        private void Construct(PlayerControl playerControl, Ammo ammo)
        {
            _playerControl = playerControl;

            _ammo = ammo;
        }

        private void OnEnable()
        {
            switch (_type)
            {
                case WeaponType.SINGLE:
                    _playerControl.ScreenDown += Shoot;
                    break;
                case WeaponType.AUTO:
                    _playerControl.ScreenHold += Shoot;
                    break;
            }

            _playerControl.ScreenUp += ResetShootTime;

            _playerControl.ScreenMove += Aim;
        }

        private void OnDisable()
        {
            switch (_type)
            {
                case WeaponType.SINGLE:
                    _playerControl.ScreenDown -= Shoot;
                    break;
                case WeaponType.AUTO:
                    _playerControl.ScreenHold -= Shoot;
                    break;
            }

            _playerControl.ScreenUp -= ResetShootTime;

            _playerControl.ScreenMove -= Aim;
        }

        public void SetDoubleGunState(bool value)
        {
            _secondGun.SetActive(value);
        }

        private void Aim(Vector3 target)
        {
            RotateShootPointsHolder(target);
        }

        private void RotateShootPointsHolder(Vector3 target)
        {
            Vector3 lookPositon = target - _shootPointsHolder.position;

            _shootDirection = lookPositon;
        }

        private void Shoot()
        {
            if (_ammo.AmmoSupply[_id] <= 0)
                return;

            if (Time.unscaledTime >= _nextTimeToShoot)
            {
                _nextTimeToShoot = Time.unscaledTime + 1f / _fireRate;

                for (int i = 0; i < _shootPoints.Length; i++)
                {
                    ShootSpread(_shootPoints[i]);

                    PlayerProjectile projectile = _projectilesPool.Pool.Get();

                    projectile.SetDamage(_damage);

                    projectile.transform.position = _shootPoints[i].position;

                    projectile.transform.forward = _shootDirection.normalized;

                    projectile.EnableTrail();

                    Vector3 spred = new Vector3(UnityEngine.Random.Range(-_xSpread, _xSpread), UnityEngine.Random.Range(-_ySpread, _ySpread), 0f);

                    Vector3 directionWithSpread = _shootDirection + spred;

                    projectile.Launch(directionWithSpread.normalized, _speed);
                }

                Shot.Invoke();

                _ammo.SpendAmmo(_id);
            }
            else return;
        }

        private void ResetShootTime()
        {
            ResetShootPoints();
        }

        private void ResetShootPoints()
        {
            for (int i = 0; i < _shootPoints.Length; i++)
            {
                _shootPoints[i].localEulerAngles = Vector3.zero;
            }
        }

        private void ShootSpread(Transform shootPoint)
        {
            shootPoint.localEulerAngles += new Vector3(UnityEngine.Random.Range(-_xSpread, _xSpread), UnityEngine.Random.Range(-_ySpread, _ySpread), 0f);
        }
    }
}