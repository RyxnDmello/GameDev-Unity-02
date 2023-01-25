using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainManager : MonoBehaviour
{
    #region VARIABLES

    [Header("PLATFORMS")]
    public bool IsAndroid;

    [Header("VISUALS")]
    [SerializeField] Button Visuals;
    [SerializeField] Sprite OpenEye;
    [SerializeField] Sprite ClosedEye;

    [Header("AQUIRED COMPONENTS")]
    Visuals Vis;
    GameCursors Curs;

    [Header("MUSIC")]
    public AudioMixer MusicMixer;
    public AudioMixer GameSFX;
    public AudioMixer GameUI;
    MusicManager Music;

    #endregion

    #region UNITY

    private void Awake()
    {
        Music = FindObjectOfType<MusicManager>();

        StartCoroutine(Music.MenuMusic(Random.Range(1, 3)));
        Music.GameMusics = false;

        if (IsAndroid == false)
        {
            PlayerPrefs.SetInt("Visuals", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Visuals", 0);
        }
    }

    private void Start()
    {
        Vis = FindObjectOfType<Visuals>();

        if (IsAndroid == false)
        {
            Curs = FindObjectOfType<GameCursors>();
            Curs.Access = 1;
        }

        Time.timeScale = 1f;

        MusicMixer.SetFloat("GameMusic", Mathf.Log10(PlayerPrefs.GetFloat("GameMusic")) * 20);
        GameSFX.SetFloat("GameSFX", Mathf.Log10(PlayerPrefs.GetFloat("GameSFX")) * 20);
        GameUI.SetFloat("GameUI", Mathf.Log10(PlayerPrefs.GetFloat("GameUI")) * 20);
    }

    private void Update()
    {
        VisualsKey();
    }

    #endregion

    #region UI

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
                if (PlayerPrefs.GetInt("GameDifficuilty") == 0)
                {
                    Music.ButtonsUI(1);
                    SceneManager.LoadScene("Level");
                }
                else
                {
                    Music.ButtonsUI(1);
                    SceneManager.LoadScene("GamePlay");
                }
            }
            else if (Which == 2)
            {
                Music.ButtonsUI(1);
                SceneManager.LoadScene("Controls");
            }
            else if (Which == 3)
            {
                Music.ButtonsUI(1);
                SceneManager.LoadScene("Settings");
            }
            else if (Which == 4)
            {
                Music.ButtonsUI(2);
                Debug.Log("QUIT");
                Application.Quit();
            }
            else if (Which == 5)
            {
                Music.ButtonsUI(3);
                Vis.ActivateEffects();
            }
        }
        else
        {
            if (Which == 1)
            {
                if (PlayerPrefs.GetInt("GameDifficuilty") == 0)
                {
                    Music.ButtonsUI(1);
                    SceneManager.LoadScene("LevelAndroid");
                }
                else
                {
                    Music.ButtonsUI(1);
                    SceneManager.LoadScene("GamePlayAndroid");
                }
            }
            else if (Which == 2)
            {
                Music.ButtonsUI(1);
                SceneManager.LoadScene("ControlsAndroid");
            }
            else if (Which == 3)
            {
                Music.ButtonsUI(1);
                SceneManager.LoadScene("SettingsAndroid");
            }
            else if (Which == 4)
            {
                Music.ButtonsUI(2);
                Debug.Log("QUIT");
                Application.Quit();
            }
            else if (Which == 5)
            {
                Music.ButtonsUI(3);
                Vis.ActivateEffects();
            }
        }
    }

    public void VisualsKey()
    {
        if (PlayerPrefs.GetInt("Visuals") == 0)
        {
            Visuals.image.sprite = ClosedEye;
        }
        else if (PlayerPrefs.GetInt("Visuals") == 1)
        {
            Visuals.image.sprite = OpenEye;
        }
    }

    #endregion
}
