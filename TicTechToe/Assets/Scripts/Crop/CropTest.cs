﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
    //[SerializeField]
    //CropStateTest cropState;

    public CropStateTest cropState;

    [SerializeField]
    float growPercentage;
    float duration;

    [Header("Other Settings")]
    public GameObject waterIndicator;
    public Tool tool;
    public bool goWater;
    public bool canInteract;

    NPCInteraction ni;
    Feedback feedback;

    private GameObject temp;

    GameObject player;

    TutorialManager tutorial;

    private void Awake()
    {
        inventory = Player.LocalPlayerInstance.GetComponent<Player>().inventory;
        itemDatabase = Player.LocalPlayerInstance.transform.GetChild(1).gameObject.GetComponent<ItemDatabase>();    
    }
    void Start()
    {
        ni = GameObject.FindGameObjectWithTag("Player").GetComponent<NPCInteraction>();
        feedback = GameObject.FindGameObjectWithTag("Player").GetComponent<Feedback>();
        waterIndicator.SetActive(false);
        cropState = CropStateTest.Seed;
        growPercentage = 0;
        sr = GetComponent<SpriteRenderer>();
        canInteract = false;
        player = GameObject.FindGameObjectWithTag("Player");

        if(!TutorialManager.doneTutorial)
        {
            tutorial = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
        }   
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
            if ((Input.GetKeyDown(KeyCode.Mouse0)) && HotKey.canUse && HotKey.canWater)
            {
                WaterCan.curFill -= 1;
                waterIndicator.SetActive(false);
                FxManager.PlayMusic("WaterFx");
                watered = true;
                canInteract = false;

                //For tutorial
                if(!TutorialManager.doneTutorial)
                {
                    tutorial.waterCount += 1;
                }
              

                //local data record
                DataRecord.AddEvents(4, this.gameObject.name);
            }              
        }   
    }

    void CropStateChange()
    {
       if(planted && cropState == CropStateTest.Seed)
        {
            cropState = CropStateTest.Planted;           
            //planted = false;

            //local data record
            DataRecord.AddEvents(3, this.gameObject.name);           
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

       if(growPercentage >= 100)
        {
            cropState = CropStateTest.Done;
        }      
    }

    void Harvest()
    {
        if (canInteract && cropState == CropStateTest.Done && !feedback.harvested)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                foreach (Item item in itemDatabase.database)
                {
                    if ((item.itemName + "(Clone)") == this.gameObject.name)
                    {
                        ni.questItemCheck(item);
                        inventory.AddItem(item.id);

                        // Quest item check
                        foreach (NPCManager.QuestInfo q in player.GetComponent<NPCInteraction>().acceptedQuestLists)
                        {
                            foreach(NPCManager.NPCItem it in q.requirement)
                            {
                                if ((it.objectName + "(Clone)") == this.gameObject.name){
                                    it.collected += 1;
                                }
                            }
                        }

                        this.gameObject.transform.parent.GetComponent<DirtTile>().needsPlowing = true;
                        this.gameObject.transform.parent.GetComponent<DirtTile>().AddDirt();

                        //local data record
                        DataRecord.AddEvents(5, this.gameObject.name);

                        //set harvested for feedback
                        feedback.harvested = true;
                        feedback.itemImage.sprite = sr.sprite;
                        feedback.itemText.text = item.itemName;

                        //For tutorial purpose
                        if (!TutorialManager.doneTutorial)
                        {
                            tutorial.harvestCount += 1;
                        }
                   
                        // gsheet data record
                        player.GetComponent<gsheet_data>().sendData(1, 1);
                        Destroy(this.gameObject);
                        break;
                    }
                }               
            }
            canInteract = false;
        }
    }

    //void CheckDistance()
    //{
    //    if(Vector2.Distance(this.transform.position, player.transform.position) > this.distance)
    //    {
    //        if (cropState == CropStateTest.Delayed || cropState == CropStateTest.Done)
    //        {
    //            canInteract = false;
    //        }
    //    }
    //}

    //IEnumerator DistanceCheck(float time)
    //{
    //    while(true)
    //    {
    //        float dis = Vector2.Distance(this.transform.position, player.transform.position);
    //        Vector2 cropDistance = this.transform.position;
    //        cropDistance.x += offSetX;
    //        cropDistance.y += offSetY;

    //        if(dis <= this.distance)
    //        {
    //            if (cropState == CropStateTest.Delayed || cropState == CropStateTest.Done)
    //            {
    //                canInteract = true;
    //            }
    //        }
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == Player.LocalPlayerInstance)
        {
            if (cropState == CropStateTest.Delayed)
            {
                canInteract = true;
            }

            else if (cropState == CropStateTest.Done)
            {
                canInteract = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Player.LocalPlayerInstance)
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
