using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public string name;
    private DialogueManager dialogueManager;
    private TutorialManager tutorialManager;
    private OpenShop openShop;

    [TextArea(3, 10)]
    public string[] dialogueLines;
    [TextArea(3, 10)]
    public string[] dialogueLines2;

    public bool interactNPCJoseph = true;
    public bool interactNPCJane = true;

    bool updateNPC = false;
    bool interacting = true;

    bool option1 = true;
    bool option2 = false;
    bool option3 = false;

    public string temp;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        tutorialManager = FindObjectOfType<TutorialManager>();
        openShop = FindObjectOfType<OpenShop>();
    }

    public void Update()
    {

    }

    void changeDialogue()
    {
        if(option1)
        {
            dialogueManager.sentences = dialogueLines;
        }
        else if(option2)
        {
            dialogueManager.sentences = dialogueLines2;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if(dialogueManager.interactable)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (!dialogueManager.dialogueActive && !dialogueManager.canInteract)
                    {                     
                        dialogueManager.npcName = name;
                        changeDialogue();

                        //set dialogue line to 0
                        dialogueManager.currentLine = -1;

                        dialogueManager.showDialogue();
                        dialogueManager.canInteract = true;
                                  
                    }
                }
            }          
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        temp = this.gameObject.name;

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
                    if(option1)
                    {
                        interactNPCJane = true;
                        dialogueManager.NPCDone = false;
                        temp = null;
                        option1 = false;
                        option2 = true;
                    }
                    else if(option2)
                    {
                        dialogueManager.NPCDone = false;
                        temp = null;
                        option1 = true;
                        option2 = false;                       
                    }
                }
            }
            else if (temp == "NPC Henry")
            {
                if (dialogueManager.NPCDone)
                {
                    dialogueManager.NPCDone = false;
                    temp = null;
                    dialogueManager.interactable = false;
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
