using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtTile : MonoBehaviour
{
	public Crop crop;
    public CropStateTest cropStateTest;

	public SpriteRenderer overlay;

	public bool needsPlowing = true;
	public Sprite extraDirt;
	public GameObject waterIndicator;

	public string onGroundLayer;
	public string normalCropLayer;

	public bool hasCrow = false;

    public GameObject[] crops;
    public GameObject temp;

    private void Start()
	{
		if (needsPlowing)
		{
			AddDirt();
		}
	}

	public void Interact (Crop c, Tool t, PlayerInteraction player)
	{
		if (c.HasCrop())
		{
            if (!needsPlowing)
            {
                PlantCrop(c, player);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<DataRecord>().AddEvents(3, c.GetName());
                // PlantSeed(c, player);
            }

            else
            {
                Debug.Log("Ground needs plowing!");
            }
			return;
		}

		if (t != null)
		{
            if (t.toolType == ToolType.Plow && needsPlowing)
            {
                Plow();
            }
            //else if (t.toolType == ToolType.Watercan && crop.state == CropState.Planted)
            //{
            //	WaterCrop();
            //}
            else if (t.toolType == ToolType.Watercan && cropStateTest == CropStateTest.Delayed)
            {
                WaterCrops();
                GameObject.FindGameObjectWithTag("GameController").GetComponent<DataRecord>().AddEvents(4, this.name.ToString());
            }

			return;
		}

		//if (crop.HasCrop())
		//{
		//	HarvestCrop(player);
		//}
	}

    void PlantCrop(Crop c, PlayerInteraction player)
    {
        if (c.asset.cropsType == CropsType.Strawberries)
        {
            temp = Instantiate(crops[0], this.transform.position, Quaternion.identity);
            temp.transform.SetParent(this.transform);
            temp.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            temp.GetComponent<CropTest>().planted = true;
            player.SetCrop(new Crop(null));         
        }
        else if (c.asset.cropsType == CropsType.Potatoes)
        {
            temp = Instantiate(crops[1], this.transform.position, Quaternion.identity);
            temp.transform.SetParent(this.transform);
            temp.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            temp.GetComponent<CropTest>().planted = true;
            player.SetCrop(new Crop(null));
        }
        else if (c.asset.cropsType == CropsType.Pumpkins)
        {
            temp = Instantiate(crops[2], this.transform.position, Quaternion.identity);
            temp.transform.SetParent(this.transform);
            temp.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            temp.GetComponent<CropTest>().planted = true;
            player.SetCrop(new Crop(null));
        }
    }

    void WaterCrops()
    {
        if(cropStateTest == CropStateTest.Delayed)
        {
            WaterCan.curFill -= 5;
            temp.GetComponent<CropTest>().watered = true;
            waterIndicator.SetActive(false);       
        }
    }

	//void PlantSeed (Crop c, PlayerInteraction player)
	//{
	//	if (c.state != CropState.Seed)
	//	{
	//		Debug.Log("Crop not seed, can't plan't.");
	//		return;
	//	}
	//	Debug.Log("Planting " + c.GetName());
	//	crop = c;
	//	crop.state = CropState.Planted;

	//	UpdateSprite();

	//	player.SetCrop(new Crop(null));
	//}

	//void HarvestCrop (PlayerInteraction player)
	//{
	//	if (crop.state == CropState.Done || crop.state == CropState.Dead)
	//	{
 //           player.SetCrop(crop);
 //           crop = new Crop(null);        
 //           needsPlowing = true;
	//		AddDirt();
	//	}   
	//}

	//public void BirdEatsCrop()
	//{
	//	crop = new Crop(null);
	//	needsPlowing = true;
	//	AddDirt();
	//}

	public void AddDirt()
	{
		overlay.sprite = extraDirt;
		overlay.sortingLayerName = onGroundLayer;
	}

	void Plow ()
	{
		Debug.Log("Plowing...");
		overlay.sprite = null;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<DataRecord>().AddEvents(2, this.name.ToString());
        needsPlowing = false;
	}

	//void WaterCrop ()
	//{
	//	if (crop.GetWaterState() == WaterState.Dry)
	//	{
	//		crop.Water();
 //           WaterCan.curFill -= 5;
	//		UpdateSprite();
	//		waterIndicator.SetActive(false);
	//	}
	//}

	void UpdateSprite ()
	{
		overlay.sprite = crop.GetCropSprite();
		if (crop.IsOnGround())
		{
			overlay.sortingLayerName = onGroundLayer;
		} else
		{
			overlay.sortingLayerName = normalCropLayer;
		}
	}

	private void Update()
	{
		if (crop.HasCrop())
		{
			if (crop.state == CropState.Planted)
			{
				bool isDone = crop.Grow(Time.deltaTime);
				if (isDone)
				{
					UpdateSprite();
				} else
				{
					WaterState state = crop.Dry(Time.deltaTime);
					if (state == WaterState.Dry)
					{
						waterIndicator.SetActive(true);
					} else if (state == WaterState.Dead)
					{
						UpdateSprite();
						waterIndicator.SetActive(false);
					}
				}
			}
		}
	}

}
