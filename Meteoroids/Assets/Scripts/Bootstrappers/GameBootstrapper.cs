using System.Collections.Generic;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private List<AsteroidConfig> _asteroidConfigurations;
    private void Awake()
    {
        if (_asteroidConfigurations == null || _asteroidConfigurations.Count == 0)
            Debug.LogError($"{nameof(GameBootstrapper)} recived null or empty asteroid config list.");

        AsteroidFactory.Init(_asteroidConfigurations);
    }
}