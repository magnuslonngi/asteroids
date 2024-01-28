using UnityEngine;

public class Asteroid : MonoBehaviour {
    [SerializeField] float _speed;
    Vector3 _direction;

    void Start() {
        _direction.x = Random.Range(-10, 10);
        _direction.y = Random.Range(-10, 10);
    }

    void Update() {
        transform.position += _speed * _direction.normalized * Time.deltaTime;
    }
}
