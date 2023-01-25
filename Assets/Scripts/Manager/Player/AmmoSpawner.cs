using UnityEngine;

[System.Serializable]
public class AmmoSpawn
{
    [Header("AMMO")]
    [SerializeField] public GameObject MagnumAmmo;
    [SerializeField] public GameObject RoadBlockAmmo;
    [SerializeField] public GameObject RiotDroneAmmo;

    [Header("SPAWN TIME")]
    [Range(0, 25)] public float StartSpawnTimeTwo;
    [Range(0, 25)] public float StartSpawnTimeThree;
    [Range(0, 25)] public float StartSpawnTimeFour;

    [Header("LOOP TIME")]
    [HideInInspector] public float SpawnTimeTwo;
    [HideInInspector] public float SpawnTimeThree;
    [HideInInspector] public float SpawnTimeFour;
}

public class AmmoSpawner : MonoBehaviour
{
    [Header("AMMO SPAWNER")]
    [SerializeField] private AmmoSpawn Ammo = new AmmoSpawn();

    [Header("REFERENCES")]
    private PlayerUniversal Player;

    private void Start()
    {
        SetData();
    }

    private void SetData()
    {
        Player = FindObjectOfType<PlayerUniversal>();

        Ammo.SpawnTimeTwo = Ammo.StartSpawnTimeTwo;
        Ammo.SpawnTimeThree = Ammo.StartSpawnTimeThree;
        Ammo.SpawnTimeFour = Ammo.StartSpawnTimeFour;
    }

    public void SpawnAmmo(int Type)
    {
        if (Type == 2) SpawnMagnumAmmo();
        else if (Type == 3) SpawnRoadBlockAmmo();
        else if (Type == 4) SpawnRiotDroneAmmo();
    }

    private void SpawnMagnumAmmo()
    {
        if (Player.AmmoTwo <= 2)
        {
            if (Ammo.SpawnTimeTwo <= 0)
            {
                Instantiate(Ammo.MagnumAmmo, transform.position, Ammo.MagnumAmmo.transform.rotation);

                Ammo.SpawnTimeTwo = Ammo.StartSpawnTimeTwo;
            }
            else
            {
                Ammo.SpawnTimeTwo -= Time.deltaTime;
            }
        }
    }

    private void SpawnRoadBlockAmmo()
    {
        if (Player.AmmoThree <= 2)
        {
            if (Ammo.SpawnTimeThree <= 0)
            {
                Instantiate(Ammo.RoadBlockAmmo, transform.position, Ammo.RoadBlockAmmo.transform.rotation);

                Ammo.SpawnTimeThree = Ammo.StartSpawnTimeThree;
            }
            else
            {
                Ammo.SpawnTimeThree -= Time.deltaTime;
            }
        }
    }

    private void SpawnRiotDroneAmmo()
    {
        if (Player.AmmoFour == 0)
        {
            if (Ammo.SpawnTimeFour <= 0)
            {
                Instantiate(Ammo.RiotDroneAmmo, transform.position, Ammo.RiotDroneAmmo.transform.rotation);

                Ammo.SpawnTimeFour = Ammo.StartSpawnTimeFour;
            }
            else
            {
                Ammo.SpawnTimeFour -= Time.deltaTime;
            }
        }
    }
}
