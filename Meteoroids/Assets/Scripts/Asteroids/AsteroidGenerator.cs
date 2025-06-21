using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    [SerializeField] private float _playerSafeRadius = 2.5f;

    [Header("Spawn Rates")]
    [SerializeField] private RandomRange _bigAsteroidRandomSpawnRate;
    [SerializeField] private RandomRange _mediumAsteroidRandomSpawnRate;
    [SerializeField] private RandomRange _smallAsteroidRandomSpawnRate;

    private GameObject _player;
    private Rect _screenBounds;

    private List<Coroutine> _spawnCoroutines;
    private bool _isWorking;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _screenBounds = ScreenBoundsData.GetScreenBounds();

        _spawnCoroutines = new List<Coroutine>();
        _isWorking = false;
    }

    public void StartGenerator()
    {
        if (_isWorking)
        {
            Debug.LogWarning($"Attempted to start {nameof(AsteroidGenerator)} when it was already started.");
            return;
        }

        _isWorking = true;

        _spawnCoroutines.Add(StartCoroutine(SpawnAsteroid(AsteroidConfig.AsteroidType.Big, _bigAsteroidRandomSpawnRate)));
        _spawnCoroutines.Add(StartCoroutine(SpawnAsteroid(AsteroidConfig.AsteroidType.Medium, _mediumAsteroidRandomSpawnRate)));
        _spawnCoroutines.Add(StartCoroutine(SpawnAsteroid(AsteroidConfig.AsteroidType.Small, _smallAsteroidRandomSpawnRate)));
    }

    public void StopGenerator()
    {
        foreach (Coroutine coroutine in _spawnCoroutines)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
        }

        _spawnCoroutines.Clear();
        _isWorking = false;
    }
    private Vector2 FindSafePositionForSpawning()
    {
        Vector2 playerPosition = _player.transform.position;
        float minDistance = _playerSafeRadius;
        float maxDistance = Mathf.Max(_screenBounds.width, _screenBounds.height);

        Vector2 direction = UnityEngine.Random.insideUnitCircle.normalized;
        float distance = UnityEngine.Random.Range(minDistance, maxDistance);

        Vector2 spawnPosition = playerPosition + direction * distance;

        spawnPosition.x = Mathf.Clamp(spawnPosition.x, _screenBounds.xMin, _screenBounds.xMax);
        spawnPosition.y = Mathf.Clamp(spawnPosition.y, _screenBounds.yMin, _screenBounds.yMax);

        return spawnPosition;
    }

    private IEnumerator SpawnAsteroid(AsteroidConfig.AsteroidType type, RandomRange spawnRateRange)
    {
        while (true)
        {
            Vector2 asteroidPosition = FindSafePositionForSpawning();
            AsteroidFactory.Create(type, asteroidPosition);

            yield return new WaitForSeconds(spawnRateRange.GetRandomValue());
        }
    }
}
