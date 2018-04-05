using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonShoot : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        InputControl.ButtonShoot = true;
        Invoke("stopShoot", 0.001f);
    }
    void stopShoot()
    {
        InputControl.ButtonShoot = false;
    }
}
