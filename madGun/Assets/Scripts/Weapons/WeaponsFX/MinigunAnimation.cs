using Zenject;

using DG.Tweening;

using PlayerInput;

using UnityEngine;

namespace Weapons
{
    public class MinigunAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration;

        [SerializeField] private Vector3 _rotation;

        [SerializeField] private RotateMode _rotateMode;

        private PlayerControl _playerControl;

        private Tween _tween;

        [Inject]
        private void Construct(PlayerControl playerControl)
        {
            _playerControl = playerControl;
        }

        private void OnEnable()
        {
            _playerControl.InputActions.Player.Shoot.Enable();

            _playerControl.InputActions.Player.Shoot.performed += _ => PlayAnimation();

            _playerControl.InputActions.Player.Shoot.canceled += _ => StopAnimation();
        }

        private void OnDisable()
        {
            _playerControl.InputActions.Player.Shoot.performed -= _ => PlayAnimation();

            _playerControl.InputActions.Player.Shoot.canceled -= _ => StopAnimation();

            StopAnimation();
        }

        private void PlayAnimation()
        {
           _tween = transform.DOLocalRotate(_rotation,_duration, _rotateMode).SetLoops(-1, LoopType.Incremental);
        }

        private void StopAnimation()
        {
            _tween.Kill();
        }
    }
}