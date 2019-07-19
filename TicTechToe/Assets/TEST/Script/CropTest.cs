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

    void Start()
    {
        cropState = CropStateTest.Seed;
        growPercentage = 0;
        sr = GetComponent<SpriteRenderer>();
    }
 
    void Update()
    {
        CropStateChange();
        UpdateSprite();
    }
   
    void UpdateSprite()
    {
        if(cropState == CropStateTest.Seed)
        {
            sr.sprite = sprites[0];
        }
        if (cropState == CropStateTest.Done)
        {
            sr.sprite = sprites[1];
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
            if (watered)
            {
                cropState = CropStateTest.Planted;               
            }
        }

       if(growPercentage % waterRate != 0) {
            watered = false;
        }

       if(growPercentage == 100)
        {
            cropState = CropStateTest.Done;
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
