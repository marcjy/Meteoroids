using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerCannonController : MonoBehaviour
{
    [Header("Lasers")]
    [SerializeField] private LaserType _defaultLaser;
    [SerializeField] private NormalLaser _normalLaser;
    [SerializeField] private PiercingLaser _piercingLaser;
    [SerializeField] private ExplodingLaser _explodingLaser;
    [SerializeField] private SplittingLaser _splittingLaser;

    [Header("Cannon")]
    [SerializeField] private Transform _laserSpawn;
    [SerializeField] private CannonType _defaultCannon;
    [SerializeField] private BaseCannon[] _cannonTypes;

    private NormalCannon _normalCannon;
    private MultishotCannon _multishotCannon;

    private BaseCannon _currentCannon;
    private BaseLaser _currentLaserType;

    private bool _canFire = true;

    private void Awake()
    {
        _currentCannon = GetCannon(_defaultCannon);
        _currentLaserType = GetLaser(_defaultLaser);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SubscribeToInputManagerEvents();
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
        if (!_canFire) return;

        if (_currentCannon.AttackType == CannonAttackType.Proyectile)
            _currentCannon.Shoot(_currentLaserType, _laserSpawn);
        else
        {
            _currentCannon.CreateBeam(_laserSpawn);

            float cannonDowntime = (_currentCannon as BeamCannon).GetBeamDuration();
            StartCoroutine(DisableCannon(cannonDowntime));
        }
    }

    private BaseLaser GetLaser(LaserType type)
    {
        return type switch
        {
            LaserType.Normal => _normalLaser,
            LaserType.Piercing => _piercingLaser,
            LaserType.Exploding => _explodingLaser,
            LaserType.Splitting => _splittingLaser,
            _ => _normalLaser
        };
    }
    private BaseCannon GetCannon(CannonType type)
    {
        BaseCannon cannon = _cannonTypes.FirstOrDefault(c => c.Type == type);

        if (cannon == null)
            Debug.LogError($"Cannon type '{type} not found in array '{_cannonTypes}'");

        return cannon;
    }

    private IEnumerator DisableCannon(float duration)
    {
        _canFire = false;

        yield return new WaitForSeconds(duration);

        _canFire = true;
    }
}