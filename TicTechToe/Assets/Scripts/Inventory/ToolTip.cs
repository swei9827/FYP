using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private Item item;
    private string data;
    private GameObject tooltip;

    private void Awake()
    {
        tooltip = GameObject.Find("Tooltip");
    }

    void Start()
    {        
        tooltip.SetActive(false);
    }

    private void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(Item item)
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString()
    {
        data = "<color=#FFFFFF><b>" + item.itemName + "</b></color>\n\n" + "<color=#FBFF84>" + item.itemDescription + "</color>";
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }
}
