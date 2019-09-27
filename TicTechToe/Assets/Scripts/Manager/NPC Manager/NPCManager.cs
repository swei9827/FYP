using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject popup;
    public List<NPCs> npcList;

    public static NPCs currentNpc;

    Transform player;
    GameObject instance;
    bool popupInstantiated;

    [System.Serializable]
    public class NPCs
    {
        public GameObject NPC;
        public float distance;
        public float offsetX;
        public float offsetY;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;        
    }

    void Awake()
    {
        StartCoroutine(DistanceCheck(.5f));
    }

    IEnumerator DistanceCheck(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            Debug.Log("x");
            foreach (NPCs n in npcList)
            {
                float dist = Vector2.Distance(n.NPC.transform.position, player.position);

                Vector2 parent = n.NPC.transform.position;
                parent.x += n.offsetX;
                parent.y += n.offsetY;

                if (dist <= n.distance && !popupInstantiated)
                {
                    instance = Instantiate(popup, parent, Quaternion.identity);
                    currentNpc = n;
                    popupInstantiated = true;
                    QuestInteraction.interactable = true;
                }
            }

            if (currentNpc != null && Vector2.Distance(currentNpc.NPC.transform.position, player.position) > currentNpc.distance)
            {
                Destroy(instance);
                popupInstantiated = false;
                currentNpc = null;
                QuestInteraction.interactable = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        foreach (NPCs n in npcList)
        {
            Vector2 pos = n.NPC.transform.position;
            pos.x += n.offsetX;
            pos.y += n.offsetY;
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(n.NPC.transform.position, n.distance);
            Gizmos.DrawWireCube(pos, new Vector2(.5f,.5f));
        }
    }
}
