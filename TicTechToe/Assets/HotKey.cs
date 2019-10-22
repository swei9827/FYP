using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HotKey : MonoBehaviour
{
    [Header("Button Settings")]
    public Button btn;

    bool isClick = false;
    bool canSelect = true;

    [Header("Sprite Settings")]
    public Sprite disableSprite;
    public Sprite pressSprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleHighlight()
    {
        isClick = !isClick;
        
        if(isClick)
        {
            Debug.Log(btn.name);
        }
    }

}
