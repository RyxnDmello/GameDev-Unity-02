using UnityEngine;
using UnityEngine.EventSystems;

public class ControlsStyling : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("ANIMATIONS")]
    [HideInInspector] int MainVal;
    Animator Main;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("ControlsTitle"))
        {
            MainVal = 1;
            Main = eventData.pointerCurrentRaycast.gameObject.GetComponent<Animator>();
            Main.SetBool("ControlsTitle", true);
        }

        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("ControlsCircleOrb"))
        {
            MainVal = 2;
            Main = eventData.pointerCurrentRaycast.gameObject.GetComponent<Animator>();
            Main.SetBool("ControlsCircleOrb", true);
        }

        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("ControlsSquareOrb"))
        {
            MainVal = 3;
            Main = eventData.pointerCurrentRaycast.gameObject.GetComponent<Animator>();
            Main.SetBool("ControlsSquareOrb", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MainVal == 1)
        {
            Main.SetBool("ControlsTitle", false);
        }

        if (MainVal == 2)
        {
            Main.SetBool("ControlsCircleOrb", false);
        }

        if (MainVal == 3)
        {
            Main.SetBool("ControlsSquareOrb", false);
        }
    }
}
