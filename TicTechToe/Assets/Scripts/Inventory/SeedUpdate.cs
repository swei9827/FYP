using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedUpdate : MonoBehaviour
{
    public Text text;
    public Seed seed;
    public int i;

    private void Start()
    {
        seed = Player.LocalPlayerInstance.GetComponent<Tool>().seeds[i];    
    }

    void Update()
    {
        text.text = seed.amount.ToString();
    }
}
