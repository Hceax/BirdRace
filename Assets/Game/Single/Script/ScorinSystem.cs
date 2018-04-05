using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScorinSystem : MonoBehaviour
{
    public GameObject[] coinList;   //
    public GameObject[] eggList;    //
    public GameObject[] emenyList; //

    public static int getCoin = 0;
    public static int getThrow = 5;
    public static int BirdEgg = 0;
    public static int Emeny = 0;
    public static float Time = 0f;

    private Text coinText;
    private Text throwText;
    private Text eggText;
    private Text emenyText;
    private Text winText;
    private Text timeText;

    private Button level;

    private int discrepancy;//吹气触发检测
    private bool jump = false;

    private GameObject audioSprint; //显示声音加速
    private Text textSprint; //加速提示文本

    // Use this for initialization
    void Start()
    {
        ScorinSystem.getCoin = 0;
        ScorinSystem.getThrow = 5;
        ScorinSystem.BirdEgg = 0;
        ScorinSystem.Emeny = emenyList.Length;
        if (InputModel.chapter == 0)
            Time = 300.0f;
        else if (InputModel.chapter == 1)
            Time = 300.0f;
        else if (InputModel.chapter == 2)
            Time = 600.0f;

        InputModel.TimeOut = false;
        InputModel.Win = false;
        jump = false;

        discrepancy = getThrow;

        coinText = GameObject.Find("Coin").GetComponent<Text>();
        throwText = GameObject.Find("Throw").GetComponent<Text>();
        eggText = GameObject.Find("Egg").GetComponent<Text>();
        emenyText = GameObject.Find("Emeny").GetComponent<Text>();
        winText = GameObject.Find("Win").GetComponent<Text>();
        timeText = GameObject.Find("Time").GetComponent<Text>();

        level = GameObject.Find("Level").GetComponent<Button>();
        level.gameObject.SetActive(false);

        InvokeRepeating("setCoin", 30, 30);

        audioSprint = GameObject.Find("AudioSprint");
        textSprint = audioSprint.transform.FindChild("Text").GetComponent<Text>();
        audioSprint.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "获得硬币: " + ScorinSystem.getCoin.ToString();
        throwText.text = "剩余攻击: " + ScorinSystem.getThrow.ToString();
        eggText.text = "得到鸟蛋: " + ScorinSystem.BirdEgg.ToString() + "/" + eggList.Length.ToString();
        emenyText.text = "敌人: " + ScorinSystem.Emeny.ToString();
        timeText.text = "时间: " + ScorinSystem.Time.ToString("0.0");

        if (ScorinSystem.BirdEgg >= eggList.Length)
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
        Invoke("disappear", 4);
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

    void chapter()
    {
        if (InputModel.chapter == 0)
        {
            InputModel.chapter = 1;
            level.gameObject.SetActive(true);
            level.onClick.AddListener(delegate () { SceneManager.LoadScene("Single[2]"); });
        }
        else if (InputModel.chapter == 1)
        {
            InputModel.chapter = 2;
            level.gameObject.SetActive(true);
            level.onClick.AddListener(delegate () { SceneManager.LoadScene("Single[3]"); });
        }
        else
        {
            InputModel.chapter = 0;
            if (!InputModel.TimeOut)
            {
                level.gameObject.SetActive(true);
                level.transform.FindChild("text").GetComponent<Text>().text = "恭喜通关！";
                level.onClick.AddListener(delegate () { SceneManager.LoadScene("Menu"); });
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }

        }
    }
}
