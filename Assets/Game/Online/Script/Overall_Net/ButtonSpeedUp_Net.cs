using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSpeedUp_Net : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        InputControl_Net.ButtonSpeedUp = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputControl_Net.ButtonSpeedUp = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InputControl_Net.ButtonSpeedUp = false;
    }
}