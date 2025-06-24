using UnityEngine;

public abstract class BaseCannon : ScriptableObject
{
    public CannonType Type;
    public abstract void Shoot(BaseLaser laser, Transform spawnTransform);
}
