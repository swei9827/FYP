using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public GameObject PopOut;
    public string name;
    private DialogueManager dialogueManager;
    private TutorialManager tutorialManager;
    private OpenShop openShop;

    [TextArea(3, 10)]
    public string[] dialogueLines;
    [TextArea(3, 10)]
    public string[] dialogueLines2;
    [TextArea(3, 10)]
    public string[] dialogueLines3;
    [TextArea(3, 10)]
    public string[] dialogueLines4;

    //for NPC Uncle Joseph
    public bool interactNPCJoseph = false;

    //for NPC Jane
    public bool interactNPCJane = false;
    public bool interactNPCJane2 = false;
    public bool interactNPCJane3 = false;

    public bool option1 = true;
    public bool option2 = false;
    public bool option3 = false;
    public bool option4 = false;

    //for NPC Auntie Mary
    public bool interactNPCMary = false;
    public bool interactNPCMary2 = false;

    bool updateNPC = false;
    bool interacting = true;

    public string temp;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        tutorialManager = FindObjectOfType<TutorialManager>();
        openShop = FindObjectOfType<OpenShop>();

        PopOut.SetActive(false);
    }

    public void Update()
    {

    }

    void changeDialogue()
    {
        if (option1)
        {
            dialogueManager.sentences = dialogueLines;
        }
        else if (option2)
        {
            dialogueManager.sentences = dialogueLines2;
        }
        else if (option3)
        {
            dialogueManager.sentences = dialogueLines3;
        }
        else if (option4)
        {
            dialogueManager.sentences = dialogueLines4;
        }
    }

    void UpdateDialogue()
    {
        if (dialogueManager.interactable)
        {
            if (Input.GetMouseButtonDown(0))
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

    void SetNPC()
    {
        temp = this.gameObject.name;

        //set player interaction to false
        Player.LocalPlayerInstance.GetComponent<PlayerInteraction>().canInteract = false;

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
                if (option1)
                {
                    interactNPCJane = true;
                    dialogueManager.NPCDone = false;
                    temp = null;
                    option1 = false;
                    option4 = true;
                }
                else if (option2)
                {
                    interactNPCJane2 = true;
                    dialogueManager.NPCDone = false;
                    temp = null;
                    option2 = false;
                    option4 = true;
                }
                else if (option3)
                {
                    interactNPCJane3 = true;
                    dialogueManager.NPCDone = false;
                    temp = null;
                    option3 = false;
                    option4 = true;
                }
                else if (option4)
                {
                    dialogueManager.NPCDone = false;
                    temp = null;
                }
            }
        }

        else if (temp == "NPC Harry")
        {
            if (dialogueManager.NPCDone)
            {
                dialogueManager.NPCDone = false;
                temp = null;
                dialogueManager.interactable = false;
            }
        }

        else if (temp == "NPC Auntie Mary")
        {
            if (dialogueManager.NPCDone)
            {
                if (option1)
                {
                    interactNPCMary = true;
                    dialogueManager.NPCDone = false;
                    temp = null;
                    option1 = false;
                    option3 = true;
                }
                else if (option2)
                {
                    interactNPCMary2 = true;
                    dialogueManager.NPCDone = false;
                    temp = null;
                    option2 = false;
                    option3 = true;
                }
                else if (option3)
                {
                    dialogueManager.NPCDone = false;
                    temp = null;
                }
            }
        }
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject == Player.LocalPlayerInstance)
    //    {
    //        PopOut.SetActive(true);

    //        //Star dialogue
    //        UpdateDialogue();
    //        SetNPC();
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == Player.LocalPlayerInstance)
        {
            PopOut.SetActive(true);

            //Star dialogue
            UpdateDialogue();
            SetNPC();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Player.LocalPlayerInstance)
        {
            PopOut.SetActive(false);
            Player.LocalPlayerInstance.GetComponent<PlayerInteraction>().canInteract = true;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject == Player.LocalPlayerInstance)
    //    {
    //        PopOut.SetActive(true);

    //        //Star dialogue
    //        UpdateDialogue();
    //        SetNPC();
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject == Player.LocalPlayerInstance)
    //    {
    //        PopOut.SetActive(false);
    //        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>().canInteract = true;
    //    }
    //}
}
