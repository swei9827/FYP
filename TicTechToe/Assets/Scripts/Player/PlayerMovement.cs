using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public static bool canMove = true;
    public bool isMoving = false;
    private Vector2 direction;
    Rigidbody2D rb;
    RaycastHit2D hit;

    private Vector3 target;

    // Use this for initialization
    void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();      
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            keyboardMove();
            clickMove();
            flip();
        }
    }

    void keyboardMove()
    {
        // Keyboard Movement
        direction = Vector2.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        // If Move
        if (direction != Vector2.zero)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            if(FxManager.instance.GetComponent<AudioSource>().isPlaying == false)
            {
                FxManager.PlayMusic("WalkFx");
            }
        }
        else
        {
            FxManager.StopMusic("WalkFx");
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
    } 

    void clickMove()
    {
        // Mouse Movement
        if (Input.GetMouseButton(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;   
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    void flip()
    {
        //Flip the character
        Vector2 characterScale = transform.localScale;
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetAxisRaw("Horizontal") < 0 || target.x < transform.position.x)
        {
            characterScale.x = -1.5f;
        }
        if (Input.GetAxisRaw("Horizontal") > 0 || target.x > transform.position.x)
        {
            characterScale.x = 1.5f;
        }
        transform.localScale = characterScale;
    }
}
