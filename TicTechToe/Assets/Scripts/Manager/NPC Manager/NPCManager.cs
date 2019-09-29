using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;
    public static NPCs currentNpc;

    [Header("Quest NPCs")]
    public QuestInfo[] QuestsLists;

    [Header("Trade NPCs")]
    public Trader[] traderList;

    [Header("NPCs Pop Up")]
    public GameObject popup;
    public float updateFrequency;
    public List<NPCs> npcList;   

    Transform player;
    GameObject temp;
    bool popupInstantiated;

    // Pop up UI
    [System.Serializable]
    public class NPCs
    {
        public GameObject NPC;
        public float distance;
        public float offsetX;
        public float offsetY;
    }
    //

    // Trading system
    [System.Serializable]
    public class Trader
    {
        public int tradeID;
        public string traderName;
        public GameObject tradeNPC;
        [TextArea(5, 15)]
        public string tradeDetail;
        public int itemsRequired;
        public List<TradeItem> availableItems;
        public List<TradeItem> requestedItems;
        public bool repeatable;
        public bool readyToTrade;
        public IEnumerator DelayReset(float time)
        {
            yield return new WaitForSeconds(time);
            if (repeatable)
            {
                readyToTrade = false;
                foreach (TradeItem r in requestedItems)
                {
                    r.playerOwned = 0;
                }
            }
        }
    }

    [System.Serializable]
    public class TradeItem
    {
        public string itemName;
        public int itemCount;
        public int playerOwned;
    }
    //

    // Quest system
    [System.Serializable]
    public class QuestInfo
    {
        public int questID;
        public string questName;
        public GameObject questProvider;
        public GameObject questCompleter;
        public string questType;
        [TextArea (5, 15)]
        public string questDetail;
        public int requirementCount;
        public List<Requirement> requirement;
        public int reward;
        public bool showQuest;
        public bool repeatable;
        public bool accepted;
        public bool completed;

        public IEnumerator DelayReset(float time)
        {
            yield return new WaitForSeconds(time);
            if(repeatable && completed && accepted)
            {
                completed = false;
                accepted = false;
                foreach(Requirement r in requirement)
                {
                    r.collected = 0;
                }
            }
        }
    }

    [System.Serializable]
    public class Requirement
    {
        public string objectName;
        public int amount;
        public int collected;
    }
    //

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
        foreach(Trader t in traderList)
        {
            t.itemsRequired = t.requestedItems.Count;   
        }
    }  

    public static QuestInfo returnQuestInfoProvider(GameObject go)
    {
        QuestInfo q = Array.Find(instance.QuestsLists, QuestInfo => QuestInfo.questProvider == go);
        if (q == null)
        {
            return null;
        }
        else
        {
            return q;
        }
    }

    public static bool returnQuestStatusProvider(GameObject go)
    {
        QuestInfo q = Array.Find(instance.QuestsLists, QuestInfo => QuestInfo.questProvider == go);
        if (q == null)
        {
            return true;
        }
        else
        {
            return q.accepted;
        }
    }

    public static Trader returnTraderInfo(GameObject go)
    {
        Trader t = Array.Find(instance.traderList, Trader => Trader.tradeNPC == go);
        if (t == null)
        {
            return null;
        }
        else
        {
            return t;
        }
    }

    public static bool returnTraderStatus(GameObject go)
    {
        Trader t = Array.Find(instance.traderList, Trader => Trader.tradeNPC == go);
        if (t == null)
        {
            return false;
        }
        else
        {
            return t.readyToTrade;
        }
    }

    IEnumerator DistanceCheck(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            foreach (NPCs n in npcList)
            {
                float dist = Vector2.Distance(n.NPC.transform.position, player.position);

                Vector2 parent = n.NPC.transform.position;
                parent.x += n.offsetX;
                parent.y += n.offsetY;

                if (dist <= n.distance && !popupInstantiated)
                {
                    temp = Instantiate(popup, parent, Quaternion.identity);
                    currentNpc = n;
                    popupInstantiated = true;
                    NPCInteraction.interactable = true;
                }
            }

            if (currentNpc != null && Vector2.Distance(currentNpc.NPC.transform.position, player.position) > currentNpc.distance)
            {
                Destroy(temp);
                popupInstantiated = false;
                currentNpc = null;
                NPCInteraction.interactable = false;
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
            Gizmos.DrawWireCube(pos, new Vector2(.5f, .5f));
        }
    }
}
