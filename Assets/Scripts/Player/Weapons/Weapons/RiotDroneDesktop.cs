using UnityEngine;

public class RiotDroneDesktop : MonoBehaviour
{
    [Header("RIOT DRONE")]
    [SerializeField] private RiotDronesBase Riot = new RiotDronesBase();

    [Header("WEAPON")]
    [SerializeField] private GameObject RiotDroneProjectile;
    private float ProjectileForce;
    private float DestroyTime;

    [Header("AIMING")]
    private Vector2 MousePos;
    private Vector2 AimDirection;
    private float Angle;

    [Header("COMPONENTS")]
    private Rigidbody2D Rb;
    private Animator Anim;

    [Header("REFERENCES")]
    private PlayerDesktopMovement Movement;
    private PlayerDesktopWeapons Weapons;
    private PlayerDesktopAiming Aiming;
    private PlayerUniversal Player;
    private Camera Main;

    private void Start()
    {
        SetComponents();
        SetData();
    }

    private void Update()
    {
        Weapon();
        SetRiotDrone();
        StartRiotDrone();
        DroneDeath();
    }

    private void FixedUpdate()
    {
        Aimings();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (!Other.CompareTag("Player") || !Other.CompareTag("RiotDroneWeapon")) Destroy(gameObject);
    }

    private void SetComponents()
    {
        Weapons = FindObjectOfType<PlayerDesktopWeapons>();
        Movement = FindObjectOfType<PlayerDesktopMovement>();
        Aiming = FindObjectOfType<PlayerDesktopAiming>();
        Player = FindObjectOfType<PlayerUniversal>();
        Anim = GetComponent<Animator>();
        Main = Camera.main;
        Rb = GetComponent<Rigidbody2D>();
    }

    private void SetData()
    {
        Player.IsDroneActive = true;
        Aiming.IsAiming = false;
        Movement.Velocity = 0f;

        ProjectileForce = 35f;
        DestroyTime = 1.5f;

        Riot.IsDroneSet = false;
        Riot.IsDroneLive = false;
        Riot.TimeToLive = 0.8f;
    }

    public void SetRiotDrone()
    {
        if (!Riot.IsDroneSet)
        {
            if (Riot.TimeToStop <= 0)
            {
                Riot.IsDroneSet = true;
                Destroy(Rb);
            }
            else Riot.TimeToStop -= Time.deltaTime;
        }
    }

    public void StartRiotDrone()
    {
        if (Riot.IsDroneSet && !Riot.IsDroneLive)
        {
            if (Riot.TimeToLive <= 0)
            {
                Player.WeaponType = 1;
                Riot.IsDroneLive = true;
                Aiming.IsAiming = true;
                Movement.Velocity = 20f;
            }
            else Riot.TimeToLive -= Time.deltaTime;
        }
    }

    private void Weapon()
    {
        if (Input.GetMouseButton(0) && Aiming.IsAiming && Player.WeaponType == 1)
        {
            if (Weapons.TimeShotsOne <= 0)
            {
                GameObject One = Instantiate(RiotDroneProjectile, transform.position, transform.rotation);
                Rigidbody2D RbOne = One.GetComponent<Rigidbody2D>();
                RbOne.AddForce(transform.up * ProjectileForce, ForceMode2D.Impulse);

                Destroy(One, DestroyTime);
            }
        }
    }

    private void Aimings()
    {
        MousePos = Main.ScreenToWorldPoint(Input.mousePosition);
        AimDirection = (Vector2)transform.position - MousePos;

        Angle = Mathf.Atan2(AimDirection.y, AimDirection.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0f, 0f, Angle);
    }

    public void DroneDeath()
    {
        if (Riot.IsDroneSet && Riot.IsDroneLive)
        {
            if (Riot.LifeTime <= 0) Anim.SetTrigger("RiotDroneEnd");
            else Riot.LifeTime -= Time.deltaTime;
        }
    }

    public void Death()
    {
        Player.IsDroneActive = false;
        Destroy(gameObject);
    }
}