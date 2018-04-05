using UnityEngine;
using UnityEngine.SceneManagement;

public class Guide : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {

        if (Application.platform == RuntimePlatform.Android && (Input.GetKeyDown(KeyCode.Escape)))
        {
            SceneManager.LoadScene("Start");
        }

    }
}
