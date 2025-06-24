using UnityEngine;

public abstract class BaseCannon : ScriptableObject
{
    public CannonType Type;
    public CannonAttackType AttackType;

    public virtual void Shoot(BaseLaser laser, Transform spawnTransform) { }
    public virtual void CreateBeam(Transform spawnTransform) { }
}
