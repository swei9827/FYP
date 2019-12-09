using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    GameObject inventoryPanel;
    GameObject inventoryIcon;
    GameObject slotPanel;
    ItemDatabase database;
    public ItemDatabase databaseRef;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    private GameObject tooltip;

    private int slotAmount;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    private RaycastHit raycastHit;

    private void Awake()
    {
        inventoryPanel = GameObject.Find("Inventory Panel");
        tooltip = GameObject.Find("Tooltip");
        inventoryIcon = GameObject.Find("Inventory Icon");
    }
    void Start()
    {
        database = GetComponent<ItemDatabase>();   //Reference to the database 
        databaseRef = GetComponent<ItemDatabase>();   //Reference to the database 
        Debug.Log("Construct Itemdatabase");
        slotAmount = 24;  //Inventory size        
        inventoryPanel.SetActive(false);
        tooltip.SetActive(false);

        slotPanel = inventoryPanel.transform.GetChild(0).gameObject;
        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<Slot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform, false);
        }
    }

    private void Update()
    {
        popInventory();
    }

    void popInventory()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (inventoryIcon == EventSystem.current.currentSelectedGameObject)
            {                
                if (inventoryPanel.activeSelf)
                {
                    inventoryPanel.SetActive(false);
                    PlayerMovement.canMove = true;
                    tooltip.SetActive(false);

                }
                else
                {
                    inventoryPanel.SetActive(true);
                    PlayerMovement.canMove = false;
                }
            }
        }     
    }

    //Add Item by item's id in item database json file
    public void AddItem(int id)
    {
        //get data&variable of item from database
        Item itemToAdd = database.FetchItemById(id);

        if(itemToAdd.stackable && CheckIfItemIsInInventory(itemToAdd))
        {
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i].id == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();

                    // write to cloud
                    GameObject.FindGameObjectWithTag("Player").GetComponent<CloudData>().writeToCloud(items[i].id.ToString(), 1);
                    Debug.Log("ItemToCloud : " + items[i].itemName);
                    break;
                }
            }
        }
        else
        {
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i].id == -1)
                {
                    items[i] = itemToAdd;
                    //instantiate canvas GO and change sprite
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().slot = i;
                    itemObj.transform.SetParent(slots[i].transform,false);
                    itemObj.GetComponent<Image>().sprite = itemToAdd.sprite;
                    itemObj.name = itemToAdd.itemName;

                    // write to cloud
                    GameObject.FindGameObjectWithTag("Player").GetComponent<CloudData>().writeToCloud(items[i].id.ToString(), 1);
                    Debug.Log("ItemToCloud : " + items[i].itemName);
                    break;
                }
            }
        }        
    }

    public void AddItemFromCloud(int id, int amount)
    {
        Item itemToAdd = database.FetchItemById(id);

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == -1 && amount > 0)
            {
                items[i] = itemToAdd;
                //instantiate canvas GO and change sprite
                GameObject itemObj = Instantiate(inventoryItem);
                itemObj.GetComponent<ItemData>().item = itemToAdd;
                itemObj.GetComponent<ItemData>().slot = i;
                itemObj.transform.SetParent(slots[i].transform, false);
                itemObj.GetComponent<Image>().sprite = itemToAdd.sprite;
                itemObj.name = itemToAdd.itemName;
                if (items[i].id == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount = amount;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                }

                Debug.Log("ItemFromCloud : " + items[i].itemName);
                break;
            }
        }
    }

    // remove item, still working, might have errors
    public int RemoveItem(int remove)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == remove)
            {
                ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                if (data.amount > 1)
                {
                    data.amount--;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();

                    //write to cloud
                    GameObject.FindGameObjectWithTag("Player").GetComponent<CloudData>().writeToCloud(items[i].id.ToString(), -1);
                    break;
                }
                else
                {
                    items[i].id = -1;
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                }
            }
        }
        return 0;
    }

    public int RemoveItem(int remove, int amount)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == remove)
            {
                ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                if (data.amount > amount)
                {
                    data.amount-= amount;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();

                    //write to cloud
                    GameObject.FindGameObjectWithTag("Player").GetComponent<CloudData>().writeToCloud(items[i].id.ToString(), -amount);
                    return data.amount;
                    //break;
                }
                else if(data.amount == amount)
                {
                    items[i] = new Item();
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    //write to cloud
                    GameObject.FindGameObjectWithTag("Player").GetComponent<CloudData>().writeToCloud(remove.ToString(), -amount);
                    return 0;
                }
                else
                {
                    Debug.Log("Over remove");
                    //items[i].id = -1;
                    //Destroy(slots[i].transform.GetChild(0).gameObject);
                }
            }
        }
        return 0;
    }

    //check is there a same item in inven
    bool CheckIfItemIsInInventory(Item item)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if(items[i].id == item.id)
            {
                return true;
            }
        }
        return false;
    }
}
