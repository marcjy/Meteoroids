using UnityEngine;

public abstract class PowerUpBase : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;

    public string Name => _name;
    public Sprite Sprite => _sprite;

    public abstract void Activate();
}