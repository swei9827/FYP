using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HelperController : MonoBehaviour
{
    [Header("Detect Target, Dont drag prefab in!")]
    public GameObject interactTarget;

    [Header("Helper Settings")]
    public float speed;
    private bool isMoving = false;

    //Player movement
    private Vector3 target;
    private Vector2 direction;

    Rigidbody2D rb;
    RaycastHit2D hit;

    //Animation
    private Animator animator;
    public Animator iconBoxAnim;

    [Header("UI Settings")]
    public TextMeshProUGUI textDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Interaction();
        ClickMove();
    } 

    void ClickMove()
    {
        if (Input.GetMouseButton(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
            isMoving = true;
        }
        else
        {
            direction = Vector2.zero;
            isMoving = false;
        }

        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        UpdateAnimation();
        UpdateSFX();
    }

    void Interaction()
    {
        if(Input.GetMouseButtonDown(1))
        {
            StartCoroutine(TriggerTextBox());

            if (interactTarget == null)
            {
                textDisplay.text = "Follow Me!";
            }
            else if(interactTarget.gameObject.CompareTag("Player"))
            {
                textDisplay.text = "Do as what I say";
            }
            else if (interactTarget.gameObject.CompareTag("NPC") || interactTarget.gameObject.CompareTag("DirtTile"))
            {
                textDisplay.text = "Interact this!";
            }                 
        }
    }

    public IEnumerator TriggerTextBox()
    {
        iconBoxAnim.SetBool("Enable", true);
        yield return new WaitForSeconds(5);
        iconBoxAnim.SetBool("Enable", false);
    }

    void UpdateAnimation()
    {
        //if Move
        if (direction != Vector2.zero)
        {
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
            animator.SetBool("Moving", true);

        }
        else
        {
            //Set animation
            animator.SetBool("Moving", false);
        }

    }

    void UpdateSFX()
    {
        if (direction != Vector2.zero)
        {
            if (FxManager.instance.GetComponent<AudioSource>().isPlaying == false)
            {
                FxManager.PlayMusic("WalkFx");
            }
        }
        else
        {
            FxManager.StopMusic("WalkFx");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        interactTarget = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == interactTarget)
        {
            interactTarget = null;
        }
    }
}
