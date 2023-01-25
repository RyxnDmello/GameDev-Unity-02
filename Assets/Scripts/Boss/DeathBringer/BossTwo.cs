using UnityEngine;

public class BossTwo : MonoBehaviour
{
    #region VARIABLES

    [Header("PLATFORMS")]
    public bool IsAndroid;        

    [Header("STATS")]
    //[HideInInspector] 
    [Range(500, 0)] public float Health;
    [SerializeField] GameObject Shield;

    [Header("MOVEMENT")]
    float LimitRange;
    float Speed;

    [Header("AIMING")]
    float RotationSpeeds;
    private float Angles;
    Vector2 Directions;

    [Header("SHIELD AIMING")]
    float RotationSpeed;
    private float Angle;
    Vector2 Direction;

    [Header("STAGES")]
    float HealthOne;
    float HealthTwo;
    float HealthThree;

    [Header("WEAPONS")]
    [SerializeField] GameObject Disks;
    [SerializeField] GameObject Ghosts;
    [SerializeField] GameObject Critter;
    [SerializeField] GameObject Laser;
    [SerializeField] GameObject Trap;

    [Header("PARTICLES")]
    [SerializeField] ParticleSystem[] Fumes;
    [SerializeField] GameObject Bleed;
    [SerializeField] GameObject Death;
    float TimeToFumes;
    float TimeToStop;
    private bool IsFumes;

    [Header("AUDIO")]
    public AudioSource Damage;

    [Header("AQUIRED COMPONENTS")]
    GameManager GameMan;
    CameraManager Cam;
    HealthManager Heal;
    BossTwoStages Stages;
    Player Play;

    #endregion

    #region UNITY

    private void Start()
    {
        GameMan = FindObjectOfType<GameManager>();
        Cam = FindObjectOfType<CameraManager>();
        Stages = GetComponent<BossTwoStages>();
        //Heal = FindObjectOfType<HealthManager>();
        Play = FindObjectOfType<Player>();

        GameMan.BossHealthUI("THE DEATHBRINGER");
        PlayerPrefs.SetInt("GameLevel", 2);

        Health = 500.0f;
        LimitRange = 10f;

        RotationSpeed = 25f;
        RotationSpeeds = 0.4f;
        TimeToFumes = 10f;

        Speed = 5f;

        HealthOne = 500;
        HealthTwo = 350;
        HealthThree = 100;
    }

    private void Update()
    {
        if (Play != null)
        {
            StageManager();

            GameMan.BossHealth(Health);
            //Heal.BossLevel();

            HealthCheck();
        }
    }

