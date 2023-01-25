using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlsManager : MonoBehaviour
{
    #region VARIABLES

    [Header("PLATFORMS")]
    public bool IsAndroid;

    [Header("SCROLL")]
    public Scrollbar Scroll;

    [Header("AQUIRED COMPOENENTS")]
    GameCursors Curs;

    [Header("MUSIC")]
    MusicManager Music;

    #endregion

    #region UNITY

    private void Awake()
    {
        Music = FindObjectOfType<MusicManager>();
    }

    private void Start()
    {
        if (IsAndroid == false)
        {
            Curs = FindObjectOfType<GameCursors>();
            Curs.Access = 1;
        }

        Scroll.value = 1;
    }

    #endregion

    #region FUNCTIONS

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
                SceneManager.LoadScene("Objectives");
            }
            else if (Which == 3)
            {
                Music.ButtonsUI(1);
                SceneManager.LoadScene("Settings");
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
                SceneManager.LoadScene("ObjectivesAndroid");
            }
            else if (Which == 3)
            {
                Music.ButtonsUI(1);
                SceneManager.LoadScene("SettingsAndroid");
            }
        }
    }

    #endregion
}
