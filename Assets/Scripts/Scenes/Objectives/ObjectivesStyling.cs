using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectivesStyling : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] int MainVal;
    Animator Main;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("ObjectivesTitle"))
        {
            MainVal = 1;
            Main = eventData.pointerCurrentRaycast.gameObject.GetComponent<Animator>();
            Main.SetBool("ObjectivesTitle", true);
        }

        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("ObjectivesEnemy"))
        {
            MainVal = 2;
            Main = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Animator>();
            Main.SetBool("ObjectivesEnemy", true);
        }

        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("ObjectivesLinks"))
        {
            MainVal = 3;
            Main = eventData.pointerCurrentRaycast.gameObject.GetComponent<Animator>();
            Main.SetBool("ObjectivesEnemy", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MainVal == 1)
        {
            Main.SetBool("ObjectivesTitle", false);
            MainVal = 0;
        }

        if (MainVal == 2)
        {
            Main.SetBool("ObjectivesEnemy", false);
            MainVal = 0;
        }

        if (MainVal == 3)
        {
            Main.SetBool("ObjectivesEnemy", false);
            MainVal = 0;
        }
    }
}
