using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        GameObject hit = col.gameObject;

        if (col.tag == "Player")
        {
            InputModel.TimeOut = true;
            Destroy(hit);
        }
        Destroy(this.gameObject);
    }
}
