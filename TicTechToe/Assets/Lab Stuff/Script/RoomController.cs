using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomController : MonoBehaviourPunCallbacks
{    
    public GameObject playerPrefab;
    public Transform spawnPoint;
    public static bool playerSpawned;

    void Start()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("Is not in the room, returning back to Lobby");
            UnityEngine.SceneManagement.SceneManager.LoadScene("New Lobby");
            return;
        }

        
        GameObject playerInstance = (GameObject) PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity, 0);
        playerSpawned = true;
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Lab_Lobby");
        UnityEngine.SceneManagement.SceneManager.LoadScene("New Lobby");
    }
}
