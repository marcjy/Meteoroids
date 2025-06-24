using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] private LayerMask _asteroidLayer;

    private float _tick;
    private float _elapsedTime;
    private float _beamHeight;

    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _beamHeight = _boxCollider.bounds.size.y;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _tick)
        {
            _elapsedTime = 0.0f;
            SplitAsteroidInRange();
        }
    }

    public void Initialize(float tick, float duration)
    {
        Destroy(gameObject, duration);

        _tick = tick;
        _elapsedTime = tick;
    }

    private void SplitAsteroidInRange()
    {
        Vector2 center = transform.position + transform.up * (_beamHeight / 2);
        Vector2 size = _boxCollider.size * transform.localScale;
        Collider2D[] collisions = Physics2D.OverlapBoxAll(center, size, transform.eulerAngles.z, _asteroidLayer);

        foreach (Collider2D collision in collisions)
            collision.GetComponent<AsteroidManager>().SplitAsteroid();
    }

    private void OnDrawGizmos()
    {
        if (_boxCollider == null) return;

        Gizmos.color = Color.red;
        Vector2 center = transform.position + transform.up * (_beamHeight / 2);
        Vector2 size = _boxCollider.size * transform.localScale;
        Gizmos.matrix = Matrix4x4.TRS(center, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, size);
    }
}
