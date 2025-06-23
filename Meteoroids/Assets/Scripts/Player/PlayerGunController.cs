using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
    [Header("Lasers")]
    [SerializeField] private LaserType _defaultLaser;
    [SerializeField] private NormalLaser _normalLaser;
    [SerializeField] private PiercingLaser _piercingLaser;

    private BaseLaser _currentLaserType;

    private void Awake()
    {
        _currentLaserType = GetLaser(_defaultLaser);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SubscribeToInputManagerEvents();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        UnsubscribeFromInputManagerEvents();
    }

    #region Event Subscription
    private void SubscribeToInputManagerEvents()
    {
        InputManager.Instance.OnPlayerShoots += HandlePlayerShoot;
    }
    private void UnsubscribeFromInputManagerEvents()
    {
        InputManager.Instance.OnPlayerShoots -= HandlePlayerShoot;
    }
    #endregion

    #region Event Handling
    private void HandlePlayerShoot(object sender, System.EventArgs e) => Shoot();
    #endregion

    private void Shoot()
    {
        BaseLaser laser = Instantiate(_currentLaserType, transform.position, transform.rotation);
    }

    private BaseLaser GetLaser(LaserType type)
    {
        return type switch
        {
            LaserType.Normal => _normalLaser,
            LaserType.Piercing => _piercingLaser,
            _ => _normalLaser
        };
    }
}