using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VRmode : MonoBehaviour
{

    private Camera main;

    // Use this for initialization
    void Start()
    {
        main = transform.FindChild("MojingMain").transform.FindChild("MojingVrHead").transform.FindChild("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //transform.Rotate(transform.up * h * 120 * Time.deltaTime);
        transform.Translate(main.transform.right * h * 3 * Time.deltaTime);
        transform.Translate(main.transform.forward * v * 3 * Time.deltaTime);

        if (Application.platform == RuntimePlatform.Android && (Input.GetKeyDown(KeyCode.Escape)))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
