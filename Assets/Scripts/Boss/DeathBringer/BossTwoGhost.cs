using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoGhost : MonoBehaviour
{
    [Header("GHOST")]
    [HideInInspector] public int Health;
    Vector2 DirectionTwo;
    float RotateSpeedTwo;
    float LifeTimeTwo;
    float RotateTwo;
    float SpeedTwo;

    [Header("PARTICLES")]
    public GameObject Bleed;
    public GameObject Death;

    [Header("AQUIRED COMPONENTS")]
    Rigidbody2D Rb;
    Player Play;

    void Start()
    {
        Play = FindObjectOfType<Player>();
        Rb = GetComponent<Rigidbody2D>();

        Health = Random.Range(4, 6);

        RotateSpeedTwo = Random.Range(200f, 300f);
        LifeTimeTwo = Random.Range(15f, 18f);
        SpeedTwo = Random.Range(15f, 18f);
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

    public void Ghost()
    {
        DirectionTwo = (Vector2)Play.transform.position - Rb.position;
        DirectionTwo.Normalize();

        RotateTwo = Vector3.Cross(DirectionTwo, transform.up).z;

        Rb.angularVelocity = -RotateTwo * RotateSpeedTwo;
        Rb.velocity = transform.up * SpeedTwo;
    }

    public void HealthCheck()
    {
        if(Health <= 0)
        {
            Destroy(gameObject);
            Instantiate(Death, transform.position, Quaternion.identity);
        }

        if(LifeTimeTwo <= 0)
        {
            Destroy(gameObject);
            Instantiate(Death, transform.position, Quaternion.identity);
        }
        else
        {
            LifeTimeTwo -= Time.deltaTime;
        }
    }
}
