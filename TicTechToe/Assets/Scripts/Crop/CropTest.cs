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
    bool goWater;
    public bool canInteract;

    public DirtTile dirtTile;

    private GameObject temp;

    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        itemDatabase = GameObject.Find("Inventory").GetComponent<ItemDatabase>();
    }
    void Start()
    {
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
        if (goWater)
        {
            if ((Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Space)) && PlayerInteraction.canUse && PlayerInteraction.canWater )
            {
                WaterCan.curFill -= 1;
                waterIndicator.SetActive(false);
                Debug.Log("Interact");
                DataRecord.AddEvents(4, this.name.ToString());
                FxManager.PlayMusic("WaterFx");
                watered = true;
                goWater = false;
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
            //GameObject.FindGameObjectWithTag("Crops").GetComponent<GetItems>().canGetCrops = true;
        }      
    }

    void Harvest()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("harvest");
                foreach (Item item in itemDatabase.database)
                {
                    Debug.Log(item.itemName);
                    if ((item.itemName + "(Clone)") == this.gameObject.name)
                    {                       
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
                goWater = true;
            }

            else if(cropState == CropStateTest.Done)
            {               
                canInteract = true;

                Debug.Log("damn");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (cropState == CropStateTest.Delayed)
            {
                goWater = false;
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
