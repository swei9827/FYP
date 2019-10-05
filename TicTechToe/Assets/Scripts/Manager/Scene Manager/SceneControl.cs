using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SceneControl : MonoBehaviour
{
    public bool isTutorial;
    public bool isGame;
    public bool isMainMenu;
    public static  bool completeAllTasks;
    public Canvas analyticCanvas;

    public void PlayButton()
    {
        SceneManager.LoadScene("Lab_Login");
        DataRecord.HarvestCount = 0;
        DataRecord.FishCount = 0;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void AnalyticButton()
    {
        analyticCanvas.gameObject.SetActive(true);
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
        if (isMainMenu)
        {
            if(Input.anyKey && analyticCanvas.isActiveAndEnabled)
            {
                analyticCanvas.gameObject.SetActive(false);
            }
        }

        if (isTutorial)
        {
            if(Input.anyKey)
            {
                SceneManager.LoadScene("Lab_Lobby");
            }
        }

        if(isGame)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(GameObject.FindGameObjectWithTag("NPCManager"));
                PhotonNetwork.LeaveRoom();
                SceneManager.LoadScene("Lab_Lobby");
                RoomController.playerSpawned = false;
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
