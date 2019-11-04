using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BucketMovement: MonoBehaviour
{
    public RectTransform panel;
    public RectTransform bucket;
    public float speed;
    private Vector2 direction;
    private Vector2 clickDirection;

    private float minX = -600;
    private float maxX = 600;

    void Start()
    {
        bucket = gameObject.GetComponent<RectTransform>();
        panel = GameObject.Find("FishingGame").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        keyboardBucketMove();
        mouseBucketMovement();
        bucketMovementLimit();
    }

    void keyboardBucketMove()
    {
        direction = Vector2.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
   
        if (direction != Vector2.zero)
        {
            transform.Translate(direction * 50 * speed * Time.deltaTime);
        }   
    }

    void mouseBucketMovement()
    {
        clickDirection = new Vector2(1, 0);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            transform.Translate(-clickDirection * 50 * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            transform.Translate(clickDirection * 50 * speed * Time.deltaTime);
        }
    }

    void bucketMovementLimit()
    {
        if (bucket.transform.localPosition.x <= minX)
        {
            transform.localPosition = new Vector2(minX, transform.localPosition.y);
        }
        else if (bucket.transform.localPosition.x >= maxX)
        {
            transform.localPosition = new Vector2(maxX, transform.localPosition.y);
        }
    }
}
