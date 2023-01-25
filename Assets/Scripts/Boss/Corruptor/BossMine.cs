using UnityEngine;

public class BossMine : MonoBehaviour
{
    [Header("MINE")]
    [HideInInspector] public float LifeTimeFour;
    float RotateSpeedFour;
    float RotateFour;
    int RandDir;

    [Header("PARTICLES")]
    [SerializeField] GameObject Death;

    void Start()
    {
        Randoms();
    }

    void Update()
    {
        HealthCheck();
        Mine();
    }

    public void Mine()
    {
        RotateFour = RotateFour + RotateSpeedFour * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, 0f, RotateFour);
    }

    private void HealthCheck()
    {
        if (LifeTimeFour <= 0)
        {
            Destroy(gameObject);
            Instantiate(Death, transform.position, Quaternion.identity);
        }
        else
        {
            LifeTimeFour -= Time.deltaTime;
        }
    }

    private void Randoms()
    {
        RandDir = Random.Range(1, 3);
        LifeTimeFour = Random.Range(10f, 15f);

        if (RandDir == 1)
        {
            RotateSpeedFour = Random.Range(150f, 200f);
        }
        else if (RandDir == 2)
        {
            RotateSpeedFour = Random.Range(-150f, -200f);
        }
    }
}
