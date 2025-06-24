using UnityEngine;

public class SplittingLaser : BaseLaser
{
    [SerializeField] private int nSpawningLasers;
    [SerializeField] private GameObject SpawnedLaserPrefab;

    protected override void OnAsteroidCollision(AsteroidManager asteroid)
    {
        asteroid.DestroyAsteroid();

        float rotationOffset = 360 / nSpawningLasers;

        for (int i = 0; i < nSpawningLasers; i++)
            Instantiate(SpawnedLaserPrefab, transform.position, Quaternion.Euler(0, 0, rotationOffset * i));

        Destroy(gameObject);
    }
}
