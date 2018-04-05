using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Coin_Net : NetworkBehaviour {


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(1, 0, 0));
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "0"|| collider.tag == "1" || collider.tag == "2" || collider.tag == "3" || collider.tag == "4")
        {
            
            this.gameObject.SetActive(false);

            collider.transform.parent.GetComponent<OnlineSystem>().getCoin();
        }
    }
}
