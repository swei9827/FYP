using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMChanger : MonoBehaviour
{
    public bool MainMenu;
    public bool InGame;
    void Start()
    {
        if (MainMenu)
        {
            BGMManager.StopMusic("BGM");
            BGMManager.PlayMusic("MainMenuBGM");
        }
        else if (InGame)
        {
            BGMManager.StopMusic("MainMenuBGM");
            BGMManager.PlayMusic("BGM");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
