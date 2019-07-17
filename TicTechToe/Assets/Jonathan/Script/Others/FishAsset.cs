using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishType
{
    Tuna,
    Sardine,
    Salmon,
    Catfish,
    Max
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Fish", menuName = "Item/Fish")]
public class FishAsset : Item
{
    public Sprite fishSprite;
    public FishType fishType;   
}

//[System.Serializable]
//public class Fish
//{
//    public Sprite fishSprite;
//    public FishType fishType;
//}


//public List<Fish> Fishes = new List<Fish>();

//public Sprite getFish(FishType fish)
//{
//    for (int i = 0; i < Fishes.Count; i++)
//    {
//        if (Fishes[i].fishType == fish)
//        {
//            return Fishes[i].fishSprite;
//        }
//    }
//    return null;
//}