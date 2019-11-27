using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class gsheet_data : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            sendData();
        }
    }



    IEnumerator PostData(string playername, int fishcount, int harvestcount, int tradecount)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1139227581", playername);
        form.AddField("entry.78893016", fishcount);
        form.AddField("entry.1611953632", harvestcount);
        form.AddField("entry.1788300909", tradecount);

        byte[] rawData = form.data;;

        // Post a request to an URL with our custom headers
        WWW www = new WWW("https://docs.google.com/forms/u/0/d/e/1FAIpQLSfhYbGnFCiByCJxZRt53rl1setBNkiCp6VL-7wf3ga0WxYBlg/formResponse", rawData);
        yield return www;
    }

    public void sendData()
    {
        StartCoroutine(PostData("labtest0",1,3,4));
    }
}
