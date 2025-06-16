using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public static class AudioManager
{
    public const float MAX_VOLUME = 20;
    public const float MIN_VOLUME = -80;

    public enum AudioChannel { BGM, SFX }

    private static AudioSource _BGM;
    private static AudioSource _SFX;
    private static AudioMixer _audioMixer;

    private static AudioLibrary _audioLibrary;

    public static void Init(AudioLibrary audioLibrary)
    {
        _audioLibrary = audioLibrary;

        _BGM = new GameObject("BGMPlayer").AddComponent<AudioSource>();
        _SFX = new GameObject("SFXPlayer").AddComponent<AudioSource>();
        GameObject.DontDestroyOnLoad(_BGM);
        GameObject.DontDestroyOnLoad(_SFX);

        _audioMixer = Resources.FindObjectsOfTypeAll<AudioMixer>().FirstOrDefault();

        AudioMixerGroup[] groups = Resources.FindObjectsOfTypeAll(typeof(AudioMixerGroup)) as AudioMixerGroup[];
        _BGM.outputAudioMixerGroup = groups.FirstOrDefault(g => g.name == "BGM");
        _SFX.outputAudioMixerGroup = groups.FirstOrDefault(g => g.name == "SFX");

        PlayMusic(_audioLibrary.MainMenuTheme);
    }

    public static void PlayMusic(AudioClip audioClip, bool enableLoop = true)
    {
        _BGM.clip = audioClip;
        _BGM.loop = enableLoop;

        _BGM.Play();
    }
    public static void PlaySFX(AudioClip audioClip)
    {
        _SFX.PlayOneShot(audioClip);
    }

    public static void SetVolume(AudioChannel channel, float volume)
    {
        switch (channel)
        {
            case AudioChannel.BGM:
                _audioMixer.SetFloat("BGMVolume", volume);
                break;
            case AudioChannel.SFX:
                _audioMixer.SetFloat("SFXVolume", volume);
                break;
        }
    }
    public static float GetVolume(AudioChannel channel)
    {
        float volume = 0.0f;

        switch (channel)
        {
            case AudioChannel.BGM:
                _audioMixer.GetFloat("BGMVolume", out volume);
                break;
            case AudioChannel.SFX:
                _audioMixer.GetFloat("SFXVolume", out volume);
                break;
        }
        return volume;
    }
}