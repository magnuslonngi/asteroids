using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField] float _thrustSpeed;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _acceleration;
    [SerializeField] float _deacceleration;

    PlayerInput _playerInput;

    Vector2 _moveInput;
    Vector3 _velocity;
    Vector3 _thrustForce;
    float _rotationAngle;

    void OnEnable() {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.OnPlayerMove += OnPlayerMove;
    }

    void Update() {
        Move();
    }

    void OnPlayerMove(Vector2 input) {
        _moveInput = input;
        _thrustForce = _moveInput.y * _thrustSpeed * transform.up;
        _rotationAngle = _moveInput.x * _rotationSpeed * -1f;
    }

    void Move() {
        _velocity += _moveInput.y > 0
            ? _acceleration * Time.deltaTime *_thrustForce
            : _deacceleration * Time.deltaTime * -_velocity;

        _velocity = math.clamp(_velocity, -_maxSpeed, _maxSpeed);
        transform.position += _velocity * Time.deltaTime;

        transform.eulerAngles += new Vector3(0, 0, _rotationAngle * Time.deltaTime);
    }
}
