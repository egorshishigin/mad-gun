using Zenject;

using PlayerInput;

using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(Animator))]
    public class WeaponAnimation : MonoBehaviour
    {
        private const string AnimationName = "Shot";

        [SerializeField] private Animator _animator;

        [SerializeField] private Weapon _weapon;

        private PlayerControl _playerControl;

        [Inject]
        private void Consruct(PlayerControl playerControl)
        {
            _playerControl = playerControl;
        }

        private void OnEnable()
        {
            switch (_weapon.Type)
            {
                case WeaponType.SINGLE:
                    _weapon.Shot += PlaySinglrShotAnimation;
                    break;
                case WeaponType.AUTO:
                    _weapon.Shot += PlayAutoShotAnimation;

                    _playerControl.ScreenUp += StopAutoShotAnimation;
                    break;
            }
        }

        private void OnDisable()
        {
            switch (_weapon.Type)
            {
                case WeaponType.SINGLE:
                    _weapon.Shot -= PlaySinglrShotAnimation;
                    break;
                case WeaponType.AUTO:
                    _weapon.Shot -= PlayAutoShotAnimation;

                    _playerControl.ScreenUp -= StopAutoShotAnimation;
                    break;
            }
        }

        private void PlaySinglrShotAnimation()
        {
            _animator.SetTrigger(AnimationName);
        }

        private void PlayAutoShotAnimation()
        {
            _animator.SetBool(AnimationName, true);
        }

        private void StopAutoShotAnimation()
        {
            _animator.SetBool(AnimationName, false);
        }
    }
}