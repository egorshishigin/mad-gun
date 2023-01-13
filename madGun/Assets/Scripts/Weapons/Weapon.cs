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

        [SerializeField] private Transform _weaponModel;

        [SerializeField] private float _fireRate;

        [SerializeField] private float _speed;

        [SerializeField] private float _lookTime;

        [SerializeField] private ParticleSystem _particleSystem;

        [SerializeField] private GameObject _secondGun;

        [SerializeField] private bool _doubleGun;

        [SerializeField] private AnimationCurve _xRecoil;

        [SerializeField] private AnimationCurve _yRecoil;

        [SerializeField] private float _xSpread;

        [SerializeField] private float _ySpread;

        [SerializeField] private AudioSource _shootAudio;

        private PlayerControl _playerControl;

        private ProjectilesPool _projectilesPool;

        private float _nextTimeToShoot;

        private float _fireTime;

        private bool _shooting;

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
            RotateShootPointsToTarget(target);
        }

        private void RotateShootPointsToTarget(Vector3 target)
        {
            for (int i = 0; i < _shootPoints.Length; i++)
            {
               // Vector3 lookPositon = target - _shootPoints[i].position;

                //_shootPoints[i].rotation = Quaternion.LookRotation(lookPositon);

                if (_shooting)
                {
                    ShootSpread(_shootPoints[i]);
                }
            }

            Quaternion rotation = Quaternion.LookRotation(target - _weaponModel.position);

            _weaponModel.rotation = Quaternion.Slerp(_weaponModel.rotation, rotation, Time.deltaTime * _lookTime);
        }

        private void Shoot()
        {
            if (Time.time >= _nextTimeToShoot)
            {
                _shooting = true;

                _nextTimeToShoot = Time.time + 1f / _fireRate;

                _fireTime += Time.time * Time.deltaTime;

                for (int i = 0; i < _shootPoints.Length; i++)
                {
                    _projectilesPool.AddProjectile(_shootPoints[i].position, _shootPoints[i].forward, _speed);

                    ShootSpread(_shootPoints[i]);
                }

                _particleSystem.Play();

                _shootAudio.PlayOneShot(_shootAudio.clip);

                ShootRecoilAnimation();
            }
            else return;
        }

        private void ResetShootTime()
        {
            _shooting = false;

            _fireTime = 0;

            ResetShootPoints();
        }

        private void ResetShootPoints()
        {
            for (int i = 0; i < _shootPoints.Length; i++)
            {
                _shootPoints[i].localEulerAngles = Vector3.zero;
            }
        }

        private void ShootRecoilAnimation()
        {
            transform.localEulerAngles += new Vector3(_xRecoil.Evaluate(_fireTime), _yRecoil.Evaluate(_fireTime), 0f);
        }

        private void ShootSpread(Transform shootPoint)
        {
            shootPoint.localEulerAngles += new Vector3(Random.Range(-_xSpread, _xSpread), Random.Range(-_ySpread, _ySpread), 0f);
        }
    }
}