using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishMovement : MonoBehaviour
{
    //script
    Fishing fishGame;

    public Image fishImage;
    Vector2 spawnPos;
    Rigidbody2D fish;
    public float speed;

    //set limit of bouncing fish
    private float maxRightLimit;
    private float maxLeftLimit;
    private float rightLimit = 30;
    private float leftLimit = -30;
    private bool dirRight = false;
    // Start is called before the first frame update
    void Start()
    {    
        fish = this.GetComponent<Rigidbody2D>();
        fishGame = GameObject.Find("Tilemap_Water").GetComponent<Fishing>();
    }

    void WaterBounceMove()
    {
        maxRightLimit = Random.Range(rightLimit, rightLimit * 10);
        maxLeftLimit = Random.Range(leftLimit, leftLimit * 10);

        if (transform.localPosition.x >= maxRightLimit)
        {
            dirRight = false;
            
        }
        else if (transform.localPosition.x <= maxLeftLimit)
        {
            dirRight = true;
           
        }

        if (dirRight)
        {
            fishImage.rectTransform.localScale = new Vector3(1, 1, 1);
            fish.AddForce(new Vector2(240, 1040), ForceMode2D.Impulse);           
        }
        else
        {
            fishImage.rectTransform.localScale = new Vector3(-1, 1, 1);
            fish.AddForce(new Vector2(-240, 1040), ForceMode2D.Impulse);           
        }
    }

    void BounceMove()
    {
        maxRightLimit = Random.Range(rightLimit, rightLimit * 10);
        maxLeftLimit = Random.Range(leftLimit, leftLimit * 10);

        if (transform.localPosition.x >= maxRightLimit)
        {
            dirRight = false;
        }
        else if (transform.localPosition.x <= maxLeftLimit)
        {
            dirRight = true;
        }

        if (dirRight)
        {
            fishImage.rectTransform.localScale = new Vector3(1, 1, 1);
            fish.AddForce(new Vector2(240, 900), ForceMode2D.Impulse);
        }
        else
        {
            fishImage.rectTransform.localScale = new Vector3(-1, 1, 1);
            fish.AddForce(new Vector2(-240, 900), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Water") )
        {
            WaterBounceMove();
            fishGame.waterHit += 1;
        }
        else if(other.collider.CompareTag("Player"))
        {
            BounceMove();
            fishGame.bucketHit += 1;
            
        }
    }
}
