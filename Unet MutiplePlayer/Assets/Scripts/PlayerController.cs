using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    void Update()
    {
        if (!isLocalPlayer)
            return;
         
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        

        transform.Rotate(Vector3.up * h * 150 * Time.deltaTime);
        transform.Translate(Vector3.forward * v * 3 * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
    }


    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    [Command]
    public void CmdFire()
    {
        GameObject ins = GameObject.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation)  as GameObject;
        ins.GetComponent<Rigidbody>().velocity = ins.transform.forward * 10;

        Destroy(ins, 2);

        NetworkServer.Spawn(ins);
    }

    
}
