using UnityEngine;

public class BossTwoStages : MonoBehaviour
{
    #region VARIABLES

    [Header("SHIELD")]
    public Animator BossShield;
    float TimeBtwShield;
    float TimeDestroyShield;
    [HideInInspector] 
    public bool ShieldActive;

    [Header("DISKS")]
    float TimeShotsOne;
    float ForceOne;
    float ResetCountOne;
    float LimitRange;
    int CountOne;

    [Header("GHOSTS")]
    float TimeShotsTwo;
    float ResetCountTwo;
    int CountTwo;

    [Header("CRITTER")]
    float TimeShotsThree;
    float ForceThree;
    float ResetCountThree;
    int CountThree;

    [Header("LASER")]
    float TimeShotsFour;
    float ForceFour;
    float ResetCountFour;
    float LimitRangeFour;
    int CountFour;

    [Header("TRAP")]
    [HideInInspector] public bool TrapActive;
    float TimeShotsFive;
    float ForceFive;
    float ResetCountFive;
    float LimitRangeFive;
    int CountFive;

    [Header("MUZZLES")]
    public GameObject One;
    public GameObject Two;
    public GameObject Three;
    public GameObject Four;
    public GameObject Five;
    public GameObject Six;
    public GameObject Seven;
    public GameObject Eight;
    public GameObject Nine;
    public GameObject Ten;
    public GameObject Eleven;
    public GameObject Twelve;

    [Header("GAME SFX")]
    [SerializeField] AudioSource DiskShot;

    [Header("AQUITED COMPONENTS")]
    BossTwo MyBoss;
    Player Play;

    #endregion

    #region BEHAVIOURS

    private void Start()
    {
        Play = FindObjectOfType<Player>();
        MyBoss = GetComponent<BossTwo>();

        ShieldActive = false;
        TimeBtwShield = 15f;
        TimeDestroyShield = 20f;

        TimeShotsOne = 1f;
        ResetCountOne = 1;
        ForceOne = 10f;
        LimitRange = 20f;

        TimeShotsTwo = 1f;
        ResetCountTwo = 4f;

        TimeShotsThree = 1f;
        ForceThree = 6.5f;
        ResetCountThree = 1f;

        TimeShotsFour = 1f;
        ResetCountFour = 1;
        ForceFour = 40f;
        LimitRangeFour = 15f;

        TrapActive = false;
        TimeShotsFive = 1f;
        ResetCountFive = 1;
        ForceFive = 10f;
        LimitRangeFive = 15f;

        if (MyBoss.IsAndroid == false)
        {            
            CountOne = 8;
            CountTwo = 2; 
            CountThree = 2;
            CountFour = 8;
            CountFive = 5;
        }
        else
        {
            CountOne = 6;
            CountTwo = 1;
            CountThree = 1;
            CountFour = 3;
            CountFive = 2;
        }
    }

    public void Shields(float TimeToActivateShield, float TimeToDestroyShield)
    {
        if (ShieldActive == false)
        {
            if (TimeBtwShield <= 0)
            {
                BossShield.SetBool("BossTwoShield", true);
                
                TimeDestroyShield = TimeToDestroyShield;
                ShieldActive = true;
            }
            else
            {
                TimeBtwShield -= Time.deltaTime;
            }
        }

        if (ShieldActive == true)
        {
            if (TimeDestroyShield <= 0)
            {
                BossShield.SetBool("BossTwoShield", false);

                TimeBtwShield = TimeToActivateShield;
                ShieldActive = false;
            }
            else
            {
                TimeDestroyShield -= Time.deltaTime;
            }
        }
    }

