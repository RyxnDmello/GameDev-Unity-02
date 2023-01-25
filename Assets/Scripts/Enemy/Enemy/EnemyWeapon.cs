using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [Header("WEAPON")]
    [SerializeField] private GameObject Projectile;
    [SerializeField] private GameObject[] Muzzles;

    [Header("STATISTICS")]
    [SerializeField] [Range(10f, 40f)] private float ProjectileForce;
    [Tooltip("+ 0.5f Random")]
    [SerializeField] [Range(0f, 4f)] private float StartSpawnTime;
    private float AimingDistance;
    private float DestroyTime;
    private float SpawnTime;

    [Header("ROTATION")]
    private int RotateDirection;
    private float RotateSpeed;
    private float Rotate;

    [Header("REFERENCES")]
    private PlayerUniversal Player;

    [Header("COMPONENTS")]
    private EnemyMovement Movement;
    private Enemy ThisEnemy;

    private void Start()
    {
        SetComponents();
        SetData();
    }

    private void Update()
    {
        Rotation();
        Weapon();
    }

    private void SetComponents()
    {
        Player = FindObjectOfType<PlayerUniversal>();
        Movement = GetComponent<EnemyMovement>();
        ThisEnemy = GetComponent<Enemy>();
    }

    private void SetData()
    {
        AimingDistance = Random.Range(Movement.ChasingDistance, Movement.ChasingDistance + 5f);
        SpawnTime = Random.Range(StartSpawnTime, StartSpawnTime + 0.5f);
        DestroyTime = Random.Range(1f, 1.25f);
        RandomRotation();
    }

    private void Weapon()
    {
        if (ThisEnemy.Type == EnemyType.EnemyFour) MultiWeapon();
        else SingleWeapon();
    }

    private void SingleWeapon()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) <= AimingDistance)
        {
            if (SpawnTime <= 0) Projectiles(Muzzles[0]);
            else SpawnTime -= Time.deltaTime;
        }
    }

    private void MultiWeapon()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) <= AimingDistance)
        {
            if (SpawnTime <= 0)
            {
                Projectiles(Muzzles[0]);
                Projectiles(Muzzles[1]);
                Projectiles(Muzzles[2]);
                Projectiles(Muzzles[3]);
            }
            else SpawnTime -= Time.deltaTime;
        }
    }

    private void Projectiles(GameObject Muzzle)
    {
        GameObject Project = Instantiate(Projectile, Muzzle.transform.position, transform.rotation);
        Rigidbody2D ProjectRb = Project.GetComponent<Rigidbody2D>();
        ProjectRb.AddForce(transform.up * ProjectileForce, ForceMode2D.Impulse);

        SpawnTime = Random.Range(StartSpawnTime, StartSpawnTime + 0.5f);
        Destroy(Project, DestroyTime);
    }

    private void Rotation()
    {
        if (ThisEnemy.Type == EnemyType.EnemyFour)
        {
            Rotate = Rotate + RotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, Rotate);
        }
    }

    private void RandomRotation()
    {
        RotateDirection = Random.Range(1, 3);
        if (RotateDirection == 1) RotateSpeed = Random.Range(100f, 200f);
        else if (RotateDirection == 2) RotateSpeed = Random.Range(-100f, -200f);
    }
}
