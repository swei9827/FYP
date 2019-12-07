using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sell : MonoBehaviour
{
    public GameObject itemGO;

    public List<GameObject> content;
    Inventory inventory;

    void Start()
    {        
        inventory = Player.LocalPlayerInstance.transform.GetChild(1).GetComponent<Inventory>();
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {   
        if(content.Capacity >= 0)
        {
            foreach (GameObject go in content)
            {
                Destroy(go);                
            }
        }
        content.Clear();
        Debug.Log("Clear List");

        for(int i = 0; i< inventory.items.Capacity; i++)
        {
            if (inventory.items[i].id >= 0)
            {
                GameObject go = Instantiate(itemGO);
                ShopRenderer shopRdr = go.GetComponent<ShopRenderer>();
                shopRdr.Initialize(
                    /*sprite*/ inventory.items[i].sprite,
                    /*name*/ inventory.items[i].itemName,
                    /*name*/ inventory.items[i].price,
                    /*starting count*/1);

                //set the max to the amount of item in your inventory so you can't over sell your items 
                shopRdr.itemCount.gameObject.GetComponent<UpdatableInt>().max = inventory.slots[i].transform.GetChild(0).GetComponent<ItemData>().amount;
                go.transform.SetParent(this.gameObject.transform.GetChild(0).transform, false);
                content.Add(go);
                Debug.Log("Create Item");
            }
        }
    }
}
