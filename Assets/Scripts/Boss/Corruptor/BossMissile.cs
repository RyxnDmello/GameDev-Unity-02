using UnityEngine;

public class BossMissile : MonoBehaviour
{
    #region VARIABLES

    [Header("MISSILE")] 
    Vector2 Direction;
    float RotateSpeed;
    float LifeTimeOne;
    float Rotate;
    float Speed;
    int Health;

    [Header("PARTICLES")]
    public GameObject Bleed;
    public GameObject Death;

    [Header("AQUIRED COMPONENTS")]
    Rigidbody2D Rb;
    Player Play;

    #endregion

    #region UNITY

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Play = FindObjectOfType<Player>();

        Health = Random.Range(4, 7);
        RotateSpeed = Random.Range(225f, 300f);
        LifeTimeOne = Random.Range(15f, 20f);
        Speed = Random.Range(15f, 20f);
    }

    private void Update()
    {
        HealthCheck();
    }

    private void FixedUpdate()
    {
        Missile();
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
            Health = 0;

            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("ProjectileThree"))
        {
            Destroy(Other.gameObject);

            Health -= 2;

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

    public void Missile()
    {
        Direction = (Vector2)Play.transform.position - Rb.position;
        Direction.Normalize();

        Rotate = Vector3.Cross(Direction, transform.up).z;

        Rb.angularVelocity = -Rotate * RotateSpeed;
        Rb.velocity = transform.up * Speed;
    }

    public void HealthCheck()
    {
        if(Health <= 0)
        {
            Destroy(gameObject);
            Instantiate(Death, transform.position, Quaternion.identity);
        }

        if(LifeTimeOne <= 0)
        {
            Destroy(gameObject);
            Instantiate(Death, transform.position, Quaternion.identity);
        }
        else
        {
            LifeTimeOne -= Time.deltaTime;
        }
    }

    #endregion
}
