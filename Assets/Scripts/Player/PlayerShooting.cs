using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerShooting : MonoBehaviour {
    [SerializeField] float _shootCooldown;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _bulletTransform;
    [SerializeField] Transform _bulletBuffer;

    PlayerInput _input;
    bool _shooting;
    bool _canShoot = true;

    void OnEnable() {
        _input = PlayerInput.Instance;
        _input.actions.Attack.Shoot.started += OnPlayerShoot;
        _input.actions.Attack.Shoot.performed += OnPlayerShoot;
        _input.actions.Attack.Shoot.canceled += OnPlayerShoot;
    }

    void Update() {
        Shoot();
    }

    void OnDisable() {
        _input.actions.Attack.Shoot.started -= OnPlayerShoot;
        _input.actions.Attack.Shoot.performed -= OnPlayerShoot;
        _input.actions.Attack.Shoot.canceled -= OnPlayerShoot;
    }

    void OnPlayerShoot(InputAction.CallbackContext context) {
        _shooting = context.ReadValueAsButton();
    }

    void Shoot() {
        if (_shooting && _canShoot) {
            GameObject bulletGO = Instantiate(_bulletPrefab, _bulletTransform);
            PlayerBullet bulletComponent = bulletGO.GetComponent<PlayerBullet>();
            bulletGO.transform.parent = _bulletBuffer;
            bulletComponent.direction = transform.up;
            _canShoot = false;
            Invoke("EnableShooting", _shootCooldown);
        }
    }

    void EnableShooting() {
        _canShoot = true;
    }
}
