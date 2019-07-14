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

    private float minX = -730;
    private float maxX = 720;

    void Start()
    {
        bucket = gameObject.GetComponent<RectTransform>();
        panel = GameObject.Find("FishingGame").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        bucketMove();
    }

    void bucketMove()
    {
        direction = Vector2.zero;
        direction.x = Input.GetAxisRaw("Horizontal");

        if (direction != Vector2.zero)
        {
            transform.Translate(direction * 50 * speed * Time.deltaTime);
        }

        if (bucket.transform.localPosition.x <= minX)
        {
            transform.localPosition = new Vector2(minX, transform.localPosition.y);
        }
        else if(bucket.transform.localPosition.x >= maxX)
        {
            transform.localPosition = new Vector2(maxX, transform.localPosition.y);
        }

    }
}
