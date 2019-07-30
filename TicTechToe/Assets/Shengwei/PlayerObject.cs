using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public GameObject PlayerUnitPrefab;
    
    void Start()
    {
        Instantiate(PlayerUnitPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
