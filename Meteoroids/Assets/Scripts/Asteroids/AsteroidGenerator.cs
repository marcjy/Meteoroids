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

    private readonly List<Coroutine> _spawnCoroutines = new List<Coroutine>();

    private Rect _screenBounds;
    private GameObject _player;
    private bool _isWorking = false;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        if (_player == null)
            Debug.LogError($"Could not find the player GO with tag '{Tags.PLAYER}'");

        _screenBounds = ScreenBoundsData.GetScreenBounds();
    }

    void Start()
    {
        SubscribeToGameManagerEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromGameManagerEvents();
    }

    #region Event Subscription
    private void SubscribeToGameManagerEvents()
    {
        GameManager.Instance.OnGameStart += HandleGameStart;
        GameManager.Instance.OnGameEnd += HandleGameEnd;
    }

    private void UnsubscribeFromGameManagerEvents()
    {
        GameManager.Instance.OnGameStart -= HandleGameStart;
        GameManager.Instance.OnGameEnd -= HandleGameEnd;
    }
    #endregion

    #region Event Handling
    private void HandleGameStart(object sender, System.EventArgs e) => TurnOn();
    private void HandleGameEnd(object sender, System.EventArgs e) => TurnOff();

    #endregion

    private void TurnOn()
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
    private void TurnOff()
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
