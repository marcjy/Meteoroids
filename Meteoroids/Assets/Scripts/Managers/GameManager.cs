using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler OnGameStart;
    public event EventHandler OnRoundStart;
    public event EventHandler OnRoundEnd;
    public event EventHandler OnGameEnd;

    [SerializeField] private int _maxPlayerLives;

    private GameObject _player;
    private PlayerCollisionManager _playerCollisionManager;
    private int _playerLives;
    private bool _isPlayerAlive;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SubscribeToPlayerEvents();

        StartCoroutine(GameLoop());
    }

    private void OnDestroy()
    {
        UnsubscribeFromPlayerEvents();
    }

    #region Event Subscription
    private void SubscribeToPlayerEvents()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_player == null)
            Debug.LogError($"Could not find the player GO with tag '{Tags.PLAYER}'");

        _playerCollisionManager = _player.GetComponent<PlayerCollisionManager>();

        _playerCollisionManager.OnAsteroidCollision += HandlePlayerAsteroidCollision;
    }
    private void UnsubscribeFromPlayerEvents()
    {
        _playerCollisionManager.OnAsteroidCollision -= HandlePlayerAsteroidCollision;
    }
    #endregion

    #region Event Handling
    private void HandlePlayerAsteroidCollision(object sender, EventArgs e) => _isPlayerAlive = false;
    #endregion

    #region GameLoop
    private IEnumerator GameLoop()
    {
        yield return null; //Wait for other scripts to subscribe to GameManager events.

        GameStart();

        while (HasLivesLeft())
        {
            RoundStart();
            yield return RoundPlaying();
            RoundEnd();
        }

        GameEnd();
    }

    private void GameStart()
    {
        _playerLives = _maxPlayerLives;

        OnGameStart?.Invoke(this, EventArgs.Empty);
    }

    private void RoundStart()
    {
        _isPlayerAlive = true;
        OnRoundStart?.Invoke(this, EventArgs.Empty);
    }
    private IEnumerator RoundPlaying()
    {
        while (_isPlayerAlive)
            yield return null;
    }
    private void RoundEnd()
    {
        _playerLives--;
        OnRoundEnd?.Invoke(this, EventArgs.Empty);
    }

    private void GameEnd()
    {
        OnGameEnd?.Invoke(this, EventArgs.Empty);
    }


    private bool HasLivesLeft() => _playerLives > 0;
    #endregion

}
