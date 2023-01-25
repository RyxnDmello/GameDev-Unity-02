using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    #region UNITY

    public bool IsAndroid;
    GameManager GameMan;

    private void Start()
    {
        GameMan = FindObjectOfType<GameManager>();
    }

    #endregion

    #region USES

    public void Death()
    {
        if (IsAndroid == false)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("MainMenuAndroid");
        }
    }

    public void GrenadeAquire()
    {
        gameObject.SetActive(false);
    }

    public void PlayerAmmoChoice()
    {
        GameMan.TimeToChoice = Random.Range(45.5f, 60f);
        GameMan.IsPlayerAmmoChoice = false;
        gameObject.SetActive(false);
    }

    #endregion
}
