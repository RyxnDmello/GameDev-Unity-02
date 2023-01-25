using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("ENEMY MANAGER")]
    [SerializeField] private EnemyManage Enemy = new EnemyManage();

    [Header("ENEMY SPAWNERS")]
    [SerializeField] private EnemySpawner[] Spawn;

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

        Enemy.LevelTwoSpawnTime = Enemy.LevelTwoStartSpawnTime;
        Enemy.LevelThreeSpawnTime = Enemy.LevelThreeStartSpawnTime;

        Enemy.LevelTwoSpawnSet = Random.Range(1, 4);
        Enemy.LevelThreeSpawnSet = Random.Range(1, 4);
    }

    private void GameLevels()
    {
        if (PlayerPrefs.GetInt("GameLevel") == 1 && Player.Points < 2000) GameLevelOne();
        else if (PlayerPrefs.GetInt("GameLevel") == 2 && Player.Points < 3000) GameLevelTwo();
        else if (PlayerPrefs.GetInt("GameLevel") == 3 && Player.Points >= 3000) GameLevelThree();

        if (PlayerPrefs.GetInt("GameLevel") == 1 && Player.Points >= 2000 && Enemy.IsBossOneActive == false) SpawnBossOne();
        if (PlayerPrefs.GetInt("GameLevel") == 2 && Player.Points >= 3000 && Enemy.IsBossTwoActive == false) SpawnBossTwo();
    }

    private void GameLevelOne()
    {
        SetOne(Enemy.LevelOnePoints, Enemy.LevelTwoPoints);
        SetTwo(Enemy.LevelTwoPoints, Enemy.LevelThreePoints);
        SetThree(Enemy.LevelThreePoints, Enemy.LevelFourPoints);
        SetFour(Enemy.LevelFourPoints, Enemy.LevelFivePoints);
    }

    private void SetOne(int StartPoints, int EndPoints)
    {
        if (Player.Points >= StartPoints && Player.Points < EndPoints)
        {
            Spawn[0].SpawnEnemy(1);
            Spawn[1].SpawnEnemy(1);
            Spawn[2].SpawnEnemy(1);
            Spawn[3].SpawnEnemy(1);
        }
    }

    private void SetTwo(int StartPoints, int EndPoints)
    {
        if (Player.Points >= StartPoints && Player.Points < EndPoints)
        {
            Spawn[4].SpawnEnemy(1);
            Spawn[5].SpawnEnemy(2);
            Spawn[6].SpawnEnemy(1);
            Spawn[7].SpawnEnemy(2);
        }
    }

    private void SetThree(int StartPoints, int EndPoints)
    {
        if (Player.Points >= StartPoints && Player.Points < EndPoints)
        {
            Spawn[0].SpawnEnemy(1);
            Spawn[2].SpawnEnemy(3);
            Spawn[4].SpawnEnemy(2);
            Spawn[6].SpawnEnemy(3);

            if (Player.Points >= (EndPoints - 150) && Player.Points < EndPoints)
            {
                Spawn[1].SpawnEnemy(1);
                Spawn[7].SpawnEnemy(4);
            }
        }
    }

    public void SetFour(int StartPoints, int EndPoints)
    {
        if (Player.Points >= StartPoints && Player.Points < EndPoints)
        {
            Spawn[7].SpawnEnemy(2);
            Spawn[6].SpawnEnemy(3);
            Spawn[5].SpawnEnemy(2);
            Spawn[4].SpawnEnemy(4);

            if (Player.Points >= (EndPoints - 150) && Player.Points < EndPoints)
            {
                Spawn[3].SpawnEnemy(1);
                Spawn[2].SpawnEnemy(4);
                Spawn[1].SpawnEnemy(3);
            }
        }
    }

    private void GameLevelTwo()
    {
        if (Enemy.LevelTwoStartTime <= 0)
        {
            if (Enemy.LevelTwoSpawnSet == 1)
            {
                Spawn[1].SpawnEnemy(3);
                Spawn[2].SpawnEnemy(2);
                Spawn[4].SpawnEnemy(1);
                Spawn[6].SpawnEnemy(2);
                Spawn[7].SpawnEnemy(3);
            }
            else if (Enemy.LevelTwoSpawnSet == 2)
            {
                Spawn[0].SpawnEnemy(1);
                Spawn[3].SpawnEnemy(3);
                Spawn[5].SpawnEnemy(4);
                Spawn[1].SpawnEnemy(2);
            }
            else if (Enemy.LevelTwoSpawnSet == 3)
            {
                Spawn[1].SpawnEnemy(4);
                Spawn[3].SpawnEnemy(3);
                Spawn[6].SpawnEnemy(2);
                Spawn[4].SpawnEnemy(4);
                Spawn[2].SpawnEnemy(3);
            }

            if (Enemy.LevelTwoSpawnTime <= 0)
            {
                if (Enemy.LevelTwoSpawnSet == 1) Enemy.LevelTwoSpawnSet = Random.Range(2, 4);
                else if (Enemy.LevelTwoSpawnSet == 2) Enemy.LevelTwoSpawnSet = Random.Range(1, 4);
                else if (Enemy.LevelTwoSpawnSet == 3) Enemy.LevelTwoSpawnSet = Random.Range(1, 3);

                Enemy.LevelTwoSpawnTime = Enemy.LevelTwoStartSpawnTime;
            }
            else
            {
                Enemy.LevelTwoSpawnTime -= Time.deltaTime;
            }
        }
        else
        {
            Enemy.LevelTwoStartTime -= Time.deltaTime;
        }
    }

    private void GameLevelThree()
    {
        if (Enemy.LevelThreeStartTime <= 0)
        {
            if (Enemy.LevelThreeSpawnSet == 1)
            {
                Spawn[1].SpawnEnemy(3);
                Spawn[2].SpawnEnemy(2);
                Spawn[4].SpawnEnemy(1);
                Spawn[6].SpawnEnemy(2);
                Spawn[7].SpawnEnemy(3);
            }
            else if (Enemy.LevelThreeSpawnSet == 2)
            {
                Spawn[0].SpawnEnemy(1);
                Spawn[3].SpawnEnemy(3);
                Spawn[5].SpawnEnemy(4);
                Spawn[1].SpawnEnemy(2);
            }
            else if (Enemy.LevelThreeSpawnSet == 3)
            {
                Spawn[1].SpawnEnemy(4);
                Spawn[3].SpawnEnemy(3);
                Spawn[6].SpawnEnemy(2);
                Spawn[4].SpawnEnemy(4);
                Spawn[2].SpawnEnemy(3);
            }

            if (Enemy.LevelThreeSpawnTime <= 0)
            {
                if (Enemy.LevelThreeSpawnSet == 1) Enemy.LevelThreeSpawnSet = Random.Range(2, 4);
                else if (Enemy.LevelThreeSpawnSet == 2) Enemy.LevelThreeSpawnSet = Random.Range(1, 4);
                else if (Enemy.LevelThreeSpawnSet == 3) Enemy.LevelThreeSpawnSet = Random.Range(1, 3);

                Enemy.LevelThreeSpawnTime = Enemy.LevelThreeStartSpawnTime;
            }
            else
            {
                Enemy.LevelThreeSpawnTime -= Time.deltaTime;
            }
        }
        else
        {
            Enemy.LevelThreeStartTime -= Time.deltaTime;
        }
    }

    private void SpawnBossOne()
    {
        if (Enemy.BossOneSpawnTime <= 0)
        {
            Instantiate(Enemy.BossOne, Enemy.BossPoint.transform.position, Quaternion.identity);

            Enemy.IsBossOneActive = true;
        }
        else
        {
            Enemy.BossOneSpawnTime -= Time.deltaTime;
        }
    }

    private void SpawnBossTwo()
    {
        if (Enemy.BossTwoSpawnTime <= 0)
        {
            Instantiate(Enemy.BossTwo, Enemy.BossPoint.transform.position, Quaternion.identity);

            Enemy.IsBossTwoActive = true;
        }
        else
        {
            Enemy.BossTwoSpawnTime -= Time.deltaTime;
        }
    }
}
