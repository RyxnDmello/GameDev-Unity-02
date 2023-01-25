using UnityEngine;

[System.Serializable]
public class Collisions
{
    [Header("ENEMY PROJECTILE")]
    [Range(0, 10)] public int EnemyTwoProjectileDamage;
    [Range(0, 10)] public int EnemyThreeProjectileDamage;
    [Range(0, 10)] public int EnemyFourProjectileDamage;

    [Header("BOSS ONE PROJECTILES")]
    [Range(0, 10)] public int BossOneMissileDamage;
    [Range(0, 10)] public int BossOneBarrageDamage;
    [Range(0, 10)] public int BossOneSplashDamage;
    [Range(0, 10)] public int BossOneMineDamage;
    [Range(0, 10)] public int BossOneGhostDamage;

    [Header("BOSS TWO PROJECTILES")]
    [Range(0, 10)] public int BossTwoDisksDamage;
    [Range(0, 10)] public int BossTwoGhostsDamage;
    [Range(0, 10)] public int BossTwoLaserDamage;
    [Range(0, 10)] public int BossTwoCritterDamage;

    [Header("AUDIO SOURCES")]
    [SerializeField] public AudioSource Glitch;
    [SerializeField] public AudioSource Damage;

    [Header("PARTICLES")]
    [SerializeField] public GameObject Bleed;
    [SerializeField] public GameObject Death;
}

public class PlayerCollision : MonoBehaviour
{
    [Header("COLLISION")]
    [SerializeField] private Collisions Collide = new Collisions();

    [Header("COMPONENTS")]
    private PlayerUniversal Player;

    [Header("REFERENCES")]
    private CameraManager CamManager;

    private void Start()
    {
        SetComponents();
    }

    private void Update()
    {
        Health();
    }

    private void SetComponents()
    {
        CamManager = FindObjectOfType<CameraManager>();
        Player = GetComponent<PlayerUniversal>();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        ProjectileCollision(Other, "EnemyTwoProjectile", Collide.EnemyTwoProjectileDamage, 1, 1.25f, true);
        ProjectileCollision(Other, "EnemyThreeProjectile", Collide.EnemyThreeProjectileDamage, 2, 1f, false);
        ProjectileCollision(Other, "EnemyFourProjectile", Collide.EnemyFourProjectileDamage, 0, 0, true);

        BossCollision(Other, "Corruptor");
        ProjectileCollision(Other, "CorruptorMissiles", Collide.BossOneMissileDamage, 1, 0.85f, true);
        ProjectileCollision(Other, "CorruptorBarrage", Collide.BossOneBarrageDamage, 2, 1f, false);
        ProjectileCollision(Other, "CorruptorSplash", Collide.BossOneSplashDamage, 1, 1f, true);
        ProjectileCollision(Other, "CorruptorMines", Collide.BossOneMineDamage, 3, 1.25f, true);
        ProjectileCollision(Other, "CorruptorGhost", Collide.BossOneGhostDamage, 2, 1.25f, true);

        BossCollision(Other, "DeathBringer");
        ProjectileCollision(Other, "DeathBringerDisks", Collide.BossTwoDisksDamage, 1, 0.85f, true);
        ProjectileCollision(Other, "DeathBringerGhost", Collide.BossTwoGhostsDamage, 2, 1.25f, true);
        ProjectileCollision(Other, "DeathBringerLaser", Collide.BossTwoLaserDamage, 2, 1f, true);
        ProjectileCollision(Other, "DeathBringerCritter", Collide.BossTwoCritterDamage, 0, 0, true);
    }

    private void BossCollision(Collider2D Other, string Name)
    {
        if (Other.CompareTag(Name))
        {
            CamManager.CameraShakes(4, 1.25f);
            gameObject.SetActive(false);
        }
    }

    private void ProjectileCollision(Collider2D Other, string Name, int HealthLoss, int ShakeType, float ShakeDuration, bool IsDamageAudio)
    {
        if (Other.CompareTag(Name))
        {
            Player.Health = Player.Health - HealthLoss;

            Instantiate(Collide.Bleed, Other.transform.position, Other.transform.rotation);

            if (IsDamageAudio) Collide.Damage.Play();
            else if (!IsDamageAudio) Collide.Glitch.Play();

            CamManager.CameraShakes(ShakeType, ShakeDuration);

            Destroy(Other.gameObject);
        }
    }

    private void Health()
    {
        if(Player.Health <= 0)
        {
            Instantiate(Collide.Death, transform.position, Quaternion.identity);
            CamManager.CameraShakes(4, 1f);
            gameObject.SetActive(false);
        }
    }
}
