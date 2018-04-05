using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonShoot_Net : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        InputControl_Net.ButtonShoot = true;
        Invoke("stopShoot",0.001f);
    }
    void stopShoot()
    {
        InputControl_Net.ButtonShoot = false;
    }
}
