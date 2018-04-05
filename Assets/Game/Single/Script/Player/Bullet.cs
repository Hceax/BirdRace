using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    public GameObject bulletPrefab;   //获得子弹的Prefab
    public Transform bulletSpawn;  //定位子弹生成的位置

    // Update is called once per frame
    void Update()
    {
        if (InputModel.Fire)
        {  //判断是否按下空格键

            if (ScorinSystem.getThrow > 0)
            {
                Fire();  //自己定义的开火方法
                ScorinSystem.getThrow--;
            }
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;//实例化子弹（预制体，位置，旋转）
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 50; //给子弹初速度, 得到子弹的刚体组件 = 方向*速度

        Destroy(bullet, 2);//实例的子弹每个两秒就销毁
    }
}
