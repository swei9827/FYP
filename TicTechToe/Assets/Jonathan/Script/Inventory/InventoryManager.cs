using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public PlayerInventory playerInventory;

    [Header("Inventory Information")]
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;

    InventorySlot slots;

    void OnEnable()
    {
        MakeInventorySlot();
        playerInventory = PlayerInventory.instance;
    }

    void MakeInventorySlot()
    {
        if(playerInventory)
        {
            for (int i = 0; i < playerInventory.items.Count; i++)
            {
                GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(inventoryPanel.transform);
                temp.transform.localScale = new Vector2(1, 1);
                slots = temp.GetComponent<InventorySlot>();
                if (slots)
                {
                    slots.Setup(playerInventory.items[i], this);
                }
                                  
            }
        }
    }

  



    //public delegate void OnItemChanged();
    //public OnItemChanged onitemChangedCallback;

    //public static Inventory instance;
    //public static Inventory Instance { get { return instance; } }
    //public List<Fish> FishList = new List<Fish>();
    //public List<Crop> CropList = new List<Crop>();

    //public List<Image> image = new List<Image>();

    //int maxCapacity = 5;
    ////public Image[] temp;
    ////public int tempIndex;
    ////int tempImageIndex;
    ////public Image[] inventoryImg;

    //private void Awake()
    //{
    //    if (instance != null)
    //    {
    //        return;
    //    }

    //    instance = this;
    //}

    //public bool Add(Fish fishItems, Crop cropItems)
    //{

    //    if (CropList.Count > maxCapacity || FishList.Count > maxCapacity)
    //    {
    //        Debug.Log("Inventory Full");
    //        return false;
    //    }
    //    CropList.Add(cropItems);

    //    if (onitemChangedCallback != null)
    //    {
    //        onitemChangedCallback.Invoke();
    //    }
    //    return true;
    //}

    //public void Remove(Fish fishItems, Crop cropItems)
    //{
    //    CropList.Remove(cropItems);
    //}

}
