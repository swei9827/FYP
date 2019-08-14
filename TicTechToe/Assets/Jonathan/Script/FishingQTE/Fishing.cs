using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{
    // GameObject
    public GameObject fishingGame;
    public GameObject player;
    public GameObject[] fishObject;

    public GameObject inventory;

    //fish Image
    public Sprite[] fishImage;

    public Image fishImg;
    public Image bucketImg;

    public float bucketHit = 0;
    public float waterHit = -1;

    private float hitBucketAmount;
    private float hitWaterAmount;

    bool canInteract = true;

    //UI
    public Text fishName;
    public Text bucketCounter;
    public Text waterCounter;

    //script
    private FishMovement fish;
    public ItemTest item;
    public FishTypeTest fishType;

    Vector2 spawnPos;
    Vector2 spawnPos2;

    GameObject temp;

    public void Start()
    {
        fishingGame.SetActive(false);
        spawnPos = new Vector2(fishImg.rectTransform.localPosition.x, fishImg.rectTransform.localPosition.y);

        bucketHit = 0;
        waterHit = -1;
    }

    public void Interact(Tool t, PlayerInteraction player)
    {
        if (t == null)
        {
            Debug.Log("Use fishing rod");
            GameObject.FindGameObjectWithTag("GameController").GetComponent<DataRecord>().AddEvents(0, t.name.ToString());
        }
        else if (t != null)
        {
            if (t.toolType == ToolType.fishRod)
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
        FishingGame();

        //update fish counter
        bucketCounter.text = "Bucket Hit : " + bucketHit.ToString() + " / " + hitBucketAmount.ToString();
        waterCounter.text = "Water Hit : " + waterHit.ToString() + " / " + hitWaterAmount.ToString();
    }

    public void PopFishingGame()
    {
        canInteract = false;
        fishingGame.SetActive(true);
        PlayerMovement.canMove = false;      
        inventory.SetActive(false);
        GetFishSprite();      
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
                    fishName.text = FishTypeTest.Catfish.ToString();                
                    break;
                }
            case FishTypeTest.Salmon:
                {
                    fishImg.sprite = fishImage[1];

                    //set hit amount
                    hitBucketAmount = 4;
                    hitWaterAmount = 2;

                    //set UI
                    fishName.text = FishTypeTest.Salmon.ToString();
                    break;
                }
            case FishTypeTest.Sardine:
                {
                    fishImg.sprite = fishImage[2];

                    //set hit amount
                    hitBucketAmount = 3;
                    hitWaterAmount = 4;

                    fishName.text = FishTypeTest.Sardine.ToString();
                    break;
                }
            case FishTypeTest.Tuna:
                {
                    fishImg.sprite= fishImage[3];

                    //set hit amount
                    hitBucketAmount = 3;
                    hitWaterAmount = 3;

                    fishName.text = FishTypeTest.Tuna.ToString();
                    break;
                }
        }
    }

    void spawnFish()
    {
        if (fishType == FishTypeTest.Catfish)
        {
            temp = Instantiate(fishObject[0], player.transform.position, Quaternion.identity);
        }
        if (fishType == FishTypeTest.Salmon)
        {
            temp = Instantiate(fishObject[1], player.transform.position, Quaternion.identity);
        }
        if(fishType == FishTypeTest.Sardine)
        {
            temp = Instantiate(fishObject[2], player.transform.position, Quaternion.identity);
        }
         if(fishType == FishTypeTest.Tuna)
        {
            temp = Instantiate(fishObject[3], player.transform.position, Quaternion.identity);
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().canGetFish = true;
    }

    public void FishingGame()
    {
        if(!canInteract)
        {
            if (bucketHit >= hitBucketAmount)
            {
                spawnFish();
                Debug.Log("Success!");
                //set everything to 0
                bucketHit = 0;
                waterHit = -1;
                fishImg.rectTransform.localPosition = spawnPos;

                //active back
                fishingGame.SetActive(false);
                inventory.SetActive(true);
                PlayerMovement.canMove = true;
                canInteract = true;
            }
            else if (waterHit >= hitWaterAmount)
            {
                Debug.Log("Fail");

                //set everything to 0
                bucketHit = 0;
                waterHit = -1;
                Destroy(temp);

                //active back
                fishImg.rectTransform.localPosition = spawnPos;
                fishingGame.SetActive(false);
                inventory.SetActive(true);
                PlayerMovement.canMove = true;
                canInteract = true;
            }
        }      
    }


    //FishManager FishManager = FindObjectOfType<FishManager>();
    //fishImage.GetComponent<Image>().sprite = FishManager.fishAsset.getFish(FishType.Catfish);
    //Debug.Log(FishManager.fishAsset.getFish(FishType.Catfish));
}
