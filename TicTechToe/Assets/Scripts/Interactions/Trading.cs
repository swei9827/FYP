using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Trading : MonoBehaviourPunCallbacks
{
    public GameObject tradingUI;

    [SerializeField]
    private Image playerBox; // items to trade show in the box
    [SerializeField]
    private Image oppositeBox; // items to trade show in the box

    public bool isTrading; //if player accept the trade offer, both player isTrading = true, if is Trading == true, open trading UI


    private void Start()
    {
        isTrading = false;
        tradingUI.SetActive(false);
    }

    [PunRPC]
    void ShowTradeItem(int i)
    {
        Sprite temp = null;
        //foreach (Item item in ItemDatabase.database)
        //{
        //    if (item.id == i)
        //    {
        //        temp = item.sprite;
        //    }
        //}
        //oppositeBox.sprite = temp;
    }

    void SelectItem()
    {

    }

    [PunRPC]
    void Trade(Item add)
    {
        //delete the item you put
        Player.LocalPlayerInstance.GetComponent<Inventory>().AddItem(add.id);
    }
}
