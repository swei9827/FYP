using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/ Player Inventory")]
public class PlayerInventory : MonoBehaviour
{
    #region singleton
    public static PlayerInventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion
    public Item currentItem;
    public int inventorySpace = 5;
    public List<Item> items = new List<Item>();

    public void AddItem(Item itemToAdd)
    {
        if(!items.Contains(itemToAdd))
        {
            items.Add(itemToAdd);
        }
    }

    //public bool Add(Item item)
    //{
    //    if(myInventory.Count >= inventorySpace)
    //    {
    //        Debug.Log("Not enough space");
    //        return false;
    //    }
    //    myInventory.Add(item);

    //    return true;
    //}

    //public void Remove(Item item)
    //{
    //    myInventory.Remove(item);
    //}
}
