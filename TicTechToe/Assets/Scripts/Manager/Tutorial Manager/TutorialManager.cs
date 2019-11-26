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
    public bool playerAction = false;    //Set player interact

    [Header("Movement Collider Settings")]
    public GameObject[] colliderObj;

    [Header("Dialogue Character Settings")]
    public GameObject[] dialogueObj;
    private DialogueManager dialogueManager;
    private DialogueHolder dialogueHolder;
    public GameObject dialogueBox;

    [Header("Shop Settings")]
    public GameObject shop;
    public GameObject PlusButtonHighlights;
    public GameObject BuyButtonHighlights;
    private UpdatableInt updateText;
    GameObject temphighlights;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueHolder = FindObjectOfType<DialogueHolder>();
        updateText = FindObjectOfType<UpdatableInt>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerAction = true;
        }

        ChangeTutorial();

        Debug.Log(updateText.temp);
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

        //Start Tutorial 
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

        //Tutorial 1 - player movement
        if (popUpIndex == 1)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                if (colliderObj[0].GetComponent<SpriteRenderer>().color == Color.red && colliderObj[1].GetComponent<SpriteRenderer>().color == Color.red)
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

        //Tutorial 2 - interact with NPC 1
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

                    //make sure doesnt interact NPC1
                    dialogueManager.canInteract = false;
                }
            }
        }

        //Tutorial 3- interact with NPC 2
        else if (popUpIndex == 3)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                //Pop Out GoldUI
                if (dialogueManager.GetComponent<DialogueManager>().currentLine == 4)
                {
                    playerAction = false;
                    popUpIndex++;
                    dialogueBox.SetActive(false);
                }
            }
        }

        // Tutorial 4- Pop Out GoldUI
        else if (popUpIndex == 4)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                dialogueBox.SetActive(true);
                Time.timeScale = 1;

                //Pop out received money
                if (dialogueManager.GetComponent<DialogueManager>().currentLine == 7)
                {
                    playerAction = false;
                    dialogueBox.SetActive(false);
                    popUpIndex++;
                }
            }
        }

        // Tutorial 5- Pop Out received Money
        else if (popUpIndex == 5)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                dialogueBox.SetActive(true);
                Time.timeScale = 1;

                //Pop Out direction to shop
                if (dialogueObj[1].GetComponent<DialogueHolder>().interactNPCJane)
                {
                    dialogueBox.SetActive(false);
                    playerAction = false;
                    popUpIndex++;

                    //make sure doesnt interact NPC2
                    dialogueManager.canInteract = false;                              
                }
            }
        }

        // Tutorial 5- Pop Out direction To Shop
        else if (popUpIndex == 6)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                //pop out Add event
                if (shop.activeInHierarchy)
                {
                    //Instantiate highlights
                    temphighlights = Instantiate(PlusButtonHighlights, shop.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0));
                    temphighlights.transform.SetSiblingIndex(3);

                    playerAction = false;
                    popUpIndex++;                   
                }
            }
        }

        // Tutorial 7- Pop Out Add event
        else if (popUpIndex == 7)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;
              
                //pop out click buy button
                if(updateText.temp == 5)
                {
                    //destroy button highlight
                    Destroy(temphighlights);

                    //Instantiate buy button highlights
                    temphighlights = Instantiate(PlusButtonHighlights, shop.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0));
                    temphighlights.transform.SetSiblingIndex(6);

                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        // Tutorial 8- Pop Out click buy button
        else if (popUpIndex == 8)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                if (updateText.temp == 5)
                {
                    //destroy button highlight
                    Destroy(temphighlights);

                    //Instantiate buy button highlights
                    temphighlights = Instantiate(BuyButtonHighlights, shop.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0));
                    temphighlights.transform.SetSiblingIndex(6);

                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        // Tutorial 8- Pop Out click buy button
        else if (popUpIndex == 9)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;
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


