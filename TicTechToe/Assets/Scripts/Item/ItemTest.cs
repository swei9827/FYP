using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;

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

public class ItemTest : MonoBehaviourPun {

    public CropsTypeTest CropType;
    public FishTypeTest FishType;

    public int id;
    public Sprite[] sprites;
    public string details;
    public bool pickUp;
};




