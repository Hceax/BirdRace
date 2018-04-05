using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{


    public const int maxHealth = 100;
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;
    private Slider healthSlider;

    void Start()
    {
        //if (isLocalPlayer)
        {
            healthSlider = transform.FindChild("HealthSlider").transform.FindChild("Slider").GetComponent<Slider>();

        }
    }

    public void TakeDamage(int damage)
    {
        if (isServer == false) return;// 血量的处理只在服务器端执行

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            //Debug.Log("Dead");
            GetComponent<StartPosition>().RpcRespawn();
        }

    }
    void OnChangeHealth(int health)
    {
        healthSlider.value = health / (float)maxHealth;
    }
}
