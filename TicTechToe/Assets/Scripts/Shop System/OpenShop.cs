using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    public GameObject shop;
    bool openShop = false;

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        //openShop = false;
        shop.SetActive(false);
        PlayerMovement.canMove = true;
        openShop = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Collide!");
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Press!");
                shop.SetActive(true);
                PlayerMovement.canMove = false;
                openShop = true;
            }
        }
    }
}
