using UnityEngine;

[System.Serializable]
public class WeaponsBase
{
    [Header("WEAPONS")]
    [SerializeField] public GameObject WeaponOne;
    [SerializeField] public GameObject WeaponTwo;
    [SerializeField] public GameObject WeaponThree;
    [SerializeField] public GameObject WeaponFour;
    [SerializeField] public GameObject WeaponFive;

    [Header("MUZZLES")]
    [SerializeField] public GameObject MuzzleA;
    [SerializeField] public GameObject MuzzleB;
    [SerializeField] public GameObject MuzzleC;

    [Header("AUDIO")]
    [SerializeField] public AudioSource Energoid;
    [SerializeField] public AudioSource Magnum;
    [SerializeField] public AudioSource RiotDrones;
    [SerializeField] public AudioSource Grenade;
}

