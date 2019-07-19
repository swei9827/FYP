using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum ToolTypeTest
{
    None,
    Plow,
    WaterCan,
    FishRod,
    Max
}

public enum CropsTypeTest
{
    None,
    Potatoes,
    Strawberries,
    Pumpkins,
    Max
}

public enum FishTypeTest
{
    None,
    Tuna,
    Sardine,
    Salmon,
    Catfish,
    Max
}

public class ItemTest : MonoBehaviour {
    public ItemTest() { }
    public CropsTypeTest CropType;
    public ToolTypeTest ToolType;
    public FishTypeTest FishType;
    public int id;
    public Sprite[] sprites;
    public string details;
};




