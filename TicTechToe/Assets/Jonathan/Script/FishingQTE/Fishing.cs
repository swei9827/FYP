using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{
    public GameObject fishingGame;
    public GameObject player;
    public GameObject[] fishObject;

    public GameObject inventory;

    //fish Image
    public Sprite[] fishImage;

    public Image fishImg;
    public Image bucketImg;

    public float bucketHit;
    public int waterHit;

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
    }

    public void Interact(Tool t, PlayerInteraction player)
    {
        if (t == null)
        {
            Debug.Log("Use fishing rod");
        }
        else if (t != null)
        {
            if (t.toolType == ToolType.fishRod)
            {
                PopFishingGame();
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
    }

    public void PopFishingGame()
    {
        fishingGame.SetActive(true);
        PlayerMovement.canMove = false;      
        inventory.SetActive(false);
        GetFishSprite();      
    }

    public void GetFishSprite()
    {
        fishType = (FishTypeTest)Random.Range(0, (int)FishTypeTest.Max);
        switch (fishType)
        {
            case FishTypeTest.Catfish:
                {
                    fishImg.sprite = fishImage[0];
                    Debug.Log("Catfish");
                    break;
                }
            case FishTypeTest.Salmon:
                {
                    fishImg.sprite = fishImage[1];
                    Debug.Log("Salmon");
                    break;
                }
            case FishTypeTest.Sardine:
                {
                    fishImg.sprite = fishImage[2];
                    Debug.Log("Sardine");
                    break;
                }
            case FishTypeTest.Tuna:
                {
                    fishImg.sprite= fishImage[3];
                    Debug.Log("Tuna");
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
        if (bucketHit >= 3)
        {
            spawnFish();
            Debug.Log("Success!");
            bucketHit = 0;
            waterHit = 0;
            fishImg.rectTransform.localPosition = spawnPos;
            fishingGame.SetActive(false);
            inventory.SetActive(true);
            PlayerMovement.canMove = true;
        }
        else if (waterHit >= 5)
        {
            Debug.Log("Fail");
            Destroy(temp);
            waterHit = 0;
            bucketHit = 0;
            fishImg.rectTransform.localPosition = spawnPos;
            fishingGame.SetActive(false);
            inventory.SetActive(true);
            PlayerMovement.canMove = true;
        }
    }


    //FishManager FishManager = FindObjectOfType<FishManager>();
    //fishImage.GetComponent<Image>().sprite = FishManager.fishAsset.getFish(FishType.Catfish);
    //Debug.Log(FishManager.fishAsset.getFish(FishType.Catfish));
}
