using UnityEngine;

[System.Serializable]
public class CollectiblesBase
{
    [Header("LEVEL ONE")]
    [SerializeField] public int LevelOnePoints;
    [SerializeField] public int LevelTwoPoints;
    [SerializeField] public int LevelThreePoints;
    [SerializeField] public int LevelFourPoints;
    [SerializeField] public int LevelFivePoints;

    [Header("LEVEL TWO")]
    [Range(5, 20)] public float LevelTwoStartTime;
    [Range(5, 25)] public float LevelTwoStartSpawnTime;

    [Header("LEVEL THREE")]
    [Range(5, 20)] public float LevelThreeStartTime;
    [Range(5, 25)] public float LevelThreeStartSpawnTime;

    [Header("LEVELS TWO & THREE")]
    [HideInInspector] public float LevelTwoSpawnTime;
    [HideInInspector] public float LevelThreeSpawnTime;
    [HideInInspector] public int LevelTwoSpawnSet;
    [HideInInspector] public int LevelThreeSpawnSet;
}

[System.Serializable]
public class CollectiblesSpawner : CollectiblesBase
{
    [Header("BOSS LEVELS")]
    [Range(5, 20)] public float BossLevelStartTime;
    [Range(5, 25)] public float BossStartSpawnTime;
    [HideInInspector] public float BossSpawnTime;
    [HideInInspector] public int BossSpawnSet;
}

[System.Serializable]
public class EnemyManage : CollectiblesBase
{
    [Header("BOSSES")]
    [SerializeField] public GameObject BossOne;
    [SerializeField] public GameObject BossTwo;
    [SerializeField] public GameObject BossPoint;

    [Header("BOSS SPAWN TIME")]
    [Range(0, 8)] public float BossOneSpawnTime;
    [Range(0, 8)] public float BossTwoSpawnTime;
    [HideInInspector] public bool IsBossOneActive;
    [HideInInspector] public bool IsBossTwoActive;
}

