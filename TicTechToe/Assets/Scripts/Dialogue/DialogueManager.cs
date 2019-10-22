using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Prefab")]
    public GameObject dialogueBox;
    public GameObject nameBox;

    [Header("Dialogue Text")]
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI npcNameDisplay;

    [Header("Dialogue Settings")]
    public bool dialogueActive;
    public string npcName;
    public string[] sentences;
    public int currentLine;
    public int currentName;

    public bool NPCDone = false;

    private void Start()
    {
    }

    private void Update()
    {
        checkStatus();
    }

    void checkStatus()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;
        }

        //if finish sentences
        if(currentLine >= sentences.Length)
        {
            dialogueBox.SetActive(false);
            nameBox.SetActive(false);
            dialogueActive = false;

            currentLine = 0;
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
        nameBox.SetActive(true);
        PlayerMovement.canMove = false;
    }
}

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
