using UnityEngine;

public class Warp : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    Vector2 screenDimensions;
    Vector2 spriteDimensions;
    float pixelsPerUnit;

    void Start() {
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

    void Update() {
        HandleWarp();
    }

    void HandleWarp() {
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
