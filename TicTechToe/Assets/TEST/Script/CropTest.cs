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
    [SerializeField]
    CropStateTest cropState;
    [SerializeField]
    float growPercentage;
    float duration;

    [Header("Other Settings")]
    public GameObject waterIndicator;
    public Tool tool;
    bool canInteract;

    public DirtTile dirtTile;

    private GameObject temp;

    void Start()
    {
        waterIndicator.SetActive(false);
        cropState = CropStateTest.Seed;
        growPercentage = 0;
        sr = GetComponent<SpriteRenderer>();
        canInteract = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().canInteract;
        //temp = GameObject.FindGameObjectWithTag("Crops").GetComponent<DirtTile>().temp;
    }
 
    void Update()
    {
        CropStateChange();
        UpdateSprite();
        WaterCrops();
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
        if(canInteract)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                WaterCan.curFill -= 5;
                Debug.Log("Interact");
                waterIndicator.SetActive(false);
                watered = true;
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().canGetCrops = true;
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (cropState == CropStateTest.Delayed)
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
