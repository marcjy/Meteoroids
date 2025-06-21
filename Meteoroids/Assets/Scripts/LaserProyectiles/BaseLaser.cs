using UnityEngine;

[RequireComponent(typeof(ScreenWrapping))]
public abstract class BaseLaser : MonoBehaviour
{
    [SerializeField] protected float _lifeTime;
    [SerializeField] protected float _speed;

    private Vector2 _direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.up, Space.Self);
        Debug.DrawRay(transform.position, transform.up * 0.5f, Color.magenta);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.ASTEROID))
        {
            AsteroidManager asteroidManager = collision.gameObject.GetComponent<AsteroidManager>();
            OnAsteroidCollision(asteroidManager);
        }
    }

    protected abstract void OnAsteroidCollision(AsteroidManager asteroid);
}
