using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Fire_Net : NetworkBehaviour
{

    public GameObject bulletPrefab;   //获得子弹的Prefab
    public Transform bulletSpawn;  //定位子弹生成的位置

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer == false)
        {
            return;
        }

        if (InputModel.Fire)
        {  //判断是否按下空格键

            if (ScorinSystem_Net.getThrow > 0)
            {
                CmdFire();  //自己定义的开火方法
                ScorinSystem_Net.getThrow--;
            }
        }
    }

    [Command]// called in client, run in server
    void CmdFire()//这个方法需要在server里面调用
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;//实例化子弹（预制体，位置，旋转）
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 50; //给子弹初速度, 得到子弹的刚体组件 = 方向*速度
        bullet.GetComponent<Purse_Net>().team = OnlineSystem.Team;
        Destroy(bullet, 2);//实例的子弹每个两秒就销毁

        NetworkServer.Spawn(bullet);  //同步到每个客户端
    }
}
