using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fish", menuName = "Fish")]
public class FishAsset : ScriptableObject
{
    public Sprite fishSprite; 
    public FishType fishType;   
}

public enum FishType
{
    Tuna, 
    Sardine,
    Salmon,
    Catfish,
}
