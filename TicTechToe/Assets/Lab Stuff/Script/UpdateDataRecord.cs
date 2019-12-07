using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using PlayFab.DataModels;
using PlayFab.ProfilesModels;

public class UpdateDataRecord : MonoBehaviour
{
    public bool allowDataTransfer;

    string playerName;

    void Start()
    {
        GetAccountInfo();
    }

    void GetAccountInfo()
    {
        GetAccountInfoRequest request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, Successs, fail);
    }


    void Successs(GetAccountInfoResult result)
    {
        playerName = result.AccountInfo.Username;
    }


    void fail(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    IEnumerator PostData(string playername, string eventData)
    {
        WWWForm form = new WWWForm();

        form.AddField("entry.1877185558", playername);
        form.AddField("entry.1742081839", eventData);

        byte[] rawData = form.data; ;

        // Post a request to an URL with our custom headers
        WWW www = new WWW("https://docs.google.com/forms/u/0/d/e/1FAIpQLScX-M_byLNBwyMF9IjfgTHv7l3IDC47tmzheJBslG9MUF2LXQ/formResponse", rawData);
        yield return www;
    }

    public void sendData(string eventData)
    {
        if (allowDataTransfer)
        {
            StartCoroutine(PostData(playerName, eventData));
        }
    }
}
