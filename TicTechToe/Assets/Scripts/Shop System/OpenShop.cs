using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    public GameObject shop;
    bool openShop = false;

    // Start is called before the first frame update
    void Start()
    {
        shop.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        onOffShop();
    }

    public void OnClick()
    {
        openShop = false;
    }

    void onOffShop()
    {
        if (openShop)
        {
            shop.SetActive(true);
            PlayerMovement.canMove = false;
        }
        else
        {
            shop.SetActive(false);
            PlayerMovement.canMove = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                openShop = true;
            }
        }
    }
}
