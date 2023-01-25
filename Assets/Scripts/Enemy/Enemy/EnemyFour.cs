using System.Collections;
using UnityEngine;

public class EnemyFour : MonoBehaviour
{
    [Header("STATS")]
    [HideInInspector] public int Health;

    [Header("MOVEMENT")]
    float Range;
    float Retreat;
    float AimRange;
    float Speed;
    float ThisSpeed;

    [Header("PROJECTILE")]
    public GameObject Projectile;
    public GameObject MuzzleOne;
    public GameObject MuzzleTwo;
    public GameObject MuzzleThree;
    public GameObject MuzzleFour;
    float Force;
    float TimeBtwShots;
    float DestroyTime;

    [Header("KNOCKBACK")]
    Vector2 Diff;
    float Recoil;

    [Header("ROTATION")]
    float RotateSpeed;
    float Rotate;
    int RandDir;

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

    private void Start()
    {
        GameMan = FindObjectOfType<GameManager>();
        Cam = FindObjectOfType<CameraManager>();
        Rb = GetComponent<Rigidbody2D>();
        Play = FindObjectOfType<Player>();

        Health = 16;
        Health = 10;

        Speed = Random.Range(15f, 20f);
        ThisSpeed = Speed;
        Play.Points = Play.Points + Random.Range(12, 16);

        Range = Random.Range(15f, 20f);
        Retreat = ((Range - 5f) / 2f);
        AimRange = (Range + 5);

        Force = 10f;
        TimeBtwShots = 0.8f;
        DestroyTime = 2f;

        Recoil = 10f;
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
            Health = Health - 2;

            Damage.Play();
            Cam.CameraShake(1);

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);

            //KnockBack(Other);
            //StartCoroutine(StopRecoil());
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
}


