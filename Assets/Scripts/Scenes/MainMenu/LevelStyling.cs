using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LevelStyling : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("DIFFICUILTY TEXTS")]
    public TMP_Text Level;
    public Animator Levels;
    [HideInInspector] int MainVal;
    Animator Main;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("LevelTitle"))
        {
            MainVal = 1;
            Main = eventData.pointerCurrentRaycast.gameObject.GetComponent<Animator>();
            Main.SetBool("LevelTitle", true);
        }

        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("LevelModeEasy"))
        {
            MainVal = 2;
            Level.text = "CHECKPOINT AT THE START OF EVERY NEW LEVEL";
        }

        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("LevelModeHard"))
        {
            MainVal = 3;
            Level.text = "CHECKPOINT ONLY AVAILABLE ONCE YOU REACH THE BOSS\nYOUR OPPONENTS WILL SHOW YOU NO MERCY!";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MainVal == 1)
        {
            Main.SetBool("LevelTitle", false);
        }

        if (MainVal == 2)
        {
            Level.text = "CHOOSE YOUR DIFFICUILTY";
        }

        if(MainVal == 3)
        {
            Level.text = "CHOOSE YOUR DIFFICUILTY";
        }
    }
}
