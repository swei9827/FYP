using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBarrel : MonoBehaviour
{
	public Crop crop;
    public CropAsset asset;

    public IconBox iconBox;

	public void Interact(Crop c, Tool t, PlayerInteraction player)
	{
		if (t == null && !c.HasCrop())
		{
            if(asset.amount > 0)
            {
                Debug.Log("Taking " + crop.GetName());
                player.SetCrop(new Crop(crop.asset));
                asset.amount -= 1;
            }
            else if(asset.amount <= 0)
            {
                Debug.Log("Not enough " + crop.GetName());
            }
                      
            Debug.Log(asset.amount);
        }

        if(c.HasCrop())
        {
            if(c.state != CropState.Seed)
            {
                Debug.Log("It is not a seed, go find trach can");
            }
            else
            {
                Debug.Log("Put back " + crop.GetName());
                player.SetCrop(new Crop(null));
                c.asset.amount += 1;
                Debug.Log(asset.amount);
            }         
        }
	}

	public void Select()
	{
		iconBox.SetIcon(crop.GetDoneSprite());
	}

	public void DeSelect()
	{
		iconBox.Close();
	}
}
