using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{  
    public Sprite dryPlant,waterPlant, growPlant;
    SpriteRenderer rend;
    bool canInteract = false;

    float currentTime = 0f;
    float endTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = dryPlant;
    }

    // Update is called once per frame
    void Update()
    {
        InteractPlant();
        //afterWaterPlant();
    }

    void InteractPlant()
    {
        if(canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (rend.sprite == dryPlant)
                {
                    rend.sprite = waterPlant;
                    //endTime = 5f;
                    //StartCoroutine(countdownPlant());
                }  
                else if(rend.sprite == waterPlant)
                {
                    rend.sprite = growPlant;
                }
                else if (rend.sprite == growPlant)
                {
                    rend.sprite = dryPlant;
                }
            }
        }       
    }

    //void afterWaterPlant()
    //{
    //    if (endTime <= 0)
    //    {           
    //        endTime = 0;
    //        rend.sprite = growPlant;
    //        StopCoroutine("countdownPlant");
    //    }
    //}

    //IEnumerator countdownPlant()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(1);
    //        Debug.Log(endTime);
    //        endTime--;            
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {    
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {      
            canInteract = false;
        }
    }
}
