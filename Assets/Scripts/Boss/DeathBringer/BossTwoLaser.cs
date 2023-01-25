using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoLaser : MonoBehaviour
{
    [Header("LASER")]
    float LifeTime;

    [Header("PARTICLES")]
    [SerializeField] GameObject Death;

    private void Start()
    {
        LifeTime = 10f;
    }

    private void Update()
    {
        Life();
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
