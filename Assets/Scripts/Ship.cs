using UnityEngine;
using Unity.Mathematics;

[RequireComponent(typeof(SpriteRenderer))]
public class Ship : MonoBehaviour {
    [SerializeField] float thrustSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float deacceleration;

    Vector3 velocity;
    Vector2 screenDimensions;
    Vector2 spriteDimensions;
    SpriteRenderer spriteRenderer;
    PlayerInput input;
    float pixelsPerUnit;

    void Start() {
        EnableInput();
        SetDimensions();
    }

    void Update() {
        Move();
    }

    void LateUpdate() {
        Warp();
    }

    void EnableInput() {
        input = new();
        input.Enable();
    }

    void SetDimensions() {
        Camera mainCamera = Camera.main;
        Vector2 screenSize = new(Screen.width, Screen.height);

        screenDimensions = mainCamera.ScreenToWorldPoint(screenSize);
        spriteRenderer = GetComponent<SpriteRenderer>();
        pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit;

        Vector2 scale = transform.localScale;
        float spriteWidth = spriteRenderer.sprite.texture.width * scale.x;
        float spriteHeight = spriteRenderer.sprite.texture.height * scale.y;

        spriteDimensions = new(spriteWidth, spriteHeight);
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

    void Warp() {
        Vector2 finalSprite = Camera.main.ScreenToWorldPoint(spriteDimensions);
        float halfWidth = -finalSprite.x / pixelsPerUnit * 2;
        float halfHeigth = -finalSprite.y / pixelsPerUnit * 2;

        if (transform.position.x - halfWidth >= screenDimensions.x
            || transform.position.x + halfWidth <= -screenDimensions.x) {

            transform.position = new(-transform.position.x,
                transform.position.y, transform.position.z);
        }

        if (transform.position.y - halfHeigth >= screenDimensions.y
            || transform.position.y + halfHeigth <= -screenDimensions.y) {

            transform.position = new(transform.position.x,
                -transform.position.y, transform.position.z);
        }
    }
}
