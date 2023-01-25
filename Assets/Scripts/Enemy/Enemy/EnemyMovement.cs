using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("LINEAR MOVEMENT")]
    [SerializeField] [Range(5f, 20f)] private float CatchingVelocity;
    [SerializeField] [Range(5f, 20f)] public float ChasingDistance;
    private Vector2 DistanceFromPlayer;
    private float RetreatDistance;
    private float RetreatVelocity;
    private bool IsRetreating;
    private bool IsChasing;

    [Header("SWIFT MOVEMENT")]
    private Vector2 SwiftDirection;
    private float SwiftRotateSpeed;
    private float SwiftVelocity;
    private float SwiftRotate;

    [Header("REFERENCES")]
    private PlayerUniversal Player;

    [Header("COMPONENTS")]
    private Rigidbody2D Rigidbody;
    private Enemy ThisEnemy;

    private void Start()
    {
        SetComponents();
        SetData();
    }

    private void Update()
    {
        LinearMovementInput();
    }

    private void FixedUpdate()
    {
        LinearMovement();
        SwiftMovement();
    }

    private void SetComponents()
    {
        Player = FindObjectOfType<PlayerUniversal>();
        Rigidbody = GetComponent<Rigidbody2D>();
        ThisEnemy = GetComponent<Enemy>();
    }

    private void SetData()
    {
        ThisEnemy.IsMoveable = true;

        CatchingVelocity = Random.Range(CatchingVelocity, CatchingVelocity + 5f);
        RetreatVelocity = -CatchingVelocity - Random.Range(2.5f, 5f);

        ChasingDistance = Random.Range(ChasingDistance, ChasingDistance + 5f);
        RetreatDistance = ChasingDistance / 2;

        SwiftRotateSpeed = Random.Range(200f, 300f);
        SwiftVelocity = Random.Range(15f, 20f);

        IsRetreating = false;
        IsChasing = true;
    }

    private void LinearMovementInput()
    {
        if (ThisEnemy.IsMoveable && ThisEnemy.Type != EnemyType.EnemyOne)
        {
            if (Vector2.Distance(transform.position, Player.transform.position) > ChasingDistance) 
                EnemyState(true, false);
            else if (Vector2.Distance(transform.position, Player.transform.position) < RetreatDistance) 
                EnemyState(false, true);
            else 
                EnemyState(false, false);

            DistanceFromPlayer = Player.transform.position - transform.position;
            DistanceFromPlayer.Normalize();
        }
    }

    private void LinearMovement()
    {
        if (ThisEnemy.IsMoveable && ThisEnemy.Type != EnemyType.EnemyOne)
        {
            if (IsChasing && !IsRetreating) Rigidbody.velocity = DistanceFromPlayer * CatchingVelocity;
            else if (IsRetreating && !IsChasing) Rigidbody.velocity = DistanceFromPlayer * RetreatVelocity;
            else if (!IsChasing && !IsRetreating) Rigidbody.velocity = Vector2.zero;
        }
    }

    private void EnemyState(bool IsChasing, bool IsRetreating)
    {
        this.IsChasing = IsChasing;
        this.IsRetreating = IsRetreating;
    }

    private void SwiftMovement()
    {
        if (ThisEnemy.IsMoveable && ThisEnemy.Type == EnemyType.EnemyOne)
        {
            SwiftDirection = (Vector2)Player.transform.position - Rigidbody.position;
            SwiftDirection.Normalize();

            SwiftRotate = Vector3.Cross(SwiftDirection, transform.up).z;
            Rigidbody.angularVelocity = -SwiftRotate * SwiftRotateSpeed;
            Rigidbody.velocity = transform.up * SwiftVelocity;
        }
    }
}