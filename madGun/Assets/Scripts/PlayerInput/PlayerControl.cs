using System;

using Zenject;

using GamePause;

using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInput
{
    public class PlayerControl : MonoBehaviour, IPauseHandler, IUpdatable
    {
        [SerializeField] private Camera _gameCamera;

        [SerializeField] private float _inputRange;

        [SerializeField] private LayerMask _inputLayer;

        private Pause _pause;

        private PlayerInputActions _inputActions;

        private UpdatesContainer _updatesContainer;

        public PlayerInputActions InputActions => _inputActions;

        public event Action<Vector3> ScreenMove = delegate { };

        public event Action ShootButtonHold = delegate { };

        public event Action ShootButtonDown = delegate { };

        public event Action ShootButtonUp = delegate { };

        public event Action PauseButtonDown = delegate { };

        [Inject]
        private void Construct(Pause pause, UpdatesContainer updatesContainer)
        {
            _inputActions = new PlayerInputActions();

            _inputActions.Enable();

            _pause = pause;

            _updatesContainer = updatesContainer;

            _pause.Register(this);

            _updatesContainer.Register(this);
        }

        void IUpdatable.Run()
        {
            if (_inputActions.Player.Shoot.phase == InputActionPhase.Started || _inputActions.Player.Shoot.phase == InputActionPhase.Performed)
            {
                ShootButtonHold.Invoke();
            }

            if (_inputActions.Player.Shoot.phase == InputActionPhase.Performed)
            {
                ShootButtonDown.Invoke();
            }

            if (_inputActions.Player.Shoot.phase == InputActionPhase.Canceled)
            {
                ShootButtonUp.Invoke();
            }

            if (_inputActions.Player.Pause.phase == InputActionPhase.Performed)
            {
                PauseButtonDown.Invoke();
            }

            ScreenMove.Invoke(GetClickedPoint());
        }

        private Vector3 GetClickedPoint()
        {
            RaycastHit raycastHit;

            Ray ray = _gameCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            Vector3 target;

            if (Physics.Raycast(ray, out raycastHit, _inputLayer))
            {
                target = raycastHit.point;
            }
            else
            {
                target = ray.GetPoint(_inputRange);
            }

            return target;
        }

        public void SetPause(bool paused)
        {
            enabled = paused ? false : true;
        }

        private void OnDestroy()
        {
            _pause.UnRegister(this);

            _updatesContainer.UnRegister(this);
        }
    }
}