using System;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionsController : MonoBehaviour
{
    public event EventHandler OnWindowClosed;

    public Slider BGMSlider;
    public Slider SFXSlider;
    public Button ExitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitSliders();
        ExitButton.onClick.AddListener(() => CloseOptionsWindow());

        gameObject.SetActive(false);
    }

    private void InitSliders()
    {
        BGMSlider.maxValue = AudioManager.MAX_VOLUME;
        BGMSlider.minValue = AudioManager.MIN_VOLUME;

        SFXSlider.maxValue = AudioManager.MAX_VOLUME;
        SFXSlider.minValue = AudioManager.MIN_VOLUME;

        BGMSlider.value = AudioManager.GetVolume(AudioManager.AudioChannel.BGM);
        SFXSlider.value = AudioManager.GetVolume(AudioManager.AudioChannel.SFX);

        BGMSlider.onValueChanged.AddListener((value) => AudioManager.SetVolume(AudioManager.AudioChannel.BGM, value));
        SFXSlider.onValueChanged.AddListener((value) => AudioManager.SetVolume(AudioManager.AudioChannel.SFX, value));
    }
    private void CloseOptionsWindow()
    {
        OnWindowClosed?.Invoke(this, EventArgs.Empty);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        UnSubSliders();
        ExitButton.onClick.RemoveAllListeners();
    }
    private void UnSubSliders()
    {
        BGMSlider.onValueChanged.RemoveAllListeners();
        SFXSlider.onValueChanged.RemoveAllListeners();
    }

}
