using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerShooting : MonoBehaviour {
    [SerializeField] float _shootCooldown;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _bulletTransform;
    [SerializeField] Transform _bulletBuffer;

    PlayerInput _playerInput;
    bool _shooting;
    bool _canShoot = true;

    void OnEnable() {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.OnPlayerShoot += OnPlayerShoot;
    }

    void Update() {
        Shoot();
    }

    void OnPlayerShoot(bool input) {
        _shooting = input;
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
