using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [Header("MIXERS")]
    public AudioMixer MusicMixer;
    public AudioMixer GameSFX;
    public AudioMixer GameUI;

    [Header("MENU MUSIC")]
    public GameObject MenuMusicHolder;
    public AudioSource MusicA;
    public AudioSource MusicB;
    public AudioSource MusicC;

    [Header("GAME MUSIC")]
    public GameObject GameMusicHolder;
    public AudioSource GameA;
    public AudioSource GameB;
    public AudioSource GameC;
    [HideInInspector] public bool GameMusics;

    [Header("GAME UI")]
    public AudioSource ButtonsA;
    public AudioSource ButtonsB;
    public AudioSource ButtonsC;

    private void Awake()
    {
        GameObject[] MusicObjects = GameObject.FindGameObjectsWithTag("SoundManager");

        if (MusicObjects.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        MusicMixer.SetFloat("GameMusic", Mathf.Log10(PlayerPrefs.GetFloat("GameMusic")) * 20);
        GameSFX.SetFloat("GameSFX", Mathf.Log10(PlayerPrefs.GetFloat("GameSFX")) * 20);
        GameUI.SetFloat("GameUI", Mathf.Log10(PlayerPrefs.GetFloat("GameUI")) * 20);
    }

    private void Update()
    {
        GameMenu();
    }

    public void GameMenu()
    {
        if (GameMusics == true)
        {
            GameMusicHolder.SetActive(true);
            MenuMusicHolder.SetActive(false);
        }
        else if (GameMusics == false)
        {
            GameMusicHolder.SetActive(false);
            MenuMusicHolder.SetActive(true);
        }
    }

    public IEnumerator MenuMusic(int MenuCurrentLoop)
    {
        if (MenuCurrentLoop == 1)
        {
            MenuMusicHolder.SetActive(true);

            MusicB.Stop();
            MusicA.Stop();

            MusicC.Play();

            yield return new WaitForSeconds(0.5f);

            MusicA.Stop();

            MusicB.Play();

            yield return new WaitForSeconds(MusicB.clip.length);

            MusicB.Stop();
            MusicC.Stop();

            MusicA.Play();

            yield return new WaitForSeconds(MusicA.clip.length);

            StartCoroutine(MenuMusic(MenuCurrentLoop));
        }
        else if (MenuCurrentLoop == 2)
        {
            MenuMusicHolder.SetActive(true);

            MusicB.Stop();
            MusicA.Stop();

            MusicC.Play();

            yield return new WaitForSeconds(0.5f);

            MusicB.Stop();

            MusicA.Play();

            yield return new WaitForSeconds(MusicA.clip.length);

            MusicA.Stop();
            MusicC.Stop();

            MusicB.Play();

            yield return new WaitForSeconds(MusicB.clip.length);

            StartCoroutine(MenuMusic(MenuCurrentLoop));
        }
    }

    public IEnumerator GameMusic(int GameCurrentLoop)
    {
        if (GameCurrentLoop == 1)
        {
            GameMusicHolder.SetActive(true);

            GameC.Stop();
            GameB.Stop();

            GameA.Play();

            yield return new WaitForSeconds(GameA.clip.length);

            GameC.Stop();
            GameA.Stop();

            GameB.Play();

            yield return new WaitForSeconds(GameB.clip.length);

            GameA.Stop();
            GameB.Stop();

            GameC.Play();

            yield return new WaitForSeconds(GameC.clip.length);

            StartCoroutine(GameMusic(GameCurrentLoop));
        }
        else if(GameCurrentLoop == 2)
        {
            GameMusicHolder.SetActive(true);

            GameC.Stop();
            GameA.Stop();

            GameB.Play();

            yield return new WaitForSeconds(GameB.clip.length);

            GameC.Stop();
            GameB.Stop();

            GameA.Play();

            yield return new WaitForSeconds(GameA.clip.length);

            GameA.Stop();
            GameB.Stop();

            GameC.Play();

            yield return new WaitForSeconds(GameC.clip.length);

            StartCoroutine(GameMusic(GameCurrentLoop));
        }
        else if (GameCurrentLoop == 3)
        {
            GameMusicHolder.SetActive(true);

            GameA.Stop();
            GameB.Stop();

            GameC.Play();

            yield return new WaitForSeconds(GameC.clip.length);

            GameA.Stop();
            GameC.Stop();

            GameB.Play();

            yield return new WaitForSeconds(GameB.clip.length);

            GameB.Stop();
            GameC.Stop();

            GameA.Play();

            yield return new WaitForSeconds(GameA.clip.length);

            StartCoroutine(GameMusic(GameCurrentLoop));
        }
    }

    public void ButtonsUI(int Which)
    {
        if (Which == 1)
        {
            ButtonsA.Play();
        }
        else if (Which == 2)
        {
            ButtonsB.Play();
        }
        else if (Which == 3)
        {
            ButtonsC.Play();
        }
    }
}