    public void Disks(GameObject Disk, bool Shield, float Start, float End)
    {
        if (Vector2.Distance(Play.transform.position, transform.position) <= LimitRange && ShieldActive == Shield)
        {
            if (CountOne > 0)
            {
                if (TimeShotsOne <= 0)
                {
                    GameObject A = Instantiate(Disk, One.transform.position, One.transform.rotation);
                    GameObject B = Instantiate(Disk, Two.transform.position, Two.transform.rotation);
                    GameObject C = Instantiate(Disk, Three.transform.position, Three.transform.rotation);
                    GameObject D = Instantiate(Disk, Four.transform.position, Four.transform.rotation);
                    GameObject E = Instantiate(Disk, Five.transform.position, Five.transform.rotation);
                    GameObject F = Instantiate(Disk, Six.transform.position, Six.transform.rotation);
                    GameObject G = Instantiate(Disk, Seven.transform.position, Seven.transform.rotation);
                    GameObject H = Instantiate(Disk, Eight.transform.position, Eight.transform.rotation);
                    GameObject I = Instantiate(Disk, Nine.transform.position, Nine.transform.rotation);
                    GameObject J = Instantiate(Disk, Ten.transform.position, Ten.transform.rotation);
                    GameObject K = Instantiate(Disk, Eleven.transform.position, Eleven.transform.rotation);
                    GameObject L = Instantiate(Disk, Twelve.transform.position, Twelve.transform.rotation);

                    Rigidbody2D RbA = A.GetComponent<Rigidbody2D>();
                    Rigidbody2D RbB = B.GetComponent<Rigidbody2D>();
                    Rigidbody2D RbC = C.GetComponent<Rigidbody2D>();
                    Rigidbody2D RbD = D.GetComponent<Rigidbody2D>();
                    Rigidbody2D RbE = E.GetComponent<Rigidbody2D>();
                    Rigidbody2D RbF = F.GetComponent<Rigidbody2D>();
                    Rigidbody2D RbG = G.GetComponent<Rigidbody2D>();
                    Rigidbody2D RbH = H.GetComponent<Rigidbody2D>();
                    Rigidbody2D RbI = I.GetComponent<Rigidbody2D>();
                    Rigidbody2D RbJ = J.GetComponent<Rigidbody2D>();
                    Rigidbody2D RbK = K.GetComponent<Rigidbody2D>();
                    Rigidbody2D RbL = L.GetComponent<Rigidbody2D>();

                    RbA.AddForce(One.transform.up * ForceOne, ForceMode2D.Impulse);
                    RbB.AddForce(Two.transform.up * ForceOne, ForceMode2D.Impulse);
                    RbC.AddForce(Three.transform.up * ForceOne, ForceMode2D.Impulse);
                    RbD.AddForce(Four.transform.up * ForceOne, ForceMode2D.Impulse);
                    RbE.AddForce(Five.transform.up * ForceOne, ForceMode2D.Impulse);
                    RbF.AddForce(Six.transform.up * ForceOne, ForceMode2D.Impulse);
                    RbG.AddForce(Seven.transform.up * ForceOne, ForceMode2D.Impulse);
                    RbH.AddForce(Eight.transform.up * ForceOne, ForceMode2D.Impulse);
                    RbI.AddForce(Nine.transform.up * ForceOne, ForceMode2D.Impulse);
                    RbJ.AddForce(Ten.transform.up * ForceOne, ForceMode2D.Impulse);
                    RbK.AddForce(Eleven.transform.up * ForceOne, ForceMode2D.Impulse);
                    RbL.AddForce(Twelve.transform.up * ForceOne, ForceMode2D.Impulse);

                    DiskShot.Play();

                    CountOne--;

                    ResetCountOne = Random.Range(2f, 2.8f);

                    TimeShotsOne = Random.Range(Start, End);
                }
                else
                {
                    TimeShotsOne -= Time.deltaTime;
                }
            }

            if (CountOne == 0)
            {
                if (ResetCountOne <= 0)
                {
                    if (MyBoss.IsAndroid == false)
                    {
                        CountOne = 8;
                    }
                    else
                    {
                        CountOne = 4;
                    }
                }
                else
                {
                    ResetCountOne -= Time.deltaTime;
                }
            }
        }
    }

