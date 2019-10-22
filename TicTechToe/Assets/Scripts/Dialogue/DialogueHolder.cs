using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public string name;
    private DialogueManager dialogueManager;
    public string[] dialogueLines;

    public bool interactNPCJoseph = false;
    public bool interactNPCJane = false;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void Update()
    {
        checkNPC();       
    }

    public void checkNPC()
    {
        if(this.gameObject.name == "NPC Uncle Joseph")
        {
            //finish conversation
            if(dialogueManager.NPCDone)
            {
                interactNPCJoseph = true;
                interactNPCJane = false;
                dialogueManager.NPCDone = false;
            }              
        }   

        else if(this.gameObject.name == "NPC Jane")
        {
            //finish conversation
            if (dialogueManager.NPCDone)
            {
                interactNPCJane = true;
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
