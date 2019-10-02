﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Inventory : MonoBehaviour
{
    GameObject inventoryPanel;
    GameObject slotPanel;
    ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    private GameObject tooltip;

    private int slotAmount;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    private void Awake()
    {
        inventoryPanel = GameObject.Find("Inventory Panel");
        tooltip = GameObject.Find("Tooltip");
    }
    void Start()
    {
        database = GetComponent<ItemDatabase>();   //Reference to the database 
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
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryPanel.activeSelf)
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
                    break;
                }
            }
        }        
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
