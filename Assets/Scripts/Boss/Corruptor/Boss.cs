using UnityEngine;

public class Boss : MonoBehaviour
{
    #region VARIABLES
    
    [Header("PLATFORMS")]
    public bool IsAndroid;

    [Header("STATS")]
    //[HideInInspector] 
    [Range(500, 0)] public float Health;

    [Header("MOVEMENT")]
    float LimitRange;
    float Speed;

    [Header("AIMING")]
    float RotationSpeed;
    private float Angle;
    Vector2 Direction;

    [Header("STAGES")]
    float HealthOne;
    float HealthTwo;
    float HealthThree;
    float HealthFour;

    [Header("WEAPONS")]
    [SerializeField] GameObject Missile;
    [SerializeField] GameObject Barrage;
    [SerializeField] GameObject Splash;
    [SerializeField] GameObject Ghost;
    [SerializeField] GameObject Mine;

    [Header("MUZZLE")]
    [SerializeField] GameObject Muzzle;
    [SerializeField] GameObject One;
    [SerializeField] GameObject Two;
    [SerializeField] GameObject Three;

    [Header("PARTICLES")]
    [SerializeField] GameObject Bleed;
    [SerializeField] GameObject Death;

    [Header("AUDIO")]
    public AudioSource Damage;

    [Header("AQUIRED COMPONENTS")]
    GameManager GameMan;
    CameraManager Cam;
    HealthManager Heal;
    BossStages Stages;
    Player Play;

    PlayerUniversal player;

    #endregion

    #region UNITY

    private void Start()
    {
        GameMan = FindObjectOfType<GameManager>();
        Cam = FindObjectOfType<CameraManager>();
        Stages = GetComponent<BossStages>();
        Heal = FindObjectOfType<HealthManager>();
        Play = FindObjectOfType<Player>();

        player = FindObjectOfType<PlayerUniversal>();

        GameMan.BossHealthUI("THE CORRUPTOR");
        PlayerPrefs.SetInt("GameLevel", 1);

        Health = 500.0f;
        LimitRange = 10f;

        RotationSpeed = 25f;

        Speed = 12f;
        HealthOne = 500;
        HealthTwo = 350;
        HealthThree = 200;
        HealthFour = 100;
    }

    private void Update()
    {
        Aiming();
        StageManager();

        GameMan.BossHealth(Health);
        //Heal.BossLevel();

        HealthCheck();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("ProjectileOne"))
        {
            Destroy(Other.gameObject);
            Health = Health - 2;

            Damage.Play();
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("ProjectileTwo"))
        {
            Destroy(Other.gameObject);
            Health = Health - 5;

            Damage.Play();
            Cam.CameraShake(1);

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("ProjectileThree"))
        {
            Destroy(Other.gameObject);
            Health = Health - 1.5f;

            Damage.Play();
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("ProjectileFourSharpnel"))
        {
            Destroy(Other.gameObject);
            Health = Health - 1.25f;
            Damage.Play();
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("RiotDrone"))
        {
            Destroy(Other.gameObject);
            Health = Health - 5;
            Damage.Play();
            Cam.CameraShake(1);
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("RiotDroneProjectile"))
        {
            Destroy(Other.gameObject);
            Health = Health - 1.5f;
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
    }

    #endregion

    #region BEHAVIOURS

    private void StageManager()
    {
        #region WINDOWS - STAGES

        if (IsAndroid == false)
        {
            if (Health <= HealthOne && Health > HealthTwo)
            {
                LimitRange = 10f;

                Stages.Missiles(Missile, 0.8f, 1.5f);

                if (Health <= (HealthOne - 75) && Health > HealthTwo)
                {
                    Stages.Barrages(Barrage, Muzzle, 1f, 1.25f);
                }
            }
            else if (Health <= HealthTwo && Health > HealthThree)
            {
                LimitRange = 15f;

                Stages.Missiles(Missile, 1.5f, 2f);
                Stages.Splash(Splash, One, Two, Three);

                if (Health <= (HealthTwo - 75) && Health > HealthThree)
                {
                    Stages.Barrages(Barrage, Muzzle, 1f, 1.5f);
                }
            }
            else if (Health <= HealthThree && Health > HealthFour)
            {
                LimitRange = 15f;

                Stages.Mines(Mine);
                Stages.Missiles(Missile, 1.5f, 2f);

                if (Health <= (HealthThree - 25) && Health > HealthFour)
                {
                    Stages.Splash(Splash, One, Two, Three);
                    Stages.Barrages(Barrage, Muzzle, 1.25f, 1.8f);
                }
            }
            else if (Health <= HealthFour && Health > 0)
            {
                LimitRange = 15f;

                Stages.Mines(Mine);
                Stages.Splash(Splash, One, Two, Three);
                Stages.Barrages(Barrage, Muzzle, 1.25f, 2f);

                if (Health <= (HealthFour - 25) && Health > 0)
                {
                    Stages.Missiles(Missile, 2f, 2.25f);
                    Stages.Ghosts(Ghost);
                }
            }
        }

        #endregion

        #region ANDROID - STAGES

        else if (IsAndroid == true)
        {
            if (Health <= HealthOne && Health > HealthTwo)
            {
                LimitRange = 10f;

                Stages.Missiles(Missile, 1f, 1.5f);

                if (Health <= (HealthOne - 75) && Health > HealthTwo)
                {
                    Stages.Barrages(Barrage, Muzzle, 2f, 2.5f);
                }
            }
            else if (Health <= HealthTwo && Health > HealthThree)
            {
                LimitRange = 15f;

                Stages.Missiles(Missile, 1.5f, 2f);
                Stages.Splash(Splash, One, Two, Three);

                if (Health <= (HealthTwo - 75) && Health > HealthThree)
                {
                    Stages.Barrages(Barrage, Muzzle, 2f, 2.25f);
                }
            }
            else if (Health <= HealthThree && Health > HealthFour)
            {
                LimitRange = 15f;

                Stages.Mines(Mine);
                Stages.Missiles(Missile, 2f, 2.25f);

                if (Health <= (HealthThree - 25) && Health > HealthFour)
                {
                    Stages.Splash(Splash, One, Two, Three);
                    Stages.Barrages(Barrage, Muzzle, 2f, 3f);
                }
            }
            else if (Health <= HealthFour && Health > 0)
            {
                LimitRange = 15f;

                Stages.Mines(Mine);
                Stages.Splash(Splash, One, Two, Three);
                Stages.Barrages(Barrage, Muzzle, 2.5f, 3f);

                if (Health <= (HealthFour - 25) && Health > 0)
                {
                    Stages.Missiles(Missile, 2f, 3f);
                    Stages.Ghosts(Ghost);
                }
            }
        }

        #endregion
    }

    private void Movement()
    {
        if (Vector2.Distance(player.transform.position, transform.position) >= LimitRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Speed * Time.fixedDeltaTime);
        }
    }

    private void Aiming()
    {
        Direction = Play.transform.position - transform.position;
        Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90;
        Quaternion Rotation = Quaternion.AngleAxis(Angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, RotationSpeed * Time.deltaTime);
    }

    public void HealthCheck()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            Cam.CameraShake(3);
            Instantiate(Death, transform.position, Quaternion.identity);
            GameMan.PlayerScoreUI();
            PlayerPrefs.SetInt("GameLevel", 2);
        }

        if (GameMan.Active == false)
        {
            Destroy(this);
        }
    }

    #endregion
}
