using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    private Rigidbody2D _rb;

    private AsteroidType _type;
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
    public void SplitAsteroid()
    {
        AsteroidType newAsteroidsType = AsteroidType.Big;

        switch (_type)
        {
            case AsteroidType.Big:
                newAsteroidsType = AsteroidType.Medium;
                break;
            case AsteroidType.Medium:
                newAsteroidsType = AsteroidType.Small;
                break;
            case AsteroidType.Small:
                //TODO: SCORE
                Destroy(gameObject);
                return;
        }

        for (int i = 0; i < _nAsteroidsAfterSplit; i++)
            AsteroidFactory.Create(newAsteroidsType, transform.position);

        Destroy(gameObject);
    }
    public void DestroyAsteroid() => Destroy(gameObject);

    private Vector2 GenerateRandomDirection() => Random.insideUnitCircle.normalized;

}
