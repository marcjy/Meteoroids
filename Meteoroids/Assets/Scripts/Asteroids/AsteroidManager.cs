using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    private Rigidbody2D _rb;

    private AsteroidConfig.AsteroidType _type;
    private float _speed;
    private float _nAsteroidsAfterSplit;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rb.linearVelocity = GenerateRandomDirection() * _speed;
    }

    public void Initialize(AsteroidConfig asteroidConfig)
    {
        _type = asteroidConfig.Type;
        _speed = asteroidConfig.Speed;
        _nAsteroidsAfterSplit = asteroidConfig.NumberAsteroidsAfterSplit;
    }

    private Vector2 GenerateRandomDirection() => Random.insideUnitCircle.normalized;

    private void SplitAsteroid()
    {
        AsteroidConfig.AsteroidType newAsteroidsType = AsteroidConfig.AsteroidType.Big;

        switch (_type)
        {
            case AsteroidConfig.AsteroidType.Big:
                newAsteroidsType = AsteroidConfig.AsteroidType.Medium;
                break;
            case AsteroidConfig.AsteroidType.Medium:
                newAsteroidsType = AsteroidConfig.AsteroidType.Small;
                break;
            case AsteroidConfig.AsteroidType.Small:
                //TODO: SCORE
                return;
        }

        for (int i = 0; i < _nAsteroidsAfterSplit; i++)
            AsteroidFactory.Create(newAsteroidsType, transform.position);

        Destroy(gameObject);
    }
}
