using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public string name;
    private DialogueManager dialogueManager;
    public string[] dialogueLines;

    public bool interactNPCJoseph = true;
    public bool interactNPCJane = true;

    bool updateNPC = false;

    string temp;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void Update()
    {
        //updateDialogue();
        //checkNPC();     
    }

    public void checkNPC()
    {
        if (dialogueManager.NPCDone && temp == "NPC Uncle Joseph")
        {
            updateNPC = false;
            interactNPCJoseph = false;
            dialogueManager.NPCDone = false;
        }

        else if (this.gameObject.name == "NPC Jane")
        {
            if (dialogueManager.NPCDone)
            {
                interactNPCJane = false;
                dialogueManager.NPCDone = false;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (!dialogueManager.dialogueActive)
                {
                    dialogueManager.npcName = name;
                    dialogueManager.sentences = dialogueLines;
                    dialogueManager.currentLine = 0;
                    dialogueManager.showDialogue();
                }
            }    
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        temp = this.gameObject.name;
        Debug.Log(temp);

        if (collision.CompareTag("Player"))
        {
            if (temp == "NPC Uncle Joseph")
            {
                if (dialogueManager.NPCDone)
                {
                    interactNPCJoseph = true;
                    dialogueManager.NPCDone = false;
                    temp = null;
                }
            }
            else if (temp == "NPC Jane")
            {
                if (dialogueManager.NPCDone)
                {
                    interactNPCJane = true;
                    dialogueManager.NPCDone = false;
                    temp = null;
                }
            }
        }
    }
}

//public bool firstChat = false;
//public bool inChat = false;
//public bool convoStarted = false;

//public void Update()
//{
//    //StartConvo();
//}


//void StartConvo()
//{
//    if (canChat)
//    {
//        //if (Input.GetKeyDown(KeyCode.Space))
//        //{
//        //    if (!inChat)
//        //    {
//        //        PlayerMovement.canMove = false;
//        //        dialogueManager.wholeDialogue.SetActive(true);

//        //        //start conversation
//        //        dialogueManager.StartCoroutine(dialogueManager.Type());
//        //        inChat = true;
//        //    }
//        //    else
//        //    {
//        //        dialogueManager.NextSentence();
//        //        dialogue.SetActive(false);
//        //    }
//        //}
//    }
//}
