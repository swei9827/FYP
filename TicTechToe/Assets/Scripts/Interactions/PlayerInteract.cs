using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && col.gameObject != Player.LocalPlayerInstance)
        {
            col.gameObject.transform.GetChild(5).gameObject.SetActive(true);
            Debug.Log("Open UI");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.GetChild(5).gameObject.SetActive(false);
            Debug.Log("Close UI");
        }
    }
}
