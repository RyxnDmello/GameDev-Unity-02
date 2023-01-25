using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameSettings : MonoBehaviour
{
    [Header("MIXERS")]
    [Header("MUSIC")]
    public AudioMixer MusicMixer;
    public Slider MusicSlider;
    [Space(5)]
    public AudioMixer GameSFX;
    public Slider SFXSlider;
    [Space(5)]
    public AudioMixer GameUI;
    public Slider UISlider;

    private void Start()
    {
        MusicMixer.SetFloat("GameMusic", Mathf.Log10(PlayerPrefs.GetFloat("GameMusic")) * 20);
        GameSFX.SetFloat("GameSFX", Mathf.Log10(PlayerPrefs.GetFloat("GameSFX")) * 20);
        GameUI.SetFloat("GameUI", Mathf.Log10(PlayerPrefs.GetFloat("GameUI")) * 20);

        MusicSlider.value = PlayerPrefs.GetFloat("GameMusic");
        SFXSlider.value = PlayerPrefs.GetFloat("GameSFX");
        UISlider.value = PlayerPrefs.GetFloat("GameUI");
    }

    public void SetVolumeMusic(float Value)
    {
        PlayerPrefs.SetFloat("GameMusic", Value);

        MusicMixer.SetFloat("GameMusic", Mathf.Log10(PlayerPrefs.GetFloat("GameMusic")) * 20);

        PlayerPrefs.SetFloat("GameMusic", Value);
    }

    public void SetVolumeSFX(float Value)
    {
        PlayerPrefs.SetFloat("GameSFX", Value);

        GameSFX.SetFloat("GameSFX", Mathf.Log10(PlayerPrefs.GetFloat("GameSFX")) * 20);

        PlayerPrefs.SetFloat("GameSFX", Value);
    }

    public void SetVolumeUI(float Value)
    {
        PlayerPrefs.SetFloat("GameUI", Value);

        GameUI.SetFloat("GameUI", Mathf.Log10(PlayerPrefs.GetFloat("GameUI")) * 20);

        PlayerPrefs.SetFloat("GameUI", Value);
    }
}
