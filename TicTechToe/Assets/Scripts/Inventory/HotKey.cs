using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HotKey : MonoBehaviour
{
    [Header("Button Settings")]
    public Transform slots;
    public int scrollPosition;

    bool isClick = false;
    bool canSelect = true;

    [Header("Sprite Settings")]
    public Sprite disableSprite;
    public Sprite pressSprite;

    [Header("Tools Settings")]
    //Tool assets
    public Tool tool;


    private PlayerInteraction player;

    // Start is called before the first frame update
    void Start()
    {
        player= FindObjectOfType<PlayerInteraction>();
        tool = player.GetComponent<Tool>();
    }

    // Update is called once per frame
    void Update()
    {
        SetItem();
    }

    public void ResetToogle()
    {
        tool.isPlow = false;
        tool.isWaterCan = false;
        tool.isFishingRod = false;
        tool.isSeed = false;
    }

    public void SetItem()
    {
        //=================== Keyboard =========================//
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            scrollPosition = 0;
            ResetToogle();
            tool.isPlow = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            scrollPosition = 1;
            ResetToogle();
            tool.isWaterCan = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            scrollPosition = 2;
            ResetToogle();
            tool.isFishingRod = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            scrollPosition = 3;
            ResetToogle();
            tool.isSeed = true;
        }

        //====================== MouseScroll ==============================//

        if (Input.mouseScrollDelta.y >= 1)
        {
            scrollPosition++;
            if (scrollPosition >= 3)
            {
                scrollPosition = 3;
            }
        }

        if (Input.mouseScrollDelta.y <= -1)
        {
            scrollPosition--;
            if (scrollPosition <= 0)
            {
                scrollPosition = 0;
            }          
        }

        Debug.Log(scrollPosition);
        //select item
        SelectButton();
    }

    void SelectButton()
    {
        if(slots.name == "Slots (" + scrollPosition + ")")
        {
            slots.GetComponent<Button>().image.sprite = pressSprite;
        }
        else
        {
            slots.GetComponent<Button>().image.sprite = disableSprite;
        }
    }
}
