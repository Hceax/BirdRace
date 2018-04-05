using UnityEngine;
using System.Collections;

public class Purse_Net : MonoBehaviour
{
    public int team = 0;

    void OnCollisionEnter(Collision col)
    {
        GameObject hit = col.gameObject;

        if (col.collider.tag == "Player")
        {
            //if (hit.GetComponent<OnlineSystem>().team == team) return;
            hit.GetComponent<Health>().TakeDamage(50);
        }
        //Destroy(this.gameObject);
    }
}
