public class PiercingLaser : BaseLaser
{
    protected override void OnAsteroidCollision(AsteroidManager asteroid)
    {
        asteroid.DestroyAsteroid();
    }
}
