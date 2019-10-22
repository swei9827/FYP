using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NPCManager : MonoBehaviour
{ 
    Transform player;
    GameObject popupInstance;
    bool popupInstantiated;

    [Header("Quest NPCs")]
    public QuestInfo[] QuestsLists;

    [Header("Trade NPCs")]
    public Trader[] traderList;

    [Header("NPCs Pop Up")]
    public GameObject popup;
    public float updateFrequency;
    public List<NPCPopUp> npcList;
    public NPCPopUp currentNpc;

    bool variableObtained;

    #region Class Declaration

    [System.Serializable]
    public class NPCData
    {
        public int id;
        public string name;
        public GameObject target;
        [TextArea(5, 15)]
        public string detail;
        [HideInInspector]
        public int requirementCount;
        public List<NPCItem> requirement;
        public bool repeatable;
        public bool accepted;

        public IEnumerator DelayReset(float time)
        {
            yield return new WaitForSeconds(time);
            if (repeatable && accepted)
            {
                accepted = false;
                foreach (NPCItem n in requirement)
                    n.collected = 0;
            }
        }
    }

    [System.Serializable]
    public class NPCItem
    {
        public string objectName;
        public int amount;
        public int collected;
    }

    [System.Serializable]
    public class Trader : NPCData
    {
        public List<NPCItem> availableItems;
    }

    [System.Serializable]
    public class QuestInfo : NPCData
    {
        public string questType;
        public GameObject questCompleter;
        public int reward;
        public bool showQuest;
        public bool completed;
    }

    [System.Serializable]
    public class NPCPopUp
    {
        public GameObject NPC;
        public float distance;
        public float offsetX;
        public float offsetY;
    }

    #endregion

    void Awake()
    {
        StartCoroutine(DistanceCheck(updateFrequency));
    }

    void Start()
    {
        currentNpc = null;
        foreach (QuestInfo q in QuestsLists)
            q.requirementCount = q.requirement.Count;
        foreach (Trader t in traderList)
            t.requirementCount = t.requirement.Count;
    }

    void Update()
    {
        if(RoomController.playerSpawned && !variableObtained)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            variableObtained = true;
        }

        if (currentNpc != null)
        {
            if (Vector2.Distance(currentNpc.NPC.transform.position, player.position) > currentNpc.distance)
            {
                Destroy(popupInstance);
                popupInstantiated = false;
                currentNpc = null;
                NPCInteraction.interactable = false;
            }
        }
    }

    public bool returnNPCType(GameObject go, int type)
    {
        switch (type)
        {
            case 0:
                QuestInfo q = Array.Find(QuestsLists, QuestInfo => QuestInfo.target == go);
                if (q == null)
                    return false;
                else if (!q.accepted)
                    return true;
                else
                    return false;

            case 1:
                q = Array.Find(QuestsLists, QuestInfo => QuestInfo.questCompleter == go);
                if (q == null)
                    return false;
                else if (q.accepted && !q.completed)
                    return true;
                else
                    return false;

            case 2:
                Trader t = Array.Find(traderList, Trader => Trader.target == go);
                if (t == null)
                    return false;
                else if (!t.accepted)
                    return true;
                else
                    return false;

            default:
                return false;
        }
    }

    public NPCData returnNPCData(GameObject go, int type)
    {
        switch (type)
        {
            case 0:
                QuestInfo q = Array.Find(QuestsLists, QuestInfo => QuestInfo.target == go);
                if (q == null)
                    return null;
                else if (!q.accepted)
                    return q;
                else
                    return null;

            case 1:
                q = Array.Find(QuestsLists, QuestInfo => QuestInfo.questCompleter == go);
                if (q == null)
                    return null;
                else if (q.accepted)
                    return q;
                else
                    return null;

            case 2:
                Trader t = Array.Find(traderList, Trader => Trader.target == go);
                if (t == null)               
                    return null;                
                else if (!t.accepted)
                    return t;
                else
                    return null;

            default:
                return null;
        }     
    }

    IEnumerator DistanceCheck(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            foreach (NPCPopUp n in npcList)
            {
                float dist = Vector2.Distance(n.NPC.transform.position, player.position);

                Vector2 parent = n.NPC.transform.position;
                parent.x += n.offsetX;
                parent.y += n.offsetY;

                if (dist <= n.distance && !popupInstantiated)
                {
                    popupInstance = Instantiate(popup, parent, Quaternion.identity);
                    currentNpc = n;
                    popupInstantiated = true;
                    NPCInteraction.interactable = true;
                }
            }           
        }
    }

    void OnDrawGizmos()
    {
        foreach (NPCPopUp n in npcList)
        {
            Vector2 pos = n.NPC.transform.position;
            pos.x += n.offsetX;
            pos.y += n.offsetY;
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(n.NPC.transform.position, n.distance);
            Gizmos.DrawWireCube(pos, new Vector2(.5f, .5f));
        }
    }
}
