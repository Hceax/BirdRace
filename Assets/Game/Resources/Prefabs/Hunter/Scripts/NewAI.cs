using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum Levels
{
    Level1 = 100,
    Level2 = 50,
    Level3 = 10
}

public class NewAI : MonoBehaviour
{
    //猎人活动的范围
    public int x1 = -5;
    public int x2 = 5;
    public int z1 = -5;
    public int z2 = 5;

    //关于猎人的变量
    public float rotateSpreed = 0.5f;  //转动的速度
    public float moveSpeed = 1f;  //移动的速度

    private Vector3 _target;   //保存目标位置
    public Transform player;  //主角的位置
    public float shootRange = 0.4f;  //射程范围
    public float seeRange = 2f; //发现的范围

    //关于子弹，及发射的准备的变量
    public GameObject shellPrefab;   //获得子弹的Prefab
    public Transform shellSpawn;  //定位子弹生成的位置
    public float shellForce = 1f; //定义一个子弹的力的大小
    public GameObject gun;   //获得物体枪
    private int levels; //难度，把枚举类型的转化成整形
    public Levels level = Levels.Level1;  //定义难度，通过改变猎人发射子弹的速度
    private int shootSpeed = 0;  //计算何时发射子弹
    private bool shooting = false;

    //关于血量及显示的变量
    public const int maxHealth = 100; //总血量
    public int currentHealth = maxHealth;  //当前血量
    public Slider healthSlider;  //显示血量的血条
    public int damageHP = 50;  //被攻击时收到的伤害

    //用于初始化的方法
    void Start()
    {
        levels = (int)level;   //难度的初始化
        _target = RandomPosition();//调用生成随机点的方法  //运行游戏，自动巡逻
    }

    //用于随时更新的方法
    void FixedUpdate()
    {

        if (currentHealth > 0)   //当血量大于0时才可执行
        {
            if (CanSeeTarget())   //当猎人发现目标时
            {
                _target = player.position; //如果主角在猎人的视野内，猎人移动的目标点就是主角所在位置 
                _target.y = transform.position.y;  //防止猎人后仰
                
                    DoMove();  //猎人行动
                    //WalkForwardAnimation();   //调用向前走的动画
                
                shooting = false;
                shootSpeed++;    //累加，为了延时射击的变量
                if (Vector3.Distance(transform.position, _target) < shootRange)   //当到达射程时，准备射击，射击
                {
                    float y = transform.position.y - player.position.y ;
                    //print(y);
                    FireAnimation();   //播放开火的动画
                    if (y <0.12 && y > -1)
                    {
                        ShootAtGoal();
                    }
                    else
                    {
                        ShootAtGoalUP();    //调用动画，并且把枪举起来
                    }

                    shooting = true;
                    if (CanShoot())   //当时间到达射击时，射击
                    {
                        Fires();   //开火射击
                    }

                }

            }
            else   //当没发现主角时，巡逻
            {
                shooting = false;
                DoMove();
                if (Vector3.Distance(transform.position, _target) < 0.1 || (transform.position.y - _target.y)>0.1 || (_target.y - transform.position.y) > 0.1)    //猎人运行到目标点后就会重新巡逻
                {
                    _target = RandomPosition();
                    _target.y = transform.position.y ;  //防止猎人后仰
                }
            }
        }
        else if (currentHealth <= 0)   //当猎人血量小于等于0时
        {
            DeathAnimation();   //播放死亡的动画
            Invoke("Death", 1.0f);  //1秒后销毁猎人
            currentHealth = 0;  //为了防止血量低于0，避免出错
        }
        healthSlider.value = currentHealth / (float)maxHealth;  //改变血条的进度，用当前血量/总血量
    }

