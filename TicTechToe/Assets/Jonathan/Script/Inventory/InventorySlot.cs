using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemNum;

    [Header("Variables for items")]
    public Item thisItem;
    public InventoryManager thisManager;

    public void Setup(Item newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;

        if(thisItem)
        {
            if(thisItem)
            {
                itemImage.sprite = thisItem.itemIcons;
                itemNum.text = "" + thisItem.numberHeld;
                itemImage.enabled = true;
            }
        }
    }

    private void Start()
    {
      
    }


    //public Image icon;

    ////Fish fish;
    //Crop crop;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //public void AddItem(Fish newfish, Crop newCrop)
    //{
    //    fish = newfish;
    //    crop = newCrop;

    //    icon.sprite = fish.fishSprite;
    //}
}
