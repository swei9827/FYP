using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int quantity = 1;
    public string itemName;
    public string itemDescription;

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

    void setToolTip(string name, string description)
    {
        InventoryController.InventoryInstance.showToolTip(name,description);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        setToolTip(itemName,itemDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        setToolTip(string.Empty,string.Empty);
    }
}

