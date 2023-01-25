using UnityEngine;

[System.Serializable]
public class HealthSpawn
{
    [Header("HEALTH")]
    [SerializeField] public GameObject HealthOrb;
    [Range(2, 5)] public int StartSpawnCount;
    [HideInInspector] public int SpawnCount;


    [Header("SPAWN TIME")]
    [Range(5, 20)] public float StartSpawnTime;
    [Range(5, 30)] public float StartResetTime;

    [Header("LOOP TIME")]
    [HideInInspector] public float SpawnTime;
    [HideInInspector] public float ResetTime;
}

public class HealthSpawner : MonoBehaviour
{
    [Header("HEALTH SPAWNER")]
    [SerializeField] public HealthSpawn Health = new HealthSpawn();

    private void Start()
    {
        SetData();
    }

    private void SetData()
    {
        Health.SpawnCount = Health.StartSpawnCount;
        Health.SpawnTime = Health.StartSpawnTime;
        Health.ResetTime = Health.StartResetTime;
    }

    public void HealthSpawn()
    {
        if (Health.SpawnCount > 0)
        {
            if (Health.SpawnTime <= 0)
            {
                Instantiate(Health.HealthOrb, transform.position, Quaternion.identity);
                Health.SpawnCount--;
                Health.SpawnTime = Health.StartSpawnTime;
            }
            else
            {
                Health.SpawnTime -= Time.deltaTime;
            }
        }

        HealthReset();
    }

    private void HealthReset()
    {
        if (Health.SpawnCount == 0)
        {
            if (Health.ResetTime <= 0)
            {
                Health.SpawnCount = Health.StartSpawnCount;
                Health.ResetTime = Health.StartResetTime;
            }
            else
            {
                Health.ResetTime -= Time.deltaTime;
            }
        }
    }
}
