using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerInteraction : MonoBehaviour
{
	public GameObject target;

	public KeyCode interactKey1;
    public KeyCode interactKey2;

	public IconBox iconBox;
    public Image waterBar;

	[SerializeField]
	private Crop crop;
	
	private Tool tool;

    //public static bool canUse = false;
    //public static bool canWater = false;

    public bool canInteract = true;

    private void Start()
    {
        waterBar.gameObject.SetActive(false);
        tool = this.gameObject.GetComponent<Tool>();
    }

    private void Update()
	{
        if(canInteract)
        {
            if (Input.GetKeyDown(interactKey1) || Input.GetKeyDown(interactKey2))
            {
                if (target == null)
                    return;

                DirtTile dirt = target.GetComponent<DirtTile>();
                if (dirt != null)
                {
                    for (int i = 0; i < tool.seeds.Length; i++)
                    {
                        if (tool.seeds[i].isSelected)
                        {
                            crop = tool.seeds[i].crop;                            
                        }
                    }
                    dirt.Interact(crop, tool, this);
                    crop = new Crop(null);
                }

                TrashCan trashcan = target.GetComponent<TrashCan>();
                {
                    if (trashcan)
                    {
                        //trashcan.Interact(crop, tool, this);
                    }
                }

                Fishing fishing = target.GetComponent<Fishing>();
                {
                    if (fishing)
                    {
                        fishing.Interact(tool, this);
                    }
                }

                RefillWater refillWater = target.GetComponent<RefillWater>();
                {
                    if (refillWater)
                    {
                        refillWater.Interact(tool, this);
                    }
                }
            }
        }
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
            //waterBar.gameObject.SetActive(false);
            iconBox.SetIcon(crop.GetCropSprite());
		}
        else if (tool != null)
		{
            if (tool.isWaterCan)
            {
                //canUse = true;
               // waterBar.gameObject.SetActive(true);
            }
            else if(!tool.isWaterCan)
            {
                //canUse = false;
               // waterBar.gameObject.SetActive(false);                      
            }       
        }
        else
		{
           // canUse = false;
           // waterBar.gameObject.SetActive(false);
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
		SpriteRenderer[] srs = target.GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer sr in srs)
		{
			sr.color = Color.white;
		}
	}

    //void checkWater()
    //{
    //    if (WaterCan.curFill == 0)
    //    {
    //        canWater = false;
    //    }
    //    else
    //    {
    //        canWater = true;
    //    }
    //}
}
