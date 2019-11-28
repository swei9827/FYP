using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnalyticUI : MonoBehaviour
{
    public bool FarmingData;
    public bool FishingData;
    int value;
    public int temp;

    void Start()
    {
       
    }

    void Update()
    {
        GetComponent<Text>().text = value.ToString();
    }
}
