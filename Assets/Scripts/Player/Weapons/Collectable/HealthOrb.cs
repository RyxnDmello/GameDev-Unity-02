using UnityEngine;

public class HealthOrb : MonoBehaviour
{
    [Header("HEALTH ORB")]
    [Range(80, 90)] [SerializeField] private int HealthMark;
    [Range(10, 20)] [SerializeField] private int HealthPoints;

    [Header("PARTICLES")]
    [SerializeField] private GameObject Particles;

    [Header("ORB MOVE & DEATH")]
    [SerializeField] private OrbUniversal OrbHealth = new OrbUniversal();

    [Header("REFERENCES")]
    private PlayerUniversal Player;

    private void Start()
    {
        SetData();
    }

    private void Update()
    {
        Life();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.CompareTag("Player")) Health();
    }

    private void SetData()
    {
        Player = FindObjectOfType<PlayerUniversal>();
    }

    private void Health()
    {
        if (Player.Health <= HealthMark) Player.Health += AddHealth(HealthPoints);
        else if (Player.Health > HealthMark) Player.Health = AddHealth(100);
    }

    private int AddHealth(int Add)
    {
        Instantiate(Particles, transform.position, Quaternion.identity);
        Destroy(gameObject);

        return Add;
    }

    private void Life()
    {
        if (OrbHealth.DeathTime <= 0) Instantiate(Particles, transform.position, Quaternion.identity);
        else OrbHealth.DeathTime -= Time.deltaTime;
    }

    private void Move()
    {
        if (Vector2.Distance(Player.transform.position, transform.position) <= OrbHealth.Distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, OrbHealth.Speed * Time.fixedDeltaTime);
        }
    }
}



