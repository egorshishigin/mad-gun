using System.Collections;
using System.Collections.Generic;

using Zenject;

using PlayerInput;

using GamePause;

using UnityEngine;

struct Cmd
{
    public float forwardMove;
    public float rightMove;
    public float upMove;
}

public class PlayerMovement : MonoBehaviour, IPauseHandler
{
    private const string SettingName = "CameraSensitivity";

    [Header("Cemera")]

    [SerializeField] private Transform _playerView;

    [SerializeField] private float _playerViewYOffset = 0.6f;

    [SerializeField] private float _xMouseSensitivity = 30.0f;

    [SerializeField] private float _yMouseSensitivity = 30.0f;

    [Header("Physics")]

    [SerializeField] private float _gravity = 20.0f;

    [SerializeField] private float _groundFriction = 6;

    [Header("Movement")]

    [SerializeField] private float _moveSpeed = 7.0f;

    [SerializeField] private float _runAcceleration = 14.0f;

    [SerializeField] private float _runDeacceleration = 10.0f;

    [SerializeField] private float _airAcceleration = 2.0f;

    [SerializeField] private float _airDecceleration = 2.0f;

    [SerializeField] private float _airControl = 0.3f;

    [SerializeField] float _sideStrafeAcceleration = 50.0f;

    [SerializeField] private float _sideStrafeSpeed = 1.0f;

    [SerializeField] private float _jumpSpeed = 8.0f;

    [SerializeField] private bool _holdJumpToBhop = false;

    [SerializeField] private AudioSource _jumpSound;

    private CharacterController _controller;

    private float _cameraXRotation = 0.0f;

    private float _cameraYRotation = 0.0f;

    private Vector3 _moveDirectionNorm = Vector3.zero;

    private Vector3 _playerVelocity = Vector3.zero;

    private float _playerTopVelocity = 0.0f;

    private bool _wishJump = false;

    private float _playerFriction = 0.0f;

    private Cmd _cmd;

    private PlayerControl _playerControl;

    private Pause _pause;

    public bool Grounded => _controller.isGrounded;

    [Inject]
    private void Construct(PlayerControl playerControl, Pause pause)
    {
        _playerControl = playerControl;

        _pause = pause;

        _pause.Register(this);
    }

    private void OnEnable()
    {
        _playerControl.ScreenMove += RotateCamera;
    }

    private void OnDisable()
    {
        _playerControl.ScreenMove -= RotateCamera;
    }

    private void Start()
    {
        SetCameraSensitivity();

        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;

        if (_playerView == null)
        {
            Camera mainCamera = Camera.main;

            if (mainCamera != null)
                _playerView = mainCamera.gameObject.transform;
        }

        _playerView.position = new Vector3(
            transform.position.x,
            transform.position.y + _playerViewYOffset,
            transform.position.z);

        _controller = GetComponent<CharacterController>();
    }

    public void SetPause(bool paused)
    {
        enabled = !paused;
    }

    public void SetCameraSensitivity()
    {
        _xMouseSensitivity = PlayerPrefs.GetFloat(SettingName);

        _yMouseSensitivity = PlayerPrefs.GetFloat(SettingName);
    }

    public void FireJump(float upSpeed, float forward)
    {
        _wishJump = true;

        Jump(upSpeed);

        _controller.Move(transform.forward * forward * Time.deltaTime);
    }

    private void RotateCamera(Vector3 obj)
    {
        _cameraXRotation -= Input.GetAxisRaw("Mouse Y") * _xMouseSensitivity * 0.02f;

        _cameraYRotation += Input.GetAxisRaw("Mouse X") * _yMouseSensitivity * 0.02f;

        if (_cameraXRotation < -90)
            _cameraXRotation = -90;
        else if (_cameraXRotation > 90)
            _cameraXRotation = 90;

        _playerView.rotation = Quaternion.Euler(_cameraXRotation, _cameraYRotation, 0);
    }

