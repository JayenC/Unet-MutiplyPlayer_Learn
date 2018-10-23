using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Health : NetworkBehaviour
{

    private const int maxHealth = 100;
    [SyncVar(hook = "OnHealthValueChanged")]
    public int currentHealth = maxHealth;
    public Slider healthSlider;

    private void Start()
    {
        healthSlider.value = 1;
    }

    public void TakeDamage()
    {
        if (!isServer)
            return;

        currentHealth -= 10;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Death");

            RpcReSpwan();
        }

    }

    public void OnHealthValueChanged(int health)
    {
        healthSlider.value = health / (float)maxHealth;
        currentHealth = health;
        print("value Change");
    }

    [ClientRpc]
    public void RpcReSpwan()
    {
        if (!isLocalPlayer) return;
        transform.position = Vector3.zero;
    }
}
