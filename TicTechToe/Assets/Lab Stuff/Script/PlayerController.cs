using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    Vector2 direction;
    
    void Update()
    {    
        direction = Vector2.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if (direction != Vector2.zero)
        {
            transform.Translate(direction* speed * Time.deltaTime);
        }
    }
} 
   
