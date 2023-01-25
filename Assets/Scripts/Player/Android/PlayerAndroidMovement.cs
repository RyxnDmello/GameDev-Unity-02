using UnityEngine;

public class PlayerAndroidMovement : MonoBehaviour
{
    [Header("PLAYER MOVEMENET")]
    [HideInInspector] 
    public float Velocity;
    private Vector2 MoveInput;
    private float MoveInputX;
    private float MoveInputY;

    [Header("JOYSTICKS")]
    private FloatingJoystick MoveJoystick;

    [Header("COMPONENTS")]
    private Rigidbody2D Rb;

    private void Start()
    {
        SetData();
    }

    private void Update()
    {
        Inputs();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void SetData()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void Inputs()
    {
        if (IsMovingHorizontal()) MoveInputX = MoveJoystick.Horizontal;
        else MoveInputX = 0f;

        if (IsMovingVertical()) MoveInputY = MoveJoystick.Vertical;
        else MoveInputY = 0f;

        MoveInput = new Vector2(MoveInputX, MoveInputY);
        MoveInput.Normalize();
    }

    private bool IsMovingHorizontal()
    {
        if (MoveJoystick.Horizontal >= 0.3f || MoveJoystick.Horizontal <= -0.3f) return true;
        else return false;
    }

    private bool IsMovingVertical()
    {
        if (MoveJoystick.Vertical >= 0.3f || MoveJoystick.Vertical <= -0.3f) return true;
        else return false;
    }

    private void Movement()
    {
        Rb.velocity = MoveInput * Velocity;
    }
}
