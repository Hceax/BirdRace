using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == InputModel.index)
                //show
                GameObject.FindWithTag(i.ToString()).SetActive(true);
            else
                //unvisible
                GameObject.FindWithTag(i.ToString()).SetActive(false);
        }
}
}
