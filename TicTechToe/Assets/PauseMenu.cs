using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool pauseGame;

    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PopPauseMenu()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        pauseGame = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        pauseGame = true;
    }

    public void loadMenu()
    {

    }

    public void QuitGame()
    {

    }


}
