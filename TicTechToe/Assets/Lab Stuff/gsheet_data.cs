using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using PlayFab.DataModels;
using PlayFab.ProfilesModels;

public class gsheet_data : MonoBehaviour
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

    IEnumerator PostData(string playername, int dataType, int dataCount)
    {
        WWWForm form = new WWWForm();

        form.AddField("entry.1139227581", playername);

        switch (dataType)
        {
            case 0: // fishcount
                form.AddField("entry.78893016", dataCount);
                break;

            case 1: //harvestcount
                form.AddField("entry.1611953632", dataCount);
                break;

            case 2://tradecount
                form.AddField("entry.1788300909", dataCount);
                break;

            case 3://humannpc
                form.AddField("entry.1265842569", dataCount);
                break;

            case 4://animalnpc
                form.AddField("entry.1176598382", dataCount);
                break;
        }       

        byte[] rawData = form.data;;

        // Post a request to an URL with our custom headers
        WWW www = new WWW("https://docs.google.com/forms/u/0/d/e/1FAIpQLSfhYbGnFCiByCJxZRt53rl1setBNkiCp6VL-7wf3ga0WxYBlg/formResponse", rawData);
        yield return www;
    }

    public void sendData(int dataType, int dataCount)
    {
        if (allowDataTransfer)
        {
            StartCoroutine(PostData(playerName, dataType, dataCount));
        }
    }
}
