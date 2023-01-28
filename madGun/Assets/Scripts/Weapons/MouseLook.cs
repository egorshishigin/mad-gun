using System.Collections;
using System.Collections.Generic;

using Zenject;

using PlayerInput;

using UnityEngine;

namespace Weapons
{
    public class MouseLook : MonoBehaviour
    {
        private const string SettingName = "CameraSensitivity";

        [SerializeField] private Camera _gameCamera;

        [SerializeField] private float _xMouseSensitivity = 30f;

        [SerializeField] private float _yMouseSensitivity = 30f;

        private float _cameraRotationX;

        private float _cameraRotationY;

        private PlayerControl _playerControl;

        [Inject]
        private void Construct(PlayerControl playerControl)
        {
            _playerControl = playerControl;
        }

        private void OnEnable()
        {
            _playerControl.ScreenMove += RotateCamera;
        }

        private void Awake()
        {
            SetCameraSensitivity();
        }

        public void SetCameraSensitivity()
        {
            _xMouseSensitivity = PlayerPrefs.GetFloat(SettingName);

            _yMouseSensitivity = PlayerPrefs.GetFloat(SettingName);
        }

        private void RotateCamera(Vector3 direction)
        {
            _cameraRotationX -= Input.GetAxisRaw("Mouse Y") * _xMouseSensitivity * 0.02f;

            _cameraRotationY += Input.GetAxisRaw("Mouse X") * _yMouseSensitivity * 0.02f;

            _cameraRotationX = Mathf.Clamp(_cameraRotationX, -90f, 90f);

            _cameraRotationY = Mathf.Clamp(_cameraRotationY, -90f, 90f);

            _gameCamera.transform.rotation = Quaternion.Euler(_cameraRotationX, _cameraRotationY, 0f);
        }
    }
}