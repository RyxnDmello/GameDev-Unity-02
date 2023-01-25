using UnityEngine;

public class BossStages : MonoBehaviour
{
    #region VARIABLES

    [Header("MISSILE")]
    float TimeShotsOne;
    float ResetCountOne;
    int CountOne;

    [Header("BARRAGE")]
    float TimeShotsTwo;
    float ResetCountTwo;
    float ForceTwo;
    int CountTwo;

    [Header("SPLASH")]
    float TimeShotsThree;
    float ResetCountThree;
    float ForceThree;
    int CountThree;

    [Header("MINES")]
    float TimeShotsFour;
    float ResetCountFour;
    int CountFour;

    [Header("GHOSTS")]
    float TimeShotsFive;
    float ResetCountFive;
    int CountFive;

    [Header("AUDIO")]
    public AudioSource BossSplash;

    [Header("AQUIRED COMPONENTS")]
    Boss BossOne;

    #endregion

    #region WEAPONS

    private void Start()
    {
        BossOne = GetComponent<Boss>();

        TimeShotsOne = 1.5f;
        ResetCountOne = 4;
        CountOne = 6;

        TimeShotsTwo = 1.5f;
        ResetCountTwo = 4;
        ForceTwo = 50f;
        CountTwo = 10;

        ForceThree = 40f;
        TimeShotsThree = 1.5f;
        ResetCountThree = 4;
        CountThree = 5;

        TimeShotsFour = 1.5f;
        ResetCountFour = 4;
        CountFour = 5;

        TimeShotsFive = 1.5f;
        ResetCountFive = 4;
        CountFive = 3;
    }

    public void Missiles(GameObject Missile, float Start, float End)
    {
        if (CountOne > 0)
        {
            if (TimeShotsOne <= 0)
            {
                Instantiate(Missile, transform.position, Quaternion.identity);

                CountOne--;

                ResetCountOne = Random.Range(2f, 5f);

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
                if (BossOne.IsAndroid == false)
                {
                    CountOne = 6;
                }
                else
                {
                    CountOne = 3;
                }
            }
            else
            {
                ResetCountOne -= Time.deltaTime;
            }
        }
    }

    public void Barrages(GameObject Barrage, GameObject Muzzle, float Start, float End)
    {
        if (CountTwo > 0)
        {
            if (TimeShotsTwo <= 0)
            {
                GameObject A = Instantiate(Barrage, Muzzle.transform.position, transform.rotation);
                Rigidbody2D RbA = A.GetComponent<Rigidbody2D>();
                RbA.AddForce(Muzzle.transform.up * ForceTwo, ForceMode2D.Impulse);

                CountTwo--;

                ResetCountTwo = Random.Range(4f, 6f);

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
                if (BossOne.IsAndroid == false)
                {
                    CountTwo = 10;
                }
                else
                {
                    CountTwo = 5;
                }
            }
            else
            {
                ResetCountTwo -= Time.deltaTime;
            }
        }
    }

    public void Splash(GameObject Splash, GameObject One, GameObject Two, GameObject Three)
    {
        if (CountThree > 0)
        {
            if (TimeShotsThree <= 0)
            {
                GameObject A = Instantiate(Splash, One.transform.position, One.transform.rotation);
                GameObject B = Instantiate(Splash, Two.transform.position, Two.transform.rotation);
                GameObject C = Instantiate(Splash, Three.transform.position, Three.transform.rotation);

                Rigidbody2D RbA = A.GetComponent<Rigidbody2D>();
                Rigidbody2D RbB = B.GetComponent<Rigidbody2D>();
                Rigidbody2D RbC = C.GetComponent<Rigidbody2D>();

                RbA.AddForce(One.transform.up * ForceThree, ForceMode2D.Impulse);
                RbB.AddForce(Two.transform.up * ForceThree, ForceMode2D.Impulse);
                RbC.AddForce(Three.transform.up * ForceThree, ForceMode2D.Impulse);

                BossSplash.Play();

                CountThree--;

                if (BossOne.IsAndroid == false)
                {
                    ResetCountThree = Random.Range(5f, 6f);
                    TimeShotsThree = Random.Range(1.25f, 2f);
                }
                else
                {
                    ResetCountThree = Random.Range(6f, 10f);
                    TimeShotsThree = Random.Range(2f, 2.5f);
                }
            }
            else
            {
                TimeShotsThree -= Time.deltaTime;
            }
        }

        if (CountThree == 0)
        {
            if (ResetCountThree <= 0)
            {
                if (BossOne.IsAndroid == false)
                {
                    CountThree = 5;
                }
                else
                {
                    CountThree = 3;
                }
            }
            else
            {
                ResetCountThree -= Time.deltaTime;
            }
        }
    }

    public void Mines(GameObject Mine)
    {
        if (CountFour > 0)
        {
            if (TimeShotsFour <= 0)
            {
                Instantiate(Mine, transform.position, transform.rotation);

                CountFour--;

                ResetCountFour = Random.Range(2f, 5f);

                TimeShotsFour = Random.Range(5f, 8f);
            }
            else
            {
                TimeShotsFour -= Time.deltaTime;
            }
        }

        if(CountFour == 0)
        {
            if(ResetCountFour <= 0)
            {
                CountFour = 5;
            }
            else
            {
                ResetCountFour -= Time.deltaTime;
            }
        }
    }

    public void Ghosts(GameObject Ghost)
    {
        if(CountFive > 0)
        {
            if (TimeShotsFive <= 0)
            {
                Instantiate(Ghost, transform.position, transform.rotation);

                CountFive--;

                ResetCountFive = Random.Range(8f, 13f);

                TimeShotsFive = Random.Range(5f, 8.5f);
            }
            else
            {
                TimeShotsFive -= Time.deltaTime;
            }
        }

        if (CountFive == 0)
        {
            if (ResetCountFive <= 0)
            {
                CountFive = 3;
            }
            else
            {
                ResetCountFive -= Time.deltaTime;
            }
        }
    }

    #endregion
}
