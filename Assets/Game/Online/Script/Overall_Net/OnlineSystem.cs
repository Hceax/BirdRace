using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class OnlineSystem : NetworkBehaviour
{

    public static int Team = 0;
    [SyncVar]
    public int team = 0;
    public float GameTime = 221.0f;

    // Use this for initialization
    void Start () {
        team = Team;
        //RecordNum.time = this.GameTime;
    }

    void Update()
    {
        //if (!GameStatus.Pause && !GameStatus.Win && RecordNum.time > 0.0f)
        {
            //RecordNum.time -= Time.deltaTime;
        }
        //else if (RecordNum.time <= 0.0f)
        {
            //RecordNum.time = 0.0f;
            //GameStatus.TimeOut = true;
        }

        //gameObject.GetComponent<Text>().text = "Time: " + RecordNum.time.ToString("0.0");
    }

    public void getCoin()
    {
        if (isLocalPlayer == false) return;
        ScorinSystem_Net.getCoin++;
        ScorinSystem_Net.getThrow += 2;
    }

    public void getEgg()
    {
        if (isLocalPlayer == false) return;
        ScorinSystem_Net.getCoin++;
        //ScorinSystem_Net.getThrow += 5;
    }

    public override void OnStartLocalPlayer()
    {
        //这个方法只会在本地角色那里调用  当角色被创建的时候
        //GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
