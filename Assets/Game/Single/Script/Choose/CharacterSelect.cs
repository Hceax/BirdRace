using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{

    public GameObject[] character;   //获得鸟的模型
    public Button selentLeft;    //选择上一个鸟的按钮
    public Button selentRight;  //选择下一个鸟的按钮
    public Button next;    //打开下一场景的按钮
    public Button before;  //打开上一场景的按钮
    public Text lebel;  //显示各个鸟的信息
    private GameObject[] charactershow; //显示鸟的模型
    private int choose1 = 0;
    private int choose2 = 0;
    private Camera cam;  //相机

    void Start()
    {
        charactershow = new GameObject[character.Length];
        instantiationCharacter();

        selentLeft.onClick.RemoveAllListeners();  //移除点击监听
        selentLeft.onClick.AddListener(delegate () { choose1 = 1; });  //重新设置点击监听

        selentRight.onClick.RemoveAllListeners();
        selentRight.onClick.AddListener(delegate () { choose2 = 1; });

        next.onClick.RemoveAllListeners();
        next.onClick.AddListener(delegate () { SceneManager.LoadScene("Single[1]"); });   //场景管理器，调用下一个场景

        before.onClick.RemoveAllListeners();
        before.onClick.AddListener(delegate () { SceneManager.LoadScene("Menu"); });
    }

    void Update()
    {
        //显示下一个小鸟
        if (choose2 == 1)
        {
            InputModel.index++;  //鸟的目录+1

            //循环显示
            if (InputModel.index >= character.Length)
            {
                InputModel.index = 0;
            }
            characterChange(InputModel.index);
            choose2 = 0;  //取消标记
        }

        if (choose1 == 1)
        {
            InputModel.index--;

            if (InputModel.index < 0)
            {
                InputModel.index = character.Length - 1;
            }
            characterChange(InputModel.index);
            choose1 = 0;
        }
    }

    //激活器
    void characterChange(int indexx)
    {
        //遍历鸟的数组，激活选择的对象
        for (int i = 0; i < character.Length; i++)
        {
            if (i == indexx)
                //show
                charactershow[i].SetActive(true);
            else
                //unvisible
                charactershow[i].SetActive(false);
        }
        informationChange();
    }

    void instantiationCharacter()
    {
        cam = GameObject.Find("Camera").GetComponent<Camera>();   //获得相机组件
        //Vector3 rotation = cam.transform.localEulerAngles;
        //rotation.x = -5; // 在这里修改坐标轴的值
        //cam.transform.localEulerAngles = rotation;
        //模型显示
        for (int i = 0; i < character.Length; i++)
        {
            charactershow[i] = (GameObject)(Instantiate(character[i], cam.transform.position + new Vector3(0, 0, 1.5f), transform.rotation)); //实例化模型
            //修改鸟的角度
            Vector3 rotation = charactershow[i].transform.localEulerAngles;  //获得鸟的初始角度
            rotation.x = 10; //修改x坐标，使小鸟低头
            rotation.y = 180; //修改y坐标，使小鸟面朝前方
            charactershow[i].transform.localEulerAngles = rotation;  //赋值修改后的坐标
        }

        characterChange(InputModel.index);  //调用激活器，激活对应的小鸟
    }

    void informationChange()
    {

        //显示小鸟信息
        if (InputModel.index == 0)
        {
            lebel.text = "玉兮";
        }
        if (InputModel.index == 1)
        {
            lebel.text = "展傲";
        }
        if (InputModel.index == 2)
        {
            lebel.text = "翚翚";
        }
        if (InputModel.index == 3)
        {
            lebel.text = "悠悠";
        }
        if (InputModel.index == 4)
        {
            lebel.text = "嘟嘟";
        }

    }
}
