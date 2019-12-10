using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnHelper : MonoBehaviour
{
    public GameObject helper;
    private HelperController helperController;
    private Transform player;

    GameObject playerInstance;

    public bool inScene = false;
    bool canSpawn = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && !inScene)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            SetHelperActive();

            inScene = true;

            //pop out textBox
            helperController = FindObjectOfType<HelperController>();
            helperController.StartCoroutine(helperController.TriggerTextBox());
            helperController.textDisplay.text = "Hello! Looks like you need my help";
        }

        else if (Input.GetKeyDown(KeyCode.H) && inScene)
        {
            //pop out textBox
            helperController = FindObjectOfType<HelperController>();
            helperController.StartCoroutine(helperController.TriggerTextBox());
            helperController.textDisplay.text = "GoodBye!";

            SetHelperActive();

            inScene = false;
        }   
    }

    void SetHelperActive()
    {       
        if(!inScene)
        {
            Vector2 spawnPos = new Vector2(player.transform.position.x + 2f, player.transform.position.y);
            playerInstance = Instantiate(helper, spawnPos, Quaternion.identity);
        }
        else
        {
            Destroy(playerInstance, 3f);
        }
    }
}

//For photon multiplayer

//void Update()
//{
//    if (RoomController.playerSpawned && Input.GetKeyDown(KeyCode.H) && !inScene)
//    {
//        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

//        Spawn();
//        inScene = true;
//    }
//    else if (RoomController.playerSpawned && Input.GetKeyDown(KeyCode.H) && inScene)
//    {
//        Spawn();
//        inScene = false;
//    }
//}

//void Spawn()
//{
//    if (!inScene)
//    {
//        Vector2 spawnPos = new Vector2(player.transform.position.x + 2f, player.transform.position.y);
//        playerInstance = (GameObject)PhotonNetwork.Instantiate(helper.name, spawnPos, Quaternion.identity, 0);
//    }
//    else
//    {
//        PhotonNetwork.Destroy(playerInstance);
//    }
//}
