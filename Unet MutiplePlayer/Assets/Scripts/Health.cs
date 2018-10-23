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
    public bool destoryOnDeath  = false ;

    private NetworkStartPosition[] spwanPoints;

    private void Start()
    {
        healthSlider.value = 1;
        spwanPoints = FindObjectsOfType<NetworkStartPosition>();
    }

    public void TakeDamage()
    {
        if (!isServer)
            return;

        currentHealth -= 10;

        if (currentHealth <= 0)
        {
            if(destoryOnDeath)
            {
                Destroy(this.gameObject);return;
            }

            currentHealth = maxHealth;
            Debug.Log("Death");

            RpcReSpwan();
        }

    }

    public void OnHealthValueChanged(int health)
    {
        healthSlider.value = health / (float)maxHealth;
    }

    [ClientRpc]
    public void RpcReSpwan()
    {
        if (!isLocalPlayer) return;

        Vector3 pos = Vector3.zero;
        if(spwanPoints != null && spwanPoints.Length >0)
        {
            pos = spwanPoints[Random.Range(0, spwanPoints.Length)].transform.position;
            print(spwanPoints.Length);
        }

        transform.position = pos;
    }
}