    private void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            if (Input.GetButtonDown("Fire1"))
                Cursor.lockState = CursorLockMode.Locked;
        }

        transform.rotation = Quaternion.Euler(0, _cameraYRotation, 0);

        QueueJump();

        if (_controller.isGrounded)
        {
            GroundMove();
        }
        else if (!_controller.isGrounded)
        {
            AirMove();
        }

        _controller.Move(_playerVelocity * Time.deltaTime);

        Vector3 udp = _playerVelocity;

        udp.y = 0.0f;

        if (udp.magnitude > _playerTopVelocity)
            _playerTopVelocity = udp.magnitude;

        _playerView.position = new Vector3(
    transform.position.x,
    transform.position.y + _playerViewYOffset,
    transform.position.z);
    }

    private void OnDestroy()
    {
        _pause.UnRegister(this);
    }

    private void SetMovementDir()
    {
        _cmd.forwardMove = Input.GetAxisRaw("Vertical");

        _cmd.rightMove = Input.GetAxisRaw("Horizontal");
    }

    private void QueueJump()
    {
        if (_holdJumpToBhop)
        {
            _wishJump = Input.GetButton("Jump");
            return;
        }

        if (Input.GetButtonDown("Jump") && !_wishJump)
            _wishJump = true;
        if (Input.GetButtonUp("Jump"))
            _wishJump = false;
    }

    private void AirMove()
    {
        Vector3 wishdir;

        float wishvel = _airAcceleration;

        float accel;

        SetMovementDir();

        wishdir = new Vector3(_cmd.rightMove, 0, _cmd.forwardMove);

        wishdir = transform.TransformDirection(wishdir);

        float wishspeed = wishdir.magnitude;

        wishspeed *= _moveSpeed;

        wishdir.Normalize();

        _moveDirectionNorm = wishdir;

        float wishspeed2 = wishspeed;

        if (Vector3.Dot(_playerVelocity, wishdir) < 0)
            accel = _airDecceleration;
        else
            accel = _airAcceleration;

        if (_cmd.forwardMove == 0 && _cmd.rightMove != 0)
        {
            if (wishspeed > _sideStrafeSpeed)
                wishspeed = _sideStrafeSpeed;
            accel = _sideStrafeAcceleration;
        }

        Accelerate(wishdir, wishspeed, accel);

        if (_airControl > 0)
            AirControl(wishdir, wishspeed2);

        _playerVelocity.y -= _gravity * Time.deltaTime;
    }

    private void AirControl(Vector3 wishdir, float wishspeed)
    {
        float zspeed;

        float speed;

        float dot;

        float k;

        if (Mathf.Abs(_cmd.forwardMove) < 0.001 || Mathf.Abs(wishspeed) < 0.001)
            return;

        zspeed = _playerVelocity.y;

        _playerVelocity.y = 0;

        speed = _playerVelocity.magnitude;

        _playerVelocity.Normalize();

        dot = Vector3.Dot(_playerVelocity, wishdir);

        k = 32;

        k *= _airControl * dot * dot * Time.deltaTime;

        if (dot > 0)
        {
            _playerVelocity.x = _playerVelocity.x * speed + wishdir.x * k;

            _playerVelocity.y = _playerVelocity.y * speed + wishdir.y * k;

            _playerVelocity.z = _playerVelocity.z * speed + wishdir.z * k;

            _playerVelocity.Normalize();

            _moveDirectionNorm = _playerVelocity;
        }

        _playerVelocity.x *= speed;

        _playerVelocity.y = zspeed;

        _playerVelocity.z *= speed;
    }

    private void GroundMove()
    {
        Vector3 wishdir;

        if (!_wishJump)
            ApplyFriction(1.0f);
        else
            ApplyFriction(0);

        SetMovementDir();

        wishdir = new Vector3(_cmd.rightMove, 0, _cmd.forwardMove);

        wishdir = transform.TransformDirection(wishdir);

        wishdir.Normalize();

        _moveDirectionNorm = wishdir;

        var wishspeed = wishdir.magnitude;

        wishspeed *= _moveSpeed;

        Accelerate(wishdir, wishspeed, _runAcceleration);

        _playerVelocity.y = -_gravity * Time.deltaTime;

        Jump(_jumpSpeed);
    }

    public void Jump(float speed)
    {
        if (_wishJump)
        {
            _playerVelocity.y = speed;

            _jumpSound.PlayOneShot(_jumpSound.clip);

            _wishJump = false;
        }
    }

    private void ApplyFriction(float t)
    {
        Vector3 vec = _playerVelocity;

        float speed;

        float newspeed;

        float control;

        float drop;

        vec.y = 0.0f;

        speed = vec.magnitude;

        drop = 0.0f;

        if (_controller.isGrounded)
        {
            control = speed < _runDeacceleration ? _runDeacceleration : speed;

            drop = control * _groundFriction * Time.deltaTime * t;
        }

        newspeed = speed - drop;

        _playerFriction = newspeed;

        if (newspeed < 0)
            newspeed = 0;
        if (speed > 0)
            newspeed /= speed;

        _playerVelocity.x *= newspeed;

        _playerVelocity.z *= newspeed;
    }

    private void Accelerate(Vector3 wishdir, float wishspeed, float accel)
    {
        float addspeed;

        float accelspeed;

        float currentspeed;

        currentspeed = Vector3.Dot(_playerVelocity, wishdir);

        addspeed = wishspeed - currentspeed;

        if (addspeed <= 0)
            return;

        accelspeed = accel * Time.deltaTime * wishspeed;

        if (accelspeed > addspeed)
            accelspeed = addspeed;

        _playerVelocity.x += accelspeed * wishdir.x;

        _playerVelocity.z += accelspeed * wishdir.z;
    }
}
