using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [SerializeField]
    private int moneyAmount;

    int finalCount;

    public GameObject shopSlot;
    public GameObject contentListPanel;

    public Text currentMoney;

    public List<Item> itemObject = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    public List<ShopItem> shopItemList;

    private int slotAmount;

    void Start()
    {
        moneyAmount = Player.LocalPlayerInstance.GetComponent<Player>().getMoney();
        slotAmount = shopItemList.Capacity;
        InstantiateShop();
    }

    private void onenable()
    {
        moneyAmount = Player.LocalPlayerInstance.GetComponent<Player>().getMoney();
    }

    private void OnDisable()
    {
        Player.LocalPlayerInstance.GetComponent<Player>().setMoney(moneyAmount);
    }

    void Update()
    {
        currentMoney.text = moneyAmount.ToString();
    }

    void InstantiateShop()
    {
        for(int i = 0; i < slotAmount; i++)
        {
            itemObject.Add(new Item());
            GameObject go = Instantiate(shopSlot);
            go.GetComponent<ShopRenderer>().Initialize(
                /*sprite*/shopItemList[i].itemSprite,
                /*name*/shopItemList[i].itemName,
                /*name*/shopItemList[i].itemPrice,
                /*starting count*/1);
            slots.Add(go);
            slots[i].transform.SetParent(contentListPanel.transform,false);
        }
    }

    public void buyItem(ShopRenderer shopRenderer)
    {
        int finalCost = int.Parse(shopRenderer.itemCount.text) * int.Parse(shopRenderer.itemPrice.text);

        if (moneyAmount >= finalCost)
        {
            moneyAmount -= finalCost;
            foreach(Seed sd in Player.LocalPlayerInstance.GetComponent<Tool>().seeds)
            {
                if (sd.seedName == shopRenderer.itemName.text)
                {
                    sd.amount += int.Parse(shopRenderer.itemCount.text);
                }
            }            
        }
        else
        {
            Debug.Log("Not Enough Money");
        }
    }

    public void sellItem(ShopRenderer shopRenderer)
    {
        int finalEarn = int.Parse(shopRenderer.itemCount.text) * int.Parse(shopRenderer.itemPrice.text);
        moneyAmount += finalEarn;
        Player.LocalPlayerInstance.GetComponent<Player>().inventory.RemoveItem(1);           
        
    }

    public void closeShop()
    {
        moneyAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getMoney();
    }
}
