using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModes : MonoBehaviour
{
    #region VARIABLES

    [Header("PLATFORMS")]
    public bool IsAndroid;

    [Header("GAME MODES")]
    GameCursors Curs;

    [Header("MUSIC")]
    MusicManager Music;

    #endregion

    #region UNITY

    public void Awake()
    {
        Music = FindObjectOfType<MusicManager>();

        if (IsAndroid == false)
        {
            if (PlayerPrefs.GetInt("GameDifficuilty") != 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("GameDifficuilty") != 0)
            {
                SceneManager.LoadScene("MainMenuAndroid");

            }
        }
    }

    public void Start()
    {
        if (IsAndroid == false)
        {
            Curs = FindObjectOfType<GameCursors>();
            Curs.Access = 1;
        }
    }

    #endregion

    #region BEHAVIOURS

    public void Modes(int Which)
    {
        StartCoroutine(Levels(Which));
    }

    public void Buttons()
    {
        StartCoroutine(MenuUI());
    }

    public IEnumerator Levels(int Which)
    {
        yield return new WaitForSeconds(0.25f);

        if (IsAndroid == false)
        {
            if (PlayerPrefs.GetInt("GameDifficuilty") == 0)
            {
                if (Which == 1)
                {
                    Music.ButtonsUI(1);
                    PlayerPrefs.SetInt("GameDifficuilty", 1);
                    PlayerPrefs.SetInt("GameLevel", 1);
                    SceneManager.LoadScene("MainMenu");
                }
                else if (Which == 2)
                {
                    Music.ButtonsUI(1);
                    PlayerPrefs.SetInt("GameDifficuilty", 2);
                    PlayerPrefs.SetInt("GameLevel", 1);
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("GameDifficuilty") == 0)
            {
                if (Which == 1)
                {
                    Music.ButtonsUI(1);
                    PlayerPrefs.SetInt("GameDifficuilty", 1);
                    PlayerPrefs.SetInt("GameLevel", 1);
                    SceneManager.LoadScene("MainMenuAndroid");
                }
                else if (Which == 2)
                {
                    Music.ButtonsUI(1);
                    PlayerPrefs.SetInt("GameDifficuilty", 2);
                    PlayerPrefs.SetInt("GameLevel", 1);
                    SceneManager.LoadScene("MainMenuAndroid");
                }
            }
        }
    }

    public IEnumerator MenuUI()
    {
        yield return new WaitForSeconds(0.25f);

        if (IsAndroid == false)
        {
            Music.ButtonsUI(1);
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Music.ButtonsUI(1);
            SceneManager.LoadScene("MainMenuAndroid");
        }
    }

    #endregion
}
