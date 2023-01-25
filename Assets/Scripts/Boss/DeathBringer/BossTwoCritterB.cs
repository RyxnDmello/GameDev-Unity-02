using System.Collections;
using UnityEngine;

public class BossTwoCritterB : MonoBehaviour
{
    #region VARIABLES

    [Header("STATS")]
    [HideInInspector] public int Health;
    float TimeToDie;

    [Header("MOVEMENT")]
    float Range;
    float Retreat;
    float AimRange;
    float Speed;
    float ThisSpeed;

    [Header("PROJECTILE")]
    public GameObject Projectile;
    public GameObject Muzzle;
    float ForceOne;
    float TimeBtwShots;
    float DestroyTime;

    [Header("AIMING")]
    Vector2 AimDirection;
    Vector2 PlayerPos;
    float AimAngle;

    [Header("KNOCKBACK")]
    Vector2 Diff;
    float Recoil;

    [Header("PARTICLES")]
    public GameObject Bleed;
    public GameObject Death;

    [Header("AUDIO")]
    public AudioSource Damage;
    public AudioSource Shoot;

    [Header("AQUITED COMPONENTS")]
    GameManager GameMan;
    CameraManager Cam;
    Rigidbody2D Rb;
    Player Play;

    #endregion

    #region UNITY

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();

        GameMan = FindObjectOfType<GameManager>();
        Cam = FindObjectOfType<CameraManager>();
        Play = FindObjectOfType<Player>();

        if (GameMan.IsWindows == true)
        {
            Health = 8;
            TimeToDie = Random.Range(14f, 16f);
        }
        else
        {
            Health = 6;
            TimeToDie = Random.Range(10f, 12f);
        }

        Speed = Random.Range(12f, 16f);
        ThisSpeed = Speed;

        Range = Random.Range(12f, 15f);
        AimRange = (Range + 2.5f);
        Retreat = (Range / 2);

        ForceOne = 35f;
        TimeBtwShots = 0.8f;
        DestroyTime = 2f;

        Recoil = 10f;
    }

    private void Update()
    {
        Weapon();
        HealthCheck();
    }

    private void FixedUpdate()
    {
        Aiming();
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            Play.Health -= 5;
            Health = 0;
        }
        else if (Other.CompareTag("ProjectileOne"))
        {
            Destroy(Other.gameObject);
            Health = Health - 1;

            Damage.Play();
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("ProjectileTwo"))
        {
            Destroy(Other.gameObject);
            Health = Health - 3;

            Damage.Play();
            Cam.CameraShake(1);

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);

            KnockBack(Other);
            StartCoroutine(StopRecoil());
        }
        else if (Other.CompareTag("ProjectileThree"))
        {
            Destroy(Other.gameObject);
            Health = Health - 1;

            Damage.Play();
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("ProjectileFourSharpnel"))
        {
            Destroy(Other.gameObject);
            Health = Health - 1;

            Damage.Play();
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("RiotDrone"))
        {
            Destroy(Other.gameObject);
            Health = Health - 1;

            Damage.Play();
            Cam.CameraShake(2);

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("RiotDroneProjectile"))
        {
            Destroy(Other.gameObject);
            Health = Health - 1;
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
    }

    #endregion

    #region BEHAVIOURS

    private void Movement()
    {
        if (Vector2.Distance(Play.transform.position, transform.position) >= Range)
        {
            transform.position = Vector2.MoveTowards(transform.position, Play.transform.position, Speed * Time.fixedDeltaTime);
        }
        else if (Vector2.Distance(Play.transform.position, transform.position) <= Retreat)
        {
            transform.position = Vector2.MoveTowards(transform.position, Play.transform.position, -Speed * Time.fixedDeltaTime);
        }
    }

    private void Aiming()
    {
        PlayerPos = Play.transform.position;
        AimDirection = (Vector2)transform.position - PlayerPos;

        AimAngle = Mathf.Atan2(AimDirection.y, AimDirection.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0f, 0f, AimAngle);
    }

    private void Weapon()
    {
        if (Vector2.Distance(Play.transform.position, transform.position) <= AimRange)
        {
            if (TimeBtwShots <= 0)
            {
                GameObject One = Instantiate(Projectile, Muzzle.transform.position, transform.rotation);
                Rigidbody2D RbOne = One.GetComponent<Rigidbody2D>();
                RbOne.AddForce(transform.up * ForceOne, ForceMode2D.Impulse);

                Shoot.Play();
                Destroy(One, DestroyTime);

                TimeBtwShots = Random.Range(2f, 2.5f);
            }
            else
            {
                TimeBtwShots -= Time.deltaTime;
            }
        }
    }

    private void KnockBack(Collider2D Other)
    {
        Recoil = 10f;
        Speed = 0;

        Diff = transform.position - Other.transform.position;
        Diff.Normalize();

        Rb.AddForce(Diff * Recoil, ForceMode2D.Impulse);
    }

    IEnumerator StopRecoil()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Rb.velocity = Vector2.zero;

        Speed = ThisSpeed;

        StopAllCoroutines();
    }

    private void HealthCheck()
    {
        if (Health <= 0)
        {
            Instantiate(Death, transform.position, Quaternion.identity);
            Cam.CameraShake(1);
            Destroy(gameObject);
        }

        if (TimeToDie <= 0)
        {
            Health = 0;
        }
        else
        {
            TimeToDie -= Time.deltaTime;
        }

        if (GameMan.Active == false)
        {
            Destroy(this);
        }
    }

    #endregion
}
