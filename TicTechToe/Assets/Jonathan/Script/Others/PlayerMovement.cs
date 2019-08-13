using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public List<Sprite> sp_Fish = new List<Sprite>();
    

    /* GetComponent<SpriteComponent>().Sprite = sp_Fish[Enum];
     * 
     * 
     * */

    public static bool canMove = true;
    private Vector2 direction;
    Rigidbody2D rb;
    RaycastHit2D hit;

    // CJ
    public Dialogue dialogueManager;
    public bool firstChat = false;
    public bool inChat = false;
    public bool canChat = false;

    public bool convoStarted = false;

    // Use this for initialization
    void Start()
    {
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<Dialogue>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            Move();
        }

        StartConvo();

    }

    void Move()
    {
        //Movement
        direction = Vector2.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        // If Move
        if (direction != Vector2.zero)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        //raycastPosition
        Vector2 raycastDir = new Vector2(direction.x, direction.y);
        Vector2 previousDir = Vector2.zero;
        if(raycastDir == Vector2.zero)
        {
            raycastDir = previousDir;
        }
        else
        {
            previousDir = raycastDir;
        }

        hit = Physics2D.Raycast(transform.position, direction, 1f);
        Debug.DrawRay(transform.position, direction, Color.green);

        //Flip the character
        Vector2 characterScale = transform.localScale;
        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            characterScale.x = -1.5f;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            characterScale.x = 1.5f;
        }
        transform.localScale = characterScale;
        

        // CJ,Dialogue Script

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(inChat == true)
            {
                dialogueManager.NextSentence();
            }
        }
    }

    void StartConvo()
    {
        if(canChat)
        {
            if(convoStarted == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    dialogueManager.wholeDialogue.SetActive(true);
                    inChat = true;
                    convoStarted = true;
                }
            }
        }      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("NPC"))
        {
            canChat = true;
        }
        else
        {
            canChat = false;
        }
    }

}
