using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoDisks : MonoBehaviour
{
    [Header("DISKS")]
    float LifeTime;

    [Header("PARTICLES")]
    [SerializeField] GameObject Bleed;
    [SerializeField] GameObject Death;

    private void Start()
    {
        LifeTime = 15f;
    }

    private void Update()
    {
        Life();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("ProjectileOne"))
        {
            Destroy(Other.gameObject);
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("ProjectileTwo"))
        {
            Destroy(gameObject);
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("ProjectileThree"))
        {
            Destroy(Other.gameObject);
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("ProjectileFourSharpnel"))
        {
            Destroy(Other.gameObject);
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("RiotDrone"))
        {
            Destroy(Other.gameObject);
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
        else if (Other.CompareTag("RiotDroneProjectile"))
        {
            Destroy(Other.gameObject);
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);
        }
    }

    private void Life()
    {
        if (LifeTime <= 0)
        {
            Destroy(gameObject);
            Instantiate(Death, transform.position, Quaternion.identity);
        }
        else
        {
            LifeTime -= Time.deltaTime;
        }
    }
}
