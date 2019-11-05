using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefillWater : MonoBehaviour
{
    public void Interact(Tool t, PlayerInteraction player)
    {
        if (t == null)
        {
            Debug.Log("Bring your water can");
        }
        else if (t != null)
        {
            if (t.isWaterCan)
            {              
                fillWater();
                Debug.Log("Full!");
            }
            else
            {
                Debug.Log("Not watercan, please bring water can");
            }
        }
    }

    public void fillWater()
    {
        WaterCan.curFill = WaterCan.maxFill;
    }
}
