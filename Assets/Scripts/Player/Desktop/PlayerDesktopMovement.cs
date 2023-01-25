using UnityEngine;

public class PlayerDesktopMovement : MonoBehaviour
{
    [Header("MOVEMENT")]
    [HideInInspector] public float Velocity;
    
    [Header("CONTROLS")]
    private Vector2 MoveInput;
    private float MoveInputX;
    private float MoveInputY;

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
        Velocity = 20f;
    }

    private void Inputs()
    {
        MoveInputX = Input.GetAxis("Horizontal");
        MoveInputY = Input.GetAxis("Vertical");

        MoveInput = new Vector2(MoveInputX, MoveInputY);
        MoveInput.Normalize();
    }

    private void Movement()
    {
        Rb.velocity = MoveInput * Velocity;
    }
}
