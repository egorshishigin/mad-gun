using System;

using Zenject;

using PlayerInput;

using Projectiles;

using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponType _type;

        [SerializeField] private Transform[] _shootPoints;

        [SerializeField] private Transform _shootPointsHolder;

        [SerializeField] private PlayerProjectile _projectile;

        [Header("Fire speed")]

        [SerializeField] private float _fireRate;

        [SerializeField] private float _speed;

        [Header("Recoil")]

        [SerializeField] private Transform _recoilRoot;

        [SerializeField] private float _xRecoil;

        [SerializeField] private float _yRecoil;

        [SerializeField] private float _recoilSpeed;

        [SerializeField] private float _returnSpeed;

        [Header("Spread")]

        [SerializeField] private float _xSpread;

        [SerializeField] private float _ySpread;

        [Header("Second gun options")]

        [SerializeField] private GameObject _secondGun;

        [SerializeField] private bool _doubleGun;

        private Vector3 _targetRotation;

        private Vector3 _currentRotation;

        private PlayerControl _playerControl;

        private ProjectilesPool _projectilesPool;

        private float _nextTimeToShoot;

        public event Action Shot = delegate { };

        public WeaponType Type => _type;

        public PlayerProjectile Projectile => _projectile;

        [Inject]
        private void Construct(PlayerControl playerControl, ProjectilesPool projectilesPool)
        {
            _playerControl = playerControl;

            _projectilesPool = projectilesPool;
        }

        private void Awake()
        {
            if (_doubleGun)
            {
                _secondGun.SetActive(true);
            }
            else return;
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

        private void Aim(Vector3 target)
        {
            RotateShootPointsHolder(target);

            WeaponRecoil();
        }

        private void WeaponRecoil()
        {
            _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnSpeed * Time.deltaTime);

            _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _recoilSpeed * Time.fixedDeltaTime);

            _recoilRoot.rotation = Quaternion.Euler(_currentRotation);
        }

        private void RotateShootPointsHolder(Vector3 target)
        {
            Vector3 lookPositon = target - _shootPointsHolder.position;

            _shootPointsHolder.transform.forward = lookPositon.normalized;
        }

        private void Shoot()
        {
            if (Time.unscaledTime >= _nextTimeToShoot)
            {
                _nextTimeToShoot = Time.unscaledTime + 1f / _fireRate;

                for (int i = 0; i < _shootPoints.Length; i++)
                {
                    ShootSpread(_shootPoints[i]);

                    _projectilesPool.AddProjectile(_shootPoints[i].position, _shootPoints[i].forward, _speed);
                }

                ShootRecoil();

                Shot.Invoke();
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

        private void ShootRecoil()
        {
            _targetRotation += new Vector3(_xRecoil, UnityEngine.Random.Range(-_yRecoil, _yRecoil), 0f);
        }

        private void ShootSpread(Transform shootPoint)
        {
            shootPoint.localEulerAngles += new Vector3(UnityEngine.Random.Range(-_xSpread, _xSpread), UnityEngine.Random.Range(-_ySpread, _ySpread), 0f);
        }
    }
}