    public void Ghosts(GameObject Ghost, float Start, float End)
    {
        if (CountTwo > 0)
        {
            if (TimeShotsTwo <= 0)
            {
                Instantiate(Ghost, One.transform.position, Quaternion.identity);
                Instantiate(Ghost, Four.transform.position, Quaternion.identity);

                CountTwo--;

                ResetCountTwo = Random.Range(10f, 15f);

                TimeShotsTwo = Random.Range(Start, End);
            }
            else
            {
                TimeShotsTwo -= Time.deltaTime;
            }
        }

        if (CountTwo == 0)
        {
            if (ResetCountTwo <= 0)
            {
                if (MyBoss.IsAndroid == false)
                {
                    CountTwo = 2;
                }
                else
                {
                    CountTwo = 1;
                }
            }
            else
            {
                ResetCountTwo -= Time.deltaTime;
            }
        }
    }

    public void Critter(GameObject Critter, bool Shield, float Start, float End)
    {
        if (ShieldActive == Shield)
        {
            if (CountThree > 0)
            {
                if (TimeShotsThree <= 0)
                {
                    GameObject A = Instantiate(Critter, Three.transform.position, One.transform.rotation);
                    GameObject B = Instantiate(Critter, Four.transform.position, Two.transform.rotation);

                    Rigidbody2D RbA = A.GetComponent<Rigidbody2D>();
                    Rigidbody2D RbB = B.GetComponent<Rigidbody2D>();

                    RbA.AddForce(Three.transform.up * ForceThree, ForceMode2D.Impulse);
                    RbB.AddForce(Four.transform.up * ForceThree, ForceMode2D.Impulse);

                    CountThree--;

                    ResetCountThree = Random.Range(5f, 8f);

                    TimeShotsThree = Random.Range(Start, End);
                }
                else
                {
                    TimeShotsThree -= Time.deltaTime;
                }
            }

            if(CountThree <= 0)
            {
                if(ResetCountThree <= 0)
                {
                    if (MyBoss.IsAndroid == false)
                    {
                        CountThree = 2;
                    }
                    else
                    {
                        CountThree = 1;
                    }
                }
                else
                {
                    ResetCountThree -= Time.deltaTime;
                }
            }
        }
    }

    public void Lasers(GameObject Laser, float Start, float End)
    {
        if (Vector2.Distance(Play.transform.position, transform.position) <= LimitRangeFour)
        {
            if (CountFour > 0)
            {
                if (TimeShotsFour <= 0)
                {
                    GameObject A = Instantiate(Laser, transform.position, transform.rotation);

                    Rigidbody2D RbA = A.GetComponent<Rigidbody2D>();

                    RbA.AddForce(transform.up * ForceFour, ForceMode2D.Impulse);

                    CountFour--;

                    ResetCountFour = Random.Range(5f, 8f);

                    TimeShotsFour = Random.Range(Start, End);
                }
                else
                {
                    TimeShotsFour -= Time.deltaTime;
                }
            }

            if (CountFour <= 0)
            {
                if (ResetCountFour <= 0)
                {
                    if (MyBoss.IsAndroid == false)
                    {
                        CountFour = 8;
                    }
                    else
                    {
                        CountFour = 3;
                    }
                }
                else
                {
                    ResetCountFour -= Time.deltaTime;
                }
            }
        }
    }

    public void Traps(GameObject Trap, float Start, float End)
    {
        if (Vector2.Distance(Play.transform.position, transform.position) <= LimitRangeFive && TrapActive == false)
        {
            if (CountFive > 0)
            {
                if (TimeShotsFive <= 0)
                {
                    GameObject A = Instantiate(Trap, transform.position, transform.rotation);

                    Rigidbody2D RbA = A.GetComponent<Rigidbody2D>();

                    RbA.AddForce(transform.up * ForceFive, ForceMode2D.Impulse);

                    CountFive--;

                    ResetCountFive = Random.Range(8f, 10f);

                    TimeShotsFive = Random.Range(Start, End);
                }
                else
                {
                    TimeShotsFive -= Time.deltaTime;
                }
            }

            if (CountFive <= 0)
            {
                if (ResetCountFive <= 0)
                {
                    if (MyBoss.IsAndroid == false)
                    {
                        CountFive = 5;
                    }
                    else
                    {
                        CountFive = 2;
                    }
                }
                else
                {
                    ResetCountFive -= Time.deltaTime;
                }
            }
        }
    }

    #endregion
}
