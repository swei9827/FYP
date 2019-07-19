using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BarrelOverlay : MonoBehaviour
{
    public SpriteRenderer sr;
    public GameObject OverlaySprite;
    bool canInteract = false;

    private void Start()
    {
        GameObject temp = Instantiate(OverlaySprite, this.transform.position, Quaternion.identity);
        temp.transform.SetParent(this.transform);
        temp.transform.position = new Vector2(this.transform.position.x, 0.6f + this.transform.position.y);
          
    }

    void Interact()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Press");
            Destroy(OverlaySprite);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("HEllo");
            canInteract = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
