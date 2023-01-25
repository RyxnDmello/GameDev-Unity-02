using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [Header("AMMO MANAGER")]
    [SerializeField] private CollectiblesSpawner Ammo = new CollectiblesSpawner();

    [Header("AMMO FIVE")]
    private int AmmoFiveStage;

    [Header("AMMO SPAWNERS")]
    [SerializeField] private AmmoSpawner[] Spawn;

    [Header("REFERENCES")]
    private PlayerUniversal Player;

    private void Start()
    {
        SetData();
    }

    private void Update()
    {
        GameLevels();
        GrenadeAmmo();
    }

    private void SetData()
    {
        Player = FindObjectOfType<PlayerUniversal>();

        Ammo.LevelTwoSpawnTime = Ammo.LevelTwoStartSpawnTime;
        Ammo.LevelThreeSpawnTime = Ammo.LevelThreeStartSpawnTime;

        Ammo.LevelTwoSpawnSet = Random.Range(1, 3);
        Ammo.LevelThreeSpawnSet = Random.Range(1, 3);

        SetAmmoFiveStage(0000, 1000, 1);
        SetAmmoFiveStage(1500, 2000, 2);
        SetAmmoFiveStage(2500, 3000, 3);
        SetAmmoFiveStage(3500, 4000, 4);
        SetAmmoFiveStage(4500, 5000, 5);
        SetAmmoFiveStage(5500, 6000, 6);
        SetAmmoFiveStage(6500, 7000, 7);
        SetAmmoFiveStage(7500, 8000, 8);
        SetAmmoFiveStage(8500, 9000, 9);
        SetAmmoFiveStage(9500, 10000, 10);
    }

    private void GameLevels()
    {
        if (PlayerPrefs.GetInt("GameLevel") == 1 && Player.Points < 2000) GameLevelOne();
        else if (PlayerPrefs.GetInt("GameLevel") == 2 && Player.Points < 3000) GameLevelTwo();
        else if (PlayerPrefs.GetInt("GameLevel") == 3 && Player.Points >= 3000) GameLevelThree();
    }

    private void GameLevelOne()
    {
        SetOne(Ammo.LevelOnePoints, Ammo.LevelTwoPoints);
        SetTwo(Ammo.LevelTwoPoints, Ammo.LevelThreePoints);
        SetThree(Ammo.LevelThreePoints, Ammo.LevelFourPoints);
        SetFour(Ammo.LevelFourPoints, Ammo.LevelFivePoints);
    }

    private void SetOne(int StartPoints, int EndPoints)
    {
        if (Player.Points >= StartPoints && Player.Points < EndPoints)
        {
            Spawn[0].SpawnAmmo(2);
            Spawn[1].SpawnAmmo(2);
            Spawn[2].SpawnAmmo(2);
            Spawn[3].SpawnAmmo(2);
        }
    }

    private void SetTwo(int StartPoints, int EndPoints)
    {
        if (Player.Points >= StartPoints && Player.Points < EndPoints)
        {
            Spawn[4].SpawnAmmo(2);
            Spawn[5].SpawnAmmo(2);
            Spawn[6].SpawnAmmo(3);
            Spawn[7].SpawnAmmo(3);
        }
    }

    private void SetThree(int StartPoints, int EndPoints)
    {
        if (Player.Points >= StartPoints && Player.Points < EndPoints)
        {
            Spawn[0].SpawnAmmo(2);
            Spawn[2].SpawnAmmo(3);
            Spawn[4].SpawnAmmo(3);
            Spawn[6].SpawnAmmo(4);
        }
    }

    public void SetFour(int StartPoints, int EndPoints)
    {
        if (Player.Points >= StartPoints && Player.Points < EndPoints)
        {
            Spawn[7].SpawnAmmo(2);
            Spawn[6].SpawnAmmo(2);
            Spawn[5].SpawnAmmo(2);
            Spawn[4].SpawnAmmo(3);
            Spawn[3].SpawnAmmo(3);
            Spawn[2].SpawnAmmo(4);
            Spawn[1].SpawnAmmo(4);
        }
    }

    private void GameLevelTwo()
    {
        if (Ammo.LevelTwoStartTime <= 0)
        {
            if (Ammo.LevelTwoSpawnSet == 1)
            {
                Spawn[0].SpawnAmmo(2);
                Spawn[2].SpawnAmmo(3);
                Spawn[4].SpawnAmmo(2);
                Spawn[6].SpawnAmmo(3);
                Spawn[7].SpawnAmmo(2);
            }
            else if (Ammo.LevelTwoSpawnSet == 2)
            {
                Spawn[1].SpawnAmmo(2);
                Spawn[3].SpawnAmmo(3);
                Spawn[5].SpawnAmmo(4);
                Spawn[7].SpawnAmmo(4);
            }


            if (Ammo.LevelTwoSpawnTime <= 0)
            {
                if (Ammo.LevelTwoSpawnSet == 1) Ammo.LevelTwoSpawnSet = 2;
                else if (Ammo.LevelTwoSpawnSet == 2) Ammo.LevelTwoSpawnSet = 1;

                Ammo.LevelTwoSpawnTime = Ammo.LevelTwoStartSpawnTime;
            }
            else
            {
                Ammo.LevelTwoSpawnTime -= Time.deltaTime;
            }
        }
        else
        {
            Ammo.LevelTwoStartTime -= Time.deltaTime;
        }
    }

    private void GameLevelThree()
    {
        if (Ammo.LevelThreeStartTime <= 0)
        {
            if (Ammo.LevelThreeSpawnSet == 1)
            {
                Spawn[1].SpawnAmmo(2);
                Spawn[3].SpawnAmmo(3);
                Spawn[5].SpawnAmmo(4);
                Spawn[7].SpawnAmmo(4);
            }
            else if (Ammo.LevelThreeSpawnSet == 2)
            {
                Spawn[0].SpawnAmmo(2);
                Spawn[2].SpawnAmmo(3);
                Spawn[4].SpawnAmmo(2);
                Spawn[6].SpawnAmmo(3);
                Spawn[7].SpawnAmmo(2);
            }


            if (Ammo.LevelThreeSpawnTime <= 0)
            {
                if (Ammo.LevelThreeSpawnSet == 1) Ammo.LevelThreeSpawnSet = 2;
                else if (Ammo.LevelThreeSpawnSet == 2) Ammo.LevelThreeSpawnSet = 1;

                Ammo.LevelThreeSpawnTime = Ammo.LevelThreeStartSpawnTime;
            }
            else
            {
                Ammo.LevelThreeSpawnTime -= Time.deltaTime;
            }
        }
        else
        {
            Ammo.LevelThreeStartTime -= Time.deltaTime;
        }
    }

    private void BossLevel()
    {
        if (Ammo.BossLevelStartTime <= 0)
        {
            if (Ammo.BossSpawnSet == 1)
            {
                Spawn[1].SpawnAmmo(2);
                Spawn[2].SpawnAmmo(3);
                Spawn[3].SpawnAmmo(2);
                Spawn[4].SpawnAmmo(3);
                Spawn[5].SpawnAmmo(4);
            }
            else if (Ammo.BossSpawnSet == 2)
            {
                Spawn[7].SpawnAmmo(4);
                Spawn[6].SpawnAmmo(2);
                Spawn[5].SpawnAmmo(3);
                Spawn[4].SpawnAmmo(2);
                Spawn[3].SpawnAmmo(4);
            }

            if (Ammo.BossSpawnTime <= 0)
            {
                if (Ammo.BossSpawnSet == 1) Ammo.BossSpawnSet = 2;
                else if (Ammo.BossSpawnSet == 2) Ammo.BossSpawnSet = 1;

                Ammo.BossSpawnTime = Ammo.BossStartSpawnTime;
            }
            else
            {
                Ammo.BossSpawnTime -= Time.deltaTime;
            }
        }
        else
        {
            Ammo.BossLevelStartTime -= Time.deltaTime;
        }
    }

    private void GrenadeAmmo()
    {
        LoadGrenadeAmmo(1000, 1500, 1);
        LoadGrenadeAmmo(1500, 2000, 2);
        LoadGrenadeAmmo(2500, 3000, 3);
        LoadGrenadeAmmo(3500, 4000, 4);
        LoadGrenadeAmmo(4500, 5000, 5);
        LoadGrenadeAmmo(5500, 6000, 6);
        LoadGrenadeAmmo(6500, 7000, 7);
        LoadGrenadeAmmo(7500, 8000, 8);
        LoadGrenadeAmmo(8500, 9000, 9);
        LoadGrenadeAmmo(9500, 10000, 10);
    }

    private void LoadGrenadeAmmo(int StartHighScore, int EndHighScore, int ThisStage)
    {
        if(PlayerPrefs.GetInt("HighScore") >= StartHighScore && PlayerPrefs.GetInt("HighScore") < EndHighScore)
        {
            if(AmmoFiveStage == ThisStage && Player.AmmoFive < 3) Player.AmmoFive += AddGrenades(2);
            else AmmoFiveStage++;
        }
    }

    private int AddGrenades(int Loaded)
    {
        AmmoFiveStage++;
        return Loaded;
    }

    private void SetAmmoFiveStage(int StartHighScore, int EndHighScore, int Value)
    {
        if (PlayerPrefs.GetInt("HighScore") >= StartHighScore && PlayerPrefs.GetInt("HighScore") < EndHighScore)
        {
            AmmoFiveStage = Value;
        }
    }
}
