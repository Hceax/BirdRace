using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    //public MovieTexture T; //电影纹理，但依赖播放器，只能在PC端显示
    public Texture[] T;   //存放要循环显示的图片
    public RawImage RI;   //获得显示的画布
    private int i = 0;   //循环标记

    void Start()
    {
        InvokeRepeating("LaunchProjectile", 0, 0.1f);
    }
    void LaunchProjectile()
    {
        if (i == T.Length)
        {
            SceneManager.LoadScene("Start");
            return;
        }
        RI.texture = T[i];  //改变画布的纹理
        if (i < T.Length)
            i++;
    }
}
