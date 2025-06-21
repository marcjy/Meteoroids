public class NormalLaser : BaseLaser
{
    protected override void OnAsteroidCollision(AsteroidManager asteroid)
    {
        asteroid.DestroyAsteroid();
        Destroy(gameObject);
    }

}
