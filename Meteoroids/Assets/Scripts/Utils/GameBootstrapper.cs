using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private AudioLibrary _audioLibrary;

    private void Awake()
    {
        AudioManager.Init(_audioLibrary);

        DontDestroyOnLoad(gameObject);
    }
}
