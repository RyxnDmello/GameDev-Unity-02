using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("HEALTH MANAGER")]
    [SerializeField] private CollectiblesSpawner Health = new CollectiblesSpawner();

    [Header("HEALTH SPAWNERS")]
    [SerializeField] private HealthSpawner[] Spawn;

    [Header("REFERENCES")]
    private PlayerUniversal Player;

    private void Start()
    {
        SetData();
    }

    private void Update()
    {
        GameLevels();
    }

    private void SetData()
    {
        Player = FindObjectOfType<PlayerUniversal>();

        Health.LevelTwoSpawnTime = Health.LevelTwoStartSpawnTime;
        Health.LevelThreeSpawnTime = Health.LevelThreeStartSpawnTime;

        Health.LevelTwoSpawnSet = Random.Range(1, 3);
        Health.LevelThreeSpawnSet = Random.Range(1, 3);
    }

    private void GameLevels()
    {
        if (PlayerPrefs.GetInt("GameLevel") == 1 && Player.Points < 2000) GameLevelOne();
        else if (PlayerPrefs.GetInt("GameLevel") == 2 && Player.Points < 3000) GameLevelTwo();
        else if (PlayerPrefs.GetInt("GameLevel") == 3 && Player.Points >= 3000) GameLevelThree();
    }

    private void GameLevelOne()
    {
        SetOne(Health.LevelOnePoints, Health.LevelTwoPoints);
        SetTwo(Health.LevelTwoPoints, Health.LevelThreePoints);
        SetThree(Health.LevelThreePoints, Health.LevelFourPoints);
        SetFour(Health.LevelFourPoints, Health.LevelFivePoints);
    }

    private void SetOne(int StartPoints, int EndPoints)
    {
        if (Player.Points >= StartPoints && Player.Points < EndPoints)
        {
            Spawn[0].HealthSpawn();
            Spawn[1].HealthSpawn();
            Spawn[2].HealthSpawn();
            Spawn[3].HealthSpawn();
        }
    }

    private void SetTwo(int StartPoints, int EndPoints)
    {
        if (Player.Points >= StartPoints && Player.Points < EndPoints)
        {
            Spawn[4].HealthSpawn();
            Spawn[5].HealthSpawn();
            Spawn[6].HealthSpawn();
            Spawn[7].HealthSpawn();
        }
    }

    private void SetThree(int StartPoints, int EndPoints)
    {
        if (Player.Points >= StartPoints && Player.Points < EndPoints)
        {
            Spawn[0].HealthSpawn();
            Spawn[2].HealthSpawn();
            Spawn[4].HealthSpawn();
            Spawn[6].HealthSpawn();
        }
    }

    public void SetFour(int StartPoints, int EndPoints)
    {
        if (Player.Points >= StartPoints && Player.Points < EndPoints)
        {
            Spawn[7].HealthSpawn();
            Spawn[6].HealthSpawn();
            Spawn[5].HealthSpawn();
            Spawn[4].HealthSpawn();
            Spawn[3].HealthSpawn();
            Spawn[2].HealthSpawn();
            Spawn[1].HealthSpawn();
        }
    }

    private void GameLevelTwo()
    {
        if (Health.LevelTwoStartTime <= 0)
        {
            if (Health.LevelTwoSpawnSet == 1)
            {
                Spawn[0].HealthSpawn();
                Spawn[2].HealthSpawn();
                Spawn[4].HealthSpawn();
                Spawn[6].HealthSpawn();
                Spawn[7].HealthSpawn();
            }
            else if (Health.LevelTwoSpawnSet == 2)
            {
                Spawn[1].HealthSpawn();
                Spawn[3].HealthSpawn();
                Spawn[5].HealthSpawn();
                Spawn[7].HealthSpawn();
            }


            if (Health.LevelTwoSpawnTime <= 0)
            {
                if (Health.LevelTwoSpawnSet == 1) Health.LevelTwoSpawnSet = 2;
                else if (Health.LevelTwoSpawnSet == 2) Health.LevelTwoSpawnSet = 1;

                Health.LevelTwoSpawnTime = Health.LevelTwoStartSpawnTime;
            }
            else
            {
                Health.LevelTwoSpawnTime -= Time.deltaTime;
            }
        }
        else
        {
            Health.LevelTwoStartTime -= Time.deltaTime;
        }
    }

    private void GameLevelThree()
    {
        if (Health.LevelThreeStartTime <= 0)
        {
            if (Health.LevelThreeSpawnSet == 1)
            {
                Spawn[1].HealthSpawn();
                Spawn[3].HealthSpawn();
                Spawn[5].HealthSpawn();
                Spawn[7].HealthSpawn();
            }
            else if (Health.LevelThreeSpawnSet == 2)
            {
                Spawn[0].HealthSpawn();
                Spawn[2].HealthSpawn();
                Spawn[4].HealthSpawn();
                Spawn[6].HealthSpawn();
                Spawn[7].HealthSpawn();
            }


            if (Health.LevelThreeSpawnTime <= 0)
            {
                if (Health.LevelThreeSpawnSet == 1) Health.LevelThreeSpawnSet = 2;
                else if (Health.LevelThreeSpawnSet == 2) Health.LevelThreeSpawnSet = 1;

                Health.LevelThreeSpawnTime = Health.LevelThreeStartSpawnTime;
            }
            else
            {
                Health.LevelThreeSpawnTime -= Time.deltaTime;
            }
        }
        else
        {
            Health.LevelThreeStartTime -= Time.deltaTime;
        }
    }

    private void BossLevel()
    {
        if (Health.BossLevelStartTime <= 0)
        {
            if (Health.BossSpawnSet == 1)
            {
                Spawn[1].HealthSpawn();
                Spawn[2].HealthSpawn();
                Spawn[3].HealthSpawn();
                Spawn[4].HealthSpawn();
                Spawn[5].HealthSpawn();
            }
            else if (Health.BossSpawnSet == 2)
            {
                Spawn[7].HealthSpawn();
                Spawn[6].HealthSpawn();
                Spawn[5].HealthSpawn();
                Spawn[4].HealthSpawn();
                Spawn[3].HealthSpawn();
            }

            if (Health.BossSpawnTime <= 0)
            {
                if (Health.BossSpawnSet == 1) Health.BossSpawnSet = 2;
                else if (Health.BossSpawnSet == 2) Health.BossSpawnSet = 1;

                Health.BossSpawnTime = Health.BossStartSpawnTime;
            }
            else
            {
                Health.BossSpawnTime -= Time.deltaTime;
            }
        }
        else
        {
            Health.BossLevelStartTime -= Time.deltaTime;
        }
    }
}