public class NormalLaser : BaseLaser
{
    protected override void OnAsteroidCollision(AsteroidManager asteroid)
    {
        asteroid.SplitAsteroid();
        Destroy(gameObject);
    }

}
