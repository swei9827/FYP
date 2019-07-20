using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slots : MonoBehaviour
{
    public GameObject item;

    public bool empty;

    public int id;
    public Sprite itemIcon;

    public CropsTypeTest CropType;
    public FishTypeTest FishType;

    public void UpdateSlot()
    {
        this.GetComponent<Image>().sprite = itemIcon;
    }
}
