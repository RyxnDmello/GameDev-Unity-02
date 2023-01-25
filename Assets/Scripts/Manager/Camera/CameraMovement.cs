using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("MOVEMENT")]
    private Vector3 CameraPosition;
    private Vector3 SmoothMovement;

    [Header("SMOOTHNESS")]
    private float SmoothValue;
    private float PosX;
    private float PosY;
    private float PosZ;

    [Header("REFERENCES")]
    private PlayerUniversal Player;

    private void Start()
    {
        SetData();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void SetData()
    {
        Player = FindObjectOfType<PlayerUniversal>();

        SmoothValue = 0.0825f;
    }

    private void Movement()
    {
        PosX = Player.transform.position.x;
        PosY = Player.transform.position.y;
        PosZ = transform.position.z;

        CameraPosition = new Vector3(PosX, PosY, PosZ);
        SmoothMovement = Vector3.Lerp(transform.position, CameraPosition, SmoothValue);
        transform.position = SmoothMovement;
    }
}