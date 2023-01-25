using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Visuals : MonoBehaviour
{
    [Header("VISUALS")]
    ChromaticAberration Chrome;
    MotionBlur Motion;

    [Header("AQUIRED COMPONENTS")]
    MusicManager Music;
    Volume Vol;

    private void Start()
    {
        Vol = GetComponent<Volume>();
        Music = FindObjectOfType<MusicManager>();

        ChromaticAberration Chro;
        MotionBlur Mo;

        if (PlayerPrefs.GetInt("Visuals") == 0)
        {
            Vol.enabled = false;
        }
        else if(PlayerPrefs.GetInt("Visuals") == 1)
        {
            Vol.enabled = true;
        }
        
        if (Vol.profile.TryGet<ChromaticAberration>(out Chro))
        {
            Chrome = Chro;
        }

        if (Vol.profile.TryGet<MotionBlur>(out Mo))
        {
            Motion = Mo;
        }
    }

    private void Update()
    {
        Activate();

        StartCoroutine(QuitGame());
    }

    public IEnumerator HitEffect()
    {
        Chrome.active = true;
        Motion.active = true;

        yield return new WaitForSecondsRealtime(1);

        Chrome.active = false;
        Motion.active = false;
    }

    private void Activate()
    {
        if (Input.GetKeyDown(KeyCode.G) && PlayerPrefs.GetInt("Visuals") == 0)
        {
            PlayerPrefs.SetInt("Visuals", 1);
            Music.ButtonsUI(3);
            Vol.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.G) && PlayerPrefs.GetInt("Visuals") == 1)
        {
            PlayerPrefs.SetInt("Visuals", 0);
            Music.ButtonsUI(3);
            Vol.enabled = false;
        }
    }

    public void ActivateEffects()
    {
        if (PlayerPrefs.GetInt("Visuals") == 0)
        {
            PlayerPrefs.SetInt("Visuals", 1);
            Vol.enabled = true;
        }
        else if (PlayerPrefs.GetInt("Visuals") == 1)
        {
            PlayerPrefs.SetInt("Visuals", 0);
            Vol.enabled = false;
        }
    }

    public IEnumerator QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
            Music.ButtonsUI(2);

            yield return new WaitForSeconds(1.5f);
            Debug.Log("QUIT");
            Application.Quit();
        }
    }
}
