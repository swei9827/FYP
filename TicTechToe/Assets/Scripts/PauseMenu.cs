using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pause;
    public GameObject settings;

    public void Resume()
    {
        pauseMenu.SetActive(false);
        pause.SetActive(true);
        settings.SetActive(false);
        PlayerMovement.canMove = true;
        Time.timeScale = 1;
    }

    public void Pause()
    {
        if (!pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            pause.SetActive(true);
            settings.SetActive(false);
            PlayerMovement.canMove = false;
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            pause.SetActive(true);
            settings.SetActive(false);
            PlayerMovement.canMove = true;
            Time.timeScale = 1;
        }
    }

    public void Settings()
    {
        pause.SetActive(false);
        settings.SetActive(true);
    }

    public void Back()
    {
        pause.SetActive(true);
        settings.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("New Lobby");
    }
}
