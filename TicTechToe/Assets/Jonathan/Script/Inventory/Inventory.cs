using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int allslots;
    private int enabledSlots;
    private GameObject[] slots;
    private GameObject itemPickedUp;
    ItemTest items;
    CropTest cropTest;
    CropStateTest state;

    public GameObject slotHolder;

    public  bool canInteract = false;
    public bool canGetFish = false;
    public bool canGetCrops = false;

    // Start is called before the first frame update
    void Start()
    {
        allslots = 5;
        slots = new GameObject[allslots];

        for (int i = 0; i < allslots; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;

            if (slots[i].GetComponent<Slots>().item == null)
            {
                slots[i].GetComponent<Slots>().empty = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Crops" || other.tag == "Fish")
        {
            itemPickedUp = other.gameObject;
            items = itemPickedUp.GetComponent<ItemTest>();
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Crops" || other.tag == "Fish")
        {
            canInteract = false;
        }
    }

    private void Update()
    {
        if (canInteract)
        {
            if(canGetCrops)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    AddCropsItem(itemPickedUp, items.id, items.CropType, items.FishType, items.sprites[1]);
                    canGetCrops = false;
                }              
            }

            if (canGetFish)
            {
                AddCropsItem(itemPickedUp, items.id, items.CropType, items.FishType, items.sprites[0]);
                canGetFish = false;
            }
        }      
    }

    void AddCropsItem(GameObject itemObject, int itemID, CropsTypeTest crops,FishTypeTest fish, Sprite itemIcon)
    {
        for (int i = 0; i < allslots; i++)
        {
            if (slots[i].GetComponent<Slots>().empty)
            {
                //add item to slots
                itemObject.GetComponent<ItemTest>().pickUp = true;

                slots[i].GetComponent<Slots>().item = itemObject;
                slots[i].GetComponent<Slots>().id = itemID;
                slots[i].GetComponent<Slots>().itemIcon = itemIcon;
                slots[i].GetComponent<Slots>().CropType = crops;

                itemObject.transform.parent = slots[i].transform;
                itemObject.SetActive(false);

                slots[i].GetComponent<Slots>().UpdateSlot();
                slots[i].GetComponent<Slots>().empty = false;
            }
            else
            {
                continue;
            }
            return;
        }
    }
 }
