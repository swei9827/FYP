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
        bucket = GetComponent<RectTransform>();
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

        //Vector2 position = bucket.anchoredPosition;

        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    transform.Translate(-clickDirection * 50 * speed * Time.deltaTime);
        //}
        //else if (Input.GetKey(KeyCode.Mouse1))
        //{
        //    transform.Translate(clickDirection * 50 * speed * Time.deltaTime);
        //}

        //if (Input.GetAxis("Mouse X") < 0)
        //{
        //    //transform.Translate(-clickDirection * 50 * speed * Time.deltaTime);
        //}
        //else if(Input.GetAxis("Mouse X") > 0)
        //{
        //    //transform.Translate(clickDirection * 50 * speed * Time.deltaTime);
        //}

        float width = Screen.width * bucket.anchorMin.x;

        float xoffset = 0;

        if(Screen.width > 1920)
        {
            float difference = Screen.width - 1920;
            float percentage = (Input.mousePosition.x / (float)Screen.width) * 50;
            xoffset = (percentage * difference) / 100.0f;
        }

        if (Screen.width < 1920)
        {
            float difference = 1920 - Screen.width;
            float percentage = (Input.mousePosition.x / (float)Screen.width) * 50;
            xoffset = -(percentage * difference) / 100.0f;
        }

        bucket.anchoredPosition = new Vector2(Input.mousePosition.x - width - xoffset, 0);

    }

    void bucketMovementLimit()
    {
        Vector2 position = bucket.anchoredPosition;

        if(bucket.anchoredPosition.x >= maxX)
        {
            bucket.anchoredPosition = new Vector2(maxX,0);
        }
        else if(bucket.anchoredPosition.x <= minX)
        {
            bucket.anchoredPosition = new Vector2(minX, 0);
        }

        //if (bucket.transform.localPosition.x <= minX)
        //{
        //    transform.localPosition = new Vector2(minX, transform.localPosition.y);
        //}
        //else if (bucket.transform.localPosition.x >= maxX)
        //{
        //    transform.localPosition = new Vector2(maxX, transform.localPosition.y);
        //}
    }
}
