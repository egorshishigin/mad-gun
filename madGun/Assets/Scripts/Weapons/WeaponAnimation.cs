using Zenject;

using PlayerInput;

using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(Animator))]
    public class WeaponAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private Weapon _weapon;

        private const string AnimationTriggerName = "Shot";

        private void OnEnable()
        {
            _weapon.Shot += PlayShotAnimation;
        }

        private void OnDisable()
        {
            _weapon.Shot -= PlayShotAnimation;
        }

        private void PlayShotAnimation()
        {
            _animator.SetTrigger(AnimationTriggerName);
        }
    }
}