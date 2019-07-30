using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Camera_Follow_Player : NetworkBehaviour
{
    public GameObject[] players;
    public GameObject localPlayer;

    private Vector3 offset;
    private bool offsetInit = false;

    void Start()
    {
        //offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<NetworkIdentity>().hasAuthority)
            {
                if (offsetInit == false)
                {
                    offset = Vector3.zero;
                    offset.z = -5;
                    offsetInit = true;
                }
                else
                {
                    transform.position = players[i].transform.position + offset;
                }
            }
        }

    }
}
