using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCinteraction : MonoBehaviour
{
    public GameObject Quests;

    // CJ
    public Dialogue dialogueManager;
    public bool firstChat = false;
    public bool inChat = false;
    public bool canChat = false;
    public bool convoStarted = false;

    private void Start()
    {
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<Dialogue>();
    }

    public void Update()
    {
        StartConvo();
    }

    void StartConvo()
    {
        if (canChat)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!inChat)
                {
                    PlayerMovement.canMove = false;
                    dialogueManager.wholeDialogue.SetActive(true);
                    dialogueManager.inventory.SetActive(false);

                    //start conversation
                    dialogueManager.StartCoroutine(dialogueManager.Type());
                    inChat = true;
                }
                else
                {
                    dialogueManager.NextSentence();
                    Quests.SetActive(false);
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            canChat = true;
            Quests.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            canChat = false;
            Quests.SetActive(false);
        }
    }
}
