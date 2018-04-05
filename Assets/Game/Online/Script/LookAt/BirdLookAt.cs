using UnityEngine;
using System.Collections;

public class BirdLookAt : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
    }
}
