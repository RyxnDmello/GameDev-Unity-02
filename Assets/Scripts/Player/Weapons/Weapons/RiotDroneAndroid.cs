using UnityEngine;

public class RiotDroneAndroid : MonoBehaviour
{
    [Header("RIOT DRONE")]
    [SerializeField] private RiotDronesBase Riot = new RiotDronesBase();

    [Header("WEAPON")]
    [SerializeField] private GameObject RiotDroneProjectile;
    private float ProjectileForce;
    private float DestroyTime;

    [Header("COMPONENTS")]
    private Rigidbody2D Rb;
    private Animator Anim;

    [Header("REFERENCES")]
    private PlayerAndroidMovement Movement;
    private PlayerAndroidWeapons Weapons;
    private PlayerAndroidAiming Aiming;
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

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (!Other.CompareTag("Player") || !Other.CompareTag("RiotDroneProjectile")) Destroy(gameObject);
    }

    private void SetComponents()
    {
        Weapons = FindObjectOfType<PlayerAndroidWeapons>();
        Movement = FindObjectOfType<PlayerAndroidMovement>();
        Aiming = FindObjectOfType<PlayerAndroidAiming>();
        Player = FindObjectOfType<PlayerUniversal>();
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        Main = Camera.main;
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
        if (Weapons.IsShootable() &&  Aiming.IsAiming && Player.WeaponType == 1)
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