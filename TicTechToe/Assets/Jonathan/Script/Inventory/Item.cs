using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int quantity = 1;

    public CropsTypeTest CropType;
    public FishTypeTest FishType;

    public virtual void use()
    {

    }

    protected virtual void Start()
    {
        SetQuantityText();
    }

    public void SetQuantityText()
    {
        transform.Find("NumberHeld").GetComponent<Text>().text = quantity.ToString();
    }

    public void Reduce()
    {
        quantity--;
        SetQuantityText();
        if (quantity == 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void Add(int amount)
    {
        quantity += amount;
        SetQuantityText();

        //Add limitation here

    }

    public void OnDestroy()
    {
        InventoryController.InventoryInstance.RemoveItem(this);
    }

   
}

