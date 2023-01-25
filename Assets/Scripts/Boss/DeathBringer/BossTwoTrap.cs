using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoTrap : MonoBehaviour
{
    [Header("TRAP")]
    float LifeTime;

    [Header("PARTICLE EFFECTS")]
    [SerializeField] GameObject Bleed;
    [SerializeField] GameObject Death;

    [Header("AQUIRED COMPONENTS")]
    BossTwoStages Stages;

    private void Start()
    {
        Stages = FindObjectOfType<BossTwoStages>();

        Stages.TrapActive = true;
        LifeTime = Random.Range(6f, 12f);
    }

    private void Update()
    {
        Health();
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
            Destroy(Other.gameObject);
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

    public void Health()
    {
        if (LifeTime <= 0)
        {
            Instantiate(Death, transform.position, Quaternion.identity);
            Stages.TrapActive = false;
            Destroy(gameObject);
        }
        else
        {
            LifeTime -= Time.deltaTime;
        }
    }
}
