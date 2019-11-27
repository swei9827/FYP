using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
	public Sprite[] sprite;
    public bool isPlow;
    public bool isWaterCan;
    public bool isFishingRod;
    public bool isSeed;
    public Seed[] seeds;

    public void Start()
    {
        isPlow = false;
        isWaterCan = false;
        isFishingRod = false;
        isSeed = false;
    }
}

[System.Serializable]
public class Seed
{
    public bool isSelected = false;
    public Crop crop;
    public int amount;
    public string seedName;
}