using UnityEngine;
using System.Collections;

public class Egg : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(0, 1, 0));
    }

    void OnTriggerEnter(Collider collider)
    {


        if (collider.tag == "Player")
        {
            ScorinSystem.BirdEgg++;
            //ScorinSystem.getThrow += 5;
            Destroy(this.gameObject);
        }
    }
}
