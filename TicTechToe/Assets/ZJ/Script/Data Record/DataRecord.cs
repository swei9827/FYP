using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class DataRecord : MonoBehaviour
{
    [MenuItem("Tools/Write file")]
    static void LogFileGeneration()
    {       
        string path = "Assets/Resource/DataRecord/Log.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(System.DateTime.Now + " Log Started");
        writer.Close();
        Debug.Log("Log file generated");
    }

    void ClearLogFile()
    {
        string path = "Assets/Resource/DataRecord/Log.txt";
        if (path != null)
        {
            File.Delete("Assets/Resource/DataRecord/Log.txt");
            Debug.Log("Log file removed");
        }
        else
        {
            Debug.Log("No Log file found");
        }
        
    }

    [MenuItem("Tools/Write file")]
    public void AddEvents(int eventID, string eventObj)
    {
        string eventName;
        string path = "Assets/Resource/DataRecord/Log.txt";
        StreamWriter writer = new StreamWriter(path, true);
        if (eventID == 0)
        {
            eventName = " obtained ";
            writer.WriteLine(System.DateTime.Now + " Player" + eventName + eventObj);
        }
        else if (eventID == 1)
        {
            eventName = " returned ";
            writer.WriteLine(System.DateTime.Now + " Player" + eventName + eventObj);                       
        }
        else if(eventID == 2)
        {
            eventName = " plowed ";
            writer.WriteLine(System.DateTime.Now + " Player" + eventName + eventObj);
        }
        else if(eventID == 3)
        {
            eventName = " planted ";
            writer.WriteLine(System.DateTime.Now + " Player" + eventName + eventObj);
        }
        else if(eventID == 4)
        {
            eventName = " watered ";
            writer.WriteLine(System.DateTime.Now + " Player" + eventName + eventObj);
        }
        else if (eventID == 5)
        {
            eventName = " harvested ";
            writer.WriteLine(System.DateTime.Now + " Player" + eventName + eventObj);
        }
        writer.Close();
    }

    void Start()
    {
        ClearLogFile();
        LogFileGeneration();
    }
}

