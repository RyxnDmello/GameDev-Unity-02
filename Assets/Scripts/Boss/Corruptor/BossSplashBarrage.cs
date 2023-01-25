using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSplashBarrage : MonoBehaviour
{
    [Header("BARRAGE/SPLASH")]
    float LifeTime;

    [Header("PARTICLES")]
    [SerializeField] GameObject Death;

    private void Start()
    {
        LifeTime = 10f;
    }

    private void Update()
    {
        Health();
    }

    private void Health()
    {
        if(LifeTime <= 0)
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
