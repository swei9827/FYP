using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;

public class CloudData : MonoBehaviour
{
    string MyPlayfabID;
    int currentDataVal;
    int itemID;
    int itemAmount;
    bool playerDataReady = false;

    public bool allowdDataTransfer = false;

    Inventory inv;

    void Start()
    {
        GetAccountInfoRequest acc = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(acc, success, fail);
        inv = Player.LocalPlayerInstance.GetComponent<Player>().inventory;
        StartCoroutine(InitializeInventory());
    }

    public void ClientSetUserPublisherData(string dataName, int incre)
    {
        Debug.Log(dataName + " " + incre + " " + currentDataVal);
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
                //Debug.Log("No " + dataName);
            }
            else
            {
                //Debug.Log(dataName + " : " + result.Data[dataName].Value);
                currentDataVal = int.Parse(result.Data[dataName].Value);
            }
        },
        error => {
            Debug.Log("Got error getting Regular Publisher Data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    void retrieveInvData(string dataID)
    {
        PlayFabClientAPI.GetUserPublisherData(new GetUserDataRequest()
        {
            PlayFabId = MyPlayfabID
        }, result => {
            if (result.Data == null || !result.Data.ContainsKey(dataID))
            {
                Debug.Log("No " + "ItemID_" + dataID);
            }
            else
            {
                Debug.Log("ItemID_" + dataID + " " + inv.databaseRef.database[int.Parse(dataID)].itemName + " : " + result.Data[dataID].Value);
                itemAmount = int.Parse(result.Data[dataID].Value);
                itemID = int.Parse(dataID);

                inv.AddItemFromCloud(itemID, itemAmount);
            }
        },
        error => {
            Debug.Log("Got error getting Regular Publisher Data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }  

    public void writeToCloud(string dataID, int incre)
    {
        if (allowdDataTransfer)
        {
            StartCoroutine(waitAndWriteData(dataID, incre));
        }
    }

    IEnumerator waitAndWriteData(string dataID, int incre)
    {
        ClientGetUserPublisherData(dataID);
        yield return new WaitForSeconds(1.0f);
        ClientSetUserPublisherData(dataID, incre);
    }

    IEnumerator InitializeInventory()
    {
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < inv.databaseRef.database.Count; i++)
        {
            retrieveInvData(i.ToString());
        }
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
