using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ShopSystem : MonoBehaviour
{
    int moneyAmount;
    public int newID;
    public Text currentMoney;
    public GameObject shopItemTemplate;
    public Transform contentPanel;
    private List<GameObject> shopItemRenderers = new List<GameObject>();
    //public List<Image> itemIcon;
    //public List<Text> itemPrice;
    //public List<Text> itemCount;
    //public List<Button> buyButton;
    //public List<Item> itemObject;

    void Start()
    {
        moneyAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getMoney();
    }

    private void OnEnable()
    {
        moneyAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getMoney();
    }

    //private void OnDisable()
    //{
    //    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().setMoney(moneyAmount);
    //}

    void Update()
    {
        currentMoney.text = moneyAmount.ToString();
    }

    public void RendererShop()
    {
        //purge renderers
        foreach (GameObject go in shopItemRenderers)
        {
            Destroy(go);
        }

        shopItemRenderers = new List<GameObject>();

        //load entries
        ShopItemList data = this.LoadItemList();
        shopItemTemplate.SetActive(false);

        data.shopList.Sort();

        //render new entries
        for (int i = 0; i < data.shopList.Count; i++)
        {
            GameObject newEntry = Instantiate(shopItemTemplate, contentPanel);
            //newEntry.GetComponent<ShopRenderer>().Initialize(
            //    i + 1,
            //    data.shopList[i].id
            //    );
            //newEntry.SetActive(true);

            if (i % 2 == 0)
            {
                newEntry.GetComponent<Image>().color = Color.grey;
            }
        }
    }

    public void buyItem(int itemN)
    {
        //int finalCost = count * cost;

        //if (moneyAmount >= finalCost)
        //{
        //    moneyAmount -= finalCost;
        //    //GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().AddCropsItem(itemObject, CropsTypeTest,)
        //}
        //else
        //{
        //    Debug.Log("Not Enough Money");
        //}
    }

    public void buyItem(int count, int cost, int slot)
    {
        List<GameObject> itemSlot;
        //int finalCost = itemSlot[slot].count * itemSlot[slot].cost;

        //int 

        //if (moneyAmount >= finalCost)
        //{
        //    moneyAmount -= finalCost;
        //    //GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().AddCropsItem(itemObject, CropsTypeTest,)
        //}
        //else
        //{
        //    Debug.Log("Not Enough Money");
        //}
    }

    public void closeShop()
    {
        moneyAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getMoney();
    }

    public void saveEntry()
    {
        Debug.Log("Saving Shop Item");
        ShopItem shopItem = new ShopItem(this.newID);

        ShopItemList loadedData = this.LoadItemList();
        bool newShopItem = true;
        for (int i = 0; i < loadedData.shopList.Count; i++)
        {
            if (loadedData.shopList[i].id == this.newID)
            {
                newShopItem = false;
                //if (this.newEntryScore > loadedData.entries[i].score)
                //{
                //    loadedData.entries[i] = new Entry(this.newEntryName, this.newEntryScore);
                //}
            }
        }

        if (newShopItem == true)
        {
            loadedData.shopList.Add(shopItem);
        }
        //convert our shop item struct into a JSON string format
        string data = JsonUtility.ToJson(loadedData, true);
        Debug.Log(data);

        // write and save our JSON string to text file in a specific folder
        string path = Path.Combine(Application.streamingAssetsPath, "ShopItem");

        File.WriteAllText(path, data);

        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    [ContextMenu("Load Entry")]
    public ShopItemList LoadItemList()
    {
        Debug.Log("Loading Item List");
        ShopItemList newList = new ShopItemList();
        newList.shopList = new List<ShopItem>();

        //Get path
        string path = Path.Combine(Application.streamingAssetsPath, "ShopItem");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            newList = JsonUtility.FromJson<ShopItemList>(json);
        }
        return newList;
    }
}
