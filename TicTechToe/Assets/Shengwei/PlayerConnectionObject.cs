using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer == false)
        {
            // This object belongs to another player.
            return;
        }

        Debug.Log("PlayerConnectionObject::Start -- Spawning my own personal unit.");

        CmdSpawnMyUnit();

    }

    public GameObject PlayerUnitPrefab;

    void Update()
    {
        if (isLocalPlayer == false)
        {
            return;
        }
    }

    [Command]
    void CmdSpawnMyUnit()
    {
        GameObject go = Instantiate(PlayerUnitPrefab);

        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

}
