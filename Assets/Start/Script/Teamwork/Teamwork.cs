using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Teamwork : MonoBehaviour {
    private Button back;

    // Use this for initialization
    void Start () {
        back = GameObject.Find("Back").GetComponent<Button>();
        back.onClick.AddListener(delegate () { SceneManager.LoadScene("Start"); });
    }
	
	// Update is called once per frame
	void Update () {
        if (Application.platform == RuntimePlatform.Android && (Input.GetKeyDown(KeyCode.Escape)))
        {
            SceneManager.LoadScene("Start");
        }
    }
}
