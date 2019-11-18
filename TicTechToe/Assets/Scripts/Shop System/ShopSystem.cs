using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    int moneyAmount;
    int finalCount;

    public GameObject shopSlot;
    public GameObject contentListPanel;

    public Text currentMoney;
    public List<Image> itemIcon;
    public List<Text> itemPrice;
    public List<Text> itemCount;
    public List<Button> buyButton;

    //public List<Item> itemObject = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    public List<ShopItem> shopItemList;

    int slotAmount;

    void Start()
    {
        //moneyAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getMoney();
        slotAmount = 4;
        InstantiateShop();
    }

    //private void OnEnable()
    //{
    //    moneyAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getMoney();
    //}

    //private void OnDisable()
    //{
    //    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().setMoney(moneyAmount);
    //}

    void Update()
    {
        //currentMoney.text = moneyAmount.ToString();
    }

    void InstantiateShop()
    {
        for(int i = 0; i < slotAmount; i++)
        {
            //itemObject.Add(new Item());
            slots.Add(Instantiate(shopSlot));
            slots[i].transform.SetParent(contentListPanel.transform,false);
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
        {
            Debug.Log("Not Enough Money");
        }
    }

    public void buyItem(int count, int cost, int slot)
    {
        List<GameObject> itemSlot;
        //int finalCost = itemSlot[slot].count * itemSlot[slot].cost;

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
}
