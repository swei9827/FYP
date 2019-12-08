using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    public GameObject dialogueBox;

    [Header("Shop Settings")]
    public GameObject shop;
    public GameObject sellMenu;
    public GameObject PlusButtonHighlights;
    public GameObject BuyButtonHighlights;
    public GameObject sellButtonHighlights;

    [Header("Dirtile Settings")]
    public GameObject[] dirtTile;

    [Header("Level Complete")]
    public bool level1 = false;
    public bool level2 = false;
    public bool level3 = false;
    public bool level4 = false;

    [Header("HotKey Settings")]
    public GameObject[] hotKeySlots;
    public GameObject hotKeyHighlight;
    public GameObject slotPanel;
    private GameObject seedPanel;
    private GameObject seedKeySlot;

    //temp store for highlights
    GameObject temphighlights;

    //Get scripts variable
    private Fishing fishing;
    private DialogueManager dialogueManager;
    private DialogueHolder dialogueHolder;
    private HotKey Hotkey;
    private TargetIndicator targetIndicator;
    //private GameObject[] crops;

    // For harvest and water plant
    public int harvestCount = 0;
    public int waterCount = 0;

    bool loaded;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueHolder = FindObjectOfType<DialogueHolder>();
        fishing = FindObjectOfType<Fishing>();

        //set NPC dialogue to non tutorial dialogue
        dialogueObj[1].GetComponent<DialogueHolder>().option4 = true;
        dialogueObj[3].GetComponent<DialogueHolder>().option3 = true;

        loaded = true;
    }

    // Update is called once per frame
    void Update()
    {
        setInstatiatePlayer();
     
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerAction = true;
        }

        ChangeTutorial();
    }

    void setInstatiatePlayer()
    {
        if (loaded)
        {
            //For target indicator
            targetIndicator = FindObjectOfType<TargetIndicator>();
            targetIndicator.target = dialogueObj[0].transform;
            targetIndicator.SetChildrenActive(false);
         
            //for Hotkey
            Hotkey = FindObjectOfType<HotKey>();
            slotPanel = GameObject.Find("SlotPanel");
          
            for (int i = 0; i < 4; i++)
            {
                hotKeySlots[i] = slotPanel.transform.GetChild(i).gameObject;
            }

            loaded = false;
        }
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
        else if (popUpIndex == 1)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                //Pop Out Interact with NPC1
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

                //Set indicator active
                targetIndicator.SetChildrenActive(true);

                //Set NPC1 Pop Out
                dialogueObj[0].GetComponent<DialogueHolder>().PopOut.SetActive(true);

                //Pop Out Interact with NPC2
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

                //Change indicator target
                targetIndicator.SetChildrenActive(true);
                targetIndicator.target = dialogueObj[1].transform;

                //Set dialogue conversation
                dialogueObj[1].GetComponent<DialogueHolder>().option1 = true;
                dialogueObj[1].GetComponent<DialogueHolder>().option4 = false;

                //Set NPC2 Pop Out
                dialogueObj[1].GetComponent<DialogueHolder>().PopOut.SetActive(true);

                //Pop Out GoldUI
                if (dialogueManager.GetComponent<DialogueManager>().currentLine == 4 && dialogueObj[1].GetComponent<DialogueHolder>().option1 &&
                    dialogueObj[1].GetComponent<DialogueHolder>().temp == "NPC Jane")
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
                if (dialogueManager.GetComponent<DialogueManager>().currentLine == 7 && dialogueObj[1].GetComponent<DialogueHolder>().option1)
                {
                    playerAction = false;
                    dialogueBox.SetActive(false);
                    Player.LocalPlayerInstance.GetComponent<Player>().setMoney(1000);
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

                    //Change indicator target
                    targetIndicator.SetChildrenActive(true);
                    targetIndicator.target = dialogueObj[2].transform;
                }
            }
        }

        // Tutorial 6- Pop Out direction To Shop
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
                    temphighlights = Instantiate(PlusButtonHighlights, shop.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0));
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

                //Set NPC3 Pop Out
                dialogueObj[2].GetComponent<DialogueHolder>().PopOut.SetActive(true);

                //pop out click buy button
                if (shop.GetComponent<ShopSystem>().slots[0].GetComponent<ShopRenderer>().itemCount.text == 5.ToString())
                {
                    //destroy button highlight
                    Destroy(temphighlights);

                    //Instantiate buy button highlights
                    temphighlights = Instantiate(BuyButtonHighlights, shop.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0));
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

                int moneyAmount = int.Parse(shop.GetComponent<ShopSystem>().currentMoney.text);
                //Pop out Exit Shop
                if (moneyAmount < 1000)
                {
                    //destroy button highlight
                    Destroy(temphighlights);

                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        // Tutorial 9- Exit Shop
        else if (popUpIndex == 9)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                //Pop out Go find Jane
                if (!shop.activeInHierarchy)
                {
                    playerAction = false;
                    popUpIndex++;

                    //Change indicator target
                    targetIndicator.SetChildrenActive(true);
                    targetIndicator.target = dialogueObj[1].transform;
                }
            }
        }

        // Tutorial 10 - Go Find Jane
        else if (popUpIndex == 10)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                dialogueObj[1].GetComponent<DialogueHolder>().option2 = true;
                dialogueObj[1].GetComponent<DialogueHolder>().option4 = false;

                //Pop Out Plow
                if (dialogueObj[1].GetComponent<DialogueHolder>().interactNPCJane2)
                {
                    playerAction = false;
                    popUpIndex++;

                    //Instantiate hotkey plow highlights
                    temphighlights = Instantiate(hotKeyHighlight, hotKeySlots[0].transform);
                    temphighlights.transform.SetAsFirstSibling();
                }
            }
        }

        // Tutorial 11 - Plow
        else if (popUpIndex == 11)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                //Set dialogue Conversation
                dialogueObj[1].GetComponent<DialogueHolder>().option4 = true;
                dialogueObj[1].GetComponent<DialogueHolder>().option2 = false;

                //Set NPC3 Pop Out
                dialogueObj[2].GetComponent<DialogueHolder>().PopOut.SetActive(true);

                //delete hotkey plow highlights
                if (Hotkey.scrollPosition == 0)
                {
                    Destroy(temphighlights);
                }

                //Pop out Plant
                int counter = 0;

                for (int i = 0; i <= 13; i++)
                {
                    if (!dirtTile[i].GetComponent<DirtTile>().needsPlowing)
                    {
                        counter++;
                    }
                }

                if (counter == 5)
                {
                    playerAction = false;
                    popUpIndex++;

                    //Instantiate hotkey seed highlights
                    temphighlights = Instantiate(hotKeyHighlight, hotKeySlots[3].transform);
                    temphighlights.transform.SetAsFirstSibling();
                }
            }
        }

        // Tutorial 12 - Plant
        else if (popUpIndex == 12)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                int counter = 0;

                //delete hotkey plow highlights
                if (Hotkey.scrollPosition == 3)
                {
                    Destroy(temphighlights);
                }

                //Set counter for player to plant
                for (int i = 0; i <= 13; i++)
                {
                    if (!dirtTile[i].GetComponent<DirtTile>().addPlant)
                    {
                        counter++;
                    }
                }

                if (counter == 5)
                {
                    playerAction = false;
                    popUpIndex++;

                    //Instantiate hotkey seed highlights
                    temphighlights = Instantiate(hotKeyHighlight, hotKeySlots[1].transform);
                    temphighlights.transform.SetAsFirstSibling();
                }
            }
        }

        // Tutorial 13 - Watering
        else if (popUpIndex == 13)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                //Set crops rate
                GameObject[] crops = GameObject.FindGameObjectsWithTag("Crops");
                foreach (GameObject StrawberryCrops in crops)
                {
                    if(StrawberryCrops.GetComponent<CropTest>().cropState == CropStateTest.Delayed)
                    {
                        StrawberryCrops.GetComponent<CropTest>().growthRate = 2;
                    }                  
                }

                //delete hotkey plow highlights
                if (Hotkey.scrollPosition == 1)
                {
                    Destroy(temphighlights);
                }

                dialogueObj[1].GetComponent<DialogueHolder>().option3 = true;
                dialogueObj[1].GetComponent<DialogueHolder>().option4 = false;

                //Pop out Refill Water
                if (WaterCan.curFill <= 0)
                {
                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        //Tutorial 14 - Pop Out refill water
        else if (popUpIndex == 14)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                dialogueObj[1].GetComponent<DialogueHolder>().option3 = true;
                dialogueObj[1].GetComponent<DialogueHolder>().option4 = false;

                //Pop out go find back Jane
                if (waterCount >= 5)
                {
                    playerAction = false;
                    popUpIndex++;

                    //Change indicator target
                    targetIndicator.SetChildrenActive(true);
                    targetIndicator.target = dialogueObj[1].transform;
                }
            }
        }

        //Tutorial 14 - Go Find Back Jane
        else if (popUpIndex == 15)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                //Set NPC3 Pop Out
                dialogueObj[2].GetComponent<DialogueHolder>().PopOut.SetActive(true);

                //Pop Out Go Find Fishing NPC
                if (dialogueObj[1].GetComponent<DialogueHolder>().option4)
                {
                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        //Tutorial 15 - Go Find Fishing NPC
        else if (popUpIndex == 16)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                //Change indicator target
                targetIndicator.SetChildrenActive(true);
                targetIndicator.target = dialogueObj[3].transform;

                //Set Dialogue Conversation
                dialogueObj[3].GetComponent<DialogueHolder>().option1 = true;
                dialogueObj[3].GetComponent<DialogueHolder>().option3 = false;

                //Set NPC4 Pop Out
                dialogueObj[3].GetComponent<DialogueHolder>().PopOut.SetActive(true);

                // Pop Out Go to Fishing Spot
                if (dialogueObj[3].GetComponent<DialogueHolder>().interactNPCMary)
                {
                    playerAction = false;
                    popUpIndex++;

                    //Instantiate hotkey seed highlights
                    temphighlights = Instantiate(hotKeyHighlight, hotKeySlots[2].transform);
                    temphighlights.transform.SetAsFirstSibling();
                }
            }
        }

        //Tutorial 16 - Go to Fishing Spot
        else if (popUpIndex == 17)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                //Set Dialogue Conversation
                dialogueObj[3].GetComponent<DialogueHolder>().option3 = true;
                dialogueObj[3].GetComponent<DialogueHolder>().option1 = false;

                //delete hotkey plow highlights
                if (Hotkey.scrollPosition == 2)
                {
                    Destroy(temphighlights);
                }

                //Pop Out Fishing Tutorial
                if (fishing.fishingGame.activeSelf)
                {
                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        //Tutorial 17 - Fishing Tutorial
        else if (popUpIndex == 18)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                dialogueObj[3].GetComponent<DialogueHolder>().option2 = true;
                dialogueObj[3].GetComponent<DialogueHolder>().option3 = false;

                //Pop Out Go Find Auntie Mary
                if (fishing.success)
                {
                    //set player not to fish for a moment
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>().canInteract = false;
                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        //Tutorial 18 - Go Find Back Auntie Mary
        else if (popUpIndex == 19)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                //Change indicator target
                targetIndicator.SetChildrenActive(true);
                targetIndicator.target = dialogueObj[3].transform;

                //Set NPC4 Pop Out
                dialogueObj[3].GetComponent<DialogueHolder>().PopOut.SetActive(true);

                //Pop Out Go harvest
                if (dialogueObj[3].GetComponent<DialogueHolder>().interactNPCMary2)
                {
                    //Set player able to fish as normal
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>().canInteract = true;
                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        //Tutorial 19 - Go Harvest
        else if (popUpIndex == 20)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                dialogueObj[3].GetComponent<DialogueHolder>().option3 = true;
                dialogueObj[3].GetComponent<DialogueHolder>().option2 = false;

                //Pop out Go sell item
                if (harvestCount >= 5)
                {
                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        //Go Shop Sell Item
        else if (popUpIndex == 21)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                //Change indicator target
                targetIndicator.SetChildrenActive(true);
                targetIndicator.target = dialogueObj[2].transform;

                //Pop Out sell highlight
                if (shop.activeInHierarchy)
                {
                    //Instantiate highlights
                    temphighlights = Instantiate(sellButtonHighlights, shop.transform.GetChild(0));
                    temphighlights.transform.SetSiblingIndex(1);

                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        //click Sell button
        else if (popUpIndex == 22)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                //Pop Out sell highlight
                if (sellMenu.activeSelf)
                {
                    //destroy button highlight
                    Destroy(temphighlights);

                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        // Straight Sell fish
        else if (popUpIndex == 23)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                if (!TutorialPopOut[22].activeSelf)
                {
                    //Instantiate buy button highlights
                    temphighlights = Instantiate(BuyButtonHighlights, shop.transform.GetChild(0).GetChild(5).GetChild(0).GetChild(0).GetChild(0));
                    temphighlights.transform.SetSiblingIndex(5);
                }
    
                int moneyAmount = int.Parse(shop.GetComponent<ShopSystem>().currentMoney.text);

                if (moneyAmount > 750)
                {
                    playerAction = false;
                    popUpIndex++;

                    //destroy button highlight
                    Destroy(temphighlights);

                    //Instantiate highlights
                    temphighlights = Instantiate(PlusButtonHighlights, shop.transform.GetChild(0).GetChild(5).GetChild(0).GetChild(0).GetChild(0));
                    temphighlights.transform.SetSiblingIndex(3);
                }            
            }
        }

        // click add button for sell strawberry
        else if (popUpIndex == 24)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                ShopRenderer sr = shop.transform.GetChild(0).GetChild(5).GetComponent<Sell>().content[1].GetComponent<ShopRenderer>();

                if (int.Parse(sr.itemCount.text) == sr.itemCount.gameObject.GetComponent<UpdatableInt>().max)
                {
                    //destroy button highlight
                    Destroy(temphighlights);

                    //Instantiate buy button highlights
                    temphighlights = Instantiate(BuyButtonHighlights, shop.transform.GetChild(0).GetChild(5).GetChild(0).GetChild(0).GetChild(0));
                    temphighlights.transform.SetSiblingIndex(5);

                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        //click sell button to sell strawberry
        else if (popUpIndex == 25)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                int moneyAmount = int.Parse(shop.GetComponent<ShopSystem>().currentMoney.text);

                if (moneyAmount >= 1000)
                {
                    //destroy button highlight
                    Destroy(temphighlights);

                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        else if(popUpIndex == 26)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}


