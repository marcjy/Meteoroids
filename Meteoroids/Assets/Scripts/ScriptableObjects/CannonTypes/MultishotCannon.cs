using UnityEngine;

[CreateAssetMenu(fileName = "MultishotCannon", menuName = "Scriptable Objects/CannonType/MultishotCannon")]
public class MultishotCannon : BaseCannon
{
    [SerializeField] private int _nLasers;
    [SerializeField] private int _spreadAngle;

    public override void Shoot(BaseLaser laser, Transform spawnTransform)
    {
        //Even
        if (_nLasers % 2 == 0)
        {
            for (int i = 0; i < _nLasers / 2; i++)
                GameObject.Instantiate(laser, spawnTransform.position, spawnTransform.rotation * Quaternion.Euler(0, 0, _spreadAngle * -1 * i));
            for (int i = 0; i < _nLasers / 2; i++)
                GameObject.Instantiate(laser, spawnTransform.position, spawnTransform.rotation * Quaternion.Euler(0, 0, _spreadAngle * i));
        }
        //Odd
        else
        {
            for (int i = 0; i < Mathf.FloorToInt(_nLasers / 2); i++)
                GameObject.Instantiate(laser, spawnTransform.position, spawnTransform.rotation * Quaternion.Euler(0, 0, _spreadAngle * -1 * i));

            GameObject.Instantiate(laser, spawnTransform.position, spawnTransform.rotation * Quaternion.Euler(0, 0, 0));

            for (int i = 0; i < Mathf.CeilToInt(_nLasers / 2); i++)
                GameObject.Instantiate(laser, spawnTransform.position, spawnTransform.rotation * Quaternion.Euler(0, 0, _spreadAngle * i));
        }

    }
}
