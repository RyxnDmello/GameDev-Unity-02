using UnityEngine;

[System.Serializable]
public class RiotDronesBase
{
    [Header("DRONE")]
    [SerializeField] [Range(0f, 20f)] public float LifeTime;
    [SerializeField] [Range(0f, 0.2f)] public float TimeToStop;
    [HideInInspector] public float TimeToLive;

    [Header("CONDITIONS")]
    [HideInInspector] public bool IsDroneLive;
    [HideInInspector] public bool IsDroneSet;
}