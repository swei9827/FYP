using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pause;

    public AudioMixer audioMixer;

    public void Resume()
    {
        pauseMenu.SetActive(false);
        pause.SetActive(true);
        PlayerMovement.canMove = true;
        Time.timeScale = 1;
    }

    public void Pause()
    {
        if (!pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            pause.SetActive(true);
            PlayerMovement.canMove = false;
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            pause.SetActive(true);
            PlayerMovement.canMove = true;
            Time.timeScale = 1;
        }
    }

    public void Back()
    {
        pause.SetActive(true);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("New Lobby");
    }

    //set audio
    public void setVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }

    public void setBGM(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void setSFX(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    //set brightness
    public void setBrightness(float value)
    {

    }
}
