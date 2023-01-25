using UnityEngine;

public class Player : MonoBehaviour
{
    #region VARIABLES WINDOWS

    [Header("WINDOWS")]

    [Header("PLATFORM")]
    public bool IsWindows;

    [Header("STATS")]
    [HideInInspector]
    [Range(100, 0)]
    public int Health;
    [Range(0, 10000)]
    public int Points;

    [Header("MOVEMENET")]
    [HideInInspector]
    public float Velocity;
    Vector2 MoveInput;
    float MoveInputX;
    float MoveInputY;
    float BoundsX;
    float BoundsY;

    [Header("AIMING")]
    [HideInInspector]
    public bool IsAiming;
    Vector2 AimDirection;
    Vector2 MousePos;
    float AimAngle;

    [Header("PROJECTILE ONE")]
    [HideInInspector]
    public float TimeBtwShotsOne;
    float ForceOne;
    float DestroyOne;

    [Header("PROJECTILE TWO")]
    float ForceTwo;
    float DestroyTwo;

    [Header("PROJECTILE THREE")]
    [HideInInspector]
    public float TimeBtwSetsThree;
    float TimeBtwShotsThree;
    float ForceThree;
    float DestroyThree;
    int Shots;

    [Header("PROJECTILE FOUR")]
    [HideInInspector]
    public bool IsActiveFour;
    float ForceFour;

    [Header("PROJECTILE FIVE")]
    [HideInInspector]
    public int Gcount;
    float ForceFive;

    [Header("WEAPONS")]
    public GameObject ProjectileOne;
    public GameObject ProjectileTwo;
    public GameObject ProjectileThree;
    public GameObject ProjectileFour;
    public GameObject ProjectileFive;
    [HideInInspector] public int AmmoTwo;
    [HideInInspector] public int AmmoThree;
    [HideInInspector] public int AmmoFour;
    [HideInInspector] public int AmmoFive;
    [HideInInspector] public int Weapon;

    [Header("MUZZLES")]
    public GameObject Muzzle;
    public GameObject LeftWing;
    public GameObject RightWing;

    [Header("AUDIO")]
    public AudioSource Energoid;
    public AudioSource Magnum;
    public AudioSource RiotDrones;
    public AudioSource Grenade;[Space]
    public AudioSource Glitch;
    public AudioSource Damage;

    [Header("PARTICLES")]
    public GameObject Bleed;
    public GameObject Death;
    public GameObject Explode;

    [Header("AQUIRED COMPONENTS")]
    GameManager GameMan;
    CameraManager Cam;
    GameCursors Curs;
    Visuals Effects;
    Rigidbody2D Rb;

    #endregion

    #region VARIABLES ANDROID

    [Space(50)]

    [Header("ANDROID")]

    [Header("MOVEMENT")]
    //FloatingJoystick MoveMe;
    //FloatingJoystick RotateMe;

    [Header("AIMING")]
    [HideInInspector]
    public Vector2 ShootValue;

    [Header("WEAPONS")]
    [HideInInspector] public float WepTwoAndShotTime;
    [HideInInspector] public float WepFourAndShotTime;
    [HideInInspector] public float WepFiveAndShotTime;

    #endregion

    #region UNITY BEHAVIOURS

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Cam = FindObjectOfType<CameraManager>();

        GameMan = FindObjectOfType<GameManager>();
        Effects = FindObjectOfType<Visuals>();
        Curs = FindObjectOfType<GameCursors>();

        if (IsWindows == false)
        {
            //MoveMe = GameObject.FindGameObjectWithTag("MovementAndroid").GetComponent<FloatingJoystick>();
            //RotateMe = GameObject.FindGameObjectWithTag("AimingAndroid").GetComponent<FloatingJoystick>();

            WepTwoAndShotTime = 1.25f;
            WepFourAndShotTime = 0.8f;
            WepFiveAndShotTime = 0.85f;
        }

        IsAiming = true;

        Health = 100;
        Velocity = 20f;

        TimeBtwShotsOne = 0.25f;
        ForceOne = 40f;
        DestroyOne = 2.5f;

        ForceTwo = 45.5f;
        DestroyTwo = 2f;

