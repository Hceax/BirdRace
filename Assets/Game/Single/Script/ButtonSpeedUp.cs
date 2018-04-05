using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSpeedUp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        InputControl.ButtonSpeedUp = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputControl.ButtonSpeedUp = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InputControl.ButtonSpeedUp = false;
    }
}