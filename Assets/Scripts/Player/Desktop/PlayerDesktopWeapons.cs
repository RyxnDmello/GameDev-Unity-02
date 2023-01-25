using UnityEngine;

public class PlayerDesktopWeapons : MonoBehaviour
{
    [Header("PLAYER WEAPONS")]
    [SerializeField] public WeaponsBase Weapons = new WeaponsBase();

    [Header("ENERGOID")]
    [HideInInspector]
    public float TimeShotsOne;
    private float StartTimeShotsOne;
    private float ForceOne;
    private float DestroyOne;

    [Header("MAGNUM")]
    private float ForceTwo;
    private float DestroyTwo;

    [Header("ROADBLOCK")]
    private float StartTimeSetsThree;
    private float TimeSetsThree;
    private float StartTimeShotsThree;
    private float TimeShotsThree;
    private float ForceThree;
    private float DestroyThree;
    private int Shots;

    [Header("RIOT-DRONE")]
    private float ForceFour;

    [Header("GRENADE")]
    private float ForceFive;
    private int SetType;

    [Header("COMPONENTS")]
    private PlayerDesktopMovement Movement;
    private PlayerDesktopAiming Aiming;
    private PlayerUniversal Player;

    private void Start()
    {
        SetData();
    }

    private void Update()
    {
        WeaponOne();
        WeaponTwo();
        WeaponThree();
        WeaponFour();
        WeaponFive();
        WeaponsSelect();
    }

    private void SetData()
    {
        Movement = GetComponent<PlayerDesktopMovement>();
        Aiming = GetComponent<PlayerDesktopAiming>();
        Player = GetComponent<PlayerUniversal>();

        Player.WeaponType = 1;

        StartTimeShotsOne = 0.25f;
        StartTimeSetsThree = 0.85f;
        StartTimeShotsThree = 0.035f;

        TimeShotsOne = 0.25f;
        TimeSetsThree = 0.85f;
        TimeShotsThree = 0.035f;
        Shots = 3;

        ForceOne = 40f;
        ForceTwo = 45.5f;
        ForceThree = 35f;
        ForceFour = 12.5f;
        ForceFive = 8f;

        DestroyOne = 2.5f;
        DestroyTwo = 2f;
        DestroyThree = 2.5f;

        Player.AmmoTwo = 2;
        Player.AmmoThree = 2;
        Player.AmmoFour = 2;
        Player.AmmoFive = 2;

        Player.IsDroneActive = false;
        Player.IsGrenadeActive = false;
        SetType = 1;
    }

    private void ShootProjectile(GameObject Projectile, int WeaponType, float Force, float DestroyTime)
    {
        if (WeaponType != 4)
        {
            GameObject Project;

            if (WeaponType == 1) Project = Instantiate(Projectile, Weapons.MuzzleA.transform.position, Weapons.MuzzleA.transform.rotation);
            else Project = Instantiate(Projectile, Weapons.MuzzleA.transform.position, transform.rotation);
            Rigidbody2D RbProject = Project.GetComponent<Rigidbody2D>();
            RbProject.AddForce(transform.up * Force, ForceMode2D.Impulse);

            Destroy(Project, DestroyTime);
        }
        else
        {
            GameObject One = Instantiate(Projectile, Weapons.MuzzleB.transform.position, transform.rotation, transform);
            Rigidbody2D RbOne = One.GetComponent<Rigidbody2D>();
            RbOne.AddForce(transform.right * -Force, ForceMode2D.Impulse);

            GameObject Two = Instantiate(Projectile, Weapons.MuzzleC.transform.position, transform.rotation, transform);
            Rigidbody2D RbTwo = Two.GetComponent<Rigidbody2D>();
            RbTwo.AddForce(transform.right * Force, ForceMode2D.Impulse);
        }
    }

