﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Transform canvas;

    //Inventory
    public Transform inventory;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        //Initialize Canvas
        canvas = GameObject.Find("Canvas").transform;

        //Initialize Inventory
        inventory = canvas.Find("Inventory");
    }

    public Vector2 WorldToCanvasPoint(Vector3 position)
    {
        //First get the position to viewport coordinates.
        //viewport point goes from 0,0 to 1,1 starting at bottom left
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(position);

        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        return (new Vector2(viewportPoint.x * canvasSize.x, viewportPoint.y * canvasSize.y) - (canvasSize / 2));
    }

    public Vector2 ScreenToCanvasPoint(Vector2 screenPosition)
    {
        Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(screenPosition);

        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        return (new Vector2(viewportPoint.x * canvasSize.x, viewportPoint.y * canvasSize.y) - (canvasSize / 2));
    }

    public void ToggleInventory()
    {
        inventory.gameObject.SetActive(!inventory.gameObject.activeInHierarchy);
        if(!inventory.gameObject.activeSelf)
        {
            PlayerMovement.canMove = true;
        }
        else
        {
            PlayerMovement.canMove = false;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }
}