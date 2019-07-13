using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBarrel : MonoBehaviour
{
	public Crop crop;

	public IconBox iconBox;

	public void Interact(Crop c, Tool t, PlayerInteraction player)
	{
		if (t == null && !c.HasCrop())
		{
			Debug.Log("Taking " + crop.GetName());
			player.SetCrop(new Crop(crop.asset));
            GameObject.FindGameObjectWithTag("DataRecorder").GetComponent<DataRecord>().AddEvents(0, crop.GetName().ToString());
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
                GameObject.FindGameObjectWithTag("DataRecorder").GetComponent<DataRecord>().AddEvents(1, crop.GetName().ToString());
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
