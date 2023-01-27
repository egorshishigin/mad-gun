using System;

using Zenject;

using GamePause;

using UnityEngine;

namespace PlayerInput
{
    public class PlayerControl : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private Camera _gameCamera;

        [SerializeField] private float _inputRange;

        [SerializeField] private LayerMask _inputLayer;

        private Pause _pause;

        public event Action<Vector3> ScreenMove = delegate { };

        public event Action ScreenHold = delegate { };

        public event Action ScreenDown = delegate { };

        public event Action ScreenUp = delegate { };

        public event Action PauseButtonDown = delegate { };

        [Inject]
        private void Construct(Pause pause)
        {
            _pause = pause;

            _pause.Register(this);
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                ScreenHold.Invoke();
            }

            if (Input.GetMouseButtonDown(0))
            {
                ScreenDown.Invoke();
            }

            if (Input.GetMouseButtonUp(0))
            {
                ScreenUp.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                PauseButtonDown.Invoke();

                Debug.Log("Pause key pressed");
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
    }
}