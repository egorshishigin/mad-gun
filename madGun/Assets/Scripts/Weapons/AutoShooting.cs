using System.Collections;
using Zenject;
using UnityEngine;

namespace Weapons
{
    public class AutoShooting : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        [SerializeField] private float _startDelay;

        [SerializeField] private float _stopDelay;

        [SerializeField] private LayerMask _shootLayer;

        private IShootable _shootTarget;

        private MobileWeaponSwitcher _weaponSwitcher;

        private bool _canShoot;

        [Inject]
        private void Construct(MobileWeaponSwitcher weaponSwitcher)
        {
            _weaponSwitcher = weaponSwitcher;
        }

        private void OnEnable()
        {
            _weaponSwitcher.WeaponSwitched += WeaponSwitchedHandler;
        }

        private void Start()
        {
            WeaponSwitchedHandler();
        }

        private void FixedUpdate()
        {
            RaycastHit raycastHit;

            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out raycastHit, _shootLayer))
            {
                if (raycastHit.transform.TryGetComponent(out _shootTarget))
                {
                    _canShoot = true;

                    StartCoroutine(ShootDelay());
                }
                else
                {
                    StartCoroutine(StopShoot());
                }
            }
        }

        private void OnDisable()
        {
            _weaponSwitcher.WeaponSwitched -= WeaponSwitchedHandler;
        }

        private void WeaponSwitchedHandler()
        {
            StopAllCoroutines();

            SetDelay();
        }

        private void SetDelay()
        {
            float startDelay = _weaponSwitcher.CurrentWeapon.FireRate / (_weaponSwitcher.CurrentWeapon.FireRate * 2);

            _startDelay = startDelay;

            float stopDelay = _weaponSwitcher.CurrentWeapon.FireRate / 4;

            _stopDelay = stopDelay;
        }

        private IEnumerator ShootDelay()
        {
            yield return new WaitForSeconds(_startDelay);

            StartCoroutine(StartShoot());
        }

        private IEnumerator StartShoot()
        {
            while (_canShoot)
            {
                _weaponSwitcher.CurrentWeapon.Shoot();

                if (_weaponSwitcher.CurrentWeapon.DoubleGun)
                {
                    _weaponSwitcher.CurrentWeapon.SecondGun.Shoot();
                }

                yield return null;
            }
        }

        private IEnumerator StopShoot()
        {
            yield return new WaitForSeconds(_stopDelay);

            _canShoot = false;
        }
    }
}