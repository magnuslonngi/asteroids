using UnityEngine;
using Unity.Mathematics;

[RequireComponent(typeof(SpriteRenderer))]
public class Ship : MonoBehaviour {
    [SerializeField] float thrustSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float deacceleration;
    [SerializeField] float shootCooldown;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletTransform;
    [SerializeField] Transform bulletBuffer;

    Vector3 velocity;
    SpriteRenderer spriteRenderer;
    PlayerInput input;
    bool canShoot = true;

    void Start() {
        EnableInput();
    }

    void Update() {
        Move();
        Shoot();
    }

    void EnableInput() {
        input = new();
        input.Enable();
    }

    void EnableShooting() {
        canShoot = true;
    }

    void Shoot() {
        bool shooting = input.Attack.Shoot.ReadValue<float>() > 0;
        if (shooting && canShoot) {
            GameObject bulletGO = Instantiate(bullet, bulletTransform);
            Bullet bulletComponent = bulletGO.GetComponent<Bullet>();
            bulletGO.transform.parent = bulletBuffer;
            bulletComponent.direction = transform.up;
            canShoot = false;
            Invoke("EnableShooting", shootCooldown);
        }
    }

    void Move() {
        Vector2 inputValue = input.Movement.Move.ReadValue<Vector2>();
        Vector3 thrustForce = inputValue.y * thrustSpeed * transform.up;
        float deltaTime = Time.deltaTime;
        float rotationAngle = inputValue.x * deltaTime * rotationSpeed * -1f;

        velocity += inputValue.y > 0
            ? acceleration * deltaTime * thrustForce
            : deacceleration * deltaTime * -velocity;

        velocity = math.clamp(velocity, -maxSpeed, maxSpeed);
        transform.position += velocity * deltaTime;

        transform.eulerAngles += new Vector3(0, 0, rotationAngle);
    }
}
