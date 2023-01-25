using UnityEngine;

public class AmmoOrb : MonoBehaviour
{
    [Header("AMMO ORB")]
    [Range(2, 4)] [SerializeField] private int Type;

    [Header("PARTICLES")]
    [SerializeField] private GameObject Particles;

    [Header("ORB MOVE & DEATH")]
    [SerializeField] private OrbUniversal OrbAmmo = new OrbUniversal();

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
        LoadAmmo(Other);
    }

    private void SetData()
    {
        Player = FindObjectOfType<PlayerUniversal>();
    }

    private void LoadAmmo(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            if (Type == 2) AmmoTwo();
            else if (Type == 3) AmmoThree();
            else if (Type == 4) AmmoFour();
        }
    }

    private void AmmoTwo()
    {
        if (Player.AmmoTwo <= 3) Player.AmmoTwo += AddAmmo(2);
        else if (Player.AmmoTwo != 5) Player.AmmoTwo = AddAmmo(5);

        Debug.Log(Player.AmmoTwo);
    }

    private void AmmoThree()
    {
        if (Player.AmmoThree <= 3) Player.AmmoThree += AddAmmo(2);
        else if (Player.AmmoThree != 5) Player.AmmoThree = AddAmmo(5);
    }

    private void AmmoFour()
    {
        if (Player.AmmoFour < 5) Player.AmmoFour += AddAmmo(1);
    }

    private int AddAmmo(int Add)
    {
        Instantiate(Particles, transform.position, Quaternion.identity);
        Destroy(gameObject);

        return Add;
    }

    private void Life()
    {
        if (OrbAmmo.DeathTime <= 0) Instantiate(Particles, transform.position, Quaternion.identity);
        else OrbAmmo.DeathTime -= Time.deltaTime;
    }

    private void Move()
    {
        if (Vector2.Distance(Player.transform.position, transform.position) <= OrbAmmo.Distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, OrbAmmo.Speed * Time.fixedDeltaTime);
        }
    }
}
