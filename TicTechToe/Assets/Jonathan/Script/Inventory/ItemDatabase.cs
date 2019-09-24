using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using LitJson;
using System.Linq;
using Mirror;

public class ItemDatabase : NetworkBehaviour
{
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    private void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText((Application.dataPath + "/StreamingAssets/Items.json")).Trim());
        ConstructItemDatabase();
    }

    public Item FetchItemById(int id)
    {
        return database.First(item => item.id == id);
    }

    void ConstructItemDatabase()
    {
        for(int i = 0; i<itemData.Count; i++)
        {
            database.Add(new Item(
                (int)itemData[i]["id"],
                itemData[i]["itemName"].ToString(),
                (bool)itemData[i]["stackable"],
                (bool)itemData[i]["marketable"],
                (int)itemData[i]["price"],
                itemData[i]["itemDescription"].ToString()));
        }
    }
}

public class Item
{
    public int id;
    public string itemName;
    public bool stackable;
    public bool marketable;
    public int price;
    public string itemDescription;
    public Sprite sprite;

    public Item(int id, string name, bool stack, bool market, int price, string desc)
    {
        this.id = id;
        this.itemName = name;
        this.stackable = stack;
        this.marketable = market;
        this.price = price;
        this.itemDescription = desc;
        this.sprite = Resources.Load<Sprite>("Sprites/Items/" + name);
    }

    public Item()
    {
        this.id = -1;
    }
}

