using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AsteroidFactory
{
    private static List<AsteroidConfig> _asteroidConfigurations;

    public static void Init(List<AsteroidConfig> asteroidConfigs)
    {
        if (_asteroidConfigurations == null || _asteroidConfigurations.Count == 0)
            Debug.LogError($"{nameof(AsteroidFactory)} recived null or empty asteroid config list.");

        _asteroidConfigurations = asteroidConfigs;
    }

    public static AsteroidManager Create(AsteroidConfig.AsteroidType type, Vector2 position)
    {
        AsteroidConfig asteroidConfig = _asteroidConfigurations.FirstOrDefault(ac => ac.Type == type);

        if (asteroidConfig == null)
        {
            Debug.LogError($"Asteroid type '{type}' was not found in the list of asteroid configurations");
            return null;
        }

        AsteroidManager asteroid = GameObject.Instantiate(asteroidConfig.Prefab, position, Quaternion.identity);
        asteroid.Initialize(asteroidConfig);

        return asteroid;
    }
}
