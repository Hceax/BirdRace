using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

    private Camera cam;

    void Start()
    {
        cam = GameObject.Find("CameraPlayer").GetComponent<Camera>();
    }
    
	void Update () {
        transform.LookAt(cam.transform);
	}
}
