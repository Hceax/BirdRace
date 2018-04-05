using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScorinSystem_Net : MonoBehaviour
{
    public GameObject[] coinList;   //
    public GameObject[] eggList;    //

    public static int getCoin = 0;
    public static int getThrow = 5;
    public static int BirdEgg = 0;
    public static int Emeny = 0;
    public static float Time = 0f;

    private Text coinText;
    private Text throwText;
    private Text eggText;
    private Text winText;
    private Text timeText;

    private int discrepancy;//吹气触发检测
    private bool jump = false;

    private GameObject audioSprint; //显示声音加速
    private Text textSprint; //加速提示文本

    // Use this for initialization
    void Start()
    {
        ScorinSystem_Net.getCoin = 0;
        ScorinSystem_Net.getThrow = 5;
        ScorinSystem_Net.BirdEgg = 0;
        Time = 600.0f;

        InputModel.TimeOut = false;
        InputModel.Win = false;
        jump = false;

        discrepancy = getThrow;

        coinText = GameObject.Find("Coin").GetComponent<Text>();
        throwText = GameObject.Find("Throw").GetComponent<Text>();
        eggText = GameObject.Find("Egg").GetComponent<Text>();
        winText = GameObject.Find("Win").GetComponent<Text>();
        timeText = GameObject.Find("Time").GetComponent<Text>();

        InvokeRepeating("setCoin", 30, 30);

        audioSprint = GameObject.Find("AudioSprint");
        textSprint = audioSprint.transform.FindChild("Text").GetComponent<Text>();
        audioSprint.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "获得硬币: " + ScorinSystem_Net.getCoin.ToString();
        throwText.text = "剩余攻击: " + ScorinSystem_Net.getThrow.ToString();
        eggText.text = "得到鸟蛋: " + ScorinSystem_Net.BirdEgg.ToString() + "/" + eggList.Length.ToString();
        timeText.text = "时间: " + ScorinSystem_Net.Time.ToString("0.0");

        if (ScorinSystem_Net.BirdEgg >= eggList.Length)
        {
            InputModel.Win = true;
            winText.text = "You win!";
            winText.gameObject.SetActive(true);
        }
        else if (InputModel.TimeOut)
        {
            winText.text = "GAME OVER";
            winText.gameObject.SetActive(true);
        }
        else
        {
            winText.gameObject.SetActive(false);
        }

        if ((getThrow - discrepancy) >= 5)
        {
            discrepancy = getThrow;
            audioSprint.gameObject.SetActive(true);
            Invoke("sprint", 3);
        }
        else if ((getThrow - discrepancy) < 0)
        {
            discrepancy = getThrow;
        }

        if (InputModel.Win && !jump)
        {
            Invoke("chapter", 5);
            jump = true;
        }
        else if (InputModel.TimeOut && !jump)
        {
            InputModel.chapter = 2;
            Invoke("chapter", 5);
            jump = true;
        }
    }

    void setCoin()
    {
        for (int i = 0; i < coinList.Length; i++)
            coinList[i].SetActive(true);
    }

    void sprint()
    {
        float a = AudioSprint.A();
        textSprint.text = "音量级别 " + ((int)a).ToString();
        Invoke("disappear", 2);
        if (a > 40)
        {
            InputModel.sprint = true;
        }
    }
    void disappear()
    {
        InputModel.sprint = false;
        audioSprint.gameObject.SetActive(false);
        textSprint.text = "请对麦克风吹气";
    }
}
