using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    public float speed;
    public static bool canMove = true;
    private Vector2 direction;
    Rigidbody2D rb;
    RaycastHit2D hit;

    [SyncVar]
    Vector3 serverPosition;
    Vector3 serverPositionSmoothVelocity;
    Vector3 serverScale;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        serverScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {

        }

        if (hasAuthority)
        {
            if (canMove)
            {
                AuthorityUpdate();
            }
            
        }
        else if (hasAuthority == false)
        {
            transform.position = Vector3.SmoothDamp(transform.position, serverPosition, ref serverPositionSmoothVelocity, 0.25f);
            transform.localScale = serverScale;
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
        Vector3 characterScale = transform.localScale;
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

    void AuthorityUpdate()
    {
        Move();
        CmdUpdatePosition(transform.position);
        CmdUpdateScale(transform.localScale);
    }

    [Command]
    void CmdUpdatePosition(Vector3 newPosition)
    {
        serverPosition = newPosition;
    }

    [Command]
    void CmdUpdateScale(Vector3 newScale)
    {
        serverScale = newScale;
    }

    [ClientRpc]
    void RpcFixPosition(Vector3 newPosition)
    {
        //if the client tried to move char in some kind of illegal manner.
        transform.position = newPosition;
    }
}
