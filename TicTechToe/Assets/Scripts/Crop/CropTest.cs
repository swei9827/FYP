using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTest : ItemTest
{
    [Header("Crops Settings")]
    public SpriteRenderer sr;
    public bool planted;
    public bool watered;
    public float growthRate;
    public float waterRate;
    public Inventory inventory;
    public ItemDatabase itemDatabase;
    [SerializeField]
    CropStateTest cropState;
    [SerializeField]
    float growPercentage;
    float duration;

    [Header("Other Settings")]
    public GameObject waterIndicator;
    public Tool tool;
    bool tempCanInteract;
    public bool goWater;
    public bool canInteract;

    public DirtTile dirtTile;

    NPCInteraction ni;

    private GameObject temp;

    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        itemDatabase = GameObject.Find("Inventory").GetComponent<ItemDatabase>();
    }
    void Start()
    {
        ni = GameObject.FindGameObjectWithTag("Player").GetComponent<NPCInteraction>();
        waterIndicator.SetActive(false);
        cropState = CropStateTest.Seed;
        growPercentage = 0;
        sr = GetComponent<SpriteRenderer>();
        canInteract = false;
    }
 
    void Update()
    {
        CropStateChange();
        UpdateSprite();
        WaterCrops();
        Harvest();
    }
   
    void UpdateSprite()
    {
        if(cropState == CropStateTest.Seed || cropState == CropStateTest.Planted)
        {
            sr.sprite = sprites[0];
        }
        if (cropState == CropStateTest.Done)
        {
            sr.sprite = sprites[1];
        }
    }

    void WaterCrops()
    {
        if (canInteract && cropState == CropStateTest.Delayed)
        {
            if ((Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Space)) && PlayerInteraction.canUse)
            {
                WaterCan.curFill -= 1;
                waterIndicator.SetActive(false);
                Debug.Log("Interact");
                DataRecord.AddEvents(4, this.name.ToString());
                FxManager.PlayMusic("WaterFx");
                watered = true;
                canInteract = false;
            }              
        }   
    }

    void CropStateChange()
    {
       if(planted && cropState == CropStateTest.Seed)
        {
            cropState = CropStateTest.Planted;
            planted = false;
        }

       if(cropState == CropStateTest.Planted)
        {
            duration += Time.deltaTime;
            if(duration >= 1)
            {
                growPercentage += growthRate;
                duration = 0;
            }
        }

       if(cropState == CropStateTest.Delayed)
        {
            duration += Time.deltaTime;
            if (duration >= 1)
            {
                growPercentage += 0;
                duration = 0;
            }
        }

       if(growPercentage != 0 && growPercentage != 100 && growPercentage % waterRate == 0)
        {
            cropState = CropStateTest.Delayed;
            waterIndicator.SetActive(true);
            if (watered)
            {
                cropState = CropStateTest.Planted;
                waterIndicator.SetActive(false);
            }
        }

       if(growPercentage % waterRate != 0)
        {
            watered = false;
        }

       if(growPercentage == 100)
        {
            cropState = CropStateTest.Done;
        }      
    }

    void Harvest()
    {
        if (canInteract && cropState == CropStateTest.Done)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Space))
            {
                foreach (Item item in itemDatabase.database)
                {
                    if ((item.itemName + "(Clone)") == this.gameObject.name)
                    {
                        ni.questItemCheck(item);
                        inventory.AddItem(item.id);
                        Destroy(this.gameObject);
                        break;
                    }
                }               
            }
            canInteract = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (cropState == CropStateTest.Delayed)
            {
                canInteract = true;
            }

            else if(cropState == CropStateTest.Done)
            {               
                canInteract = true;                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (cropState == CropStateTest.Delayed)
            {
                canInteract = false;
            }

            else if (cropState == CropStateTest.Done)
            {
                canInteract = false;
            }
        }
    }
}

public enum CropStateTest
{
    Seed,
    Planted,
    Delayed,
    Done
}
