using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;

public class DisplayUserName : MonoBehaviour
{
    public string displayName;

    public bool showUserName;
    public GameObject usernameDisplay;


    void Start()
    {
        GetAccountInfoRequest acc = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(acc, success, fail);
    }

    void Update()
    {
        if (showUserName)
        {
            usernameDisplay.GetComponent<Text>().text = displayName;
        }
    }
   
    void success(GetAccountInfoResult result)
    {
        displayName = result.AccountInfo.Username;
    }


    void fail(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
}

