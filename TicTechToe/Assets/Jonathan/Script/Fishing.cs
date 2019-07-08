using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public void Interact(Crop c, Tool t, PlayerInteraction player)
    {
        if(t == null)
        {
            Debug.Log("Use fishing rod");
        }
    }

}
