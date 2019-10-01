﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataRecord : MonoBehaviour
{
    // Singleton Declaration
    public static DataRecord instance;

    // Event Name
    static string eventName;

    // Activities Data Collection
    static public int HarvestCount;
    static public int FishCount;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

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

    public static void AddEvents(int eventid, string eventObj)
    {
        string path = "Assets/Resource/DataRecord/Log.txt";
        StreamWriter writer = new StreamWriter(path, true);

        writer.WriteLine(System.DateTime.Now + " Player" + EventAction(eventid) + eventObj);

        writer.Close();
    } 

    public void ActivitiesRecord(int eventid)
    {
        switch (eventid)
        {
            case 0:
                HarvestCount += 1;
                break;

            case 1:
                FishCount += 1;
                break;

            default:
                break;
        }
    }

    static string EventAction(int eventID)
    {
        switch (eventID)
        {
            case 0:
                eventName = " obtained ";
                break;

            case 1:
                eventName = " returned ";
                break;

            case 2:
                eventName = " plowed ";
                break;

            case 3:
                eventName = " planted ";
                break;

            case 4:
                eventName = " watered ";
                break;

            case 5:
                eventName = " harvested ";
                break;

            default:
                eventName = "NULL";
                break;
        }

        return eventName;
    }

    void Start()
    {
        ClearLogFile();
        LogFileGeneration();
    }

    void Update()
    {
        
    }
}


