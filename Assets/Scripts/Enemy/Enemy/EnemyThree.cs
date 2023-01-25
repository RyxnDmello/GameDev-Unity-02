using System.Collections;
using UnityEngine;

public class EnemyThree : MonoBehaviour
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

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();

        GameMan = FindObjectOfType<GameManager>();
        Cam = FindObjectOfType<CameraManager>();
        Play = FindObjectOfType<Player>();

        Health = 8;
        Health = 6;

        Range = Random.Range(15f, 20f);
        Retreat = ((Range - 5f) / 2f);
        AimRange = (Range + Random.Range(2.5f, 5f));

        ForceOne = 35f;
        TimeBtwShots = 0.8f;
        DestroyTime = 2f;

        Recoil = 10f;
        Speed = Random.Range(8f, 10f);
        ThisSpeed = Speed;
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
}
