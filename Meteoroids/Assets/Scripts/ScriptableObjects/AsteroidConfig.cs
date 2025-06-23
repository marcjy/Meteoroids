using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidConfig", menuName = "Scriptable Objects/AsteroidConfig")]
public class AsteroidConfig : ScriptableObject
{
    public AsteroidType Type;
    public AsteroidManager Prefab;
    public float Speed;
    public int NumberAsteroidsAfterSplit;
}
