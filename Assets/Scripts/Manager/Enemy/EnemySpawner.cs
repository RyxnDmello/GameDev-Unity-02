using UnityEngine;

[System.Serializable]
public class EnemySpawn
{
    [Header("ENEMY")]
    [SerializeField] public GameObject EnemyOne;
    [SerializeField] public GameObject EnemyTwo;
    [SerializeField] public GameObject EnemyThree;
    [SerializeField] public GameObject EnemyFour;

    [Header("ENEMY COUNT")]
    [Range(2, 8)] public int StartCountOne;
    [Range(2, 8)] public int StartCountTwo;
    [Range(2, 8)] public int StartCountThree;
    [Range(2, 8)] public int StartCountFour;

    [Header("ENEMY ONE")]
    [Range(10, 30)] public float StartSpawnTimeOne;
    [Range(10, 30)] public float StartResetTimeOne;
    [HideInInspector] public float SpawnTimeOne;
    [HideInInspector] public float ResetTimeOne;
    [HideInInspector] public int CountOne;

    [Header("ENEMY TWO")]
    [Range(10, 30)] public float StartSpawnTimeTwo;
    [Range(10, 30)] public float StartResetTimeTwo;
    [HideInInspector] public float SpawnTimeTwo;
    [HideInInspector] public float ResetTimeTwo;
    [HideInInspector] public int CountTwo;

    [Header("ENEMY THREE")]
    [Range(10, 30)] public float StartSpawnTimeThree;
    [Range(10, 30)] public float StartResetTimeThree;
    [HideInInspector] public float SpawnTimeThree;
    [HideInInspector] public float ResetTimeThree;
    [HideInInspector] public int CountThree;

    [Header("ENEMY FOUR")]
    [Range(10, 30)] public float StartSpawnTimeFour;
    [Range(10, 30)] public float StartResetTimeFour;
    [HideInInspector] public float SpawnTimeFour;
    [HideInInspector] public float ResetTimeFour;
    [HideInInspector] public int CountFour;
}

public class EnemySpawner : MonoBehaviour
{
    [Header("ENEMY SPAWNER")]
    [SerializeField] private EnemySpawn Enemy = new EnemySpawn();

    private void Start()
    {
        SetData();
    }

    private void SetData()
    {
        Enemy.CountOne = Enemy.StartCountOne;
        Enemy.CountTwo = Enemy.StartCountTwo;
        Enemy.CountThree = Enemy.StartCountThree;
        Enemy.CountFour = Enemy.StartCountFour;

        Enemy.ResetTimeOne = Enemy.StartResetTimeOne;
        Enemy.ResetTimeTwo = Enemy.StartResetTimeTwo;
        Enemy.ResetTimeThree = Enemy.StartResetTimeThree;
        Enemy.ResetTimeFour = Enemy.StartResetTimeFour;

        Enemy.SpawnTimeOne = 6f;
        Enemy.SpawnTimeTwo = 6f;
        Enemy.SpawnTimeThree = 6f;
        Enemy.SpawnTimeFour = 6f;
    }

    public void SpawnEnemy(int Type)
    {
        if (Type == 1) EnemyOne();
        else if (Type == 2) EnemyTwo();
        else if (Type == 3) EnemyThree();
        else if (Type == 4) EnemyFour();
    }

    private void EnemyOne()
    {
        if (Enemy.CountOne > 0)
        {
            if (Enemy.SpawnTimeOne <= 0)
            {
                Instantiate(Enemy.EnemyOne, transform.position, Quaternion.identity);
                Enemy.SpawnTimeOne = Enemy.StartSpawnTimeOne;
                Enemy.CountOne--;
            }
            else
            {
                Enemy.SpawnTimeOne -= Time.deltaTime;
            }
        }

        if (Enemy.CountOne == 0) ResetOne();
    }

    private void EnemyTwo()
    {
        if (Enemy.CountTwo > 0)
        {
            if (Enemy.SpawnTimeTwo <= 0)
            {
                Instantiate(Enemy.EnemyTwo, transform.position, Quaternion.identity);
                Enemy.SpawnTimeTwo = Enemy.StartSpawnTimeTwo;
                Enemy.CountTwo--;
            }
            else
            {
                Enemy.SpawnTimeTwo -= Time.deltaTime;
            }
        }

        if (Enemy.CountTwo == 0) ResetTwo();
    }

    private void EnemyThree()
    {
        if (Enemy.CountThree > 0)
        {
            if (Enemy.SpawnTimeThree <= 0)
            {
                Instantiate(Enemy.EnemyThree, transform.position, Quaternion.identity);
                Enemy.SpawnTimeThree = Enemy.StartSpawnTimeThree;
                Enemy.CountThree--;
            }
            else
            {
                Enemy.SpawnTimeThree -= Time.deltaTime;
            }
        }

        if (Enemy.CountThree == 0) ResetThree();
    }

    private void EnemyFour()
    {
        if (Enemy.CountFour > 0)
        {
            if (Enemy.SpawnTimeFour <= 0)
            {
                Instantiate(Enemy.EnemyFour, transform.position, Quaternion.identity);
                Enemy.SpawnTimeFour = Enemy.StartSpawnTimeFour;
                Enemy.CountFour--;
            }
            else
            {
                Enemy.SpawnTimeFour -= Time.deltaTime;
            }
        }

        if (Enemy.CountFour == 0) ResetFour();
    }

    private void ResetOne()
    {
        if (Enemy.ResetTimeOne <= 0)
        {
            Enemy.CountOne = Enemy.StartCountOne;
            Enemy.ResetTimeOne = Enemy.StartResetTimeOne;
        }
        else Enemy.ResetTimeOne -= Time.deltaTime;
    }

    private void ResetTwo()
    {
        if (Enemy.ResetTimeTwo <= 0)
        {
            Enemy.CountTwo = Enemy.StartCountTwo;
            Enemy.ResetTimeTwo = Enemy.StartResetTimeTwo;
        }
        else Enemy.ResetTimeTwo -= Time.deltaTime;
    }

    private void ResetThree()
    {
        if (Enemy.ResetTimeThree <= 0)
        {
            Enemy.CountThree = Enemy.StartCountThree;
            Enemy.ResetTimeThree = Enemy.StartResetTimeThree;
        }
        else Enemy.ResetTimeThree -= Time.deltaTime;
    }

    private void ResetFour()
    {
        if (Enemy.ResetTimeFour <= 0)
        {
            Enemy.CountFour = Enemy.StartCountFour;
            Enemy.ResetTimeFour = Enemy.StartResetTimeFour;
        }
        else Enemy.ResetTimeFour -= Time.deltaTime;
    }
}
