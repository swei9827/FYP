using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
	public GameObject target;
    public GameObject Quests;

	public KeyCode interactKey;

	public IconBox iconBox;
    public Image waterIndicator;

	[SerializeField]
	private Crop crop;
	[SerializeField]
	private Tool tool;

    public static bool canUse = false;
    public static bool canWater = false;


    // CJ
    public Dialogue dialogueManager;
    public bool firstChat = false;
    public bool inChat = false;
    public bool canChat = false;
    public bool convoStarted = false;

    private void Start()
    {
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<Dialogue>();
        waterIndicator.enabled = false;   
    }

    private void Update()
	{
		if (Input.GetKeyDown(interactKey))
		{
			if (target == null)
				return;

			DirtTile dirt = target.GetComponent<DirtTile>();
			if (dirt != null)
			{
				dirt.Interact(crop, tool, this);
			}

			TableTile table = target.GetComponent<TableTile>();
			if (table != null)
			{
				table.Interact(crop, tool, this);
            }

			SeedBarrel barrel = target.GetComponent<SeedBarrel>();
			if (barrel != null)
			{
				barrel.Interact(crop, tool, this);
            }

            TrashCan trashcan = target.GetComponent<TrashCan>();
            {
                if(trashcan)
                {
                    trashcan.Interact(crop,tool, this);
                }
            }

            Fishing fishing = target.GetComponent<Fishing>();
            {
                if(fishing)
                {
                    fishing.Interact(tool,this);
                }
            }

            RefillWater refillWater = target.GetComponent<RefillWater>();
            {
                if(refillWater)
                {
                    refillWater.Interact(tool, this);
                }
            }
		}

        checkWater();
        StartConvo();
    }

    public void SetCrop(Crop c)
	{
		crop = c;
		DisplayInventory();
	}

	public void SetTool(Tool t)
	{
		tool = t;
		DisplayInventory();
	}

	void DisplayInventory ()
	{
		if (crop.HasCrop())
		{
            waterIndicator.enabled = false;
            iconBox.SetIcon(crop.GetCropSprite());
		}
        else if (tool != null)
		{
            if (tool.toolType == ToolType.Watercan)
            {
                canUse = true;
                waterIndicator.enabled = true;                
                iconBox.SetIcon(tool.sprite);             
            }
            else if(tool.toolType != ToolType.Watercan)
            {
                canUse = false;
                waterIndicator.enabled = false;
                iconBox.SetIcon(tool.sprite);            
            }       
        }
        else
		{
            canUse = false;
            waterIndicator.enabled = false;
            iconBox.Close();
		}

       
    }

	private void OnTriggerStay2D(Collider2D col)
	{
		if (target != col.gameObject && target != null)
		{
			Deselect();
		}

		target = col.gameObject;

		SeedBarrel barrel = target.GetComponent<SeedBarrel>();
		if (barrel != null)
		{
			barrel.Select();
		}

		SpriteRenderer[] srs = target.GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer sr in srs)
		{
			sr.color = new Color(1f, 1f, 1f, 0.9f);
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject == target)
		{
			Deselect();
			target = null;
		}
	}

	void Deselect()
	{
		SeedBarrel barrel = target.GetComponent<SeedBarrel>();
		if (barrel != null)
		{
			barrel.DeSelect();
		}

		SpriteRenderer[] srs = target.GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer sr in srs)
		{
			sr.color = Color.white;
		}
	}

    void checkWater()
    {
        if (WaterCan.curFill == 0)
        {
            canWater = false;
        }
        else
        {
            canWater = true;
        }

    }

    void StartConvo()
    {
        if (canChat)
        {         
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!inChat)
                {
                    PlayerMovement.canMove = false;
                    dialogueManager.wholeDialogue.SetActive(true);
                    dialogueManager.inventory.SetActive(false);

                    //start conversation
                    dialogueManager.StartCoroutine(dialogueManager.Type());
                    inChat = true;
                }
                else
                {
                    dialogueManager.NextSentence();
                }
            }       
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("NPC"))
        {
            canChat = true;
            Quests.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("NPC"))
        {
            canChat = false;
            Quests.SetActive(false);
        }
    }

}
