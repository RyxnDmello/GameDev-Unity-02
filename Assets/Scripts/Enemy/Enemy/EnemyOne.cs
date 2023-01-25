using UnityEngine;

public class EnemyOne : MonoBehaviour
{
    [Header("STATS")]
    [HideInInspector]
    public bool Active;
    [HideInInspector] 
    public int Health;
    private float Die;

    [Header("MOVEMENT")]
    Vector2 Direction;
    float RotateSpeed;
    float Speed;
    float Rotate;

    [Header("PARTICLE SYSTEM")]
    public GameObject Bleed;
    public GameObject Death;

    [Header("AUDIO")]
    public AudioSource Damage;

    [Header("AQUITED COMPONENTS")]
    GameManager GameMan;
    CameraManager CamMan;
    Rigidbody2D Rb;
    Player Play;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        GameMan = FindObjectOfType<GameManager>();
        CamMan = FindObjectOfType<CameraManager>();
        Play = FindObjectOfType<Player>();

        Health = 4;
        Die = 200f;
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            Play.Health -= 5;
            Destroy(gameObject);

            CamMan.CameraShake(2);

            Instantiate(Death, transform.position, Quaternion.identity);
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
            Health = 0;

            Damage.Play();
            CamMan.CameraShake(1);

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("ProjectileThree"))
        {
            Destroy(Other.gameObject);
            Health = 0;

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
            Health = 0;
            Damage.Play();
            CamMan.CameraShake(2);
            Destroy(Other.gameObject);
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
