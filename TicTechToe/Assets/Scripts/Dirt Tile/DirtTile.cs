using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtTile : MonoBehaviour
{
	public Crop crop;
    public CropStateTest cropStateTest;

	public SpriteRenderer overlay;

	public bool needsPlowing = true;
    public bool addPlant = true;
    public static bool isEmpty = false;
	public Sprite extraDirt;
	public GameObject waterIndicator;

	public string onGroundLayer;
	public string normalCropLayer;

	//public bool hasCrow = false;

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
        if (t.isPlow && needsPlowing)
        {
            Plow();
            return;
        }

        if (c.HasCrop())
        {
            if (!needsPlowing)
            {
                if (temp == null)
                {
                    PlantCrop(c, player);
                    addPlant = false;
                }
            }

            else
            {
                Debug.Log("Ground needs plowing!");
            }
            return;
        }
    }

    void PlantCrop(Crop c, PlayerInteraction player)
    {
        if (c.asset.cropsType == CropsType.Strawberries)
        {
            temp = Instantiate(crops[0], this.transform.position, Quaternion.identity);
            temp.transform.SetParent(this.transform);
            temp.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            temp.GetComponent<CropTest>().planted = true;
            //player.SetCrop(new Crop(null));
        }
        else if (c.asset.cropsType == CropsType.Potatoes)
        {
            temp = Instantiate(crops[1], this.transform.position, Quaternion.identity);
            temp.transform.SetParent(this.transform);
            temp.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            temp.GetComponent<CropTest>().planted = true;
            //player.SetCrop(new Crop(null));
        }
        else if (c.asset.cropsType == CropsType.Pumpkins)
        {
            temp = Instantiate(crops[2], this.transform.position, Quaternion.identity);
            temp.transform.SetParent(this.transform);
            temp.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            temp.GetComponent<CropTest>().planted = true;
            //player.SetCrop(new Crop(null));
        }
        FxManager.PlayMusic("PlantFx");
    }

	public void AddDirt()
	{
		overlay.sprite = extraDirt;
		overlay.sortingLayerName = onGroundLayer;
	}

	void Plow ()
	{
		Debug.Log("Plowing...");
		overlay.sprite = null;
        FxManager.PlayMusic("PlowFx");
        needsPlowing = false;

        //local data record

        DataRecord.AddEvents(2, this.gameObject.name);
	}

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

        if(addPlant)
        {
            temp = null;
        }
    }

}
