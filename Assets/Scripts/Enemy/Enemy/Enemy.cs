using UnityEngine;

public enum EnemyType
{
    EnemyOne,
    EnemyTwo,
    EnemyThree,
    EnemyFour,
}

public class Enemy : MonoBehaviour
{
    [Header("ENEMY")]
    [SerializeField] public EnemyType Type;
    [Space(10)]
    [SerializeField] [Range(0, 10)] public float Health;

    [Header("POINTS")]
    [SerializeField] [Range(0, 20)] public int Points;
    [SerializeField] [Range(1, 5)] public int Bonus;
    [HideInInspector] public bool IsMoveable;

    [Header("CAMERA SHAKE")]
    [Tooltip("0 < ShakeType < 5")]
    [SerializeField] [Range(1, 3)] private int ShakeType;
    [Tooltip("0.5f < Duration <= 1.5f")]
    [SerializeField] [Range(0.65f, 1.5f)] private float Duration;

    [Header("PARTICLES")]
    [SerializeField] private GameObject Death; 

    [Header("REFERENCES")]
    private CameraManager CamManager;
    private PlayerUniversal Player;

    private void Start()
    {
        SetComponents();
    }

    private void Update()
    {
        EnemyHealth();
    }

    private void SetComponents()
    {
        CamManager = FindObjectOfType<CameraManager>();
        Player = FindObjectOfType<PlayerUniversal>();
    }

    private void EnemyHealth()
    {
        if (Health <= 0) OnDeath();
    }

    private void OnDeath()
    {
        Player.Points += Random.Range(Points, Points + Bonus + 1);
        Instantiate(Death, transform.position, Quaternion.identity);
        CamManager.CameraShakes(ShakeType, Duration);
        Destroy(gameObject);
    }
}
