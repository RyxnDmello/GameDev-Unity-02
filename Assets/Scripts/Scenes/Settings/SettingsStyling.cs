using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsStyling : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("ANIMATIONS")]
    public Animator Warn;
    [HideInInspector] int MainVal;
    Animator Main;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("SettingsTitle"))
        {
            MainVal = 1;
            Main = eventData.pointerCurrentRaycast.gameObject.GetComponent<Animator>();
            Main.SetBool("SettingsTitle", true);
        }

        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("SettingsResetWarn"))
        {
            MainVal = 2;
            Warn.SetBool("SettingsResetWarn", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MainVal == 1)
        {
            Main.SetBool("SettingsTitle", false);
        }

        if (MainVal == 2)
        {
            Warn.SetBool("SettingsResetWarn", false);
        }
    }
}
