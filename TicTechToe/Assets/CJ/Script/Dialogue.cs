using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject inventory;

    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI npcNameDisplay;
    public string[] sentences;
    public string[] npcNames;
    public float typingSpeed;
    private int index;

    public GameObject wholeDialogue;
    public PlayerInteraction player;
    public bool finishChat = false;

    public IEnumerator Type()
    {     
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
    }

    private void Update()
    {
         npcNameDisplay.text = "George";
    }

    public void NextSentence()
    {
        if(player.inChat)
        {
            if (index == 0 || index == 3)
            {
                index++;
                textDisplay.text = "";
                npcNameDisplay.text = "";
                StartCoroutine(Type());
            }
            else
            {
                textDisplay.text = "";
                npcNameDisplay.text = "";
                wholeDialogue.SetActive(false);
                inventory.SetActive(true);
                player.inChat = false;
                player.canChat = false;
                PlayerMovement.canMove = true;

                //set dialogue index
                index = 2;
            }
        }
    }
}
