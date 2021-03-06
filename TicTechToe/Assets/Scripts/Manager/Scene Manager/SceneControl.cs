﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SceneControl : MonoBehaviour
{
    public bool isTutorial;
    public bool isGame;
    public bool isMainMenu;
    public bool isSignUp;
    public static  bool completeAllTasks;


    public void PlayButton()
    {
        SceneManager.LoadScene("New Login");
    }

    public void ExitButton()
    {
        Application.Quit();
    }


    public void signUpButton()
    {
        SceneManager.LoadScene("Sign Up");
    }

    void Start()
    {
        if (isMainMenu)
        {
            BGMManager.Scene_MainMenu = true;
            BGMManager.Scene_InGame = false;
            BGMManager.isPlaying = false;
            RoomController.playerSpawned = false;
        }
        else if (isGame)
        {
            BGMManager.Scene_MainMenu = false;
            BGMManager.Scene_InGame = true;
            BGMManager.isPlaying = false;
        }
    }

    public void Update()
    {
        if (isTutorial)
        {
            if(Input.anyKey)
            {
                SceneManager.LoadScene("New Lobby");
            }
        }

        if(isGame)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(GameObject.FindGameObjectWithTag("NPCManager"));
                PhotonNetwork.LeaveRoom();
                SceneManager.LoadScene("New Lobby");
                RoomController.playerSpawned = false;
            }          
        }

        if(isSignUp)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ActivitiesAnalytic"))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ToBeContinue"))
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
