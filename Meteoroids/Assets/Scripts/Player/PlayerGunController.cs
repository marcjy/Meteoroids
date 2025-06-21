using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
    [SerializeField] private NormalLaser _normalLaser;

    private BaseLaser _currentLaserType;

    private void Awake()
    {
        _currentLaserType = _normalLaser;
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
}