using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class NetworkManager_Custom : NetworkManager
{
    public void StartupHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }

    void SetIPAddress()
    {
        NetworkManager.singleton.networkAddress = Overall.IP;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    /*
    void OnLevelWasLoaded(int level)
    {
        if (level == 10)
        {
            setupMenuSceneButtons();
        }
        else
        {
            setupOtherSceneButtons();
        }
    }*/

    void Start()
    {
        GameObject.Find("ButtonNext").GetComponent<Button>().onClick.RemoveAllListeners();
        if (Overall.Host)
            GameObject.Find("ButtonNext").GetComponent<Button>().onClick.AddListener(StartupHost);
        else if (Overall.Client)
            GameObject.Find("ButtonNext").GetComponent<Button>().onClick.AddListener(JoinGame);
    }

    void setupOtherSceneButtons()
    {
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }
}
