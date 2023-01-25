using UnityEngine;

public class PlayerUniversal : MonoBehaviour
{
    [Header("PLAYER STATS")]
    [Range(0, 100)] public int Health;[Space(10)]
    [Range(0, 10000)] public int Points;

    [Header("AMMO")]
    [HideInInspector] public int WeaponType;
    [HideInInspector] public int AmmoTwo;
    [HideInInspector] public int AmmoThree;
    [HideInInspector] public int AmmoFour;
    [HideInInspector] public int AmmoFive;

    [Header("RIOT-DRONE")]
    [HideInInspector] public bool IsGrenadeActive;
    [HideInInspector] public bool IsDroneActive;

    [Header("GAME BOUNDS")]
    private float BoundsX;
    private float BoundsY;

    private void Start()
    {
        SetData();
    }

    private void Update()
    {
        PlayBounds();
    }

    private void SetData()
    {
        Health = 100;
        Points = 0;
    }

    private void PlayBounds()
    {
        BoundsX = Mathf.Clamp(transform.position.x, -400f, 400f);
        BoundsY = Mathf.Clamp(transform.position.y, -400f, 400f);

        transform.position = new Vector2(BoundsX, BoundsY);
    }
}
