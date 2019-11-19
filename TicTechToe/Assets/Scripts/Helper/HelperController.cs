using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

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

    [Header("Hotkey")]
    public GameObject slotPanel;
    public GameObject[] hotKeySlots;

    [Header("Highlight")]
    public GameObject Highlights;
    GameObject temphighlights;
    public bool inventoryHighlights = false;
    public bool hoeHighlights = false;
    public bool waterHighlights = false;
    public bool fishRodHighlights = false;
    public bool seedHighlights = false;

    private HotKey Hotkey;
    private GameObject InventoryIcon;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //Inventory Icon
        InventoryIcon = GameObject.Find("Inventory Icon");

        //Intialize HotKey
        Hotkey = FindObjectOfType<HotKey>();

        //Initialize SlotPanel
        slotPanel = GameObject.Find("SlotPanel");             
        for (int i = 0; i < 4; i++)
        {
            hotKeySlots[i] = slotPanel.transform.GetChild(i).gameObject;
        }       
    }

    // Update is called once per frame
    void Update()
    {
        Interaction();
        KeyboardMove();
        //ClickMove();
        DisableHighlight();
    } 

    void KeyboardMove()
    {
        direction = Vector2.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        transform.Translate(direction * speed * Time.deltaTime);
        UpdateAnimation();
        UpdateSFX();
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
        // Interact
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(TriggerTextBox());

            // In game world
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

        //Inventory
        else if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryHighlights = !inventoryHighlights;

            //make sure other boolean are off
            hoeHighlights = false;
            waterHighlights = false;
            fishRodHighlights = false;
            seedHighlights = false;

            //delete other highlights
            Destroy(temphighlights);

            if (inventoryHighlights)
            {
                temphighlights = Instantiate(Highlights, InventoryIcon.transform);
                temphighlights.transform.SetAsFirstSibling();
                StartCoroutine(TriggerTextBox());
                textDisplay.text = "Check Your Inventory!";
            }
            else if(!inventoryHighlights)
            {
                Destroy(temphighlights);
            }          
        }

        //Quests
        else if (Input.GetKeyDown(KeyCode.Tab))
        {

        }

        //Hoe
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            hoeHighlights = !hoeHighlights;

            //make sure other boolean are off
            inventoryHighlights = false;
            waterHighlights = false;
            fishRodHighlights = false;
            seedHighlights = false;

            //delete other hotkey's highlight
            Destroy(temphighlights);

            //Set highlights
            if(hoeHighlights)
            {
                temphighlights = Instantiate(Highlights, hotKeySlots[0].transform);
                temphighlights.transform.SetAsFirstSibling();
                StartCoroutine(TriggerTextBox());
                textDisplay.text = "Scroll to the highlighted box";
            }
            else
            {
                Destroy(temphighlights);
            }         
        }

        //WaterCan
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            waterHighlights = !waterHighlights;

            //make sure other boolean are off
            inventoryHighlights = false;
            hoeHighlights = false;
            fishRodHighlights = false;
            seedHighlights = false;

            //delete other hotkey's highlight
            Destroy(temphighlights);

            //Set highlights
            if (waterHighlights)
            {
                temphighlights = Instantiate(Highlights, hotKeySlots[1].transform);
                temphighlights.transform.SetAsFirstSibling();
                StartCoroutine(TriggerTextBox());
                textDisplay.text = "Scroll to the highlighted box";
            }
            else
            {
               Destroy(temphighlights);
            }
        }

        //Fishing Rod
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            fishRodHighlights = !fishRodHighlights;

            //make sure other boolean are off
            inventoryHighlights = false;
            hoeHighlights = false;
            waterHighlights = false;
            seedHighlights = false;

            //delete other hotkey's highlight
            Destroy(temphighlights);

            //Set highlights
            if (fishRodHighlights)
            {
                temphighlights = Instantiate(Highlights, hotKeySlots[2].transform);
                temphighlights.transform.SetAsFirstSibling();
                StartCoroutine(TriggerTextBox());
                textDisplay.text = "Scroll to the highlighted box";
            }
            else
            {
                Destroy(temphighlights);
            }
        }

        //Seed
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            seedHighlights = !seedHighlights;

            //make sure other boolean are off
            inventoryHighlights = false;
            hoeHighlights = false;
            waterHighlights = false;
            fishRodHighlights = false;

            //delete other hotkey's highlight
            Destroy(temphighlights);

            //Set highlights
            if (seedHighlights)
            {
                temphighlights = Instantiate(Highlights, hotKeySlots[3].transform);
                temphighlights.transform.SetAsFirstSibling();
                StartCoroutine(TriggerTextBox());
                textDisplay.text = "Scroll to the highlighted box and click the seed you want";
            }
            else
            {
                Destroy(temphighlights);
            }
        }     
    }

    void DisableHighlight()
    {
        // Disable highlights once interact
        if (InventoryIcon == EventSystem.current.currentSelectedGameObject && inventoryHighlights)
        {
            inventoryHighlights = false;
            Destroy(temphighlights);
            EventSystem.current.SetSelectedGameObject(null);
        }

        else if (Hotkey.scrollPosition == 0 && hoeHighlights)
        {
            hoeHighlights = false;
            Destroy(temphighlights);
        }
        else if (Hotkey.scrollPosition == 1 && waterHighlights)
        {
            waterHighlights = false;
            Destroy(temphighlights);
        }
        else if (Hotkey.scrollPosition == 2 && fishRodHighlights)
        {
            fishRodHighlights = false;
            Destroy(temphighlights);
        }
        else if (Hotkey.scrollPosition == 3 && seedHighlights)
        {
            seedHighlights = false;
            Destroy(temphighlights);
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
