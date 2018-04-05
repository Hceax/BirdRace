using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPause : MonoBehaviour, IPointerDownHandler
{
    public Sprite pause;
    public Sprite start;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!InputModel.paused && !InputModel.Win && !InputModel.TimeOut)
        {
            InputModel.paused = !InputModel.paused;
        }
        else if (InputModel.paused)
        {
            InputModel.paused = !InputModel.paused;
        }
    }

    void Update()
    {
        if (InputModel.paused)
        {
            gameObject.GetComponent<Image>().sprite = start;
            Time.timeScale = 0;
        }
        else if (!InputModel.paused)
        {
            gameObject.GetComponent<Image>().sprite = pause;
            Time.timeScale = 1;
        }
    }
}
