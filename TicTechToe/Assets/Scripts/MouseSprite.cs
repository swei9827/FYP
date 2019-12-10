using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseSprite : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D hoverCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public bool onCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(defaultCursor, hotSpot, cursorMode);
    }

    //for UI
    public void OnMouseEnter()
    {
        Cursor.SetCursor(hoverCursor, hotSpot, cursorMode);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(defaultCursor, hotSpot, cursorMode);
    }

    //for GameObject
    public void GameObjectMouseEnter()
    {
        if(onCollision)
        {
            Cursor.SetCursor(hoverCursor, hotSpot, cursorMode);
        }
    }

    public void GameObjectMouseExit()
    {
        Cursor.SetCursor(defaultCursor, hotSpot, cursorMode);
    }
}
