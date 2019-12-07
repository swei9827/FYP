using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public static TargetIndicator instance;
    public Transform target;
    public float hideArrowDistance;

    // Find NPC
    private Transform UncleJoseph;
    private Transform Jane;
    private Transform Harry;

    // Start is called before the first frame update
    void Start()
    {
        UncleJoseph = GameObject.Find("NPC Uncle Joseph").transform;
        Jane = GameObject.Find("NPC Jane").transform;
        Harry = GameObject.Find("NPC Harry").transform;

        //target = Jane;
        //SetChildrenActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        targetObject();
    }

    void targetObject()
    {
        Vector2 dir = target.position - transform.position;

        if(dir.magnitude < hideArrowDistance)
        {
            foreach(Transform child in transform)
            {
                SetChildrenActive(false);
            }
        }

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void setTarget()
    {

    }

    public void SetChildrenActive(bool value)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
}
