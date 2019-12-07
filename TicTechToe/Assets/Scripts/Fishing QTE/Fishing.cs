using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fishing : MonoBehaviour
{
    // GameObject
    public GameObject fishingGame;
    public GameObject player;
    public GameObject[] fishObject;

    public Inventory inventory;
    private ItemDatabase itemDatabase;

    //fish Image
    public Sprite[] fishImage;

    public Image fishImg;
    public Image bucketImg;

    public float bucketHit = 0;
    public float waterHit = -1;

    private float hitBucketAmount;
    private float hitWaterAmount;

    public bool canInteract = true;
    public bool success = false;
    public bool fail = false;

    //UI
    public TextMeshProUGUI fishNames;
    public TextMeshProUGUI bucketCounters;
    public Text waterCounter;

    //script
    private FishMovement fish;
    public ItemTest item;
    public FishTypeTest fishType;

    Vector2 spawnPos;
    Vector2 spawnPos2;

    NPCInteraction ni;
    GameObject temp;

    bool variableObtained;
    
    public void Start()
    {      
        fishingGame.SetActive(false);
        spawnPos = new Vector2(fishImg.rectTransform.localPosition.x, fishImg.rectTransform.localPosition.y);
        spawnPos2 = new Vector2(bucketImg.rectTransform.localPosition.x, bucketImg.rectTransform.localPosition.y);
        bucketHit = 0;
        waterHit = -1;
    }

    public void Interact(Tool t, PlayerInteraction player)
    {
        if (t == null)
        {
            Debug.Log("Use fishing rod");
        }
        else if (t != null)
        {
            if (t.isFishingRod)
            {
                if(canInteract)
                {
                    PopFishingGame();
                }             
            }
            else
            {
                Debug.Log("Not fishing rod, use fishing rod");
            }
        }
    }

    public void Update()
    {
        if(RoomController.playerSpawned && !variableObtained)
        {
            ni = GameObject.FindGameObjectWithTag("Player").GetComponent<NPCInteraction>();
            inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
            itemDatabase = GameObject.Find("Inventory").GetComponent<ItemDatabase>();
            variableObtained = true;
        }

        FishingGame();
        //update fish counter
        bucketCounters.text = "Bucket Hit : " + bucketHit.ToString() + " / " + hitBucketAmount.ToString();
        waterCounter.text = "Water Hit : " + waterHit.ToString() + " / " + hitWaterAmount.ToString();
    }

    public void PopFishingGame()
    {
        FxManager.PlayMusic("FishingFx");
        canInteract = false;
        fishingGame.SetActive(true);
        PlayerMovement.canMove = false;
        GetFishSprite();

        //local data record
        DataRecord.AddEvents(6, "Fishing");
    }

    public void GetFishSprite()
    {
        fishType = (FishTypeTest)Random.Range(1, (int)FishTypeTest.Max);
        switch (fishType)
        {
            case FishTypeTest.Catfish:
                {
                    fishImg.sprite = fishImage[0];

                    //set hit amount
                    hitBucketAmount = 2;
                    hitWaterAmount = 3;

                    //set text
                    fishNames.text = FishTypeTest.Catfish.ToString();                
                    break;
                }
            case FishTypeTest.Salmon:
                {
                    fishImg.sprite = fishImage[1];

                    //set hit amount
                    hitBucketAmount = 4;
                    hitWaterAmount = 2;

                    //set UI
                    fishNames.text = FishTypeTest.Salmon.ToString();
                    break;
                }
            case FishTypeTest.Sardine:
                {
                    fishImg.sprite = fishImage[2];

                    //set hit amount
                    hitBucketAmount = 3;
                    hitWaterAmount = 4;

                    fishNames.text = FishTypeTest.Sardine.ToString();
                    break;
                }
            case FishTypeTest.Tuna:
                {
                    fishImg.sprite= fishImage[3];

                    //set hit amount
                    hitBucketAmount = 3;
                    hitWaterAmount = 3;

                    fishNames.text = FishTypeTest.Tuna.ToString();
                    break;
                }
        }
    }

    void spawnFish()
    {
        //if (fishType == FishTypeTest.Catfish)
        //{
        //    temp = Instantiate(fishObject[0], player.transform.position, Quaternion.identity);
        //}
        //if (fishType == FishTypeTest.Salmon)
        //{
        //    temp = Instantiate(fishObject[1], player.transform.position, Quaternion.identity);
        //}
        //if(fishType == FishTypeTest.Sardine)
        //{
        //    temp = Instantiate(fishObject[2], player.transform.position, Quaternion.identity);
        //}
        // if(fishType == FishTypeTest.Tuna)
        //{
        //    temp = Instantiate(fishObject[3], player.transform.position, Quaternion.identity);
        //}

        foreach (Item item in itemDatabase.database)
        {
            if ((item.itemName) == fishImg.sprite.name)
            {
                ni.questItemCheck(item);
                inventory.AddItem(item.id);

                //Quest item check
                foreach (NPCManager.QuestInfo q in GameObject.FindGameObjectWithTag("Player").GetComponent<NPCInteraction>().acceptedQuestLists)
                {
                    foreach (NPCManager.NPCItem it in q.requirement)
                    {
                        if ((it.objectName) == this.gameObject.name)
                        {
                            it.collected += 1;
                        }
                    }
                }

                //gsheet data record
                GameObject.FindGameObjectWithTag("Player").GetComponent<gsheet_data>().sendData(0,1);
                
                //local data record
                DataRecord.AddEvents(0, fishImg.sprite.name);

                break;
            }
        }
    }

    public void FishingGame()
    {
        if(!canInteract)
        {
            if (bucketHit >= hitBucketAmount)
            {
                //Add Fish to Inventory
                spawnFish();
               
                //set everything to 0
                bucketHit = 0;
                waterHit = -1;
                fishImg.rectTransform.localPosition = spawnPos;
                bucketImg.rectTransform.localPosition = spawnPos2;

                //active back
                fishingGame.SetActive(false);
                PlayerMovement.canMove = true;
                canInteract = true;
                success = true;
            }

            else if (waterHit >= hitWaterAmount)
            {
                //set everything to 0
                bucketHit = 0;
                waterHit = -1;
                fishImg.rectTransform.localPosition = spawnPos;
                bucketImg.rectTransform.localPosition = spawnPos2;

                //active back
                fishingGame.SetActive(false);
                PlayerMovement.canMove = true;
                canInteract = true;
                fail = true;
            }

        }      
    }


    //FishManager FishManager = FindObjectOfType<FishManager>();
    //fishImage.GetComponent<Image>().sprite = FishManager.fishAsset.getFish(FishType.Catfish);
    //Debug.Log(FishManager.fishAsset.getFish(FishType.Catfish));
}
