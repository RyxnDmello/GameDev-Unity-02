using System.Collections;
using UnityEngine;

public class BossTwoCritterA : MonoBehaviour
{
    #region VARIABLES

    [Header("STATS")]
    [HideInInspector] public int Health;
    float TimeToDie;

    [Header("MOVEMENT")]
    float Speed;
    float ThisSpeed;
    float Range;
    float AimRange;
    float Retreat;

    [Header("PROJECTILE")]
    public GameObject Projectile;
    public GameObject Muzzle;
    float ForceOne;
    float TimeBtwShots;
    float DestroyTime;

    [Header("KNOCKBACK")]
    Vector2 Diff;
    float Recoil;

    [Header("PARTICLES")]
    public GameObject Bleed;
    public GameObject Death;

    [Header("AIMING")]
    Vector2 AimDirection;
    Vector2 PlayerPos;
    float AimAngle;

    [Header("AUDIO")]
    public AudioSource Damage;

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
            Health = Random.Range(8, 11);
            TimeToDie = Random.Range(16f, 20f);
        }
        else
        {
            Health = Random.Range(6, 8);
            TimeToDie = Random.Range(10f, 12f);
        }

        Speed = Random.Range(15f, 20f);
        ThisSpeed = Speed;

        Range = Random.Range(10f, 15f);
        AimRange = (Range + 5f);
        Retreat = (Range / 2);

        ForceOne = 25f;
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
            Health = Health - 2;

            Damage.Play();
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("RiotDrone"))
        {
            Destroy(Other.gameObject);
            Cam.CameraShake(2);
            Health = Health - 1;
            Damage.Play();
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
        if (Vector2.Distance(transform.position, Play.transform.position) <= AimRange)
        {
            if (TimeBtwShots <= 0)
            {
                GameObject One = Instantiate(Projectile, Muzzle.transform.position, transform.rotation);
                Rigidbody2D RbOne = One.GetComponent<Rigidbody2D>();
                RbOne.AddForce(transform.up * ForceOne, ForceMode2D.Impulse);

                Destroy(One, DestroyTime);

                TimeBtwShots = Random.Range(0.85f, 1f);
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
