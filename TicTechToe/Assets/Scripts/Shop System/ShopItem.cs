using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/ShopItemScriptableObject", order = 1)]
[Serializable]
public class ShopItem : ScriptableObject, IComparable<ShopItem>
{
    public int id;
    public Sprite itemSprite;
    public string itemName;
    public int itemPrice;

    public ShopItem()
    {
        this.id = -1;
        this.itemSprite = null;
        this.itemName = "non";
        this.itemPrice = -1;
    }

    public int CompareTo(ShopItem sItems)
    {
        return sItems.id - this.id;
    }
}

[Serializable]
public struct ShopItemList
{
    public List<ShopItem> shopList;
}
