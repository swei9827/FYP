using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ShopItem : IComparable<ShopItem>
{
    public int id;

    public ShopItem(int i)
    {
        this.id = i;
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
