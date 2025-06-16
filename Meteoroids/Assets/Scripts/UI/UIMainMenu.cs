using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("Main Buttons")]
    [SerializeField] private GameObject _mainButtonsWindow;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private string _gameSceneName;
    [SerializeField] private Button _howToPlayButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _quitButton;

    [Header("HowToPlay")]
    [SerializeField] private GameObject _howToPlayWindow;
    [SerializeField] private Button _howToPlayExitButton;

    [Header("Options")]
    [SerializeField] private GameObject _optionsWindow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    private void Init()
    {
        InitMainButtons();
        InitHowToPlay();

        InitOptionsController();
    }

    private void InitOptionsController()
    {
        UIOptionsController optionsController = GetComponentInChildren<UIOptionsController>(true);

        if (optionsController != null)
            optionsController.OnWindowClosed += HandleOptionsWindowClosed;
        else
            Debug.LogWarning($"{nameof(UIOptionsController)} not found in {nameof(gameObject.name)}'s children");
    }

    #region Event Handling
    private void HandleOptionsWindowClosed(object sender, System.EventArgs e) => _mainButtonsWindow.SetActive(true);
    #endregion

    #region Main Buttons
    private void InitMainButtons()
    {
        _startGameButton.onClick.AddListener(StartGame);
        _howToPlayButton.onClick.AddListener(OpenHowToPlayWindow);
        _optionsButton.onClick.AddListener(OpenOptionsWindow);
        _quitButton.onClick.AddListener(QuitGame);
    }
    private void UnSubMainButtons()
    {
        _startGameButton.onClick.RemoveListener(StartGame);
        _howToPlayButton.onClick.RemoveListener(OpenHowToPlayWindow);
        _optionsButton.onClick.RemoveListener(OpenOptionsWindow);
        _quitButton.onClick.RemoveListener(QuitGame);
    }

    private void StartGame()
    {
        if (string.IsNullOrEmpty(_gameSceneName))
        {
            Debug.LogError($"{nameof(_gameSceneName)} is not set");
            return;
        }

        SceneManager.LoadScene(_gameSceneName, LoadSceneMode.Single);
    }
    private void OpenHowToPlayWindow()
    {
        _howToPlayWindow.SetActive(true);
        _mainButtonsWindow.SetActive(false);
    }
    private void OpenOptionsWindow()
    {
        _optionsWindow.SetActive(true);
        _mainButtonsWindow.SetActive(false);
    }
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion

    #region HowToPlay
    private void InitHowToPlay()
    {
        _howToPlayWindow.SetActive(false);
        _howToPlayExitButton.onClick.AddListener(CloseHowToPlayWindow);
    }
    private void UnSubHowToPlayButtons() => _howToPlayExitButton.onClick.RemoveListener(CloseHowToPlayWindow);

    private void CloseHowToPlayWindow()
    {
        _howToPlayWindow.SetActive(false);
        _mainButtonsWindow.SetActive(true);
    }
    #endregion

    private void OnDestroy()
    {
        UnSubButtons();
    }
    private void UnSubButtons()
    {
        UnSubMainButtons();
        UnSubHowToPlayButtons();
    }
}
