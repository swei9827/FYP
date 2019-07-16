using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Image items;
    public GameObject Icons;

    Inventory inventory;

    void Start()
    {
        //inventory = Inventory.instance;
        //inventory.onitemChangedCallback += UpdateUI;
    }

   void Update()
    {
        
    }

    void UpdateUI()
    {
        Debug.Log("Updating UI");
    }
}
