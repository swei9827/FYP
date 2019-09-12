using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    int moneyAmount;

    public Text currentMoney;
    public Image[] itemIcon;
    public Text[] itemPrice;
    public Button[] buyButton;
    public Item[] itemObject;

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

    void buyItem()
    {
        moneyAmount -= 5;
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().AddCropsItem(itemObject, CropsTypeTest,)
    }

    void buyItem(int item, int cost)
    {
        if(moneyAmount>=cost)
        {
            moneyAmount -= cost;
        }
        else
        {
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().AddCropsItem(itemObject, CropsTypeTest,)
        }        
    }

    void closeShop()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().setMoney(moneyAmount);
    }
}
