using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public static bool canMove = true;
    private Vector2 direction;
    Rigidbody2D rb;
    RaycastHit2D hit;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            Move();
        }
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
    } 
}
