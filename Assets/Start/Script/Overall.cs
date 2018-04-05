using UnityEngine;

public class Overall : MonoBehaviour {

    public static bool Rocker = true;   //是否使用摇杆
    public static bool Gravity = false;  //是否使用重力感应
    public static bool Mute = false;   //是否静音
    public static string IP = "localhost";
    public static bool Host = false;
    public static bool Client = true;
    public static bool Audio = false;

    // Use this for initialization
    void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
