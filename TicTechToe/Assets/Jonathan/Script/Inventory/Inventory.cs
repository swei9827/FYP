using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    private int allslots;
    private int enabledSlots;
    private GameObject[] slots;
    private GameObject itemPickedUp;
    public GameObject dialogue;
    ItemTest items;
    CropTest cropTest;
    CropStateTest state;

    public GameObject slotHolder;

    public bool canInteract = false;
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

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Crops" || other.tag == "Fish")
        {
            canInteract = false;
        }
    }

    private void Update()
    {       
        if(canInteract)
        {
            if (canGetCrops)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    AddCropsItem(itemPickedUp, items.CropType, items.sprites[1], 0);
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<DataRecord>().AddEvents(5, items.CropType.ToString());
                    FxManager.PlayMusic("HarvestFx");
                    canGetCrops = false;
                    DirtTile.addPlant = true;
                }
            }

            if (canGetFish)
            {
                AddFishItem(itemPickedUp, items.FishType, items.sprites[0], 1);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<DataRecord>().AddEvents(0, items.FishType.ToString());
                canGetFish = false;
            }
        }       
    }

    void AddCropsItem(GameObject itemObject, CropsTypeTest crops, Sprite itemIcon, int id)
    {
        if(dialogue.GetComponent<Dialogue>().getIndex() == 2 && !Dialogue.completeTask1)
        {
            dialogue.GetComponent<Dialogue>().setIndex(3);
            Dialogue.completeTask1 = true;
        }

        for (int i = 0; i < allslots; i++)
        {
            if (slots[i].GetComponent<Slots>().empty)
            {
                //add item to slots
                itemObject.GetComponent<ItemTest>().pickUp = true;

                //add item's component
                slots[i].GetComponent<Slots>().item = itemObject;
                slots[i].GetComponent<Slots>().itemIcon = itemIcon;
                slots[i].GetComponent<Slots>().CropType = crops;
                slots[i].GetComponent<Slots>().id = id;

                //add number held
                slots[i].GetComponent<Slots>().numberHeld += 1;

                itemObject.transform.parent = slots[i].transform;
                itemObject.SetActive(false);

                slots[i].GetComponent<Slots>().UpdateSlot();
                slots[i].GetComponent<Slots>().UpdateNumHeld();
                slots[i].GetComponent<Slots>().empty = false;                
            }

           else if(slots[i].GetComponent<Slots>().CropType == itemObject.GetComponent<ItemTest>().CropType )
            {
                itemObject.GetComponent<ItemTest>().pickUp = true;

                //add number held
                slots[i].GetComponent<Slots>().numberHeld += 1;

                itemObject.transform.parent = slots[i].transform;
                itemObject.SetActive(false);

                slots[i].GetComponent<Slots>().UpdateSlot();
                slots[i].GetComponent<Slots>().UpdateNumHeld();
            }    

            else
            {
                continue;
            }
            return;
        }     
    }

    void AddFishItem(GameObject itemObject, FishTypeTest fish, Sprite itemIcon,int id)
    {
        if (dialogue.GetComponent<Dialogue>().getIndex() == 2 && Dialogue.completeTask1)
        {
            dialogue.GetComponent<Dialogue>().setIndex(6);
            Dialogue.completeTask2 = true;
        }

        for (int i = 0; i < allslots; i++)
        {
            if (slots[i].GetComponent<Slots>().empty)
            {
                //add item to slots
                itemObject.GetComponent<ItemTest>().pickUp = true;

                //add item's component
                slots[i].GetComponent<Slots>().item = itemObject;
                slots[i].GetComponent<Slots>().itemIcon = itemIcon;
                slots[i].GetComponent<Slots>().FishType = fish;
                slots[i].GetComponent<Slots>().id = id;

                //add number held
                slots[i].GetComponent<Slots>().numberHeld += 1;

                itemObject.transform.parent = slots[i].transform;
                itemObject.SetActive(false);

                slots[i].GetComponent<Slots>().UpdateSlot();
                slots[i].GetComponent<Slots>().UpdateNumHeld();
                slots[i].GetComponent<Slots>().empty = false;
            }
            else if (slots[i].GetComponent<Slots>().FishType == itemObject.GetComponent<ItemTest>().FishType)
            {
                itemObject.GetComponent<ItemTest>().pickUp = true;

                //add number held
                slots[i].GetComponent<Slots>().numberHeld += 1;

                itemObject.transform.parent = slots[i].transform;
                itemObject.SetActive(false);

                slots[i].GetComponent<Slots>().UpdateSlot();
                slots[i].GetComponent<Slots>().UpdateNumHeld();          
            }
            else
            {
                continue;
            }
            return;
        }
    }
}
