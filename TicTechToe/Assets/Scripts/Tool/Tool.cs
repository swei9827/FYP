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

    public void Start()
    {
        isPlow = false;
        isWaterCan = false;
        isFishingRod = false;
        isSeed = false;
    }
}

[System.Serializable]
class Seed
{
    public CropAsset crop;
    public int amount;
}