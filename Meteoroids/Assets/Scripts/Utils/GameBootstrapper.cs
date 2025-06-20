using System.Collections.Generic;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private AudioLibrary _audioLibrary;
    [SerializeField] private List<AsteroidConfig> _asteroidConfigurations;

    private void Awake()
    {
        AudioManager.Init(_audioLibrary);
        AsteroidFactory.Init(_asteroidConfigurations);

        DontDestroyOnLoad(gameObject);
    }
}
