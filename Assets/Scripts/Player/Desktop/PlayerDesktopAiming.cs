using UnityEngine;

public class PlayerDesktopAiming : MonoBehaviour
{
    [Header("AIMING")]
    [HideInInspector] public bool IsAiming;

    [Header("CONTROLS")]
    private Vector2 MousePosition;
    private Vector2 Direction;
    private float AimAngle;

    [Header("COMPONENTS")]
    private Rigidbody2D Rb;
    private Camera Main;

    private void Start()
    {
        SetData();
    }

    private void FixedUpdate()
    {
        Aiming();
    }

    private void SetData()
    {
        Rb = GetComponent<Rigidbody2D>();
        Main = Camera.main;
        IsAiming = true;
    }

    private void Aiming()
    {
        if (IsAiming)
        {
            MousePosition = Main.ScreenToWorldPoint(Input.mousePosition);
            Direction = Rb.position - MousePosition;

            AimAngle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg + 90f;
            Rb.rotation = AimAngle;
        }
    }
}
