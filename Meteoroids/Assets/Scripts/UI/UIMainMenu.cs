using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("Main Buttons")]
    public GameObject MainButtonsWindow;
    public Button StartGameButton;
    public string GameSceneName;
    public Button HowToPlayButton;
    public Button OptionsButton;
    public Button QuitButton;

    [Header("HowToPlay")]
    public GameObject HowToPlayWindow;
    public Button HowToPlayExitButton;

    [Header("Options")]
    public GameObject OptionsWindow;

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
        UIOptionsController optionsController = GetComponentInChildren<UIOptionsController>();

        if (optionsController != null)
            optionsController.OnWindowClosed += HandleOptionsWindowClosed;
        else
            Debug.LogWarning($"{nameof(UIOptionsController)} not found in {nameof(gameObject.name)}'s children");
    }

    #region Event Handling
    private void HandleOptionsWindowClosed(object sender, System.EventArgs e) => MainButtonsWindow.SetActive(true);
    #endregion

    #region Main Buttons
    private void InitMainButtons()
    {
        StartGameButton.onClick.AddListener(StartGame);
        HowToPlayButton.onClick.AddListener(OpenHowToPlayWindow);
        OptionsButton.onClick.AddListener(OpenOptionsWindow);
        QuitButton.onClick.AddListener(QuitGame);
    }
    private void UnSubMainButtons()
    {
        StartGameButton.onClick.RemoveListener(StartGame);
        HowToPlayButton.onClick.RemoveListener(OpenHowToPlayWindow);
        OptionsButton.onClick.RemoveListener(OpenOptionsWindow);
        QuitButton.onClick.RemoveListener(QuitGame);
    }

    private void StartGame()
    {
        if (string.IsNullOrEmpty(GameSceneName))
        {
            Debug.LogError($"{nameof(GameSceneName)} is not set");
            return;
        }

        SceneManager.LoadScene(GameSceneName, LoadSceneMode.Single);
    }
    private void OpenHowToPlayWindow()
    {
        HowToPlayWindow.SetActive(true);
        MainButtonsWindow.SetActive(false);
    }
    private void OpenOptionsWindow()
    {
        OptionsWindow.SetActive(true);
        MainButtonsWindow.SetActive(false);
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
        HowToPlayWindow.SetActive(false);
        HowToPlayExitButton.onClick.AddListener(CloseHowToPlayWindow);
    }
    private void UnSubHowToPlayButtons() => HowToPlayExitButton.onClick.RemoveListener(CloseHowToPlayWindow);

    private void CloseHowToPlayWindow()
    {
        HowToPlayWindow.SetActive(false);
        MainButtonsWindow.SetActive(true);
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
