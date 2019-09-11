using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crop", menuName = "Item/Crop")]
public class CropAsset : Item
{
    public Sprite seedSprite;
    public Sprite deadSprite;
    public Sprite doneSprite;

    public bool seedIsOnGround = false;
    public CropsType cropsType;
}

public enum CropsType
{
    Potatoes,
    Strawberries,
    Pumpkins,
}
