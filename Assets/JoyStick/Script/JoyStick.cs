using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Vector2 JoystickInput = Vector2.zero;
    //[SerializeField, Range(.0f, 1.0f)]
    //public float DeadZone = .0f;  //摇杆死区

    public bool AutoHide = true;

    private Image thumb;     //点
    private Image bg;     //圈
    private bool isActive;     //是否起用

    private float maxRaduis;//thumb移动区域半径

    // Use this for initialization
    void Start()
    {
        //Get Images
        bg = transform.FindChild("bg").GetComponent<Image>();
        thumb = bg.transform.FindChild("thumb").GetComponent<Image>();

        //由于视口大小不同，bg宽高会被缩放，需乘缩放比率
        //用到虚拟摇杆的触控设备，视口大小一般不会变（UWP？），不必实时更新此值
        maxRaduis = bg.rectTransform.rect.width * bg.transform.lossyScale.x * 0.5f;

        CrossFade(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            //摇杆回位。完全模拟物理摇杆，需插值回位。当然直接赋值也可以
            thumb.rectTransform.anchoredPosition
                = Vector2.Lerp(thumb.rectTransform.anchoredPosition, Vector2.zero, 24 * Time.deltaTime);
            //插值回位将导致轴输出数值插值归零，此处强制归零
            UpdateAxis(Vector2.zero);
        }
        else
        {
            UpdateAxis(thumb.rectTransform.anchoredPosition);
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //避免bg超出左下屏幕边缘
        //bg.transform.position.x =Mathf.Clamp(eventData.position.x, *thumb.transform.lossyScale.x);
        bg.transform.position
            = new Vector2(
                Mathf.Clamp(eventData.position.x, bg.transform.lossyScale.x * bg.rectTransform.rect.width / 2, Mathf.Infinity),
                Mathf.Clamp(eventData.position.y, bg.transform.lossyScale.y * bg.rectTransform.rect.height / 2, Mathf.Infinity)
            );
        thumb.transform.position = ValidPoint(eventData.position);

        isActive = true;
        CrossFade(1);

    }

    public void OnDrag(PointerEventData eventData)
    {
        thumb.transform.position = ValidPoint(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isActive = false;
        //bg.rectTransform.anchoredPosition = Vector3.zero;
        CrossFade(0);
    }

    //限制thumb移动区域
    private Vector2 ValidPoint(Vector2 point)
    {
        Vector2 delt;

        delt = (point - (Vector2)bg.transform.position);

        if (delt.magnitude > maxRaduis)
        {
            delt = delt.normalized * maxRaduis;
        }

        return delt + (Vector2)bg.transform.position;
    }

    //更新轴数值
    private void UpdateAxis(Vector2 dir)
    {

        dir.Set(dir.x / maxRaduis * transform.lossyScale.x, dir.y / maxRaduis * transform.lossyScale.y);

        /*输出范围圆转方*/
        float m = dir.magnitude;//模长

        dir.Normalize();//单位化

        float Alpha = Vector2.Angle(Vector2.right, dir);
        float Beta = Vector2.Angle(Vector2.up, dir);
        float subAlpha = Vector2.Angle(Vector2.left, dir);
        float subBeta = Vector2.Angle(Vector2.down, dir);

        //print("A"+ Mathf.Round(Alpha) +"/B"+ Mathf.Round(Beta) 
        //    + "//sA" + Mathf.Round(subAlpha) + "/sB" + Mathf.Round(subBeta));

        if (Alpha <= 45)
        {
            m /= Mathf.Cos(Mathf.Deg2Rad * Alpha);
        }
        else if (Beta <= 45)
        {
            m /= Mathf.Cos(Mathf.Deg2Rad * Beta);
        }
        else if (subAlpha <= 45)
        {
            m /= Mathf.Cos(Mathf.Deg2Rad * subAlpha);
        }
        else if (subBeta <= 45)
        {
            m /= Mathf.Cos(Mathf.Deg2Rad * subBeta);
        }

        dir *= m;

        JoystickInput.Set(Round(dir.x), Round(dir.y));

    }

    //交叉减淡
    private void CrossFade(float Alpha)
    {
        if (AutoHide)
        {
            bg.CrossFadeAlpha(Alpha, .2f, true);
            thumb.CrossFadeAlpha(Alpha, .2f, true);
        }
    }

    //舍入到0.01
    private float Round(float f)
    {
        return Mathf.Round(f * 100) / 100;
        //return Mathf.Clamp01(Mathf.Round(f * 100) / 100);
    }

    void LateUpdate()
    {
        //print(Mathf.Tan(Mathf.Deg2Rad * 45));
        //print(thumb.transform.position + "/" + bg.transform.position);
        //print(thumb.rectTransform.anchoredPosition + "/" + JoystickInput);

    }
}