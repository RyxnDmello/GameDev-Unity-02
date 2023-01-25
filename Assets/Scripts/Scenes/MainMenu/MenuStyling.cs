using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuStyling : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] int MainVal;
    Animator Main;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("MenuTitle"))
        {
            MainVal = 1;
            Main = eventData.pointerCurrentRaycast.gameObject.GetComponent<Animator>();
            Main.SetBool("MenuTitle", true);
        }

        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("MenuCool"))
        {
            MainVal = 2;
            Main = eventData.pointerCurrentRaycast.gameObject.GetComponent<Animator>();
            Main.SetBool("MenuCool", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MainVal == 1)
        {
            Main.SetBool("MenuTitle", false);
        }
        
        if(MainVal == 2)
        {
            Main.SetBool("MenuCool", false);
        }
    }
}
