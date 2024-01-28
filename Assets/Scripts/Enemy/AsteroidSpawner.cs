using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {
    [SerializeField] GameObject _asteroidPrefab;
    [SerializeField] int _asteroidCount;
    [SerializeField] float _playerTolerance;

    Vector2 _screenDimensions;

    void Start() {
        _screenDimensions = Camera.main.ScreenToWorldPoint(new(Screen.width, Screen.height, 0));
        Spawn();
    }

    void Spawn() {
        for (int i = 0; i < _asteroidCount; i++) {
            Vector3 spawnPosition = GetRandomPosition();

            while (Vector3.Distance(spawnPosition, transform.position) < _playerTolerance)
                spawnPosition = GetRandomPosition();

            _asteroidPrefab.transform.position = spawnPosition;
            Instantiate(_asteroidPrefab);
        }
    }

    Vector3 GetRandomPosition() {
        return new() {
            x = Random.Range(-_screenDimensions.x / 2, _screenDimensions.x / 2),
            y = Random.Range(-_screenDimensions.y / 2, _screenDimensions.y / 2)
        };
    }
}
