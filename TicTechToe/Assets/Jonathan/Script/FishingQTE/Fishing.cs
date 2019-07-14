using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{

    public GameObject fishingGame;

    public GameObject inventory;

    //fish Image
    public Sprite[] fishImage;

    public Image fishImg;
    public Image bucketImg;

    public float bucketHit;
    public int waterHit;

    //script
    private FishMovement fish;
    public FishAsset asset;
    public FishType fishType;

    Vector2 spawnPos;
    Vector2 spawnPos2;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            PopFishingGame();
        }
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
        fishType = (FishType)Random.Range(0, (int)FishType.Max);     
        switch (fishType)
        {
            case FishType.Catfish:
                {
                    fishImg.sprite = fishImage[0];                   
                    Debug.Log("Catfish");
                    break;
                }
            case FishType.Salmon:
                {
                    fishImg.sprite = fishImage[1];
                    Debug.Log("Salmon");
                    break;
                }
            case FishType.Sardine:
                {
                    fishImg.sprite = fishImage[2];
                    Debug.Log("Sardine");
                    break;
                }
            case FishType.Tuna:
                {
                    fishImg.sprite= fishImage[3];
                    Debug.Log("Tuna");
                    break;
                }
        }
    }

    public void FishingGame()
    {
        if (bucketHit >= 3)
        {
            Debug.Log("Success!");           
            bucketHit = 0;
            fishImg.rectTransform.localPosition = spawnPos;
            fishingGame.SetActive(false);
            inventory.SetActive(true);
            PlayerMovement.canMove = true;
        }
        else if (waterHit >= 5)
        {
            Debug.Log("Fail");
            waterHit = 0;
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
