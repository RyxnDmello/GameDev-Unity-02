using UnityEngine;

public class EnemyAiming : MonoBehaviour
{
    [Header("AIMING")]
    [Range(0f, 90f)] public float CorrectorAngle;
    private Vector2 AimingDirection;
    private Vector2 PlayerPosition;
    private float AimingAngle;

    [Header("REFERENCES")]
    private PlayerUniversal Player;

    [Header("COMPONENTS")]
    private Rigidbody2D Rigidbody;

    private void Start()
    {
        Player = FindObjectOfType<PlayerUniversal>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Aiming();
    }

    public void Aiming()
    {
        PlayerPosition = Player.transform.position;
        AimingDirection = (Vector2)transform.position - PlayerPosition;

        AimingAngle = Mathf.Atan2(AimingDirection.y, AimingDirection.x) * Mathf.Rad2Deg + CorrectorAngle;
        Rigidbody.rotation = AimingAngle;
    }
}
