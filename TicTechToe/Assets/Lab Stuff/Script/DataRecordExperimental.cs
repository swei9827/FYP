using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;

public class DataRecordExperimental : MonoBehaviour
{
    string MyPlayfabID;
    int currentDataVal;

    void Start()
    {
        GetAccountInfoRequest acc = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(acc, success, fail);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            ClientGetUserPublisherData("FishCount");
            StartCoroutine(waitAndWriteData("FishCount", 1));
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ClientSetUserPublisherData("HarvestCount", 0);
            ClientSetUserPublisherData("FishCount", 0);
            ClientSetUserPublisherData("TradeCount", 0);
        }
    }

    public void ClientSetUserPublisherData(string dataName, int incre)
    {
        int dataToSet = currentDataVal + incre;        

        PlayFabClientAPI.UpdateUserPublisherData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
             { dataName, dataToSet.ToString() }
         }
        },
        result => Debug.Log("Complete setting Regular User Publisher Data"),
        error =>
        {
            Debug.Log("Error setting Regular User Publisher Data");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void ClientGetUserPublisherData(string dataName)
    {
        PlayFabClientAPI.GetUserPublisherData(new GetUserDataRequest()
        {
            PlayFabId = MyPlayfabID
        }, result => {
            if (result.Data == null || !result.Data.ContainsKey(dataName))
            {
                Debug.Log("No" + dataName);
            }
            else
            {
                Debug.Log(dataName + " : " + result.Data[dataName].Value);
                currentDataVal = int.Parse(result.Data[dataName].Value);
            }
        },
        error => {
            Debug.Log("Got error getting Regular Publisher Data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    IEnumerator waitAndWriteData(string dataName, int incre)
    {
        yield return new WaitForSeconds(1.0f);
        ClientSetUserPublisherData(dataName, incre);
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
