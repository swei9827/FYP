using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Trading : MonoBehaviourPunCallbacks
{
    public GameObject tradingUI;

    [SerializeField]
    private Image playerBox;
    [SerializeField]
    private Image oppositeBox;

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

    [PunRPC]
    void Trade(Item add)
    {
        //delete the item you put
        Player.LocalPlayerInstance.GetComponent<Inventory>().AddItem(add.id);
    }
}
