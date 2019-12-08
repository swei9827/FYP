using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Prefab")]
    public GameObject dialogueBox;
    public GameObject continueButton;

    [Header("Dialogue Text")]
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI npcNameDisplay;

    [Header("Dialogue Settings")]
    public bool dialogueActive;
    public string npcName;

    [TextArea (3,10)]
    public string[] sentences;

    [TextArea(3, 10)]
    public string[] sentences2;

    public float textSpeed;
    public int currentLine;
    public int currentName;

    public bool NPCDone = false;
    public bool canInteract = false;
    public bool interactable = true;

    private void Start()
    {

    }

    private void Update()
    {
        //if (textDisplay.text == sentences[currentLine])
        //{
        //    continueButton.SetActive(true);
        //}

        updateStatus();
    }

    public void updateStatus()
    {
        if(interactable)
        {
            if (canInteract)
            {
                if (dialogueActive && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    currentLine++;
                }
            }
        }
     
        //if finish sentences
        if (currentLine >= sentences.Length)
        {
            dialogueBox.SetActive(false);

            currentLine = 0;

            dialogueActive = false;
            canInteract = false;

            NPCDone = true;
            PlayerMovement.canMove = true;
        }

        npcNameDisplay.text = npcName;
        textDisplay.text = sentences[currentLine];    
    }

    public void showDialogue()
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
        PlayerMovement.canMove = false;
    }

    IEnumerator Type()
    {      
        foreach (char letter in sentences[currentLine].ToCharArray())
        {
            textDisplay.text += letter;          
            yield return new WaitForSeconds(textSpeed);
        }
    }
}

/////for couroutine
//if (dialogueActive)
//{
//    //if (continueButton.activeInHierarchy && Input.GetKeyDown(KeyCode.Mouse0))
//    //{
//    //    continueButton.SetActive(false);
//    //    currentLine++;
//    //    //textDisplay.text = "";
//    //    //StartCoroutine(Type());
//    //}
//}


//public float typingSpeed;
//private int index;

//public NPCinteraction player;
//public bool finishChat = false;

//public static bool completeTask1 = false;
//public static bool completeTask2 = false;

//public IEnumerator Type()
//{     
//    foreach (char letter in sentences[index].ToCharArray())
//    {
//        textDisplay.text += letter;
//        yield return new WaitForSeconds(typingSpeed);
//    }

//}

//void Start()
//{
//    player = GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCinteraction>();
//}

//private void Update()
//{
//    //npcNameDisplay.text = "George";
//}


//public void setIndex(int i)
//{
//    index = i;
//}

//public int getIndex()
//{
//    return index;
//}

//public void NextSentence()
//{
//    if(player.inChat)
//    {
//        if (index == 0  && !completeTask1)
//        {
//            index++;
//            textDisplay.text = "";
//            npcNameDisplay.text = "";
//            StartCoroutine(Type());
//        }
//        else if(index == 3 ||index == 4 && completeTask1)
//        {
//            index++;
//            textDisplay.text = "";
//            npcNameDisplay.text = "";
//            StartCoroutine(Type());
//        }
//        else if(!completeTask1 || ! completeTask2)
//        {
//            index = 2;
//            textDisplay.text = "";
//            npcNameDisplay.text = "";
//            dialogueBox.SetActive(false);
//            player.inChat = false;
//            player.canChat = false;
//            PlayerMovement.canMove = true;           
//        }
//        else
//        {
//            index = 6;
//            textDisplay.text = "";
//            npcNameDisplay.text = "";
//            dialogueBox.SetActive(false);
//            player.inChat = false;
//            player.canChat = false;
//            PlayerMovement.canMove = true;
//            SceneControl.completeAllTasks = true;
//            SceneManager.LoadScene(3);
//            completeTask1 = false;
//            completeTask2 = false;
//        }
//    }
//}
