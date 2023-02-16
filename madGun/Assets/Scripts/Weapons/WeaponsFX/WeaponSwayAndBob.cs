using UnityEngine;

namespace Weapons
{
    public class WeaponSwayAndBob : MonoBehaviour
    {
        [Header("Smooth values")]

        [SerializeField] private float _positionSmooth = 10f;

        [SerializeField] private float _rotationSmooth = 12f;

        [Header("Sway position")]

        [SerializeField] private float _swayPositionStep = 0.01f;

        [SerializeField] private float _maxSwayPositionStepDistance = 0.06f;

        [Header("Sway rotation")]

        [SerializeField] private float _swayRotationStep = 4f;

        [SerializeField] private float _maxSwayRotationStepDistance = 5f;

        [Header("Bob position")]

        [SerializeField] private CharacterController _characterController;

        [SerializeField] private float _speedCurve;

        [SerializeField] private Vector3 _travelLimit = Vector3.one * 0.025f;

        [SerializeField] private Vector3 _bobLimit = Vector3.one * 0.01f;

        [Header("Bob rotation")]

        [SerializeField] private Vector3 _rotationMultiplier;

        private Vector3 _bobRotation;

        private Vector3 _bobPosition;

        private float _curveSin => Mathf.Sin(_speedCurve);

        private float _curveCos => Mathf.Cos(_speedCurve);

        private Vector3 _swayPosition;

        private Vector3 _swayRotation;

        private Vector3 _lookInput;

        private Vector3 _walkInput;

        private void Update()
        {
            GetLookPosition();

            GetWalkInput();

            SwayPosition();

            SwayRotation();

            BobPosition();

            BobRotation();

            CompositePositionRotation();
        }

        private void GetLookPosition()
        {
            _lookInput.x = Input.GetAxis("Mouse X");

            _lookInput.y = Input.GetAxis("Mouse Y");
        }

        private void GetWalkInput()
        {
            _walkInput.x = Input.GetAxis("Horizontal");

            _walkInput.y = Input.GetAxis("Vertical");
        }

        private void BobPosition()
        {
            _speedCurve += (Time.deltaTime * (_characterController.isGrounded ? _characterController.velocity.magnitude : 1f)) + 0.01f;

            _bobPosition.x = (_curveCos * _bobLimit.x * (_characterController.isGrounded ? 1 : 0)) - _walkInput.x * _travelLimit.x;

            _bobPosition.y = (_curveSin * _bobLimit.y) - (_characterController.velocity.y * _travelLimit.y);

            _bobPosition.z = -(_walkInput.y * _travelLimit.z);
        }

        private void BobRotation()
        {
            _bobRotation.x = (_walkInput != Vector3.zero ? _rotationMultiplier.x * Mathf.Sin(2 * _speedCurve) : _rotationMultiplier.x * Mathf.Sin(2 * _speedCurve) / 2);

            _bobRotation.y = (_walkInput != Vector3.zero ? _rotationMultiplier.y * _curveCos : 0);

            _bobRotation.z = (_walkInput != Vector3.zero ? _rotationMultiplier.z * _curveCos * _walkInput.x : 0);
        }

        private void SwayPosition()
        {
            Vector3 invertLook = _lookInput * -_swayPositionStep;

            invertLook.x = Mathf.Clamp(invertLook.x, -_maxSwayPositionStepDistance, _maxSwayPositionStepDistance);

            invertLook.y = Mathf.Clamp(invertLook.y, -_maxSwayPositionStepDistance, _maxSwayPositionStepDistance);

            _swayPosition = invertLook;
        }

        private void SwayRotation()
        {
            Vector2 invertLook = _lookInput * -_swayRotationStep;

            invertLook.x = Mathf.Clamp(invertLook.x, -_maxSwayRotationStepDistance, _maxSwayRotationStepDistance);

            invertLook.y = Mathf.Clamp(invertLook.y, -_maxSwayRotationStepDistance, _maxSwayRotationStepDistance);

            _swayRotation = new Vector3(invertLook.y, invertLook.x, invertLook.x);
        }

        private void CompositePositionRotation()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _swayPosition + _bobPosition, Time.deltaTime * _positionSmooth);

            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(_swayRotation) * Quaternion.Euler(_bobRotation), Time.deltaTime * _rotationSmooth);
        }
    }
}