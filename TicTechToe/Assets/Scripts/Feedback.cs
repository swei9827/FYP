using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Feedback : MonoBehaviour
{
    public GameObject feedbackPanel;

    public Image itemImage;
    public TextMeshProUGUI itemText;
    public TextMeshProUGUI action;

    private Fishing fishingGame;
    private CropTest crops;
    private DirtTile dirt;

    public float maxFishActiveTime;
    public float maxCropsActiveTime;
    private float activeTime;

    public bool harvested = false;

    // Start is called before the first frame update
    void Start()
    {
        fishingGame = FindObjectOfType<Fishing>();
        crops = FindObjectOfType<CropTest>();
        
        if(feedbackPanel.activeInHierarchy)
        {
            feedbackPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        setItem();
    }

    void setItem()
    {
        if (fishingGame.success)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>().canInteract = false;
            feedbackPanel.SetActive(true);

            //start countdown
            activeTime += Time.deltaTime;

            //Set feedback
            action.text = "Successfully Catch";
            itemImage.sprite = fishingGame.fishImg.sprite;
            itemText.text = fishingGame.fishNames.text;

            //set countdown
            if(activeTime >= maxFishActiveTime)
            {
                activeTime = 0;
                fishingGame.success = false;
                feedbackPanel.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>().canInteract = true;
            }
        }

        else if(fishingGame.fail)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>().canInteract = false;
            feedbackPanel.SetActive(true);

            //start countdown
            activeTime += Time.deltaTime;

            action.text = "Fail to Catch";
            itemImage.sprite = fishingGame.fishImg.sprite;
            itemText.text = fishingGame.fishNames.text;

            //set countdown
            if (activeTime >= maxFishActiveTime)
            {
                activeTime = 0;
                fishingGame.fail = false;
                feedbackPanel.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>().canInteract = true;
            }
        }

        if (harvested)
        {
            feedbackPanel.SetActive(true);

            //start countdown
            activeTime += Time.deltaTime;

            //Set feedback
            action.text = "Harvested";

            //itemImage & itemText set at CropTest

            //set countdown
            if (activeTime >= maxCropsActiveTime)
            {
                activeTime = 0;
                harvested = false;
                feedbackPanel.SetActive(false);
            }      
        }
    }
}
