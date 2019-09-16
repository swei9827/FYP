using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TutorialManager : MonoBehaviour
{
    //Tutorial
    public GameObject[] TutorialPopOut;
    private int popUpIndex;

    //set timer for player to move
    private float timer = 1f;

    //Set player interact
    bool playerAction = false;

    public Fishing fish;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAction = true;
        }

        ChangeTutorial();
    }

    void ChangeTutorial()
    {
        for (int i = 0; i < TutorialPopOut.Length; i++)
        {
            if (i == popUpIndex)
            {
                Time.timeScale = 0;
                TutorialPopOut[popUpIndex].SetActive(true);
            }
        }

        //First Tutorial
        if (popUpIndex == 0)
        {
            if (playerAction)
            {
                timer -= Time.deltaTime;
                Time.timeScale = 1;
                TutorialPopOut[popUpIndex].SetActive(false);
                if (timer <= 0)
                {
                    popUpIndex++;
                    playerAction = false;

                    //set for next tutorial
                    timer = 5f;
                }
            }
        }

        //player movement
        if (popUpIndex == 1)
        {
            if (playerAction)
            {
                timer -= Time.deltaTime;
                Time.timeScale = 1;
                TutorialPopOut[popUpIndex].SetActive(false);

                if (timer <= 0)
                {
                    popUpIndex++;
                    playerAction = false;
                    timer = 5f;
                }
            }
        }

        // plow 
        else if (popUpIndex == 2)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                if (!GameObject.Find("DirtTile").GetComponent<DirtTile>().needsPlowing)
                {
                    popUpIndex++;
                    playerAction = false;
                }
            }
        }

        //plant seed
        else if (popUpIndex == 3)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                if (!DirtTile.addPlant)
                {
                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        //Water plant
        else if (popUpIndex == 4)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                if (GameObject.FindGameObjectWithTag("Crops").GetComponent<CropTest>().watered)
                {
                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        // harvest
        else if (popUpIndex == 5)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                if (GameObject.FindGameObjectWithTag("Crops").GetComponent<GetItems>().canGetCrops)
                {
                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        //fishing
        else if (popUpIndex == 6)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                if (GameObject.Find("Tilemap_River").GetComponent<Fishing>().success)
                {
                    playerAction = false;
                    popUpIndex++;
                }
            }
        }

        else if(popUpIndex == 7)
        {
            if (playerAction)
            {
                TutorialPopOut[popUpIndex].SetActive(false);
                Time.timeScale = 1;

                playerAction = false;
                popUpIndex++;
            }
        }
    }  
}

