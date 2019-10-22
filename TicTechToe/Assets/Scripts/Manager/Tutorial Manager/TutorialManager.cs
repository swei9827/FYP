using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TutorialManager : MonoBehaviour
{
    [Header("Tutorial Settings")]
    public GameObject[] TutorialPopOut;
    private int popUpIndex;
    private float timer = 1f;     //set timer for player to move
    bool playerAction = false;    //Set player interact

    [Header("Movement Collider Settings")]
    public GameObject[] colliderObj;

    [Header("Dialogue Character Settings")]
    public GameObject[] dialogueObj;

    [Header("Inventory Settings")]
    private Inventory inventory;     //inventory

    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAction = true;
        }

        ChangeTutorial();
    }

    void ChangeTutorial()
    {
        for (int i = 0; i < TutorialPopOut.Length; i++)
        {
            if (i == popUpIndex)
            {
                Time.timeScale = 0;
                TutorialPopOut[popUpIndex].SetActive(true);
            }
        }

        //First Tutorial
        if (popUpIndex == 0)
        {
            if (playerAction)
            {
                timer -= Time.deltaTime;
                Time.timeScale = 1;
                TutorialPopOut[popUpIndex].SetActive(false);
                if (timer <= 0)
                {
                    popUpIndex++;
                    playerAction = false;
                }
            }
        }

        //player movement
        if (popUpIndex == 1)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                if (colliderObj[0].GetComponent<SpriteRenderer>().color == Color.green && colliderObj[1].GetComponent<SpriteRenderer>().color == Color.green)
                {
                    for (int i = 0; i < colliderObj.Length; i++)
                    {
                        Destroy(colliderObj[i]);
                    }
                    popUpIndex++;
                    playerAction = false;
                }
            }
        }

        //interact with NPC 1
        else if (popUpIndex == 2)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                //NPC Uncle Joseph
                if (dialogueObj[0].GetComponent<DialogueHolder>().interactNPCJoseph)
                {
                    playerAction = false;
                    popUpIndex++;                    
                }
            }
        }

        //interact with NPC 2
        else if (popUpIndex == 3)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                //NPC Jane
                if (dialogueObj[1].GetComponent<DialogueHolder>().interactNPCJane)
                {
                    playerAction = false;
                    popUpIndex++;                  
                }
            }
        }

        //// plow 
        //else if (popUpIndex == 2)
        //{
        //    if (playerAction)
        //    {
        //        TutorialPopOut[popUpIndex].SetActive(false);
        //        Time.timeScale = 1;

        //        if (!GameObject.Find("DirtTile").GetComponent<DirtTile>().needsPlowing)
        //        {
        //            popUpIndex++;
        //            playerAction = false;
        //        }
        //    }
        //}

        ////plant seed
        //else if (popUpIndex == 3)
        //{
        //    if (playerAction)
        //    {
        //        TutorialPopOut[popUpIndex].SetActive(false);
        //        Time.timeScale = 1;

        //        if (!DirtTile.addPlant)
        //        {
        //            playerAction = false;
        //            popUpIndex++;
        //        }
        //    }
        //}

        ////Water plant
        //else if (popUpIndex == 4)
        //{
        //    if (playerAction)
        //    {
        //        TutorialPopOut[popUpIndex].SetActive(false);
        //        Time.timeScale = 1;

        //        if (GameObject.FindGameObjectWithTag("Crops").GetComponent<CropTest>().watered)
        //        {
        //            playerAction = false;
        //            popUpIndex++;
        //        }
        //    }
        //}

        //// harvest
        //else if (popUpIndex == 5)
        //{
        //    if (playerAction)
        //    {
        //        TutorialPopOut[popUpIndex].SetActive(false);
        //        Time.timeScale = 1;

        //        //if (GameObject.FindGameObjectWithTag("Crops").GetComponent<GetItems>().canGetCrops)
        //        //{                   
        //        //    playerAction = false;
        //        //    popUpIndex++;
        //        //}
        //        foreach (Item item in inventory.items)
        //        {
        //            if (item.id >= 10)
        //            {
        //                playerAction = false;
        //                popUpIndex++;
        //                break;
        //            }
        //        }
        //    }
        //}

        ////fishing
        //else if (popUpIndex == 6)
        //{
        //    if (playerAction)
        //    {
        //        TutorialPopOut[popUpIndex].SetActive(false);
        //        Time.timeScale = 1;

        //        if (GameObject.Find("Tilemap_River").GetComponent<Fishing>().success)
        //        {
        //            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>().canInteract = false;
        //            playerAction = false;
        //            popUpIndex++;
        //        }
        //    }
        //}

        //else if (popUpIndex == 7)
        //{
        //    if (playerAction)
        //    {
        //        TutorialPopOut[popUpIndex].SetActive(false);
        //        Time.timeScale = 1;

        //        timer -= Time.deltaTime;
        //        if (Input.GetKeyDown(KeyCode.Space) && timer <= 0)
        //        {
        //            playerAction = false;
        //            popUpIndex++;
        //            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>().canInteract = true;
        //        }
        //    }
        //}
    }
}


