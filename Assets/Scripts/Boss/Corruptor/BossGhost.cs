using UnityEngine;

public class BossGhost : MonoBehaviour
{
    #region VARIABLES

    [Header("GHOST")]
    public GameObject Mines;
    Vector2 DirectionFive;
    float RotateSpeedFive;
    float LifeTimeFive;
    float RotateFive;
    float SpeedFive;
    [HideInInspector] public int Health;

    [Header("PARTICLES")]
    [SerializeField] GameObject Bleed;
    [SerializeField] GameObject Death;

    [Header("AQUITED COMPONENTS")]
    Rigidbody2D Rb;
    Player Play;

    #endregion

    #region UNITY

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Play = FindObjectOfType<Player>();

        if (Play.IsWindows == true)
        {
            Health = Random.Range(4, 6);
        }
        else
        {
            Health = Random.Range(3, 4);
        }

        RotateSpeedFive = Random.Range(200f, 300f);
        LifeTimeFive = Random.Range(15f, 18f);
        SpeedFive = Random.Range(15f, 20f);
    }

    private void Update()
    {
        HealthCheck();
    }

    private void FixedUpdate()
    {
        Ghost();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("ProjectileOne"))
        {
            Destroy(Other.gameObject);

            Health--;

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("ProjectileTwo"))
        {
            Destroy(Other.gameObject);

            Health = Health - 3;

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("ProjectileThree"))
        {
            Destroy(Other.gameObject);

            Health--;

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("ProjectileFourSharpnel"))
        {
            Destroy(Other.gameObject);

            Health--;

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("RiotDrone"))
        {
            Destroy(Other.gameObject);

            Health = 0;

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("RiotDroneProjectile"))
        {
            Destroy(Other.gameObject);

            Health--;

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
    }

    #endregion

    #region BEHAVIOURS

    public void Ghost()
    {
        DirectionFive = (Vector2)Play.transform.position - Rb.position;
        DirectionFive.Normalize();

        RotateFive = Vector3.Cross(DirectionFive, transform.up).z;

        Rb.angularVelocity = -RotateFive * RotateSpeedFive;
        Rb.velocity = transform.up * SpeedFive;
    }

    private void HealthCheck()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            Instantiate(Death, transform.position, Quaternion.identity);
            Instantiate(Mines, transform.position, Quaternion.identity);
        }

        if (LifeTimeFive <= 0)
        {
            Destroy(gameObject);
            Instantiate(Death, transform.position, Quaternion.identity);
            Instantiate(Mines, transform.position, Quaternion.identity);
        }
        else
        {
            LifeTimeFive -= Time.fixedDeltaTime;
        }
    }

    #endregion
}
