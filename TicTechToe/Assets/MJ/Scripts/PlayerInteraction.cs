using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	public GameObject target;

	public KeyCode interactKey;

	public IconBox iconBox;
    public InventoryUI itemBox;

	[SerializeField]
	private Crop crop;
	[SerializeField]
	private Tool tool;

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
            iconBox.SetIcon(crop.GetCropSprite());
		} else if (tool != null)
		{
			iconBox.SetIcon(tool.sprite);
		} else
		{
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

}
