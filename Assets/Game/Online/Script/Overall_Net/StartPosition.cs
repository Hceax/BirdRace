using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class StartPosition : NetworkBehaviour
{
    public NetworkStartPosition[] StartPoints;


    // Use this for initialization
    void Start () {

        if (isServer == false) return;
        RpcRespawn();
    }
	

    [ClientRpc]
    public void RpcRespawn()
    {
        if (isLocalPlayer == false) return;

        //if (StartPoints.Length > 0)
        {
            transform.position = StartPoints[OnlineSystem.Team].transform.position;
            transform.forward = StartPoints[OnlineSystem.Team].transform.forward;

        }
    }
}
