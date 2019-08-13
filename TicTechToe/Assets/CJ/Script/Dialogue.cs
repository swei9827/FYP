using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI npcNameDisplay;
    public string[] sentences;
    public string[] npcNames;
    public float typingSpeed;
    private int index;

    public GameObject wholeDialogue;
    public PlayerMovement player;
    public bool finishChat = false;

    public IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        //NPC Name
        foreach (char letter in npcNames[index].ToCharArray())
        {
            npcNameDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        StartCoroutine(Type());
    }

    public void NextSentence()
    {
        if(player.inChat == true)
        {
            if (index < sentences.Length - 1)
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
                player.inChat = false;
                player.canChat = false;
            }
        }
    }
}