        TimeBtwShotsThree = 0.035f;
        TimeBtwSetsThree = 0.85f;
        ForceThree = 35f;
        DestroyThree = 2.5f;
        Shots = 3;

        IsActiveFour = false;
        ForceFour = 12.5f;

        ForceFive = 8f;
        Gcount = 1;

        Weapon = 1;

        BoundsX = 450f;
        BoundsY = 450f;
    }

    private void Update()
    {
        if (IsWindows == true)
        {
            GameInputs();
            Weapons();
            WeaponOne();
            WeaponTwo();
            WeaponThree();
            WeaponFour();
            WeaponFive();

        }
        else if (IsWindows == false)
        {
            //GameInputsAndroid();
            WeaponOneAndroid();
            WeaponTwoAndroid();
            WeaponThreeAndroid();
            WeaponFourAndroid();
            WeaponFiveAndroid();
        }

        PlayBounds();
        HealthCheck();
    }

    private void FixedUpdate()
    {
        if (IsWindows == true)
        {
            Aiming();
        }
        else if (IsWindows == false)
        {
            //AimingAndroid();
        }

        Movement();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Boss"))
        {
            Health = 0;

            Cam.CameraShake(2);
        }
        else if (Other.CompareTag("BossTwo"))
        {
            Health = 0;

            Cam.CameraShake(2);
        }
        else if (Other.CompareTag("EnemyTwoProjectile"))
        {
            Health -= 5;
            Destroy(Other.gameObject);

            Damage.Play();
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("EnemyThreeProjectile"))
        {
            Health -= 10;
            Destroy(Other.gameObject);

            Glitch.Play();
            StartCoroutine(Effects.HitEffect());
            Cam.CameraShake(1);

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("EnemyFourProjectile"))
        {
            Health -= 2;
            Destroy(Other.gameObject);

            Damage.Play();
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("BossMissile"))
        {
            Health -= 5;
            Destroy(Other.gameObject);

            Damage.Play();
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("BossBarrage"))
        {
            Health -= Random.Range(5, 9);
            Destroy(Other.gameObject);

            Glitch.Play();
            StartCoroutine(Effects.HitEffect());
            Cam.CameraShake(1);

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("BossSplash"))
        {
            Health -= Random.Range(3, 6);
            Destroy(Other.gameObject);

            Damage.Play();
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("BossMine"))
        {
            Health -= Random.Range(8, 11);
            Other.GetComponent<BossMine>().LifeTimeFour = 0;

            Damage.Play();
            StartCoroutine(Effects.HitEffect());
            Cam.CameraShake(3);

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("BossGhost"))
        {
            Health -= Random.Range(6, 9);
            Destroy(Other.gameObject);
            Cam.CameraShake(2);

            Damage.Play();
            StartCoroutine(Effects.HitEffect());

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("BossTwoDisks"))
        {
            Health -= Random.Range(4, 7);
            Destroy(Other.gameObject);

            Damage.Play();
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("BossTwoGhosts"))
        {
            Health -= Random.Range(6, 9);
            Other.GetComponent<BossTwoGhost>().Health = 0;
            Cam.CameraShake(2);

            Damage.Play();
            StartCoroutine(Effects.HitEffect());

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("BossTwoLaser"))
        {
            Health -= Random.Range(5, 7);
            Destroy(Other.gameObject);

            Damage.Play();
            StartCoroutine(Effects.HitEffect());
            Cam.CameraShake(1);

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("BossTwoCritter"))
        {
            Health -= Random.Range(5, 7);
            Other.GetComponent<BossTwoCritter>().Health = 0;

            Damage.Play();
            Cam.CameraShake(2);

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
    }

    #endregion

    #region BEHAVIOURS - WINDOWS

    private void GameInputs()
    {
        if (GameMan.Pause == false)
        {
            MoveInputX = Input.GetAxis("Horizontal");
            MoveInputY = Input.GetAxis("Vertical");

            MoveInput = new Vector2(MoveInputX, MoveInputY);
            MoveInput.Normalize();
        }
    }

    private void Movement()
    {
        if (GameMan.Pause == false)
        {
            Rb.velocity = MoveInput * Velocity;
        }
    }

    private void Aiming()
    {
        if (IsAiming == true)
        {
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            AimDirection = Rb.position - MousePos;

            AimAngle = Mathf.Atan2(AimDirection.y, AimDirection.x) * Mathf.Rad2Deg + 90f;
            Rb.rotation = AimAngle;
        }
    }

    private void WeaponOne()
    {
        if (Weapon == 1 && GameMan.Pause == false)
        {
            if (Input.GetMouseButton(0))
            {
                if (TimeBtwShotsOne <= 0)
                {
                    GameObject One = Instantiate(ProjectileOne, Muzzle.transform.position, Muzzle.transform.rotation);
                    Rigidbody2D RbOne = One.GetComponent<Rigidbody2D>();
                    RbOne.AddForce(transform.up * ForceOne, ForceMode2D.Impulse);
                    Destroy(One, DestroyOne);

                    Energoid.Play();

                    TimeBtwShotsOne = 0.25f;
                }
                else
                {
                    TimeBtwShotsOne -= Time.deltaTime;
                }
            }
        }
    }

    private void WeaponTwo()
    {
        if (Weapon == 2 && AmmoTwo > 0 && GameMan.Pause == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject One = Instantiate(ProjectileTwo, Muzzle.transform.position, transform.rotation);
                Rigidbody2D RbOne = One.GetComponent<Rigidbody2D>();
                RbOne.AddForce(transform.up * ForceTwo, ForceMode2D.Impulse);
                Destroy(One, DestroyTwo);

                Magnum.Play();

                AmmoTwo--;

                if (AmmoTwo == 0)
                {
                    GameMan.AmmoAnim();
                    Weapon = 1;
                }
            }
        }
    }

    private void WeaponThree()
    {
        if (Weapon == 3 && AmmoThree > 0 && GameMan.Pause == false)
        {
            if (Input.GetMouseButton(0))
            {
                if (TimeBtwSetsThree <= 0)
                {
                    if (Shots > 0 && TimeBtwShotsThree <= 0)
                    {
                        GameObject One = Instantiate(ProjectileThree, Muzzle.transform.position, transform.rotation);
                        Rigidbody2D RbOne = One.GetComponent<Rigidbody2D>();
                        RbOne.AddForce(transform.up * ForceThree, ForceMode2D.Impulse);
                        Destroy(One, DestroyThree);

                        Shots--;
                        TimeBtwShotsThree = 0.035f;
                    }
                    else if (Shots == 0)
                    {
                        TimeBtwShotsThree = 0.035f;
                        TimeBtwSetsThree = 0.85f;
                        Shots = 3;

                        AmmoThree--;

                        if (AmmoThree == 0)
                        {
                            GameMan.AmmoAnim();
                            Weapon = 1;
                        }
                    }
                    else
                    {
                        TimeBtwShotsThree -= Time.deltaTime;
                    }
                }
                else
                {
                    TimeBtwSetsThree -= Time.deltaTime;
                }
            }
        }
    }

    private void WeaponFour()
    {
        if (Weapon == 4 && AmmoFour > 0 && IsActiveFour == false && GameMan.Pause == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject One = Instantiate(ProjectileFour, LeftWing.transform.position, transform.rotation, transform);
                Rigidbody2D RbOne = One.GetComponent<Rigidbody2D>();
                RbOne.AddForce(transform.right * -ForceFour, ForceMode2D.Impulse);

                GameObject Two = Instantiate(ProjectileFour, RightWing.transform.position, transform.rotation, transform);
                Rigidbody2D RbTwo = Two.GetComponent<Rigidbody2D>();
                RbTwo.AddForce(transform.right * ForceFour, ForceMode2D.Impulse);

                RiotDrones.Play();

                IsActiveFour = true;

                AmmoFour--;
            }
        }
    }

    private void WeaponFive()
    {
        if (Weapon == 5 && AmmoFive > 0 && GameMan.Pause == false)
        {
            if (Input.GetMouseButtonDown(0) && Gcount == 1)
            {
                GameObject One = Instantiate(ProjectileFive, Muzzle.transform.position, transform.rotation);
                Rigidbody2D RbOne = One.GetComponent<Rigidbody2D>();
                RbOne.AddForce(transform.up * ForceFive, ForceMode2D.Impulse);

                Gcount = 0;

                Grenade.Play();

                AmmoFive--;

                if (AmmoFive == 0)
                {
                    GameMan.AmmoAnim();
                    Weapon = 1;
                }
            }
        }
    }

    private void Weapons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Weapon = 1;

            GameMan.AmmoAnim();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && AmmoTwo > 0)
        {
            Weapon = 2;

            GameMan.AmmoAnim();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && AmmoThree > 0)
        {
            Weapon = 3;

            GameMan.AmmoAnim();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && AmmoFour > 0 && IsActiveFour == false)
        {
            Weapon = 4;

            GameMan.AmmoAnim();
        }
        else if (Input.GetKeyDown(KeyCode.X) && AmmoFive > 0)
        {
            Weapon = 5;

            GameMan.AmmoAnim();
        }

        Curs.WeaponsCursors(Weapon);

        if (Weapon == 4)
        {
            IsAiming = false;
            Velocity = 0f;
        }
        else
        {
            IsAiming = true;
            Velocity = 20f;
        }
    }

    private void PlayBounds()
    {
        BoundsX = Mathf.Clamp(transform.position.x, -400f, 400f);
        BoundsY = Mathf.Clamp(transform.position.y, -400f, 400f);

        transform.position = new Vector2(BoundsX, BoundsY);
    }

    private void HealthCheck()
    {
        if (Health <= 0)
        {
            Instantiate(Death, transform.position, Quaternion.identity);
            Cam.CameraShake(3);
            gameObject.SetActive(false);
        }
    }

    #endregion

    #region BEHAVIOURS - ANDROID

    /*private void GameInputsAndroid()
    {
        if (GameMan.Pause == false)
        {
            if (MoveMe.Horizontal >= 0.3f || MoveMe.Horizontal <= -0.3f)
            {
                MoveInputX = MoveMe.Horizontal;
            }
            else
            {
                MoveInputX = 0f;
            }

            if (MoveMe.Vertical >= 0.3f || MoveMe.Vertical <= -0.3f)
            {
                MoveInputY = MoveMe.Vertical;
            }
            else
            {
                MoveInputY = 0f;
            }

            MoveInput = new Vector2(MoveInputX, MoveInputY);
            MoveInput.Normalize();
        }

        ShootValue = new Vector2(RotateMe.Horizontal, RotateMe.Vertical);
    } */

    /*private void AimingAndroid()
    {
        if (IsAiming == true)
        {
            if (RotateMe.Horizontal != 0 || RotateMe.Vertical != 0)
            {
                AimAngle = Mathf.Atan2(RotateMe.Vertical, RotateMe.Horizontal) * Mathf.Rad2Deg - 90f;
            }
            else
            {
                AimAngle = Mathf.Atan2(RotateMe.Vertical, RotateMe.Horizontal) * Mathf.Rad2Deg;
            }

            Rb.rotation = AimAngle;
        }
    }*/

    private void WeaponOneAndroid()
    {
        if (Weapon == 1 && GameMan.Pause == false)
        {
            if (ShootValue.magnitude >= 0.95f)
            {
                if (TimeBtwShotsOne <= 0)
                {
                    GameObject One = Instantiate(ProjectileOne, Muzzle.transform.position, Muzzle.transform.rotation);
                    Rigidbody2D RbOne = One.GetComponent<Rigidbody2D>();
                    RbOne.AddForce(transform.up * ForceOne, ForceMode2D.Impulse);
                    Destroy(One, DestroyOne);

                    Energoid.Play();

                    TimeBtwShotsOne = 0.25f;
                }
                else
                {
                    TimeBtwShotsOne -= Time.deltaTime;
                }
            }
        }
    }

    private void WeaponTwoAndroid()
    {
        if (Weapon == 2 && AmmoTwo > 0 && GameMan.Pause == false)
        {
            if (ShootValue.magnitude >= 0.95f)
            {
                if (WepTwoAndShotTime <= 0)
                {
                    GameObject One = Instantiate(ProjectileTwo, Muzzle.transform.position, transform.rotation);
                    Rigidbody2D RbOne = One.GetComponent<Rigidbody2D>();
                    RbOne.AddForce(transform.up * ForceTwo, ForceMode2D.Impulse);
                    Destroy(One, DestroyTwo);

                    Magnum.Play();

                    WepTwoAndShotTime = 1.25f;
                    AmmoTwo--;

                    if (AmmoTwo == 0)
                    {
                        GameMan.AmmoAndroid(1);
                        GameMan.AmmoAnim();
                        Weapon = 1;
                    }
                }
                else
                {
                    WepTwoAndShotTime -= Time.deltaTime;
                }
            }
        }
    }

    private void WeaponThreeAndroid()
    {
        if (Weapon == 3 && AmmoThree > 0 && GameMan.Pause == false)
        {
            if (ShootValue.magnitude >= 0.95f)
            {
                if (TimeBtwSetsThree <= 0)
                {
                    if (Shots > 0 && TimeBtwShotsThree <= 0)
                    {
                        GameObject One = Instantiate(ProjectileThree, Muzzle.transform.position, transform.rotation);
                        Rigidbody2D RbOne = One.GetComponent<Rigidbody2D>();
                        RbOne.AddForce(transform.up * ForceThree, ForceMode2D.Impulse);
                        Destroy(One, DestroyThree);

                        Shots--;
                        TimeBtwShotsThree = 0.035f;
                    }
                    else if (Shots == 0)
                    {
                        TimeBtwShotsThree = 0.035f;
                        TimeBtwSetsThree = 0.85f;
                        Shots = 3;

                        AmmoThree--;

                        if (AmmoThree == 0)
                        {
                            GameMan.AmmoAndroid(1);
                            GameMan.AmmoAnim();
                            Weapon = 1;
                        }
                    }
                    else
                    {
                        TimeBtwShotsThree -= Time.deltaTime;
                    }
                }
                else
                {
                    TimeBtwSetsThree -= Time.deltaTime;
                }
            }
        }
    }

    private void WeaponFourAndroid()
    {
        if (Weapon == 4 && AmmoFour > 0 && IsActiveFour == false && GameMan.Pause == false)
        {
            if (ShootValue.magnitude >= 0.95f)
            {
                if (WepFourAndShotTime <= 0)
                {
                    GameObject One = Instantiate(ProjectileFour, LeftWing.transform.position, transform.rotation, transform);
                    Rigidbody2D RbOne = One.GetComponent<Rigidbody2D>();
                    RbOne.AddForce(transform.right * -ForceFour, ForceMode2D.Impulse);

                    GameObject Two = Instantiate(ProjectileFour, RightWing.transform.position, transform.rotation, transform);
                    Rigidbody2D RbTwo = Two.GetComponent<Rigidbody2D>();
                    RbTwo.AddForce(transform.right * ForceFour, ForceMode2D.Impulse);

                    RiotDrones.Play();

                    IsActiveFour = true;

                    WepFourAndShotTime = 0.5f;

                    AmmoFour--;
                }
                else
                {
                    WepFourAndShotTime -= Time.deltaTime;
                }
            }
        }
    }

    private void WeaponFiveAndroid()
    {
        if (Weapon == 5 && AmmoFive > 0 && GameMan.Pause == false)
        {
            if (ShootValue.magnitude >= 0.95f && Gcount == 1)
            {
                if (WepFiveAndShotTime <= 0)
                {
                    GameObject One = Instantiate(ProjectileFive, Muzzle.transform.position, transform.rotation);
                    Rigidbody2D RbOne = One.GetComponent<Rigidbody2D>();
                    RbOne.AddForce(transform.up * ForceFive, ForceMode2D.Impulse);

                    Gcount = 0;

                    Grenade.Play();

                    WepFiveAndShotTime = 0.85f;
                    AmmoFive--;

                    if (AmmoFive == 0)
                    {
                        GameMan.AmmoAndroid(1);
                        GameMan.AmmoAnim();
                        Weapon = 1;
                    }
                }
                else
                {
                    WepFiveAndShotTime -= Time.deltaTime;
                }
            }
        }
    }

    #endregion

}




