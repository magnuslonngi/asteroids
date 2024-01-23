using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] float range;
    float currentDistance;

    void Update() {
        transform.position += direction * speed * Time.deltaTime;
        currentDistance += speed * Time.deltaTime;
        if (currentDistance >= range)
            Destroy(gameObject);
    }
}
