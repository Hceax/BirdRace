using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(1, 0, 0));
    }

    void OnTriggerEnter(Collider collider)
    {


        if (collider.tag == "Player")
        {
            ScorinSystem.getCoin++;
            ScorinSystem.getThrow += 2;
            this.gameObject.SetActive(false);
        }
    }
}
