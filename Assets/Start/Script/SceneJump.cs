using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneJump : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

    }

    public void menu2Start()
    {
        SceneManager.LoadScene("Start");
    }

    public void start2menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void menu2single()
    {
        SceneManager.LoadScene("Choose");
    }

    public void menu2online()
    {
        SceneManager.LoadScene("Choose_Net");
    }

    public void menu2vr()
    {
        SceneManager.LoadScene("VRmode");
    }

}
