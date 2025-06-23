using UnityEngine;

public class ExplodingLaser : BaseLaser
{
    [SerializeField] private LayerMask _asteroidLayer;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionDuration = 1.0f;
    [SerializeField] private GameObject _blackHolePrefab;

    protected override void OnAsteroidCollision(AsteroidManager asteroid)
    {
        //Create black hole effect
        GameObject blackHole = Instantiate(_blackHolePrefab, transform.position, Quaternion.identity);
        Destroy(blackHole, _explosionDuration);

        //Get all asteroids that are inside the black hole and destroy them.
        Collider2D[] collisions = GetAsteroidsInsideCircle(_explosionRadius);
        foreach (Collider2D collision in collisions)
        {
            if (collision.gameObject.TryGetComponent(out AsteroidManager asteroidInsideExplosion))
                asteroidInsideExplosion.DestroyAsteroid();
        }

        Destroy(gameObject);
    }

    private Collider2D[] GetAsteroidsInsideCircle(float raidus) => Physics2D.OverlapCircleAll(transform.position, raidus, _asteroidLayer);

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
