using UnityEngine;

[CreateAssetMenu(fileName = "NormalCannon", menuName = "Scriptable Objects/CannonType/NormalCannon")]
public class NormalCannon : BaseCannon
{
    public override void Shoot(BaseLaser laserType, Transform spawnTransform)
    {
        GameObject.Instantiate(laserType, spawnTransform.position, spawnTransform.rotation);
    }
}
