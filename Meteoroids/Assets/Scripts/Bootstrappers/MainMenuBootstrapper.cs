using UnityEngine;

public class MainMenuBootstrapper : MonoBehaviour
{
    [SerializeField] private AudioLibrary _audioLibrary;

    private void Awake()
    {
        AudioManager.Init(_audioLibrary);
    }
}
