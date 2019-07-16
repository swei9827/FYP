using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;

    Fish fish;
    Crop crop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Fish newfish, Crop newCrop)
    {
        fish = newfish;
        crop = newCrop;

        icon.sprite = fish.fishSprite;
    }
}
