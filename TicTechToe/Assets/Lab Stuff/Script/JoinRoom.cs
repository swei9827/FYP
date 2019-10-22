using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class JoinRoom : MonoBehaviour
{
    public LobbyManager manager;

    public void joinRoom()
    {
        string roomKey = GetComponentInChildren<Text>().text;
        Debug.Log(roomKey);
        manager.joiningRoom = true;

        PhotonNetwork.NickName = manager.playerName;
        PhotonNetwork.JoinRoom(roomKey);
    }
}
