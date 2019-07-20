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

    public static bool canInteract = false;

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
        if (other.tag == "Crops")
        {
            Debug.Log("Enter");
            itemPickedUp = other.gameObject;
            items = itemPickedUp.GetComponent<ItemTest>();         
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddItem(itemPickedUp, items.id, items.FishType, items.CropType, items.sprites[1]);
        }
    }


    void AddItem(GameObject itemObject, int itemID, FishTypeTest fish, CropsTypeTest crops, Sprite itemIcon)
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
                slots[i].GetComponent<Slots>().FishType = fish;
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
