using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Set : MonoBehaviour
{
    public InputField IP;
    public Toggle[] player;
    public Toggle[] controller;
    public Scrollbar scrollbar;
    public Toggle mute;
    public Button back;

    // Use this for initialization
    void Start()
    {

        IP.text = Overall.IP;
        player[0].isOn = Overall.Client; //获取客户端开关
        player[1].isOn = Overall.Host;//获取主机开关

        controller[0].isOn = Overall.Rocker; //获取摇杆状态
        controller[1].isOn = Overall.Gravity;//获取重力感应状态
        mute.isOn = Overall.Mute;
        player[1].isOn = Overall.Host;


        back.onClick.AddListener(delegate () { SceneManager.LoadScene("Start"); });
    }

    // Update is called once per frame
    void Update()
    {



        if (mute.isOn != Overall.Mute)  //控制静音
        {
            Overall.Mute = mute.isOn;
        }

        //控制Host&Client不能同时被选中
        if (player[0].isOn != Overall.Client)
        {
            if (player[0].isOn == false)
            {
                player[0].isOn = !player[0].isOn;
                return;
            }
            Overall.Host = player[1].isOn = !player[0].isOn;
            Overall.Client = player[0].isOn;

            IP.readOnly = false;
            IP.text = Overall.IP;
        }
        else if (player[1].isOn != Overall.Host)
        {
            if (player[1].isOn == false)
            {
                player[1].isOn = !player[1].isOn;
                return;
            }
            Overall.Client = player[0].isOn = !player[1].isOn;
            Overall.Host = player[1].isOn;

            IP.text = Network.player.ipAddress;
            IP.readOnly = true;
        }

        //保存输入
        if (player[0].isOn)
        {
            IP.readOnly = false;
            Overall.IP = IP.text;
        }

        //控制Rocker&Gravity不能同时被选中
        if (controller[0].isOn != Overall.Rocker)
        {
            if (controller[0].isOn == false)
            {
                controller[0].isOn = !controller[0].isOn;
                return;
            }
            Overall.Gravity = controller[1].isOn = !controller[0].isOn;
            Overall.Rocker = controller[0].isOn;
        }
        else if (controller[1].isOn != Overall.Gravity)
        {
            if (controller[1].isOn == false)
            {
                controller[1].isOn = !controller[1].isOn;
                return;
            }
            Overall.Rocker = controller[0].isOn = !controller[1].isOn;
            Overall.Gravity = controller[1].isOn;
        }

        if (mute.isOn != Overall.Mute)  //控制静音
        {
            Overall.Mute = mute.isOn;
        }

        if (Application.platform == RuntimePlatform.Android && (Input.GetKeyDown(KeyCode.Escape)))
        {
            SceneManager.LoadScene("Start");
        }
    }
}
