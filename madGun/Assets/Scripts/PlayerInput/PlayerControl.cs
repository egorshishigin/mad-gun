using System;

using UnityEngine;

namespace PlayerInput
{
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] private Camera _gameCamera;

        [SerializeField] private float _inputRange;

        public event Action<Vector3> ScreenMove = delegate { };

        public event Action ScreenHold = delegate { };

        public event Action ScreenDown = delegate { };

        public event Action ScreenUp = delegate { };

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

            ScreenMove.Invoke(GetClickedPoint());
        }

        private Vector3 GetClickedPoint()
        {
            RaycastHit raycastHit;

            Ray ray = _gameCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _gameCamera.farClipPlane));

            Vector3 target;

            if (Physics.Raycast(ray, out raycastHit))
            {
                target = raycastHit.point;
            }
            else
            {
                target = ray.origin + ray.direction * _inputRange;
            }

            return target;
        }
    }
}