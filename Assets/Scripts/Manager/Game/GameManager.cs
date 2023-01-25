using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region VARIABLES

    [Header("PLATFORMS")]
    public bool IsWindows;

    [Header("GAME MANAGER")]
    [HideInInspector]
    public bool Active;
    public GameObject Gren;
    public Animator Anim;

    [Header("LEVELS")]
    public Animator Levels;
    public TMP_Text LevelsText;
    int LevelCount;

    [Header("GAME SCORE")]
    [SerializeField] TMP_Text Score;

    [Header("PAUSE GAME")]
    [SerializeField] GameObject PauseCanvas;
    [SerializeField] GameObject SettingsCanvas;
    [SerializeField] Animator Jumpy;
    [SerializeField] TMP_Text ScoreP;
    [SerializeField] TMP_Text HighP;
    [HideInInspector] public bool Pause;
    [HideInInspector] public bool Settings;

    [Header("PLAYER HEALTH")]
    [SerializeField] Image HealthRing;
    [SerializeField] TMP_Text Health;
    private Color HealthColor;
    private float MaxHealth;
    private float LerpSpeed;

    [Header("BOSS HEALTH")]
    [SerializeField] Image HealthBar;
    [SerializeField] TMP_Text BossName;
    private float BMaxHealth;
    private float BLerpSpeed;

    [Header("PLAYER AMMO")]
    [SerializeField] Image[] Shots;
    [SerializeField] Image[] Types;
    [SerializeField] TMP_Text Weapon;
    [SerializeField] RectTransform[] WepsAmmoImage;
    [SerializeField] 
    public GameObject PlayerAmmoChoice;
    [HideInInspector]
    public bool IsPlayerAmmoChoice;
    [HideInInspector]
    public float TimeToChoice;
    [SerializeField] Image AmmoAndroidRing;

    [Header("PLAYER DEATH")]
    [SerializeField] GameObject DeathTransition;

    [Header("MUSIC")]
    public AudioMixer MusicMixer;
    public AudioMixer GameSFX;
    public AudioMixer GameUI;
    MusicManager Music;

    [Header("AQUIRED COMPONENTS")]
    public Animator BossUI;
    GameCursors Curs;
    Visuals Vis;
    Player Play;

    #endregion

    #region UNITY

    private void Awake()
    {
        Music = FindObjectOfType<MusicManager>();

        StartCoroutine(Music.GameMusic(Random.Range(1, 4)));
        Music.GameMusics = true;
    }

    private void Start()
    {
        Play = FindObjectOfType<Player>();
        Vis = FindObjectOfType<Visuals>();
        Curs = FindObjectOfType<GameCursors>();

        SetLevelCount();
        Active = true;

        LEVELONE();
        LEVELTWO();
        LEVELTHREE();

        MusicMixer.SetFloat("GameMusic", Mathf.Log10(PlayerPrefs.GetFloat("GameMusic")) * 20);
        GameSFX.SetFloat("GameSFX", Mathf.Log10(PlayerPrefs.GetFloat("GameSFX")) * 20);
        GameUI.SetFloat("GameUI", Mathf.Log10(PlayerPrefs.GetFloat("GameUI")) * 20);

        Debug.Log("GAME DIFFICUILTY: " + PlayerPrefs.GetInt("GameDifficuilty"));
        Debug.Log("GAME LEVEL: " + PlayerPrefs.GetInt("GameLevel"));

        if (IsWindows == true)
        {
            Gren.SetActive(false);
            IsPlayerAmmoChoice = false;
            PlayerAmmoChoice.SetActive(false);
            TimeToChoice = 10f;

            WepsAmmoImage = null;
        }
        else
        {
            WepsAmmoImage[0].localScale = new Vector2(1f, 1f);
        }

        Pause = false;
        Settings = false;

        MaxHealth = 100;
        LerpSpeed = 2.5f;

        BMaxHealth = 500;
        BLerpSpeed = 3f;
    }

    private void Update()
    {
        LevelDisplay();
        GameScore();

        PlayerHealth();
        PlayerAmmoChoices();
        AmmoManager();

        PauseGame();
        Cursors();

        StartCoroutine(PlayerDeath());
        StartCoroutine(Retry());

        if(IsWindows == false)
        {
            AmmoAndroidRings();
        }
    }

    #endregion

    #region LEVELS

    public void LEVELONE()
    {
        if (PlayerPrefs.GetInt("GameLevel") == 1)
        {
            if (PlayerPrefs.GetInt("GameDifficuilty") == 1)
            {
                if (PlayerPrefs.GetInt("HighScore") >= 0 && PlayerPrefs.GetInt("HighScore") < 500)
                {
                    Play.AmmoTwo = 4;
                    Play.AmmoThree = 4;
                    Play.AmmoFour = 0;
                    Play.AmmoFive = 0;
                    Play.Points = 0;
                }
                else if (PlayerPrefs.GetInt("HighScore") >= 500 && PlayerPrefs.GetInt("HighScore") < 1000)
                {
                    Play.Points = 500;
                }
                else if (PlayerPrefs.GetInt("HighScore") >= 1000 && PlayerPrefs.GetInt("HighScore") < 1500)
                {
                    Play.Points = 1000;
                }
                else if (PlayerPrefs.GetInt("HighScore") >= 1500 && PlayerPrefs.GetInt("HighScore") < 2000)
                {
                    Play.Points = 1500;
                }
                else if (PlayerPrefs.GetInt("HighScore") >= 2000)
                {
                    Play.AmmoTwo = 4;
                    Play.AmmoThree = 4;

                    Play.Points = 2000;
                }
            }
            else if (PlayerPrefs.GetInt("GameDifficuilty") == 2)
            {
                if (PlayerPrefs.GetInt("HighScore") >= 2000)
                {
                    Play.AmmoTwo = 4;
                    Play.AmmoThree = 4;

                    Play.Points = 2000;
                }
                else
                {
                    Play.AmmoTwo = 4;
                    Play.AmmoThree = 4;
                    Play.AmmoFour = 0;
                    Play.AmmoFive = 0;

                    Play.Points = 0;
                }
            }
        }
    }

    public void LEVELTWO()
    {
        if (PlayerPrefs.GetInt("GameLevel") == 2)
        {
            if (PlayerPrefs.GetInt("GameDifficuilty") == 1)
            {
                if (PlayerPrefs.GetInt("HighScore") >= 2000 && PlayerPrefs.GetInt("HighScore") < 2500)
                {
                    Play.AmmoTwo = 4;
                    Play.AmmoThree = 4;
                    Play.AmmoFour = 1;
                    Play.AmmoFive = 1;
                    Play.Points = 2000;
                }
                else if (PlayerPrefs.GetInt("HighScore") >= 2500 && PlayerPrefs.GetInt("HighScore") < 3000)
                {
                    Play.Points = 2500;
                }
                else if (PlayerPrefs.GetInt("HighScore") >= 3000)
                {
                    Play.AmmoTwo = 4;
                    Play.AmmoThree = 4;

                    Play.Points = 3000;
                }
            }
            else if (PlayerPrefs.GetInt("GameDifficuilty") == 2)
            {
                if (PlayerPrefs.GetInt("HighScore") >= 3000)
                {
                    Play.AmmoTwo = 4;
                    Play.AmmoThree = 4;

                    Play.Points = 3000;
                }
                else
                {
                    Play.AmmoTwo = 4;
                    Play.AmmoThree = 4;
                    Play.AmmoFour = 1;
                    Play.AmmoFive = 1;

                    Play.Points = 2000;
                }
            }
        }
    }

    public void LEVELTHREE()
    {
        if (PlayerPrefs.GetInt("GameLevel") == 3)
        {
            if (PlayerPrefs.GetInt("GameDifficuilty") == 1)
            {
                if (PlayerPrefs.GetInt("HighScore") >= 3000 && PlayerPrefs.GetInt("HighScore") < 3500)
                {
                    Play.AmmoTwo = 4;
                    Play.AmmoThree = 4;
                    Play.AmmoFour = 1;
                    Play.AmmoFive = 1;
                    Play.Points = 3000;
                }
                else if (PlayerPrefs.GetInt("HighScore") >= 3500 && PlayerPrefs.GetInt("HighScore") < 4000)
                {
                    Play.Points = 3500;
                }
                else if (PlayerPrefs.GetInt("HighScore") >= 4000 && PlayerPrefs.GetInt("HighScore") < 4500)
                {
                    Play.Points = 4000;
                }
                else if (PlayerPrefs.GetInt("HighScore") >= 4500 && PlayerPrefs.GetInt("HighScore") < 5000)
                {
                    Play.Points = 4500;
                }
                else if (PlayerPrefs.GetInt("HighScore") >= 5000 && PlayerPrefs.GetInt("HighScore") < 5500)
                {
                    Play.AmmoTwo = 4;
                    Play.AmmoThree = 4;

                    Play.Points = 5000;
                }
                else if (PlayerPrefs.GetInt("HighScore") >= 5500 && PlayerPrefs.GetInt("HighScore") < 6000)
                {
                    Play.Points = 5500;
                }
                else if (PlayerPrefs.GetInt("HighScore") >= 6000)
                {
                    Play.AmmoTwo = 4;
                    Play.AmmoThree = 4;

                    Play.Points = 6000;
                }
            }
            else if (PlayerPrefs.GetInt("GameDifficuilty") == 2)
            {
                if (PlayerPrefs.GetInt("HighScore") >= 6000)
                {
                    Play.AmmoTwo = 4;
                    Play.AmmoThree = 4;

                    Play.Points = 6000;
                }
                else
                {
                    Play.AmmoTwo = 4;
                    Play.AmmoThree = 4;
                    Play.AmmoFour = 1;
                    Play.AmmoFive = 1;

                    Play.Points = 3000;
                }
            }
        }
    }

    public void LevelDisplay()
    {
        if (PlayerPrefs.GetInt("GameLevel") == 1 && LevelCount == 1)
        {
            LevelsText.text = "LEVEL ONE";
            Levels.SetTrigger("LEVELS");

            LevelCount = 2;
        }
        else if(PlayerPrefs.GetInt("GameLevel") == 2 && LevelCount == 2)
        {
            LevelsText.text = "LEVEL TWO";
            Levels.SetTrigger("LEVELS");

            LevelCount = 3;
        }
        else if(PlayerPrefs.GetInt("GameLevel") == 3 && LevelCount == 3)
        {
            LevelsText.text = "LEVEL THREE";
            Levels.SetTrigger("LEVELS");

            LevelCount = 4;
        }
    }

    public void SetLevelCount()
    {
        if (PlayerPrefs.GetInt("GameLevel") == 1)
        {
            LevelCount = 1;
        }
        else if (PlayerPrefs.GetInt("GameLevel") == 2)
        {
             LevelCount = 2;
        }
        else if (PlayerPrefs.GetInt("GameLevel") == 3)
        {
            LevelCount = 3;
        }
    }

    #endregion

    #region PLAYER AND BOSS

    private void GameScore()
    {
        Score.text = Play.Points.ToString();

        if (Play.Points > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", Play.Points);
        }

        if (Pause == true)
        {
            ScoreP.text = Play.Points.ToString();
            HighP.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore").ToString();
        }
    }

    public void PlayerScoreUIJumpy()
    {
        Jumpy.SetTrigger("PlayerUIJumpy");
    }

    private void PlayerHealth()
    {
        HealthRing.fillAmount = Mathf.Lerp(HealthRing.fillAmount, (Play.Health / MaxHealth), LerpSpeed * Time.deltaTime);

        HealthColor = Color.Lerp(Color.red, Color.green, ((Play.Health / MaxHealth)));
        HealthRing.color = HealthColor;

        if (Play.Health >= 0)
        {
            Health.text = Play.Health.ToString();
        }
        else
        {
            Health.text = "";
        }
    }

    public IEnumerator PlayerDeath()
    {
        if (Play.Health <= 0)
        {
            Active = false;

            yield return new WaitForSecondsRealtime(3);

            DeathTransition.SetActive(true);
        }
    }

    public void BossHealth(float Health)
    {
        HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, (Health / BMaxHealth), BLerpSpeed * Time.deltaTime);
    }

    public void BossHealthUI(string Name)
    {
        BossName.text = Name;

        BossUI.SetBool("BossHealthUI", true);
    }

    public void PlayerScoreUI()
    {
        BossUI.SetBool("BossHealthUI", false);
    }

    #endregion

    #region PLAYER WEAPONS

    public void AmmoManager()
    {
        if (Play.Weapon == 1)
        {
            Weapon.text = "ENERGOID\nINFINITE";
            Shots[0].enabled = false;
            Shots[1].enabled = false;
            Shots[2].enabled = false;
            Shots[3].enabled = false;
            Shots[4].enabled = false;

            Types[0].enabled = true;
            Types[1].enabled = false;
            Types[2].enabled = false;
            Types[3].enabled = false;
            Types[4].enabled = false;
            Types[5].enabled = false;
            Types[6].enabled = false;
            Types[7].enabled = false;
        }
        else if (Play.Weapon == 2)
        {
            Weapon.text = "MAGNUM";
            Ammo(Play.AmmoTwo);

            Types[0].enabled = false;
            Types[1].enabled = true;
            Types[2].enabled = false;
            Types[3].enabled = false;
            Types[4].enabled = false;
            Types[5].enabled = true;
            Types[6].enabled = false;
            Types[7].enabled = false;
        }
        else if (Play.Weapon == 3)
        {
            Weapon.text = "ROADBLOCK";
            Ammo(Play.AmmoThree);

            Types[0].enabled = false;
            Types[1].enabled = false;
            Types[2].enabled = true;
            Types[3].enabled = false;
            Types[4].enabled = false;
            Types[5].enabled = false;
            Types[6].enabled = false;
            Types[7].enabled = false;
        }
        else if (Play.Weapon == 4)
        {
            Weapon.text = "RIOT-DRONES\nSHOOT-DEPLOY";
            Shots[0].enabled = false;
            Shots[1].enabled = false;
            Shots[2].enabled = false;
            Shots[3].enabled = false;
            Shots[4].enabled = false;

            Types[0].enabled = false;
            Types[1].enabled = false;
            Types[2].enabled = false;
            Types[3].enabled = true;
            Types[4].enabled = false;
            Types[5].enabled = false;
            Types[6].enabled = false;
            Types[7].enabled = false;
        }
        else if (Play.Weapon == 5)
        {
            Weapon.text = "GRENADE";
            Ammo(Play.AmmoFive);

            Types[0].enabled = false;
            Types[1].enabled = false;
            Types[2].enabled = false;
            Types[3].enabled = false;
            Types[4].enabled = true;
            Types[5].enabled = false;
            Types[6].enabled = true;
            Types[7].enabled = true;
        }
    }

    public void Ammo(int Ammo)
    {
        if (Ammo == 1)
        {
            Shots[0].enabled = true;
            Shots[1].enabled = false;
            Shots[2].enabled = false;
            Shots[3].enabled = false;
            Shots[4].enabled = false;
        }
        else if (Ammo == 2)
        {
            Shots[0].enabled = true;
            Shots[1].enabled = true;
            Shots[2].enabled = false;
            Shots[3].enabled = false;
            Shots[4].enabled = false;
        }
        else if (Ammo == 3)
        {
            Shots[0].enabled = true;
            Shots[1].enabled = true;
            Shots[2].enabled = true;
            Shots[3].enabled = false;
            Shots[4].enabled = false;
        }
        else if (Ammo == 4)
        {
            Shots[0].enabled = true;
            Shots[1].enabled = true;
            Shots[2].enabled = true;
            Shots[3].enabled = true;
            Shots[4].enabled = false;
        }
        else if (Ammo == 5)
        {
            Shots[0].enabled = true;
            Shots[1].enabled = true;
            Shots[2].enabled = true;
            Shots[3].enabled = true;
            Shots[4].enabled = true;
        }
    }

    public void AmmoAnim()
    {
        Anim.SetTrigger("AmmoUI");
    }

    public void PlayerAmmoChoices()
    {
        if (IsWindows == true)
        {
            if (PlayerPrefs.GetInt("HighScore") >= 100)
            {
                if (IsPlayerAmmoChoice == false)
                {
                    if (TimeToChoice <= 0)
                    {
                        PlayerAmmoChoice.SetActive(true);
                        IsPlayerAmmoChoice = true;
                        TimeToChoice = Random.Range(45.5f, 60f);
                    }
                    else
                    {
                        TimeToChoice -= Time.deltaTime;
                    }
                }
            }
        }
    }

    public void GrenadeAquire()
    {
        Gren.SetActive(true);
    }

    #endregion

    #region GAME FUNCTIONS

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P) && Pause == false)
        {
            Pause = true;
            Settings = false;

            Music.ButtonsUI(1);
            Time.timeScale = 0f;

            PauseCanvas.SetActive(true);
            SettingsCanvas.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.P) && Pause == true)
        {
            Pause = false;
            Settings = false;

            Music.ButtonsUI(1);
            Time.timeScale = 1f;

            PauseCanvas.SetActive(false);
            SettingsCanvas.SetActive(false);
        }
    }

    public void GameMenuButton(int A)
    {
        if (A == 1)
        {
            if (Pause == false)
            {
                Pause = true;
                Settings = false;

                Music.ButtonsUI(1);
                Time.timeScale = 0f;

                PauseCanvas.SetActive(true);
                SettingsCanvas.SetActive(false);
            }
            else if (Pause == true)
            {
                Pause = false;
                Settings = false;

                Music.ButtonsUI(1);
                Time.timeScale = 1f;

                PauseCanvas.SetActive(false);
                SettingsCanvas.SetActive(false);
            }
        }
        else if (A == 2)
        {
            if (Settings == false)
            {
                Pause = false;
                PauseCanvas.SetActive(false);

                Music.ButtonsUI(1);
                Time.timeScale = 0f;

                Settings = true;
                SettingsCanvas.SetActive(true);
            }
            else if (Settings == true)
            {
                Pause = true;
                PauseCanvas.SetActive(true);

                Music.ButtonsUI(1);
                Time.timeScale = 0f;

                Settings = false;
                SettingsCanvas.SetActive(Settings);
            }
        }
    }

    public void PauseCanvasButtons(int Which)
    {
        StartCoroutine(PauseGameUI(Which));
    }

    public IEnumerator PauseGameUI(int Which)
    {
        Time.timeScale = 1f;

        yield return new WaitForSeconds(0.25f);

        if (IsWindows == true)
        {
            if (Which == 1)
            {
                Music.ButtonsUI(1);
                SceneManager.LoadScene("MainMenu");
            }
            else if (Which == 2)
            {
                Settings = false;
                Pause = false;

                Music.ButtonsUI(1);

                PauseCanvas.SetActive(false);
                SettingsCanvas.SetActive(false);
            }
            else if (Which == 3)
            {
                Music.ButtonsUI(1);
                SceneManager.LoadScene("GamePlay");
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
                Settings = false;
                Pause = false;

                Music.ButtonsUI(1);

                PauseCanvas.SetActive(false);
                SettingsCanvas.SetActive(false);
            }
            else if (Which == 3)
            {
                Music.ButtonsUI(1);
                SceneManager.LoadScene("GamePlayAndroid");
            }
        }
    }

    public void VisualEffects()
    {
        Music.ButtonsUI(1);
        Vis.ActivateEffects();
    }

    private IEnumerator Retry()
    {
        if (IsWindows == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Music.ButtonsUI(2);

                yield return new WaitForSeconds(0.25f);

                SceneManager.LoadScene("GamePlay");
            }
        }
    }

    public void Cursors()
    {
        if (Play.IsWindows == true)
        {
            if (Pause == false && Settings == false)
            {
                Curs.Access = 2;
                Curs.WeaponsCursors(Play.Weapon);
            }
            else if (Pause == true || Settings == true)
            {
                Curs.Access = 1;
            }
        }
    }

    #endregion

    #region ANDROID

    public void AmmoAndroid(int Which)
    {
        if (Which == 1)
        {
            Play.Weapon = 1;

            WepsAmmoImage[0].localScale = new Vector2(1f, 1f);
            WepsAmmoImage[1].localScale = new Vector2(0.8f, 0.8f);
            WepsAmmoImage[2].localScale = new Vector2(0.8f, 0.8f);
            WepsAmmoImage[3].localScale = new Vector2(0.8f, 0.8f);
            WepsAmmoImage[4].localScale = new Vector2(0.8f, 0.8f);

            AmmoAnim();
        }
        else if (Which == 2 && Play.AmmoTwo > 0)
        {
            Play.Weapon = 2;

            WepsAmmoImage[0].localScale = new Vector2(0.8f, 0.8f);
            WepsAmmoImage[1].localScale = new Vector2(1f, 1f);
            WepsAmmoImage[2].localScale = new Vector2(0.8f, 0.8f);
            WepsAmmoImage[3].localScale = new Vector2(0.8f, 0.8f);
            WepsAmmoImage[4].localScale = new Vector2(0.8f, 0.8f);

            AmmoAnim();
        }
        else if (Which == 3 && Play.AmmoThree > 0)
        {
            Play.Weapon = 3;

            WepsAmmoImage[0].localScale = new Vector2(0.8f, 0.8f);
            WepsAmmoImage[1].localScale = new Vector2(0.8f, 0.8f);
            WepsAmmoImage[2].localScale = new Vector2(1f, 1f);
            WepsAmmoImage[3].localScale = new Vector2(0.8f, 0.8f);
            WepsAmmoImage[4].localScale = new Vector2(0.8f, 0.8f);

            AmmoAnim();
        }
        else if (Which == 4 && Play.AmmoFour > 0 && Play.IsActiveFour == false)
        {
            Play.Weapon = 4;

            WepsAmmoImage[0].localScale = new Vector2(0.8f, 0.8f);
            WepsAmmoImage[1].localScale = new Vector2(0.8f, 0.8f);
            WepsAmmoImage[2].localScale = new Vector2(0.8f, 0.8f);
            WepsAmmoImage[3].localScale = new Vector2(1f, 1f);
            WepsAmmoImage[4].localScale = new Vector2(0.8f, 0.8f);

            AmmoAnim();
        }
        else if (Which == 5 && Play.AmmoFive > 0)
        {
            Play.Weapon = 5;

            WepsAmmoImage[0].localScale = new Vector2(0.8f, 0.8f);
            WepsAmmoImage[1].localScale = new Vector2(0.8f, 0.8f);
            WepsAmmoImage[2].localScale = new Vector2(0.8f, 0.8f);
            WepsAmmoImage[3].localScale = new Vector2(0.8f, 0.8f);
            WepsAmmoImage[4].localScale = new Vector2(1f, 1f);

            AmmoAnim();
        }

        if (Play.Weapon == 4)
        {
            Play.IsAiming = false;
            Play.Velocity = 0f;
        }
        else
        {
            Play.IsAiming = true;
            Play.Velocity = 20f;
        }
    }

    public void AmmoAndroidRings()
    {
        if (Play.Weapon == 1)
        {
            AmmoAndroidRing.fillAmount = Mathf.Lerp(AmmoAndroidRing.fillAmount, (Play.TimeBtwShotsOne / 0.25f), 5f * Time.deltaTime);
        }

        if (Play.Weapon == 2)
        {
            AmmoAndroidRing.fillAmount = Mathf.Lerp(AmmoAndroidRing.fillAmount, (Play.WepTwoAndShotTime/ 1.25f), 5f * Time.deltaTime);
        }

        if (Play.Weapon == 3)
        {
            AmmoAndroidRing.fillAmount = Mathf.Lerp(AmmoAndroidRing.fillAmount, (Play.TimeBtwSetsThree / 0.85f), 5f * Time.deltaTime);
        }

        if (Play.Weapon == 4)
        {
            AmmoAndroidRing.fillAmount = Mathf.Lerp(AmmoAndroidRing.fillAmount, (Play.WepFourAndShotTime / 1.25f), 5f * Time.deltaTime);
        }

        if(Play.Weapon == 5)
        {
            AmmoAndroidRing.fillAmount = Mathf.Lerp(AmmoAndroidRing.fillAmount, (Play.WepFiveAndShotTime / 1.25f), 5f * Time.deltaTime);
        }
    }

    #endregion
}
