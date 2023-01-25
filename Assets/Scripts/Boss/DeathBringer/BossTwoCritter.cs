using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoCritter : MonoBehaviour
{
    [Header("CRITTERS")]
    public GameObject CritterA;
    public GameObject CritterB;
    public GameObject HealthOrb;
    float TimeToStopThree;
    float RotateSpeedThree;
    float RotateThree;
    int RandDir;
    [HideInInspector] public int Health;

    [Header("PARTICLES")]
    [SerializeField] GameObject Bleed;
    [SerializeField] GameObject Death;

    [Header("AQUIRED COMPONENTS")]
    Rigidbody2D Rb;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();

        Health = Random.Range(2, 4);
        RandomRotate();
        TimeToStopThree = Random.Range(5f, 8f);
    }

    private void Update()
    {
        Critter();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("ProjectileOne"))
        {
            Destroy(Other.gameObject);
            Health--;
            Instantiate(Bleed, transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("ProjectileTwo"))
        {
            Destroy(Other.gameObject);
            Health = Health - 3;
            Instantiate(Bleed, transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("ProjectileThree"))
        {
            Destroy(Other.gameObject);
            Health--;
            Instantiate(Bleed, transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("ProjectileFourSharpnel"))
        {
            Destroy(Other.gameObject);
            Health--;
            Instantiate(Bleed, transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("RiotDrone"))
        {
            Destroy(Other.gameObject);
            Health = 0;
            Instantiate(Bleed, transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("RiotDroneProjectile"))
        {
            Destroy(Other.gameObject);
            Health--;
            Instantiate(Bleed, transform.position, Quaternion.identity);
        }
    }

    public void Critter()
    {
        if (TimeToStopThree <= 0)
        {
            Destroy(Rb);
            Destroy(gameObject);
            Instantiate(Death, transform.position, Quaternion.identity);
            Instantiate(CritterA, transform.position, Quaternion.identity);
            Instantiate(CritterB, transform.position, Quaternion.identity);
        }
        else
        {
            RotateThree = RotateThree + RotateSpeedThree * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, RotateThree);

            TimeToStopThree -= Time.deltaTime;
        }

        if(Health <= 0)
        {
            Destroy(gameObject);
            Instantiate(Death, transform.position, Quaternion.identity);
            Instantiate(HealthOrb, transform.position, Quaternion.identity);
        }
    }

    private void RandomRotate()
    {
        RandDir = Random.Range(1, 3);

        if (RandDir == 1)
        {
            RotateSpeedThree = Random.Range(150f, 200f);
        }
        else if (RandDir == 2)
        {
            RotateSpeedThree = Random.Range(-150f, -200f);
        }
    }
}
