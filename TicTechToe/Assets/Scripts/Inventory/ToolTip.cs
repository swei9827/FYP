using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public static ToolTip instance;

    public Text itemText;
    public Text descriptionText;
    private Image toolTip;
    private bool isHovering;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        toolTip = GetComponent<Image>();
        toolTip.enabled = false;
        offset = new Vector3(0, -100, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isHovering)
        {
            transform.position = Input.mousePosition + offset;
        }
    }

    public void setToolTip(string itemName, string description)
    {
        if(itemName.Length > 0)
        {
            isHovering = true;
            itemText.text = itemName;
            descriptionText.text = description;
            toolTip.enabled = true;
        }
        else
        {
            toolTip.enabled = false;
            itemText.text = string.Empty;
            descriptionText.text = description;
            isHovering = false;
        }
    }

}
