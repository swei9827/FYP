using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopRenderer : MonoBehaviour
{
    public Image itemSprite;
    public Text itemName;
    public Text itemPrice;
    public Button itemAdd;
    public Button itemRemove;
    public Text itemCount;

    public void Initialize(Sprite img, string name, int price, Button add, Button remove, int count)
    {
        itemSprite.sprite = img;
        itemName.text = name;
        itemPrice.text = price.ToString();
        itemAdd = add;
        itemRemove = remove;
        itemCount.text = count.ToString();
    }
}
