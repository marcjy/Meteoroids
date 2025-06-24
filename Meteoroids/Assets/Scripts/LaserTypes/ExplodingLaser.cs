using UnityEngine;

public class ExplodingLaser : BaseLaser
{
    [Header("Explosion")]
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionDuration = 1.0f;

    [SerializeField] private LayerMask _asteroidLayer;

    [SerializeField] private GameObject _blackHolePrefab;
    [SerializeField] private AudioClip _blackHoleCreated;


    protected override void OnAsteroidCollision(AsteroidManager asteroid)
    {
        //Create black hole effect
        GameObject blackHole = Instantiate(_blackHolePrefab, transform.position, Quaternion.identity);
        Destroy(blackHole, _explosionDuration);

        //Play audio clip for the black hole
        AudioManager.PlaySFX(_blackHoleCreated);

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
