using UnityEngine;

[CreateAssetMenu(fileName = "BeamCannon", menuName = "Scriptable Objects/CannonType/BeamCannon")]
public class BeamCannon : BaseCannon
{
    [SerializeField] private Beam _beamPrefab;
    [SerializeField] private float _tick = 0.25f;
    [SerializeField] private float _duration = 10.0f;

    public override void CreateBeam(Transform spawnTransform)
    {
        Beam beam = Instantiate(_beamPrefab, spawnTransform);
        beam.Initialize(_tick, _duration);
    }

    public float GetBeamDuration() => _duration;
}
