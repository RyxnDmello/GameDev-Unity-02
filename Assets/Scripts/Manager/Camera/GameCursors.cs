using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCursors : MonoBehaviour
{
    #region VARIABLES

    [Header("CROSSHAIRS")]
    [HideInInspector] public int Access;
    [SerializeField] SpriteRenderer Weps;
    public Sprite WeaponOne;
    public Sprite WeaponTwo;
    public Sprite WeaponThree;
    public Sprite WeaponFive;

    [Header("MENUS CURSOR")]
    [SerializeField] RectTransform Rect;
    [SerializeField] Image Menus;
    public Sprite Menu;
    Vector2 MousPos;

    [Header("AQUIRED COMPONENTS")]
    Player Play;

    #endregion

    #region UNITY

    private void Start()
    {
        Play = FindObjectOfType<Player>();
    }

    private void Update()
    {
        Move();

        MenuCursorClick();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.CompareTag("EnemyOne") || Other.CompareTag("EnemyTwo") || Other.CompareTag("EnemyThree") || Other.CompareTag("EnemyFour") || Other.CompareTag("Boss") || Other.CompareTag("BossMissile") || Other.CompareTag("BossGhost") || Other.CompareTag("BossTwo") || Other.CompareTag("BossTwoGhosts") || Other.CompareTag("BossTwoCritter"))
        {
            EnemyRed(1);
        }
    }

    private void OnTriggerExit2D(Collider2D Other)
    {
        if (Other.CompareTag("EnemyOne") || Other.CompareTag("EnemyTwo") || Other.CompareTag("EnemyThree") || Other.CompareTag("EnemyFour") || Other.CompareTag("Boss") || Other.CompareTag("BossMissile") || Other.CompareTag("BossGhost") || Other.CompareTag("BossTwo") || Other.CompareTag("BossTwoGhosts") || Other.CompareTag("BossTwoCritter"))
        {
            EnemyRed(2);
        }
    }

    #endregion

    #region FUNCTIONS

    public void Move()
    {
        Cursor.visible = false;

        MousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = MousPos;
    }

    public void MenuCursorClick()
    {
        if (SceneManager.GetSceneByName("GamePlay").isLoaded == false)
        {
            if (Access == 1)
            {
                Rect = GetComponent<RectTransform>();

                if (Input.GetMouseButton(0))
                {
                    Rect.localScale = new Vector2(0.7f, 0.7f);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    Rect.localScale = new Vector2(0.8f, 0.8f);
                }
                else
                {
                    Rect.localScale = new Vector2(0.8f, 0.8f);
                }
            }
        }
    }

    public void WeaponsCursors(int Which)
    {
        if (Access == 2)
        {
            if (Which == 1)
            {
                Weps.sprite = WeaponOne;
                transform.localScale = new Vector2(2.25f, 2.25f);
                transform.rotation = Play.transform.rotation;
            }
            else if (Which == 2)
            {
                Weps.sprite = WeaponTwo;
                transform.localScale = new Vector2(1.5f, 1.5f);
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (Which == 3)
            {
                Weps.sprite = WeaponThree;
                transform.localScale = new Vector2(1.75f, 1.75f);
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (Which == 4)
            {
                Weps.sprite = null;
                transform.localScale = new Vector2(1f, 1f);
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (Which == 5)
            {
                Weps.sprite = WeaponFive;
                transform.localScale = new Vector2(1.75f, 1.75f);
                transform.rotation = Play.transform.rotation;
            }
        }
    }

    public void EnemyRed(int A)
    {
        if (Access == 2 && A == 1)
        {
            Weps.color = Color.red;
        }
        else if (Access == 2 && A == 2)
        {
            Weps.color = Color.white;
        }
    }

    #endregion
}
