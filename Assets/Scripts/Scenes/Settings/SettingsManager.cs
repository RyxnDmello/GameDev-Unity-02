using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    #region VARIABLES

    [Header("PLATFORMS")]
    public bool IsAndroid;

    [Header("SCROLL")]
    public Scrollbar Scroll;

    [Header("AQUIRED COMPONENTS")]
    Visuals Vis;
    GameCursors Curs;

    [Header("MUSIC")]
    public AudioMixer MusicMixer;
    public Slider MusicSlider;
    [Space(5)]
    public AudioMixer GameSFX;
    public Slider SFXSlider;
    [Space(5)]
    public AudioMixer GameUI;
    public Slider UISlider;
    [Space(2)]
    MusicManager Music;

    [Header("HIGH SCORE")]
    public GameObject LineA;
    public GameObject LineB;
    public TMP_Text High;

    #endregion

    #region UNITY
    private void Awake()
    {
        Music = FindObjectOfType<MusicManager>();
    }

    private void Start()
    {
        Vis = FindObjectOfType<Visuals>();

        if (IsAndroid == false)
        {
            Curs = FindObjectOfType<GameCursors>();
            Curs.Access = 1;
        }

        Scroll.value = 1;
        HighScore();

        MusicMixer.SetFloat("GameMusic", Mathf.Log10(PlayerPrefs.GetFloat("GameMusic")) * 20);
        GameSFX.SetFloat("GameSFX", Mathf.Log10(PlayerPrefs.GetFloat("GameSFX")) * 20);
        GameUI.SetFloat("GameUI", Mathf.Log10(PlayerPrefs.GetFloat("GameUI")) * 20);

        MusicSlider.value = PlayerPrefs.GetFloat("GameMusic");
        SFXSlider.value = PlayerPrefs.GetFloat("GameSFX");
        UISlider.value = PlayerPrefs.GetFloat("GameUI");
    }

    #endregion

    #region MANAGER

    public void GameReset()
    {
        StartCoroutine(ResetWholeGame());
    }

    private IEnumerator ResetWholeGame()
    {
        yield return new WaitForSeconds(0.25f);

        Music.ButtonsUI(1);

        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetInt("GameDifficuilty", 0);
        PlayerPrefs.SetInt("GameLevel", 0);

        PlayerPrefs.SetFloat("GameMusic", 1);
        PlayerPrefs.SetFloat("GameSFX", 1);
        PlayerPrefs.SetFloat("GameUI", 1);

        Debug.Log("PLAYER POINTS: " + PlayerPrefs.GetInt("HighScore"));
        Debug.Log("GAME DIFFICUILTY: " + PlayerPrefs.GetInt("GameDifficuilty"));
        Debug.Log("GAME LEVEL: " + PlayerPrefs.GetInt("GameLevel"));


        if (IsAndroid == false)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("MainMenuAndroid");
        }
    }

    public void Buttons(int Which)
    {
        StartCoroutine(MenuUI(Which));
    }

    public IEnumerator MenuUI(int Which)
    {
        yield return new WaitForSeconds(0.25f);

        if (IsAndroid == false)
        {
            if (Which == 1)
            {
                Music.ButtonsUI(1);
                SceneManager.LoadScene("MainMenu");
            }
            else if (Which == 2)
            {
                Music.ButtonsUI(1);
                SceneManager.LoadScene("Controls");
            }
            else if (Which == 3)
            {
                Music.ButtonsUI(3);
                Vis.ActivateEffects();
            }
        }
        else
        {
            if (Which == 1)
            {
                Music.ButtonsUI(1);
                SceneManager.LoadScene("MainMenuAndroid");
            }
            else if (Which == 2)
            {
                Music.ButtonsUI(1);
                SceneManager.LoadScene("ControlsAndroid");
            }
            else if (Which == 3)
            {
                Music.ButtonsUI(3);
                Vis.ActivateEffects();
            }
        }
    }

    public void HighScore()
    {
        if(PlayerPrefs.GetInt("HighScore") > 0)
        {
            High.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("HighScore");

            LineA.SetActive(true);
            LineB.SetActive(true);
        }
        else
        {
            High.text = " ";

            LineA.SetActive(false);
            LineB.SetActive(false);
        }
    }

    public void SetVolumeMusic(float Value)
    {
        PlayerPrefs.SetFloat("GameMusic", Value);

        MusicMixer.SetFloat("GameMusic", Mathf.Log10 ( PlayerPrefs.GetFloat("GameMusic") ) * 20);

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

    #endregion
}