    private void FixedUpdate()
    {
        if (Play != null)
        {
            ShieldAiming();
            Aiming();
            Movement();
        }
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("ProjectileOne"))
        {
            Destroy(Other.gameObject);
            Health = Health - 2f;

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
                RotationSpeeds = 0.35f;
                LimitRange = 15f;
                Speed = 6.5f;

                Stages.Shields(20f, 20f);

                Stages.Disks(Disks, false, 0.5f, 0.65f);
                Stages.Critter(Critter, true, 10f, 12.5f);

                if (Health <= (HealthOne - 75) && Health > HealthTwo)
                {
                    Stages.Ghosts(Ghosts, 1.8f, 2f);
                }
            }
            else if (Health <= HealthTwo && Health > HealthThree)
            {
                RotationSpeeds = 5f;
                LimitRange = 15f;
                Speed = 5f;

                Stages.Shields(25f, 15f);

                Stages.Disks(Disks, false, 0.5f, 0.65f);
                Stages.Ghosts(Ghosts, 1.5f, 1.85f);

                if (Health <= (HealthTwo - 125) && Health > HealthThree)
                {
                    Stages.Critter(Critter, true, 9f, 12f);
                    Stages.Lasers(Laser, 1.25f, 1.85f);
                }
            }
            else if (Health <= HealthThree && Health > 0)
            {
                RotationSpeeds = 4f;
                LimitRange = 15f;
                Speed = 5f;

                Stages.Shields(20f, 12.5f);

                Stages.Disks(Disks, false, 0.5f, 0.65f);
                Stages.Ghosts(Ghosts, 1.6f, 2f);
                Stages.Lasers(Laser, 1.5f, 2.25f);

                if (Health <= (HealthThree - 25) && Health > 0)
                {
                    RotationSpeeds = 5f;

                    Stages.Critter(Critter, true, 8f, 12.5f);
                    Stages.Traps(Trap, 1.25f, 1.5f);
                }
            }
        }

        #endregion

        #region ANDROID - STAGES

        else if(IsAndroid == true)
        {
            if (Health <= HealthOne && Health > HealthTwo)
            {
                RotationSpeeds = 0.35f;
                LimitRange = 15f;
                Speed = 6.5f;

                Stages.Shields(20f, 20f);

                Stages.Disks(Disks, false, 0.6f, 0.85f);
                Stages.Critter(Critter, true, 10f, 12.5f);

                if (Health <= (HealthOne - 75) && Health > HealthTwo)
                {
                    Stages.Ghosts(Ghosts, 2f, 2.5f);
                }
            }
            else if (Health <= HealthTwo && Health > HealthThree)
            {
                RotationSpeeds = 5f;
                LimitRange = 15f;
                Speed = 5f;

                Stages.Shields(25f, 15f);

                Stages.Disks(Disks, false, 0.8f, 1f);
                Stages.Ghosts(Ghosts, 1.8f, 2.5f);

                if (Health <= (HealthTwo - 125) && Health > HealthThree)
                {
                    Stages.Critter(Critter, true, 10f, 12.5f);
                    Stages.Lasers(Laser, 1.8f, 2.25f);
                }
            }
            else if (Health <= HealthThree && Health > 0)
            {
                RotationSpeeds = 4f;
                LimitRange = 15f;
                Speed = 5f;

                Stages.Shields(20f, 12.5f);

                Stages.Disks(Disks, false, 0.8f, 1f);
                Stages.Ghosts(Ghosts, 2f, 2.25f);
                Stages.Lasers(Laser, 2f, 2.5f);

                if (Health <= (HealthThree - 25) && Health > 0)
                {
                    RotationSpeeds = 5f;

                    Stages.Critter(Critter, true, 10f, 12f);
                    Stages.Traps(Trap, 2f, 2.25f);
                }
            }
        }

        #endregion

        Fume();
    }

    private void Movement()
    {
        if (Vector2.Distance(Play.transform.position, transform.position) >= LimitRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, Play.transform.position, Speed * Time.fixedDeltaTime);
        }
    }

    private void ShieldAiming()
    {
        Direction = Play.transform.position - Shield.transform.position;
        Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90;
        Quaternion Rotation = Quaternion.AngleAxis(Angle, Vector3.forward);
        Shield.transform.rotation = Quaternion.Slerp(Shield.transform.rotation, Rotation, RotationSpeed * Time.deltaTime);
    }

    private void Aiming()
    {
        Directions = Play.transform.position - transform.position;
        Angles = Mathf.Atan2(Directions.y, Directions.x) * Mathf.Rad2Deg - 90;
        Quaternion Rotations = Quaternion.AngleAxis(Angles, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotations, RotationSpeeds * Time.deltaTime);
    }

    public void Fume()
    {
        if (Vector2.Distance(Play.transform.position, transform.position) <= 25f)
        {
            if (IsFumes == false && Stages.ShieldActive == false)
            {
                if (TimeToFumes <= 0)
                {
                    Fumes[0].Play();
                    Fumes[1].Play();
                    Fumes[2].Play();
                    Fumes[3].Play();

                    IsFumes = true;

                    TimeToStop = Random.Range(8f, 12f);
                }
                else
                {
                    TimeToFumes -= Time.deltaTime;
                }
            }
        }

        if (IsFumes == true)
        {
            if (TimeToStop <= 0)
            {
                Fumes[0].Stop();
                Fumes[1].Stop();
                Fumes[2].Stop();
                Fumes[3].Stop();

                IsFumes = false;

                TimeToFumes = Random.Range(18f, 20f);
            }
            else
            {
                TimeToStop -= Time.deltaTime;
            }
        }
    }

    public void HealthCheck()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            Cam.CameraShake(3);
            Instantiate(Death, transform.position, Quaternion.identity);
            GameMan.PlayerScoreUI();
            PlayerPrefs.SetInt("GameLevel", 3);
        }

        if (GameMan.Active == false)
        {
            Destroy(this);
        }
    }

    #endregion
}
