using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public bool isTutorial;
    public bool isGame;

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void Update()
    {
        if (isTutorial)
        {
            if(Input.anyKey)
            {
                SceneManager.LoadScene(2);
            }
        }

        if(isGame)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