    //是否能看到主角
    bool CanSeeTarget()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);   //计算两个点的距离   //得到敌人和主角的距离
            //print("distance" + distance);  //输出距离
            if (distance < seeRange)
            {
                return true; //当主角和敌人的距离小于2时，敌人就发现了主角
            }
        }
        return false;
    }

    //移动猎人用的方法
    void DoMove()
    {
        Vector3 deleteV = _target - transform.position;  //目标的点-自己的位置 得到一个向量
        Quaternion rotationNum = Quaternion.LookRotation(deleteV);  //要旋转的目标值//把距离转化成角度  注视旋转，使物体的Z轴朝向目标
        rotationNum = Quaternion.Slerp(transform.rotation, rotationNum, Time.deltaTime * rotateSpreed);  //计算插值，表示从哪个地方到哪个地方  每一帧旋转一小度
        transform.rotation = rotationNum;   //计算完插值，使物体旋转过去
        if (!shooting || !CanSeeTarget())
        {
            transform.Translate(transform.forward * moveSpeed, Space.World);  //朝着物体的正前方移动  --世界坐标系
            WalkForwardAnimation();
        }
        
    }

    //做好向上开火的姿势
    void ShootAtGoalUP()
    {
        //调整枪口的位置，使其斜向上
        Vector3 rotation = gun.transform.localEulerAngles;
        Vector3 position = gun.transform.localPosition;
        position.x = 0.01902193f;
        position.y = -0.005610736f;
        position.z = -0.03169343f;
        rotation.x = 353.8838f;
        rotation.y = 334.9155f;
        rotation.z = 346.9366f;
        gun.transform.localPosition = position;
        gun.transform.localEulerAngles = rotation;
    }

    //做好开火的姿势
    void ShootAtGoal()
    {
        //调整枪口的位置，使其向上
        Vector3 rotation = gun.transform.localEulerAngles;
        Vector3 position = gun.transform.localPosition;
        position.x = -7.629394e-07f;
        position.y = -3.051758e-07f;
        position.z = -6.866455e-07f;
        rotation.x = 5.183174e-06f;
        rotation.y = 0.0001719094f;
        rotation.z = 344.9999f;
        gun.transform.localPosition = position;
        gun.transform.localEulerAngles = rotation;
    }

    //定义一个生成随机的点的方法
    Vector3 RandomPosition()
    {
        //在指定的范围内生成随机的宽高
        float randomX = Random.Range(x1, x2);  //生成一个随机数
        float randomZ = Random.Range(z1, z2);
        Vector3 returnV = new Vector3(randomX, transform.position.y, randomZ);  //随机生成一个位置坐标
        return returnV;  //返回随机生成坐标
    }

    //定义一个方法，判断何时射击
    bool CanShoot()
    {
        if (shootSpeed > levels)
        {
            shootSpeed = 0;
            return true;
        }
        return false;
    }

    //开火的方法
    void Fires()
    {
        GameObject bullet = Instantiate(shellPrefab, shellSpawn.position, shellSpawn.rotation) as GameObject;//实例化子弹（预制体，位置，旋转）
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * shellForce; //给子弹初速度, 得到子弹的刚体组件 = 方向*速度
        Destroy(bullet, 2);//实例的子弹每个两秒就销毁
    }

    //控制敌人血量的方法  与子弹碰撞后会减血
    void OnCollisionEnter(Collision collision)  //注意方法名的开头要全部大写
    {
        if (collision.collider.tag == "purse")
        {
            TakeDamage(damageHP);  //调用TakeDamage方法
            DamageAnimation();
        }
    }

    //处理受伤的方法
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    //用于死亡后销毁物体
    void Death()
    {
        if (ScorinSystem.Emeny > 0)
        {
            ScorinSystem.Emeny--;
        }
        Destroy(this.gameObject);
    }

    //播放先前走的动画
    void WalkForwardAnimation()
    {
        GetComponent<Animation>().Play("Walk_Rifle_Forward");
    }

    //播放开火的动画
    void FireAnimation()
    {
        GetComponent<Animation>().Play("Fire_Rifle_Auto");
    }

    //播放受伤的动画
    void DamageAnimation()
    {
        GetComponent<Animation>().Play("Damage_Rifle");
    }

    //播放死亡的动画
    void DeathAnimation()
    {
        GetComponent<Animation>().Play("Death");
    }
}