using UnityEngine;

[System.Serializable]
public struct RandomRange
{
    public float Min;
    public float Max;

    public readonly float GetRandomValue() => Random.Range(Min, Max);
}
