using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell : MonoBehaviour
{
    public GameObject itemGO;

    List<GameObject> content;
    Inventory inventory;

    void Start()
    {
        inventory = Player.LocalPlayerInstance.transform.GetChild(1).GetComponent<Inventory>();
    }

    private void OnEnable()
    {   
        if(content != null)
        {
            if(content.Capacity > 0)
            {
                foreach (GameObject go in content)
                {
                    Destroy(go);
                }
            }            
        }
        
        foreach (Item item in inventory.items)
        {
            GameObject go = Instantiate(itemGO);
            go.GetComponent<ShopRenderer>().Initialize(
                /*sprite*/item.sprite,
                /*name*/item.itemName,
                /*name*/item.price,
                /*starting count*/1);
            content.Add(go);
        }
    }
}
