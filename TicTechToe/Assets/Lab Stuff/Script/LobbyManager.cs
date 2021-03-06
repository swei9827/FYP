﻿using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using PlayFab.ClientModels;

public class LobbyManager : MonoBehaviourPunCallbacks
{   
    List<RoomInfo> createdRooms = new List<RoomInfo>();
    List<GameObject> buttons = new List<GameObject>();

    string MyPlayfabID;
    int tutorialStatus = -1;

    string gameVersion = "1.0";    
    bool roomListCreated = false;

    [HideInInspector]
    public string playerName = "";
    [HideInInspector]
    public int roomCapacity;
    [HideInInspector]
    public bool joiningRoom = false;

    public string levelToLoad;
    public Text info;
    public InputField roomName;
    public GameObject buttonListContent;    
    public GameObject roomPrefab;
      
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        playerName = PlayerPrefs.GetString("Email");
        roomName.text = "abc";

        GetAccountInfoRequest acc = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(acc, success, fail);
    }

    void Update()
    {
        roomListUpdate();
        if (tutorialStatus == -1)
        {
            ClientGetUserPublisherData("tdone");
        }
    }

    public void ClientGetUserPublisherData(string dataName)
    {
        PlayFabClientAPI.GetUserPublisherData(new GetUserDataRequest()
        {
            PlayFabId = MyPlayfabID
        }, result => {
            if (result.Data == null || !result.Data.ContainsKey(dataName))
            {
                //Debug.Log("No " + dataName);
                tutorialStatus = 0;
                Debug.Log("Tutorial NOT Done");
            }
            else
            {
                //Debug.Log(dataName + " : " + result.Data[dataName].Value);
                tutorialStatus = 1;
                TutorialManager.doneTutorial = true;
                Debug.Log("Tutorial Done");
            }
        },
        error => {
            Debug.Log("Got error getting Regular Publisher Data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    void roomListUpdate()
    {
        info.text = "User : " + playerName + "\nVersion : " + gameVersion + "\nStatus : " + PhotonNetwork.NetworkClientState;

        if (joiningRoom || !PhotonNetwork.IsConnected || PhotonNetwork.NetworkClientState != ClientState.JoinedLobby)
        {
            Debug.Log("Connecting...");
        }

        if (createdRooms.Count != 0 && !roomListCreated)
        {
            for (int i = 0; i < createdRooms.Count; i++)
            {
                if (!createdRooms[i].RemovedFromList)
                {
                    GameObject temp = Instantiate(roomPrefab, buttonListContent.transform.position, Quaternion.identity);
                    temp.transform.SetParent(buttonListContent.transform);
                    temp.GetComponentInChildren<Text>().text = createdRooms[i].Name;
                    temp.transform.Find("Capacity").GetComponent<Text>().text = createdRooms[i].PlayerCount + " / " + createdRooms[i].MaxPlayers;
                }
            }
            roomListCreated = true;
        }
    }

    public void createRoom()
    {
        if (roomName.text != "")
        {
            joiningRoom = true;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsOpen = true;
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = (byte)roomCapacity;

            PhotonNetwork.CreateRoom(roomName.text, roomOptions, TypedLobby.Default);
        }
    }

    public void refresh()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        if (PhotonNetwork.IsConnected)
        {            
            PhotonNetwork.JoinLobby(TypedLobby.Default);
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + cause.ToString() + " ServerAddress: " + PhotonNetwork.ServerAddress);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("We have received the Room list");
        createdRooms = roomList;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
        joiningRoom = false;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
        joiningRoom = false;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed got called. This can happen if the room is not existing or full or closed.");
        joiningRoom = false;
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        PhotonNetwork.NickName = playerName;
        PhotonNetwork.LoadLevel(levelToLoad);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    void success(GetAccountInfoResult result)
    {
        MyPlayfabID = result.AccountInfo.PlayFabId;
        Debug.Log("Successfully obtain PlayFabID");
    }


    void fail(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
}