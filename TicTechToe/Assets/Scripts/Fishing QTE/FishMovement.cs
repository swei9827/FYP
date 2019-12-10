using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishMovement : MonoBehaviour
{
    //script
    Fishing fishGame;

    public Image fishImage;

    public RectTransform bucket;
    public RectTransform water;

    Vector2 spawnPos;
    Rigidbody2D fish;
    public float speed;

    //set limit of bouncing fish
    private float maxRightLimit;
    private float maxLeftLimit;
    private float rightLimit = 30;
    private float leftLimit = -30;
    private bool dirRight = false;

    private float maxTime = 1f;
    private float countTime;

    // Start is called before the first frame update
    void Start()
    {    
        fish = this.GetComponent<Rigidbody2D>();
        fishGame = GameObject.Find("Tilemap_Water").GetComponent<Fishing>();
    }

    void Update()
    {
        CheckCollision();
        BounceOutBorder();
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
            bucket.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            fishImage.rectTransform.localScale = new Vector3(-1, 1, 1);
            fish.AddForce(new Vector2(-240, 1040), ForceMode2D.Impulse);
            bucket.GetComponent<BoxCollider2D>().enabled = false;           
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
            bucket.GetComponent<BoxCollider2D>().enabled = false;

        }
        else
        {
            fishImage.rectTransform.localScale = new Vector3(-1, 1, 1);
            fish.AddForce(new Vector2(-240, 900), ForceMode2D.Impulse);
            bucket.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void CheckCollision()
    {
        if (!bucket.GetComponent<BoxCollider2D>().enabled)
        {
            countTime += Time.deltaTime;
        }

        if (countTime >= maxTime)
        {
            countTime = 0;
            bucket.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void BounceOutBorder()
    {
        if (transform.localPosition.y < -300 || transform.localPosition.y > 500)
        {
            fishGame.waterHit = fishGame.hitWaterAmount;
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
