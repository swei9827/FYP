﻿using System.Collections;
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
    }

    public void Pause()
    {
        if (!pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            pause.SetActive(true);
            PlayerMovement.canMove = false;
        }
        else
        {
            pauseMenu.SetActive(false);
            pause.SetActive(true);
            PlayerMovement.canMove = true;
        }
    }

    public void Back()
    {
        pause.SetActive(true);
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
    public void getBrightness(float value)
    {
        BrightnessSetting.instance.setBrightness(value);
    }
}
