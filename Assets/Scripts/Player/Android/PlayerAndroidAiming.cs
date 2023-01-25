using UnityEngine;

public class PlayerAndroidAiming : MonoBehaviour
{
    [Header("AIMING")]
    [HideInInspector] public Vector2 ShootValue;
    [HideInInspector] public bool IsAiming;
    private float AimAngle;

    [Header("JOYSTICK")]
    private FloatingJoystick RotateJoystick;

    [Header("COMPONENTS")]
    private Rigidbody2D Rb;

    private void Start()
    {
        SetData();
    }

    private void Update()
    {
        GetShootValue();
    }

    private void FixedUpdate()
    {
        Aiming();
    }

    private void SetData()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void GetShootValue()
    {
        ShootValue = new Vector2(RotateJoystick.Horizontal, RotateJoystick.Vertical);
    }

    private void Aiming()
    {
        if (IsAiming)
        {
            if (IsAimable())
            {
                AimAngle = Mathf.Atan2(RotateJoystick.Vertical, RotateJoystick.Horizontal) * Mathf.Rad2Deg - 90f;
            }
            else
            {
                AimAngle = Mathf.Atan2(RotateJoystick.Vertical, RotateJoystick.Horizontal) * Mathf.Rad2Deg;
            }

            Rb.rotation = AimAngle;
        }
    }

    private bool IsAimable()
    {
        if (RotateJoystick.Horizontal != 0 || RotateJoystick.Vertical != 0) return true;
        else return false;
    }
}
