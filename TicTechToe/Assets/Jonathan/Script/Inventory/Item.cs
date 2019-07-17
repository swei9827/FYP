using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [Header("Inventory Items")]
    public Sprite itemIcons;
    public int numberHeld;   
    public bool usable;

    public virtual void use()
    {

    }
}

