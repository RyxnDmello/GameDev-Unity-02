using System.Collections;
using UnityEngine;

[System.Serializable]
public class EnemyCollisionBase
{
    [Header("DAMGE")]
    [SerializeField] [Range(0, 10)] public float EnergoidDamage;
    [SerializeField] [Range(0, 10)] public float MagnumDamage;
    [SerializeField] [Range(0, 10)] public float RoadBlockDamage;
    [SerializeField] [Range(0, 10)] public float RiotDroneDamage;
    [Space(8)]
    [SerializeField] [Range(0, 50)] public int PlayerDamage;
}

public class EnemyCollision : MonoBehaviour
{
    [Header("COLLISION")]
    [SerializeField] private EnemyCollisionBase CollisionBase = new EnemyCollisionBase();

    [Header("KNOCKBACK")]
    [Range(0f, 15f)] public float RecoilForce;
    private Vector2 KnockDifference;
    private float KnockBackTime;

    [Header("PARTICLES")]
    [SerializeField] private GameObject Bleed;
    [SerializeField] private GameObject Death;

    [Header("AUDIO")]
    [SerializeField] private AudioSource Damage;

    [Header("REFERENCES")]
    private CameraManager CamManager;
    private PlayerUniversal Player;

    [Header("COMPONENTS")]
    private Rigidbody2D Rigidbody;
    private Enemy ThisEnemy;

    private void Start()
    {
        SetComponents();

        KnockBackTime = Random.Range(0.45f, 0.65f);
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        PlayerCollision(Other, CollisionBase.PlayerDamage);

        ProjectileDamage(Other, "Energoid", CollisionBase.EnergoidDamage);
        ProjectileDamage(Other, "Magnum", CollisionBase.MagnumDamage);
        ProjectileDamage(Other, "RoadBlock", CollisionBase.RoadBlockDamage);
        ProjectileDamage(Other, "RiotDroneWeapon", CollisionBase.RiotDroneDamage);
    }

    private void SetComponents()
    {
        CamManager = FindObjectOfType<CameraManager>();
        Player = FindObjectOfType<PlayerUniversal>();
        Rigidbody = GetComponent<Rigidbody2D>();
        ThisEnemy = GetComponent<Enemy>();
    }

    private void ProjectileDamage(Collider2D Other, string Tag, float Damage)
    {
        //if(Other.CompareTag("Magnum")) KnockBack(Other);

        if (Other.CompareTag(Tag))
        {
            Instantiate(Bleed, Other.transform.position, Quaternion.identity);

            if(Other.CompareTag("RoadBlock")) ThisEnemy.Health -= Damage;
            else if (ThisEnemy.Type != EnemyType.EnemyOne) ThisEnemy.Health -= (Damage / 2);
            else ThisEnemy.Health -= Damage;

            Debug.Log(ThisEnemy.Health);

            this.Damage.Play();
            Destroy(Other.gameObject);
        }
    }

    private void PlayerCollision(Collider2D Other, int Damage)
    {
        if (Other.CompareTag("Player"))
        {
            Other.GetComponent<PlayerUniversal>().Health -= Damage;
            ThisEnemy.Health = 0;
        }
    }

    private void KnockBack(Collider2D Other)
    {
        StartKnockBack(Other);
        StartCoroutine(StopKnockBack());
    }

    private void StartKnockBack(Collider2D Other)
    {
        ThisEnemy.IsMoveable = false;
        CamManager.CameraShakes(1, 0.85f);
        KnockDifference = transform.position - Other.transform.position; KnockDifference.Normalize();
        Rigidbody.AddForce(KnockDifference * RecoilForce, ForceMode2D.Impulse);
    }

    private IEnumerator StopKnockBack()
    {
        ThisEnemy.IsMoveable = false;
        yield return new WaitForSeconds(KnockBackTime);
        ThisEnemy.IsMoveable = true;
    }
}

