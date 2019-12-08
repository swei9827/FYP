using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Transform canvas;

    //Inventory
    public Transform inventory;

    //Fishing QTE
    public Transform fishingGame;

    //Pause
    public Transform pause;

    //Quests
    public Transform quests;

    //dialogue
    public Transform dialogue;

    //NPC Quests
    public Transform NPCQuests;

    //Player interaction
    private GameObject player;

    public bool loaded = false;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        //Initialize Canvas
        canvas = GameObject.Find("Canvas").transform;

        fishingGame = canvas.GetChild(2);
        inventory = canvas.GetChild(3);
        pause = canvas.GetChild(8);
    }

    private void Start()
    {
        loaded = true;
    }

    void Update()
    {
        setComponent();
        UICheck();
    }

    void setComponent()
    {
        if(loaded)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            quests = player.transform.GetChild(5).GetChild(2);
            NPCQuests = player.transform.GetChild(5).GetChild(0);
            loaded = false;
        }
    }

    void UICheck()
    {
        if (fishingGame.gameObject.activeInHierarchy)
        {
            inventory.gameObject.SetActive(false);
            pause.gameObject.SetActive(false);
            quests.gameObject.SetActive(false);
            NPCQuests.gameObject.SetActive(false);
        }

        else if (inventory.gameObject.activeInHierarchy)
        {
            fishingGame.gameObject.SetActive(false);
            pause.gameObject.SetActive(false);
            quests.gameObject.SetActive(false);
            NPCQuests.gameObject.SetActive(false);
        }

        else if(pause.gameObject.activeInHierarchy)
        {
            fishingGame.gameObject.SetActive(false);
            inventory.gameObject.SetActive(false);
            quests.gameObject.SetActive(false);
            NPCQuests.gameObject.SetActive(false);
        }

        else if(quests.gameObject.activeInHierarchy)
        {
            fishingGame.gameObject.SetActive(false);
            inventory.gameObject.SetActive(false);
            pause.gameObject.SetActive(false);
            NPCQuests.gameObject.SetActive(false);
        }

        else if (NPCQuests.gameObject.activeInHierarchy)
        {
            fishingGame.gameObject.SetActive(false);
            pause.gameObject.SetActive(false);
            inventory.gameObject.SetActive(false);
            quests.gameObject.SetActive(false);
        }
    }

    public Vector2 WorldToCanvasPoint(Vector3 position)
    {
        //First get the position to viewport coordinates.
        //viewport point goes from 0,0 to 1,1 starting at bottom left
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(position);

        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        return (new Vector2(viewportPoint.x * canvasSize.x, viewportPoint.y * canvasSize.y) - (canvasSize / 2));
    }

    public Vector2 ScreenToCanvasPoint(Vector2 screenPosition)
    {
        Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(screenPosition);

        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        return (new Vector2(viewportPoint.x * canvasSize.x, viewportPoint.y * canvasSize.y) - (canvasSize / 2));
    }

  
}
