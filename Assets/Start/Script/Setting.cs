using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public GameObject Audio; //得到声音组件

    public GameObject[] Buttons;  //存放所有按钮

    public Sprite[] setting;

    private bool show = false; //显示开关，判断是否打开
    private int index;  //数组下标，方便管理按钮的状态

    void Start()
    {
        if (!Overall.Audio)
        {
            Instantiate(Audio, transform.position, transform.rotation);
            Overall.Audio = true;
        }

        Buttons[1].GetComponent<Button>().onClick.RemoveAllListeners();   //移除点击监听
        Buttons[1].GetComponent<Button>().onClick.AddListener(delegate () { Application.Quit(); }); //给退出按键加上退出事件

        Buttons[5].GetComponent<Button>().onClick.RemoveAllListeners();
        Buttons[5].GetComponent<Button>().onClick.AddListener(delegate () { SceneManager.LoadScene("Guide"); });

        Buttons[4].GetComponent<Button>().onClick.RemoveAllListeners();
        Buttons[4].GetComponent<Button>().onClick.AddListener(delegate () { SceneManager.LoadScene("Drama"); });

        Buttons[3].GetComponent<Button>().onClick.RemoveAllListeners();
        Buttons[3].GetComponent<Button>().onClick.AddListener(delegate () { SceneManager.LoadScene("Teamwork"); });

        Buttons[2].GetComponent<Button>().onClick.RemoveAllListeners();
        Buttons[2].GetComponent<Button>().onClick.AddListener(delegate () { SceneManager.LoadScene("Set"); });
    }

    //按钮点击
    public void Click()
    {
        if (!show)  //如果未打开，调用打开方法
        {
            Open();
        }
        else if (show)  //如果打开，调用关闭方法
        {
            Close();
        }
        show = !show;  //拨动显示开关
    }

    //打开方法
    void Open()
    {
        for (int j = 0; j < Buttons.Length; j++)
        {
            if (j == 0) //判断是否为背景图片
            {
                for (int i = 0; i < Buttons.Length; i++)
                {
                    if (i == 0)
                    {
                        Buttons[i].gameObject.SetActive(true);  //只激活背景
                    }
                    else
                    {
                        Buttons[i].gameObject.SetActive(false);  //其他全关闭
                    }
                }
                index++; //下一个按钮
            }
            else
            {
                Invoke("Active", j * 0.05f);  //调用延时函数，使按钮依次打开
            }
        }
    }

    //关闭方法
    void Close()
    {
        for (int j = Buttons.Length - 1; j >= 0; j--)
        {
            Invoke("Hidden", j * 0.05f);
        }
    }

    //激活方法
    void Active()
    {

        this.GetComponent<Image>().sprite = setting[index];
        Buttons[index++].gameObject.SetActive(true);
        if (index > 5)  //排除越界的异常
        {
            index = 5;
        }
    }

    //隐藏方法
    void Hidden()
    {

        this.GetComponent<Image>().sprite = setting[index];
        Buttons[index--].gameObject.SetActive(false);
        if (index < 0)  //排除越界的异常
        {
            index = 0;
        }
    }

}
