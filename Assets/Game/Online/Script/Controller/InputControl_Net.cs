using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputControl_Net : NetworkBehaviour
{
    [SerializeField, Range(0.0f, 1.0f), Header("重力感应有效控制区范围"), Tooltip("'左'和'右'范围")]
    public float AcclrtnZoneX = 0.4f;
    [SerializeField, Range(0.0f, 1.0f), Tooltip("'前'和'后'范围")]
    public float AcclrtnZoneY = 0.3f;//*前后*范围

    private Vector2 startPose = Vector2.zero;//初始重力感应位置
    private Vector2 dir = Vector2.zero;//操纵方向

    private JoyStick JS;//摇杆对象

    private Button buttonSpeedUp;
    private Button buttonShoot;

    /// <summary>
    /// public struct ButtonStatusModel
    /// YOU DO KNOW WHAT I MEAN
    /// </summary>
    public static bool ButtonSpeedUp = false;
    //public static bool ButtonSpeedDown = false;
    public static bool ButtonShoot = false;

    //public static bool ButtonPause = false;

    //游戏控制初始化
    void Start()
    {
        //InputModel.paused = ButtonPause;

        startPose.x = Input.acceleration.x;
        startPose.y = Input.acceleration.y;

        //Get Instance of public class JoyStick
        JS = GameObject.Find("JoyStick").GetComponent<JoyStick>();

        buttonSpeedUp = GameObject.Find("ButtonSpeedUp").GetComponent<Button>();
        buttonShoot = GameObject.Find("ButtonShoot").GetComponent<Button>();

        if (Overall.Gravity)
        {
            JS.gameObject.SetActive(false);
            JS.transform.parent.FindChild("Buttons").gameObject.SetActive(false);

            buttonSpeedUp.gameObject.SetActive(true);
            buttonShoot.gameObject.SetActive(true);
        }
        else if (Overall.Rocker)
        {
            JS.gameObject.SetActive(true);

            buttonSpeedUp.gameObject.SetActive(false);
            buttonShoot.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (isLocalPlayer == false)
        {
         //   return;
        }

        if (InputModel.Win || InputModel.TimeOut)
        {
            return;
        }
        dir.x = Overall.Gravity ? Mathf.Clamp((Input.acceleration.x - startPose.x) / AcclrtnZoneX, -1.0f, 1.0f) : Mathf.Clamp(JS.JoystickInput.x, -1.0f, 1.0f);
        dir.y = Overall.Gravity ? Mathf.Clamp((Input.acceleration.y - startPose.y) / AcclrtnZoneY, -1.0f, 1.0f) : Mathf.Clamp(JS.JoystickInput.y, -1.0f, 1.0f);
        //if (dir.sqrMagnitude > 1) dir.Normalize();

        InputModel.SpeedUp = Input.GetKey("u") || ButtonSpeedUp ? true : false;
        if (Input.GetKeyDown("p"))
            InputModel.paused = !InputModel.paused;

        InputModel.Axis.x = Input.GetKey("left") || Input.GetKey("right") ? Input.GetAxis("Horizontal") : dir.x;
        InputModel.Axis.y = Input.GetKey("up") || Input.GetKey("down") ? Input.GetAxis("Vertical") : dir.y;

        InputModel.Fire = Input.GetKeyDown(KeyCode.Space) || ButtonShoot;

        if (Application.platform == RuntimePlatform.Android && (Input.GetKeyDown(KeyCode.Escape)))
        {
            SceneManager.LoadScene("Choose_Net");
        }
    }
}