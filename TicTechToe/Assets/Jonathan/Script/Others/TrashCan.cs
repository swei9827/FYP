using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public void Interact(Crop c, Tool t, PlayerInteraction player)
    {
        if(c != null)
        {
            if(c.state !=CropState.Seed)
            {
                player.SetCrop(new Crop(null));
                Debug.Log("Remove crops!");
            }       
            else
            {
                Debug.Log("Put back seed to seed barrel");
            }
        }

        else if (t != null)
        {
            Debug.Log("Cannot throw tools");
        }
    }
}