    private void WeaponOne()
    {
        if (Player.WeaponType == 1)
        {
            if (Input.GetMouseButton(0))
            {
                if (TimeShotsOne <= 0)
                {
                    ShootProjectile(Weapons.WeaponOne, 1, ForceOne, DestroyOne);

                    Weapons.Energoid.Play();

                    TimeShotsOne = StartTimeShotsOne;
                }
                else
                {
                    TimeShotsOne -= Time.deltaTime;
                }
            }
        }
    }

    private void WeaponTwo()
    {
        if (Player.WeaponType == 2 && Player.AmmoTwo > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShootProjectile(Weapons.WeaponTwo, 2, ForceTwo, DestroyTwo);

                Weapons.Magnum.Play();

                Player.AmmoTwo--;

                if (Player.AmmoTwo == 0)
                {
                    Player.WeaponType = 1;
                }
            }
        }
    }

    private void WeaponThree()
    {
        if (Player.WeaponType == 3 && Player.AmmoThree > 0)
        {
            if (Input.GetMouseButton(0))
            {
                if (TimeSetsThree <= 0)
                {
                    if (Shots > 0 && TimeShotsThree <= 0)
                    {
                        ShootProjectile(Weapons.WeaponThree, 3, ForceThree, DestroyThree);

                        Shots--;

                        TimeShotsThree = StartTimeShotsThree;
                    }
                    else if (Shots == 0)
                    {
                        TimeShotsThree = StartTimeShotsThree;
                        TimeSetsThree = StartTimeSetsThree;

                        Player.AmmoThree--;
                        Shots = 3;

                        if (Player.AmmoThree == 0)
                        {
                            Player.WeaponType = 1;
                        }
                    }
                    else
                    {
                        TimeShotsThree -= Time.deltaTime;
                    }
                }
                else
                {
                    TimeSetsThree -= Time.deltaTime;
                }
            }
        }
    }

    private void WeaponFour()
    {
        if (Player.WeaponType == 4 && Player.AmmoFour > 0 && !Player.IsDroneActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShootProjectile(Weapons.WeaponFour, 4, ForceFour, 200f);

                Player.IsDroneActive = true;
                Weapons.RiotDrones.Play();
                Player.WeaponType = 4;

                Player.AmmoFour--;
            }
        }
    }

    private void WeaponFive()
    {
        if (Player.WeaponType == 5 && Player.AmmoFive > 0)
        {
            if (Input.GetMouseButtonDown(0) && !Player.IsGrenadeActive)
            {
                ShootProjectile(Weapons.WeaponFive, 5, ForceFive, 200f);

                Player.IsGrenadeActive = true;
                Weapons.Grenade.Play();
                Player.AmmoFive--;

                if (Player.AmmoFive == 0)
                {
                    Player.WeaponType = 1;
                }
            }
        }
    }

    private void WeaponsSelect()
    {
        WeaponKey(KeyCode.Alpha1, 1);
        WeaponKey(KeyCode.Alpha2, Player.AmmoTwo);
        WeaponKey(KeyCode.Alpha3, Player.AmmoThree);
        WeaponKey(KeyCode.Alpha4, Player.AmmoFour);
        WeaponKey(KeyCode.X, Player.AmmoFive);
    }

    private void WeaponKey(KeyCode Key, int Ammo)
    {
        if (Input.GetKeyDown(Key) && Ammo > 0)
        {
            if (Key == KeyCode.X) SetType = 5;
            else SetType = (int)Key - 48;

            if (SetType == 4)
            {
                if (!Player.IsDroneActive)
                {
                    Player.WeaponType = 4;
                    Aiming.IsAiming = false;
                    Movement.Velocity = 0f;
                }
            }
            else
            {
                Player.WeaponType = SetType;
                Aiming.IsAiming = true;
                Movement.Velocity = 20f;
            }

            ResetTimeAllWeapons();
        }
    }

    private void ResetTimeAllWeapons()
    {
        TimeShotsOne = StartTimeShotsOne;
        TimeShotsThree = StartTimeShotsThree;
        TimeSetsThree = StartTimeSetsThree;
    }
}