using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnalyticUI : MonoBehaviour
{
    public bool FarmingData;
    public bool FishingData;
    int value;

    [Header("Developer Use")]
    public float Modifier;

    void Start()
    {
        if (FarmingData)
        {
            value = GameObject.FindGameObjectWithTag("GameController").GetComponent<DataRecord>().HarvestCount;
        }
        else if (FishingData)
        {
            value = GameObject.FindGameObjectWithTag("GameController").GetComponent<DataRecord>().FishCount;
        }
    }

    void Update()
    {
        this.GetComponent<Image>().fillAmount = value / 100;
    }
}
