using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMovementCollision : MonoBehaviour
{
    public static SpawnMovementCollision instance;
    public GameObject collisionObj;
    public GameObject indicator;

    public Sprite collidedSprite;

    public bool collided = false;
    public bool completeCollision = false;

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
    }

    void ChangeColor()
    {
        if(collided)
        {
            collisionObj.GetComponent<SpriteRenderer>().sprite = collidedSprite;
            Destroy(indicator);
        }     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == Player.LocalPlayerInstance)
        {
            collided = true;
        }
    }
}
