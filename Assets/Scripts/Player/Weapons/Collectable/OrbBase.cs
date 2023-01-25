using UnityEngine;

[System.Serializable]
public class OrbUniversal
{
    [Header("ORB TIME")]
    [Range(10f, 20f)] public float DeathTime;

    [Header("ORB MOVE")]
    [Range(2.5f, 6f)] public float Distance;
    [Range(2.5f, 6f)] public float Speed;
}
