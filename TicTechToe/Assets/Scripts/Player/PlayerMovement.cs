using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player Settings
    public float speed;
    public static bool canMove;
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
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            clickMovement();
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
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            //isMoving = true;
        }
        else
        {
            direction = Vector2.zero;
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

