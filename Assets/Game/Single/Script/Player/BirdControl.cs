using UnityEngine;

public class BirdControl : MonoBehaviour
{
    private Vector3 des;//下一帧的位置
    private Vector3 frontV;
    private Vector3 rightV;
    private Vector3 topV;
    private Vector3 bTOP;//平衡矢量

    public float acceleration = 2.0f;//加速度
    public float angularAcceleration = 70.0f;//角加速度
    public float vMax = 8.0f;
    public float vMin = 2.0f;
    public float speed = 1;//初速度

    private Camera cam;
    private Vector3 camPos;
    private float camShift;//摄像机左右偏移量
    public float camOffset = 1.0f;//摄像机最大偏移量
    private float camMove;//摄像机前后偏移量

    void Start()
    {
        des = gameObject.transform.position + gameObject.transform.forward;

        topV = gameObject.transform.up;
        frontV = gameObject.transform.forward;
        rightV = gameObject.transform.right;
        bTOP = Vector3.up;

        cam = GameObject.Find("CameraPlayer").GetComponent<Camera>();
        camPos = cam.transform.position;

        camShift = 0.0f;
        camMove = 0.0f;
    }

    float Accelerate(float v0, float a, float t)//加速
    {
        float vt = v0 + a * t;

        if (vt < vMin) vt = vMin;
        if (vt > vMax) vt = vMax;

        return vt;
    }

    void FixedUpdate()
    {

        float T = Time.deltaTime;

        if (InputModel.sprint)
        {
            speed = vMax;
            camMove = camOffset;
        }

        // 加减速
        if (InputModel.SpeedUp)  // 加速
        {
            //speed = 110.0f;
            speed = Accelerate(speed, acceleration, T);

            if (camMove < camOffset)
            {
                // 加速时，摄像机位移往后拉
                camMove += T * 8;
                if (camMove > camOffset) camMove = camOffset;
            }
        }

        else // 减速
        {
            speed = Accelerate(speed, -acceleration / 1.5f, T);
        }

        // 操控
        if (Vector3.Angle(bTOP, topV) <= 40.0f)        //回旋时最大侧倾角
        {
            if (Vector3.Angle(bTOP, Vector3.up) <= 30.0f)   //最大俯角和仰角
            {

                if (InputModel.Axis.y > 0)
                {
                    // 向上平衡向量往前倾斜
                    bTOP = Vector3.Lerp(bTOP, frontV, Mathf.Abs(InputModel.Axis.y) * T * angularAcceleration / 100);

                    // 调整方向向量
                    topV = Vector3.Lerp(topV, frontV, Mathf.Abs(InputModel.Axis.y) * T * angularAcceleration / 100);
                    frontV = Vector3.Cross(rightV, topV);
                }

                if (InputModel.Axis.y < 0)
                {
                    // 向上平衡向量往后倾斜
                    bTOP = Vector3.Lerp(bTOP, -frontV, Mathf.Abs(InputModel.Axis.y) * T * angularAcceleration / 100);

                    // 调整方向向量
                    topV = Vector3.Lerp(topV, -frontV, Mathf.Abs(InputModel.Axis.y) * T * angularAcceleration / 100);
                    frontV = Vector3.Cross(rightV, topV);
                }
            }
            else    // (C)
            {
                while (Vector3.Angle(bTOP, Vector3.up) > 30.0f)
                {
                    bTOP = Vector3.Lerp(bTOP, Vector3.up, T * 0.1f);
                }
            }
            topV = Vector3.Lerp(topV, rightV * InputModel.Axis.x, T * 4);
            rightV = Vector3.Cross(topV, frontV);
        }
        // 左右回旋飞行
        if (InputModel.Axis.x != 0)
        {
            float X = -InputModel.Axis.x * T * angularAcceleration * Mathf.PI / 180.0f;

            Vector3 tmp;
            float xx, zz;

            xx = bTOP.x * Mathf.Cos(X) - bTOP.z * Mathf.Sin(X);
            zz = bTOP.x * Mathf.Sin(X) + bTOP.z * Mathf.Cos(X);
            tmp = new Vector3(xx, bTOP.y, zz);
            bTOP = tmp;

            xx = frontV.x * Mathf.Cos(X) - frontV.z * Mathf.Sin(X);
            zz = frontV.x * Mathf.Sin(X) + frontV.z * Mathf.Cos(X);
            tmp = new Vector3(xx, frontV.y, zz);
            frontV = tmp;

            xx = rightV.x * Mathf.Cos(X) - rightV.z * Mathf.Sin(X);
            zz = rightV.x * Mathf.Sin(X) + rightV.z * Mathf.Cos(X);
            tmp = new Vector3(xx, rightV.y, zz);
            rightV = tmp;

            xx = topV.x * Mathf.Cos(X) - topV.z * Mathf.Sin(X);
            zz = topV.x * Mathf.Sin(X) + topV.z * Mathf.Cos(X);
            tmp = new Vector3(xx, topV.y, zz);
            topV = tmp;

            camShift = Mathf.Lerp(camShift, InputModel.Axis.x, T * angularAcceleration / 10);

        }

        // 左右平衡
        // topV向量会一直往bTOP向量做靠拢
        topV = Vector3.Lerp(topV, bTOP, T * 8);
        if (Vector3.Angle(topV, bTOP) <= 0.01f)
        {
            topV = bTOP;
        }

        // 靠拢完后都还要再对其他两个向量做叉积，算出新的两个向量
        frontV = Vector3.Cross(rightV, topV);
        rightV = Vector3.Cross(topV, frontV);

        // 向上平衡向量也要做外积调整
        // 因为在做倾斜飞行和向上爬升动作时
        // 该向量很可能会因为浮点数误差而偏移了
        bTOP = Vector3.Cross(frontV, new Vector3(rightV.x, 0, rightV.z));

        // 摄像机左右位移平衡(摄像机回正)
        if (InputModel.Axis.x == 0.0f)
        {
            camShift = Mathf.Lerp(camShift, InputModel.Axis.x, T * 6);
        }

        // 摄像机前后位移平衡
        if (!InputModel.SpeedUp)//&& !InputModel.SpeedDown)
        {
            if (Mathf.Abs(camMove) > 0.01f)
            {
                if (Mathf.Abs(camMove) < 0.02f)
                {
                    camMove = 0.0f;
                    return;
                }
                //解决抖动
                if (camMove > 0.1f) camMove -= T * 8;
                else if (camMove < -0.1f) camMove += T * 8;
                else camMove = 0.0f;
            }
            else
            {
                camMove = 0.0f;
            }
        }

        // 向量的单位化
        topV = Vector3.Normalize(topV);
        frontV = Vector3.Normalize(frontV);
        rightV = Vector3.Normalize(rightV);
        bTOP = Vector3.Normalize(bTOP);

        // 更新下一个frame的des
        des = gameObject.transform.position + frontV;

        // 并且设置面向的方向为des
        gameObject.transform.LookAt(des, topV);

        // 更新位置
        gameObject.transform.Translate(T * Vector3.forward * speed);

        // 更新摄像机的位置和方向
        Vector3 tmp2 = Vector3.Cross(Vector3.up, gameObject.transform.forward);
        camPos = gameObject.transform.position
            + Vector3.up * 0.5f
            - gameObject.transform.forward * (2.0f + camMove)
            + camShift * tmp2 * 0.5f;

        cam.transform.position = camPos;

        cam.transform.LookAt(gameObject.transform.position
            + gameObject.transform.forward * 100
            + camShift * tmp2 * 3);
    }
}

