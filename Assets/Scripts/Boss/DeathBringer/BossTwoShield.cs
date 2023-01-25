using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoShield : MonoBehaviour
{
    [Header("SHIELDS")]
    [SerializeField] CapsuleCollider2D A;
    [SerializeField] CapsuleCollider2D B;

    [Header("PARTICLES")]
    [SerializeField] GameObject Bleed;

    [Header("SOUND EFFECTS")]
    [SerializeField] AudioSource Damage;

    [Header("AQUITED COMPONENTS")]
    BossTwoStages Stages;

    private void Start()
    {
        Stages = GetComponentInParent<BossTwoStages>();
    }

    private void Update()
    {
        Active();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Stages.ShieldActive == true)
        {
            if(Other.CompareTag("Player"))
            {
                Other.GetComponent<Player>().Health = 0;
            }
            else if (Other.CompareTag("ProjectileOne"))
            {
                Destroy(Other.gameObject);
                Damage.Play();
                Instantiate(Bleed, Other.transform.position, Quaternion.identity);
            }
            else if (Other.CompareTag("ProjectileTwo"))
            {
                Destroy(Other.gameObject);
                Damage.Play();
                Instantiate(Bleed, Other.transform.position, Quaternion.identity);
            }
            else if (Other.CompareTag("ProjectileThree"))
            {
                Destroy(Other.gameObject);
                Damage.Play();
                Instantiate(Bleed, Other.transform.position, Quaternion.identity);
            }
            else if (Other.CompareTag("ProjectileFourSharpnel"))
            {
                Destroy(Other.gameObject);
                Damage.Play();
                Instantiate(Bleed, Other.transform.position, Quaternion.identity);
            }
            else if (Other.CompareTag("RiotDrone"))
            {
                Destroy(Other.gameObject);
                Damage.Play();
                Instantiate(Bleed, Other.transform.position, Quaternion.identity);
            }
            else if (Other.CompareTag("RiotDroneProjectile"))
            {
                Destroy(Other.gameObject);
                Damage.Play();
                Instantiate(Bleed, Other.transform.position, Quaternion.identity);
            }
        }
    }

    public void Active()
    {
        if(Stages.ShieldActive == false)
        {
            A.enabled = false;
            B.enabled = false;
        }
        else if(Stages.ShieldActive == true)
        {
            A.enabled = true;
            B.enabled = true;
        }
    }

}
