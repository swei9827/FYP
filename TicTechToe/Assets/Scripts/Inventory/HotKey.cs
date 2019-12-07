using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HotKey : MonoBehaviour
{
    private HelperController helper;

    [Header("Button Settings")]
    public Button[] slots;
    public int scrollPosition;

    bool canSelect = true;

    [SerializeField]
    private GameObject SeedBar;

    [Header("Sprite Settings")]
    public Sprite disableSprite;
    public Sprite pressSprite;

    public Sprite[] seedPressSprite;
    public Sprite[] seedDisableSprite;

    [Header("Tools Settings")]
    //Tool assets
    public Tool tool;

    private PlayerInteraction player;

    public Button[] button;

    [SerializeField]
    private Crop crop;

    public IconBox iconBox;
    public Image waterBar;

    public static bool canUse = false;
    public static bool canWater = false;

    bool isClick = false;
    bool plowClick = false;
    bool waterClick = false;
    bool fishingRodClick = false;
    bool seedClick = false;

    // Start is called before the first frame update
    void Start()
    {
        player= FindObjectOfType<PlayerInteraction>();
        tool = this.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Tool>();
        //tool = this.gameObject.GetComponent<Tool>();
        waterBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SetItem();
        CheckWaterStatus();
    }

    public void ResetToogle()
    {
        tool.isPlow = false;
        tool.isWaterCan = false;
        tool.isFishingRod = false;
        tool.isSeed = false;      
        foreach (Seed s in tool.seeds)
        {
            s.isSelected = false;
        }
        SeedBar.SetActive(false);
        waterBar.gameObject.SetActive(false);
        canUse = false;
    }

    public void SetItem()
    {
        //====================== MouseScroll ==============================//

        if (Input.mouseScrollDelta.y >= 1)
        {
            scrollPosition--;
            ResetToogle();
            EventSystem.current.SetSelectedGameObject(null);
            if (scrollPosition <= -1)
            {
                scrollPosition = -1;
            }
        }

        if (Input.mouseScrollDelta.y <= -1)
        {
            scrollPosition++;
            ResetToogle();
            EventSystem.current.SetSelectedGameObject(null);
            if (scrollPosition >= 3)
            {
                scrollPosition = 3;
            }                  
        }

        //===================== MouseClick =============================//
        if (slots[0].gameObject == EventSystem.current.currentSelectedGameObject)
        {            
            plowClick = !plowClick;

            scrollPosition = 0;
            ResetToogle();

            EventSystem.current.SetSelectedGameObject(null);
        }

        else if (slots[1].gameObject == EventSystem.current.currentSelectedGameObject)
        {
            waterClick = !waterClick;

            scrollPosition = 1;
            ResetToogle();

            EventSystem.current.SetSelectedGameObject(null);          
        }

        else if (slots[2].gameObject == EventSystem.current.currentSelectedGameObject)
        {
            fishingRodClick = !fishingRodClick;

            scrollPosition = 2;
            ResetToogle();
            EventSystem.current.SetSelectedGameObject(null);
        }

        else if (slots[3].gameObject == EventSystem.current.currentSelectedGameObject)
        {
            seedClick = !seedClick;

            scrollPosition = 3;
            ResetToogle();
            EventSystem.current.SetSelectedGameObject(null);
        }

        if (!plowClick && !waterClick && !fishingRodClick && !seedClick && !isClick)
        {
            scrollPosition = -1;
            ResetToogle();
        }

        if (scrollPosition == -1)
        {
            isClick = true;

            if(isClick)
            {
                iconBox.Close();
                ResetToogle();
            }
          
            plowClick = false;
            waterClick = false;
            fishingRodClick = false;
            seedClick = false;
        }

        if (scrollPosition == 0)
        {
            plowClick = true;

            if(plowClick)
            {
                tool.isPlow = true;
                iconBox.SetIcon(tool.sprite[1]);
            }

            isClick = false;
            waterClick = false;
            fishingRodClick = false;
            seedClick = false;
        }

        if (scrollPosition == 1)
        {
            waterClick = true;

            if(waterClick)
            {
                tool.isWaterCan = true;
                iconBox.SetIcon(tool.sprite[2]);
            }

            isClick = false;
            plowClick = false;
            fishingRodClick = false;
            seedClick = false;
        }

        if (scrollPosition == 2)
        {
            fishingRodClick = true;

            if(fishingRodClick)
            {
                tool.isFishingRod = true;
                iconBox.SetIcon(tool.sprite[3]);
            }

            isClick = false;
            plowClick = false;
            waterClick = false;
            seedClick = false;
        }

        if (scrollPosition == 3)
        {
            seedClick = true;

            if(seedClick)
            {
                tool.isSeed = true;
                SeedBar.SetActive(true);
            }

            isClick = false;
            plowClick = false;
            waterClick = false;
            fishingRodClick = false;         
        }

        if (button[0].gameObject == EventSystem.current.currentSelectedGameObject)
        {
            tool.seeds[0].isSelected = true;
            tool.seeds[1].isSelected = false;
            tool.seeds[2].isSelected = false;

            crop = tool.seeds[0].crop;
            iconBox.SetIcon(crop.asset.seedSprite);
            seedSelectButton();
        }
        else if (button[1].gameObject == EventSystem.current.currentSelectedGameObject)
        {
            tool.seeds[0].isSelected = false;
            tool.seeds[1].isSelected = true;
            tool.seeds[2].isSelected = false;

            crop = tool.seeds[1].crop;
            iconBox.SetIcon(crop.asset.seedSprite);
            seedSelectButton();
        }
        else if (button[2].gameObject == EventSystem.current.currentSelectedGameObject)
        {
            tool.seeds[0].isSelected = false;
            tool.seeds[1].isSelected = false;
            tool.seeds[2].isSelected = true;

            crop = tool.seeds[2].crop;
            iconBox.SetIcon(crop.asset.seedSprite);
            seedSelectButton();
        }

        SelectButton();
    }

    void CheckWaterStatus()
    {
        if(tool.isWaterCan)
        {
            canUse = true;
            waterBar.gameObject.SetActive(true);
        }
        else
        {
            canUse = false;
            waterBar.gameObject.SetActive(false);
        }

        if (WaterCan.curFill <= 0)
        {
            canWater = false;
        }
        else
        {
            canWater = true;
        }
    }

    void SelectButton()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].name == "Slots (" + scrollPosition + ")")
            {
                slots[i].GetComponent<Button>().image.sprite = pressSprite;
            }
            else
            {
                slots[i].GetComponent<Button>().image.sprite = disableSprite;
            }    
        } 
    }

    void seedSelectButton()
    {
        for (int i = 0; i < button.Length; i++)
        {
            if (button[i].gameObject == EventSystem.current.currentSelectedGameObject)
            {
                button[i].gameObject.GetComponent<Button>().image.sprite = seedPressSprite[i];
                slots[3].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = crop.asset.seedSprite;
            }
            else
            {
                button[i].gameObject.GetComponent<Button>().image.sprite = seedDisableSprite[i];
            }
        }
    }
}
