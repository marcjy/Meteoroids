using System;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionsController : MonoBehaviour
{
    public event EventHandler OnWindowClosed;

    [SerializeField] private Slider _BGMSlider;
    [SerializeField] private Slider _SFXSlider;
    [SerializeField] private Button _exitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitSliders();
        _exitButton.onClick.AddListener(() => CloseOptionsWindow());

        gameObject.SetActive(false);
    }

    private void InitSliders()
    {
        _BGMSlider.maxValue = AudioManager.MAX_VOLUME;
        _BGMSlider.minValue = AudioManager.MIN_VOLUME;

        _SFXSlider.maxValue = AudioManager.MAX_VOLUME;
        _SFXSlider.minValue = AudioManager.MIN_VOLUME;

        _BGMSlider.value = AudioManager.GetVolume(AudioManager.AudioChannel.BGM);
        _SFXSlider.value = AudioManager.GetVolume(AudioManager.AudioChannel.SFX);

        _BGMSlider.onValueChanged.AddListener((value) => AudioManager.SetVolume(AudioManager.AudioChannel.BGM, value));
        _SFXSlider.onValueChanged.AddListener((value) => AudioManager.SetVolume(AudioManager.AudioChannel.SFX, value));
    }
    private void CloseOptionsWindow()
    {
        OnWindowClosed?.Invoke(this, EventArgs.Empty);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        UnSubSliders();
        _exitButton.onClick.RemoveAllListeners();
    }
    private void UnSubSliders()
    {
        _BGMSlider.onValueChanged.RemoveAllListeners();
        _SFXSlider.onValueChanged.RemoveAllListeners();
    }

}
