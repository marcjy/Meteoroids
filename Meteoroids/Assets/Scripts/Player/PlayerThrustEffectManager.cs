using UnityEngine;

public class PlayerThrustEffectManager : MonoBehaviour
{
    private ParticleSystem[] _thrusterEffects;

    private void Awake()
    {
        _thrusterEffects = GetComponentsInChildren<ParticleSystem>();
        if (_thrusterEffects.Length == 0)
            Debug.LogError($"No {nameof(ParticleSystem)} found in {gameObject.name}' children");
    }
    void Start()
    {
        SubscribeToInputManagerEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromInputManagerEvents();
    }

    #region Event Subscription
    private void SubscribeToInputManagerEvents()
    {
        InputManager.Instance.OnPlayerThrusts += HandlePlayerThrusts;
    }
    private void UnsubscribeFromInputManagerEvents()
    {
        InputManager.Instance.OnPlayerThrusts -= HandlePlayerThrusts;
    }
    #endregion

    #region Event Handling
    private void HandlePlayerThrusts(object sender, bool isThrusting)
    {
        foreach (ParticleSystem ps in _thrusterEffects)
        {
            if (isThrusting)
                ps.Play();
            else
                ps.Stop();
        }
    }
    #endregion

}
