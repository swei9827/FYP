using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    int moneyAmount;

    public Text currentMoney;
    public List<Image> itemIcon;
    public List<Text> itemPrice;
    public List<Text> itemCount;
    public List<Button> buyButton;
    public List<Item> itemObject;

    void Start()
    {
        moneyAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getMoney();
    }

    private void OnEnable()
    {
        moneyAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getMoney();
    }

    private void OnDisable()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().setMoney(moneyAmount);
    }

    void Update()
    {
        currentMoney.text = moneyAmount.ToString();
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
}
