using System;

using UnityEngine;

namespace PlayerInput
{
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] private Camera _gameCamera;

        [SerializeField] private float _inputRange;

        public event Action<Vector3> ScreenHold = delegate { };

        private void Update()
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            {
                ScreenHold.Invoke(GetClickedPoint());
            }
            else return;
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