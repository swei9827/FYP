using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HotBar : MonoBehaviour
{
    public static HotBar HotBarInstance;

    GraphicRaycaster graphicRayCaster;
    PointerEventData pointerEventData;
    List<RaycastResult> raycastResults;

    GameObject draggedItem;
    Transform dragItemParent;

    List<Item> _items = new List<Item>();
    List<Transform> slots = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        HotBarInstance = this;

        // Initialize inventory lists
        foreach (Transform s in transform.Find("HotBarBackground/HotbarHolder"))
        {
            slots.Add(s);
            if (s.GetComponentInChildren<Item>() != null)
            {
                _items.Add(s.GetComponentInChildren<Item>());
            }
        }

        graphicRayCaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
        raycastResults = new List<RaycastResult>();
    }

    // Update is called once per frame
    void Update()
    {
        DragItem();
    }

    void DragItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointerEventData.position = Input.mousePosition;
            graphicRayCaster.Raycast(pointerEventData, raycastResults);
            if (raycastResults.Count > 0)
            {
                if (raycastResults[0].gameObject.GetComponent<Item>())
                {
                    draggedItem = raycastResults[0].gameObject;
                    dragItemParent = draggedItem.transform.parent;
                    draggedItem.transform.SetParent(UIManager.Instance.canvas);
                }
                else
                {
                    raycastResults.Clear();
                }
            }
        }

        //check if dragged item is null
        if (draggedItem == null)
        {
            return;
        }

        //Item Follow Mouse
        if (draggedItem != null)
        {
            draggedItem.GetComponent<RectTransform>().localPosition = UIManager.Instance.ScreenToCanvasPoint(Input.mousePosition);
        }

        // End Drag
        if (Input.GetMouseButtonUp(0))
        {
            pointerEventData.position = Input.mousePosition;
            raycastResults.Clear();
            graphicRayCaster.Raycast(pointerEventData, raycastResults);

            draggedItem.transform.SetParent(dragItemParent);
            if (raycastResults.Count > 0)
            {
                foreach (var results in raycastResults)
                {
                    // Skip drag item when it is result
                    if (results.gameObject == draggedItem)
                    {
                        continue;
                    }

                    //swap with empty slots
                    if (results.gameObject.CompareTag("Slots"))
                    {
                        // if slots contain item, prevent overlay
                        if (results.gameObject.transform.childCount > 0)
                        {
                            continue;
                        }
                        else
                        {
                            //Set New Parent for dragItem
                            draggedItem.transform.SetParent(results.gameObject.transform);
                            break;
                        }
                    }

                    //swap with items
                    if (results.gameObject.CompareTag("ItemIcon"))
                    {
                        //Swap Item
                        if (results.gameObject.name != draggedItem.name)
                        {
                            draggedItem.transform.SetParent(results.gameObject.transform.parent);
                            results.gameObject.transform.SetParent(dragItemParent);
                            results.gameObject.transform.localPosition = Vector3.zero;
                            break;
                        }
                        //stack Item if same
                        else
                        {
                            results.gameObject.GetComponent<Item>().quantity += draggedItem.GetComponent<Item>().quantity;
                            results.gameObject.transform.Find("NumberHeld").GetComponent<Text>().text = results.gameObject.GetComponent<Item>().quantity.ToString();
                            GameObject.Destroy(draggedItem);
                            draggedItem = null;
                            raycastResults.Clear();
                            return;
                        }
                    }                  
                }
            }
            //Reset position to 0
            draggedItem.transform.localPosition = Vector3.zero;
            draggedItem = null;
        }
        raycastResults.Clear();
    }

    public bool AddItem(GameObject itemGo)
    {
        Item item = itemGo.GetComponent<Item>();

        // QUEST //
        QuestInteraction qi = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestInteraction>();
        foreach (QuestInteraction.Quests q in qi.acceptedQuestLists)
        {
            if (q.questType == "Harvesting" && q.requirement == item.CropType.ToString())
            {
                q.collected += 1;
            }
            else if(q.questType == "Fishing" && q.requirement == item.FishType.ToString())
            {
                q.collected += 1;
            }
        }
        // QUEST //

        //check all items
        foreach (Item i in _items)
        {
            //if item already inside
            if (i.CropType == item.CropType && i.FishType == item.FishType)
            {
                i.Add(1);
                GameObject.Destroy(itemGo);
                return true;
            }
        }

        //check all slots
        foreach (Transform s in slots)
        {
            if (s.GetComponentInChildren<Item>() == null)
            {
                itemGo.transform.SetParent(s);
                itemGo.transform.localScale = Vector3.one;
                itemGo.transform.localPosition = Vector3.zero;
                _items.Add(item);
                return true;
            }
        }

        //if Inventory full
        GameObject.Destroy(itemGo);
        return false;
    }

    public void RemoveItem(Item item)
    {
        if (_items.Contains(item))
        {
            _items.Remove(item);
        }
    }
}
