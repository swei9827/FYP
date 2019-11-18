using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player Settings
    public float speed;
    public static bool canMove = true;
    private bool isMoving = false;

    //set input key
    private bool keyboardMove = false;
    private bool mouseMove = false;
  
    Rigidbody2D rb;
    RaycastHit2D hit;

    //Animation
    private Animator animator;

    //Player movement
    private Vector3 target;
    private Vector2 direction;

    // Use this for initialization
    void Start()
    {
        canMove = true;
        keyboardMove = false;
        mouseMove = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            if(Input.GetKey(KeyCode.K))
            {
                keyboardMove = true;
                mouseMove = false;
            }
            else if (Input.GetKey(KeyCode.M))
            {
                mouseMove = true;
                keyboardMove = false;
            }

            if (keyboardMove)
            {
                keyboardMovement();
            }
            else if(mouseMove)
            {
                clickMovement();
            }             
        }
    }

    void keyboardMovement()
    {
        // Keyboard Movement
        direction = Vector2.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        transform.Translate(direction * speed * Time.deltaTime);
        UpdateAnimation();
        UpdateSFX();
    } 

    void clickMovement()
    {
        // Mouse Movement
        if (Input.GetMouseButton(1))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
            isMoving = true;
        }
        else
        {
            direction = Vector2.zero;
            isMoving = false;
        }

        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        UpdateAnimation();
        UpdateSFX();
    }

    void UpdateAnimation()
    {  
        //if Move
        if (direction != Vector2.zero)
        {
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
            animator.SetBool("Moving", true);

        }
        else
        {
            //Set animation
            animator.SetBool("Moving", false);
        }

    }

    void UpdateSFX()
    {
        if (direction != Vector2.zero)
        {
            if (FxManager.instance.GetComponent<AudioSource>().isPlaying == false)
            {
                FxManager.PlayMusic("WalkFx");
            }
        }
        else
        {
            FxManager.StopMusic("WalkFx");
        }
    }
}


//void flip()
//{
//    //Flip the character
//    Vector2 characterScale = transform.localScale;
//    target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//    if (Input.GetAxisRaw("Horizontal") < 0 || target.x < transform.position.x)
//    {
//        characterScale.x = -1.5f;
//    }
//    if (Input.GetAxisRaw("Horizontal") > 0 || target.x > transform.position.x)
//    {
//        characterScale.x = 1.5f;
//    }
//    transform.localScale = characterScale;
//}


////raycastPosition
//Vector2 raycastDir = new Vector2(direction.x, direction.y);
//Vector2 previousDir = Vector2.zero;
//        if(raycastDir == Vector2.zero)
//        {
//            raycastDir = previousDir;
//        }
//        else
//        {
//            previousDir = raycastDir;
//        }

//        hit = Physics2D.Raycast(transform.position, direction, 1f);
//        Debug.DrawRay(transform.position, direction, Color.green);   
