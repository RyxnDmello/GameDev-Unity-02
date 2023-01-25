using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoTrapG : MonoBehaviour
{
    [Header("GRENADE")]
    public GameObject Trap;
    float LifeTime;
    float RotateSpeedFour;
    float RotateFour;
    int RandDir;

    [Header("PARTICLE EFFECTS")]
    [SerializeField] GameObject Death;

    [Header("AQUIRED COMPONENTS")]
    BossTwoStages Stages;

    private void Start()
    {
        Stages = FindObjectOfType<BossTwoStages>();

        Randoms();
        LifeTime = 20f;
    }

    private void Update()
    {
        Rotation();
        Health();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.CompareTag("Player"))
        {
            if (Stages.TrapActive == false)
            {
                Instantiate(Trap, Other.transform.position, Other.transform.rotation, Other.transform);
                Instantiate(Death, transform.position, Quaternion.identity);
                Stages.TrapActive = true;
                Destroy(gameObject);
            }
            else
            {
                Instantiate(Death, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public void Health()
    {
        if(LifeTime <= 0)
        {
            Instantiate(Death, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            LifeTime -= Time.deltaTime;
        }
    }

    public void Rotation()
    {
        RotateFour = RotateFour + RotateSpeedFour * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, 0f, RotateFour);
    }

    private void Randoms()
    {
        RandDir = Random.Range(1, 3);

        if (RandDir == 1)
        {
            RotateSpeedFour = Random.Range(150f, 200f);
        }
        else if (RandDir == 2)
        {
            RotateSpeedFour = Random.Range(-150f, -200f);
        }
    }
}
