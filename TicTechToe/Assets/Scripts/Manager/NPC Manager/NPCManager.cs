using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;
    public static NPCPopUp currentNpc;

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

    #region NPC Data Declaration

    [System.Serializable]
    public class NPCData
    {
        public int id;
        public string name;
        public GameObject target;
        [TextArea(5, 15)]
        public string detail;
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
                {
                    n.collected = 0;
                }
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

    #endregion

    #region Trading System

    [System.Serializable]
    public class Trader : NPCData
    {
        public List<NPCItem> availableItems;
    }

    #endregion

    #region Quest System

    [System.Serializable]
    public class QuestInfo : NPCData
    {
        public string questType;
        public GameObject questCompleter;
        public int reward;
        public bool showQuest;
        public bool completed;
    }

    #endregion

    #region NPC Pop Up UI

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
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        StartCoroutine(DistanceCheck(updateFrequency));
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        foreach (QuestInfo q in QuestsLists)
        {
            q.requirementCount = q.requirement.Count;
        }
        foreach (Trader t in traderList)
        {
            t.requirementCount = t.requirement.Count;
        }
    }

    public static bool returnNPCType(GameObject go, int type)
    {
        if (type == 0)
        {
            QuestInfo q = Array.Find(instance.QuestsLists, QuestInfo => QuestInfo.target == go);
            if (q == null)
            {
                return false;
            }
            else if (!q.accepted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (type == 1)
        {
            QuestInfo q = Array.Find(instance.QuestsLists, QuestInfo => QuestInfo.questCompleter == go);
            if (q == null)
            {
                return false;
            }
            else if (q.accepted && !q.completed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (type == 2)
        {
            Trader t = Array.Find(instance.traderList, Trader => Trader.target == go);
            if (t == null)
            {
                return false;
            }
            else if (!t.accepted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static NPCData returnNPCData(GameObject go, int type)
    {
        if (type == 0)
        {
            QuestInfo q = Array.Find(instance.QuestsLists, QuestInfo => QuestInfo.target == go);
            if (q == null)
            {
                return null;
            }
            else if (!q.accepted)
            {
                return q;
            }
            else
            {
                return null;
            }
        }
        else if (type == 1)
        {
            QuestInfo q = Array.Find(instance.QuestsLists, QuestInfo => QuestInfo.questCompleter == go);
            if (q == null)
            {
                return null;
            }
            else if (q.accepted)
            {
                return q;
            }
            else
            {
                return null;
            }
        }
        else if (type == 2)
        {
            Trader t = Array.Find(instance.traderList, Trader => Trader.target == go);
            if (t == null)
            {
                return null;
            }
            else if (!t.accepted)
            {
                return t;
            }
            else
            {
                return null;
            }
        }
        else
        {
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

            if (currentNpc != null && Vector2.Distance(currentNpc.NPC.transform.position, player.position) > currentNpc.distance)
            {
                Destroy(popupInstance);
                popupInstantiated = false;
                currentNpc = null;
                NPCInteraction.interactable = false;
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
