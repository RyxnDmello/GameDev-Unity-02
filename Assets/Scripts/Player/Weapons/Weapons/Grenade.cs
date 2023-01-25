using UnityEngine;

[System.Serializable]
public class GrenadeBase
{
    [Header("GRENADE")]
    [Range(1f, 1.5f)] public float StopTime;
    [Range(10f, 15f)] public float LifeTime;
    [HideInInspector] public bool IsSet;

    [Header("WEAPON")]
    [SerializeField] public GameObject Sharpnel;
    [Space(8)]
    [Range(0.25f, 0.5f)] public float StartSpawnTime;
    [HideInInspector] public float SpawnTime;
    [HideInInspector] public float DestroyTime;
    [HideInInspector] public float Force;

    [Header("MUZZLES")]
    [SerializeField] public GameObject MuzzleOne;
    [SerializeField] public GameObject MuzzleTwo;
    [SerializeField] public GameObject MuzzleThree;
    [SerializeField] public GameObject MuzzleFour;

    [Header("ROTATION")]
    [HideInInspector] public float RotateSpeed;
    [HideInInspector] public float Rotate;
    [HideInInspector] public int RandDirection;
}

public class Grenade : MonoBehaviour
{
    [Header("GRENADE")]
    [SerializeField] private GrenadeBase Grenades = new GrenadeBase();

    [Header("COLLISION")]
    private bool IsEnemy;
    private bool IsBoss;

    [Header("PARTICLES")]
    [SerializeField] public GameObject Boom;

    [Header("AUDIO SOURCES")]
    [SerializeField] public AudioSource FireAudio;

    [Header("COMPONENTS")]
    private CircleCollider2D Collider;
    private Rigidbody2D Rb;

    [Header("REFERENCES")]
    private CameraManager CamManager;
    private PlayerUniversal Player;

    private void Start()
    {
        SetData();
    }

    private void Update()
    {
        StopGrenade();
        Rotation();
        Weapon();
        Death();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (IgnoreTags(Other))
        {
            EnemyCollision(Other);
            BossCollision(Other);

            if (!IsEnemy && !IsBoss)
            {
                Destroy(Other.gameObject);
                ResetDestroy();
            }
        }
    }

    private void SetData()
    {
        CamManager = FindObjectOfType<CameraManager>();
        Collider = GetComponent<CircleCollider2D>();
        Player = FindObjectOfType<PlayerUniversal>();
        Rb = GetComponent<Rigidbody2D>();

        Player.IsGrenadeActive = true;
        Grenades.SpawnTime = Grenades.StartSpawnTime;
        Grenades.DestroyTime = Random.Range(1f, 1.25f);
        Grenades.Force = Random.Range(10f, 12f);

        SetRotationDirection();
    }

    private void StopGrenade()
    {
        if (!Grenades.IsSet)
        {
            if (Grenades.StopTime <= 0)
            {
                Collider.radius = 1f;
                Grenades.IsSet = true;
                Destroy(Rb);
            }
            else Grenades.StopTime -= Time.deltaTime;
        }
    }

    private void Death()
    {
        if (Grenades.IsSet)
        {
            if (Grenades.LifeTime <= 0)
            {
                Instantiate(Boom, transform.position, Quaternion.identity);
                Player.IsGrenadeActive = false;
                CamManager.CameraShakes(3, 0.85f);
                Collider.radius = 8f;
                Destroy(gameObject);
            }
            else Grenades.LifeTime -= Time.deltaTime;
        }
    }

    private void Weapon()
    {
        if (Grenades.IsSet)
        {
            if (Grenades.SpawnTime <= 0)
            {
                Projectile(Grenades.MuzzleOne);
                Projectile(Grenades.MuzzleTwo);
                Projectile(Grenades.MuzzleThree);
                Projectile(Grenades.MuzzleFour);
                FireAudio.Play();

                Grenades.SpawnTime = Grenades.StartSpawnTime;
            }
            else Grenades.SpawnTime -= Time.deltaTime;
        }
    }

    private void Projectile(GameObject Muzzle)
    {
        GameObject This = Instantiate(Grenades.Sharpnel, Muzzle.transform.position, Muzzle.transform.rotation);
        Rigidbody2D ThisRb = This.GetComponent<Rigidbody2D>();
        ThisRb.AddForce(Muzzle.transform.up * Grenades.Force, ForceMode2D.Impulse);

        Destroy(This, Grenades.DestroyTime);
    }

    private void Rotation()
    {
        Grenades.Rotate = Grenades.Rotate + Grenades.RotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, 0f, Grenades.Rotate);
    }

    private void SetRotationDirection()
    {
        Grenades.RandDirection = Random.Range(0, 2);

        if (Grenades.RandDirection == 0) Grenades.RotateSpeed = Random.Range(150f, 180f);
        else if (Grenades.RandDirection == 1) Grenades.RotateSpeed = Random.Range(-150f, -180);
    }

    private void EnemyCollision(Collider2D Other)
    {
        if (Other.CompareTag("EnemyOne"))
        {
            IsEnemy = true;
            Other.GetComponent<Enemy>().Health = 0;
            ResetDestroy();
        }
        else if (Other.CompareTag("EnemyTwo"))
        {
            IsEnemy = true;
            Other.GetComponent<Enemy>().Health = 0;
            ResetDestroy();
        }
        else if (Other.CompareTag("EnemyThree"))
        {
            IsEnemy = true;
            Other.GetComponent<Enemy>().Health = 0;
            ResetDestroy();
        }
        else if (Other.CompareTag("EnemyFour"))
        {
            IsEnemy = true;
            Other.GetComponent<Enemy>().Health = 0;
            ResetDestroy();
        }
    }

    private void BossCollision(Collider2D Other)
    {
        if (Other.CompareTag("Corruptor"))
        {
            IsBoss = true;
            Other.GetComponent<Boss>().Health -= 50;
            ResetDestroy();
        }
        else if (Other.CompareTag("DeathBringer"))
        {
            IsBoss = true;
            Other.GetComponent<BossTwo>().Health -= 50;
            ResetDestroy();
        }
    }

    private void ResetDestroy()
    {
        Instantiate(Boom, transform.position, Quaternion.identity);
        CamManager.CameraShakes(3, 1.25f);
        Player.IsGrenadeActive = false;
        Collider.radius = 8.5f;
        Destroy(gameObject);
    }

    private bool IgnoreTags(Collider2D Other)
    {
        if (Other.CompareTag("Player") || Other.CompareTag("GrenadeSharpnel")) return false;
        else return true;
    }
}