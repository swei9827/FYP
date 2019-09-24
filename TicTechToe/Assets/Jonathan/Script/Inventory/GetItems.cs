using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItems : MonoBehaviour
{
    [SerializeField] GameObject _itemPrefab;

    public bool canInteract = false;
    public bool canGetFish = false;
    public bool canGetCrops = false;

    //for tutorial use
    public bool getCropsTutorial = false;
    public bool getFishTutorial = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canInteract = false;
        }
    }

    public void Update()
    {
        if (canInteract)
        {
            if (canGetCrops)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject crops = GameObject.Instantiate(_itemPrefab);
                    if (HotBar.HotBarInstance.AddItem(crops))
                    {
                        GameObject.Destroy(this.gameObject);
                        GameObject.FindGameObjectWithTag("GameController").GetComponent<DataRecord>().AddEvents(0, crops.GetComponentInChildren<Item>().itemName.ToString());
                        canGetFish = false;
                        getCropsTutorial = true;
                    }
                    else if (!HotBar.HotBarInstance.AddItem(crops))
                    {
                        //InventoryController.InventoryInstance.AddItem(crops);
                        GameObject.Destroy(this.gameObject);
                        GameObject.FindGameObjectWithTag("GameController").GetComponent<DataRecord>().AddEvents(0, crops.GetComponentInChildren<Item>().itemName.ToString());
                        canGetFish = false;
                    }
                    else
                    {
                        Debug.Log("Inventory is full");
                    }
                }           
            }

            if (canGetFish)
            {
                GameObject fish = GameObject.Instantiate(_itemPrefab);
                if (HotBar.HotBarInstance.AddItem(fish))
                {
                    GameObject.Destroy(this.gameObject);
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<DataRecord>().AddEvents(0, fish.GetComponentInChildren<Item>().itemName.ToString());
                    canGetFish = false;
                    getFishTutorial = true;
                }
                else if(!HotBar.HotBarInstance.AddItem(fish))
                {
                    //InventoryController.InventoryInstance.AddItem(fish);
                    GameObject.Destroy(this.gameObject);
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<DataRecord>().AddEvents(0, fish.GetComponentInChildren<Item>().itemName.ToString());
                    canGetFish = false;
                }
                else
                {
                    Debug.Log("Inventory is full");
                }
            }
        }
    }
}



//previous method
//if (InventoryController.InventoryInstance.AddItem(fish))
//{
//    GameObject.Destroy(this.gameObject);
//    GameObject.FindGameObjectWithTag("GameController").GetComponent<DataRecord>().AddEvents(0, fish.ToString());
//    canGetFish = false;
//}
//else
//{
//    Debug.Log("Inventory is full");
//}
