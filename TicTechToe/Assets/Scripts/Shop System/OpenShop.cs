using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    public GameObject shop;
    public GameObject dialogueBox;
    public bool openShop = false;

    private DialogueManager dialogueManager;
    private DialogueHolder dialogueHolder;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!dialogueManager.interactable)
        {
            shop.SetActive(true);
            dialogueBox.SetActive(false);
            PlayerMovement.canMove = false;
        }
    }

    public void OnClick()
    {
        dialogueManager.interactable = true;
        shop.SetActive(false);
        PlayerMovement.canMove = true;
    }
}
