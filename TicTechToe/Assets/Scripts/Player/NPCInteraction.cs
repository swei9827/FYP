using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public static bool interactable;

    Text Title;
    Text Detail;
    Text Reward;
    bool Accepted;

    // Quest system
    [Header("Quest System")]
    public List<NPCManager.QuestInfo> acceptedQuestLists;
    public List<QuestLog> completedQuestLists;

    GameObject questUI;
    GameObject questUICompletion;
    NPCManager.QuestInfo currentQuest;        

    [System.Serializable]
    public class QuestLog
    {
        public int questID;
        public string questName;
        [TextArea(5, 15)]
        public string questDetail;
        public int reward;
        public bool completed;
    }
    //

    // Trade system
    [Header("Trade System")]
    public NPCManager.Trader traderInfo;
    public List<TradeLog> tradeLog;

    GameObject tradeUI;
    NPCManager.Trader currentTrader;

    [System.Serializable]
    public class TradeLog
    {
        public int tradeID;
        public string tradeNPC;
        [TextArea(5, 15)]
        public string tradeDetail;
    }
    //
    
    void Awake()
    {
        questUI = GameObject.FindGameObjectWithTag("QuestUI");
        questUICompletion = GameObject.FindGameObjectWithTag("QuestUICompletion");
        tradeUI = GameObject.FindGameObjectWithTag("TradeUI");
    }

    void Start()
    {
        questUI.SetActive(false);
        questUICompletion.SetActive(false);
        tradeUI.SetActive(false);
    }

    void Update()
    {
        if (currentQuest != null)
        {
            questUIPrompt(currentQuest);
        }

        if(currentTrader != null)
        {
            tradeUIPrompt(currentTrader);
        }

        if(interactable && Input.GetKeyDown(KeyCode.Q))
        {
            if (!NPCManager.returnQuestStatusProvider(NPCManager.currentNpc.NPC))
            {
                currentQuest = NPCManager.returnQuestInfoProvider(NPCManager.currentNpc.NPC);
            }

            if (!NPCManager.returnTraderStatus(NPCManager.currentNpc.NPC))
            {
                getTraderInfo(NPCManager.currentNpc.NPC);
            }

            foreach (NPCManager.QuestInfo q in acceptedQuestLists)
            {
                if (q.questCompleter == NPCManager.currentNpc.NPC)
                {
                    questStatusCheck(q);
                }
            }           
        }
    }

    public void accept()
    {
        Accepted = true;
    }

    public void decline()
    {
        Accepted = false;
        questUI.SetActive(false);
        tradeUI.SetActive(false);
        currentQuest = null;
        currentTrader = null;
        traderInfo = null;
        PlayerMovement.canMove = true;
    }

    void questUIPrompt(NPCManager.QuestInfo info)
    {
        PlayerMovement.canMove = false;
        questUI.SetActive(true);
        Title = GameObject.FindGameObjectWithTag("QuestTitle").GetComponent<Text>();
        Detail = GameObject.FindGameObjectWithTag("QuestDetail").GetComponent<Text>();
        Reward = GameObject.FindGameObjectWithTag("QuestReward").GetComponent<Text>();
        NPCManager.QuestInfo q = new NPCManager.QuestInfo();
        q = info;        
        Title.text = q.questName;
        Detail.text = q.questDetail;
        Reward.text = "Reward " + q.reward + " gold";

        if (Accepted)
        {
            q.accepted = true;
            acceptedQuestLists.Add(q);
            Accepted = false;
            currentQuest = null;
            questUI.SetActive(false);
            PlayerMovement.canMove = true;
        }
    }   

    public void questStatusCheck(NPCManager.QuestInfo q)
    {
        int count = 0;
        if (q != null)
        {
            foreach (NPCManager.Requirement r in q.requirement)
            {
                if (r.collected >= r.amount)
                {
                    count++;
                }
            }
            if (count == q.requirementCount)
            {
                QuestLog log = new QuestLog();    
                
                q.completed = true;
                log.questID = q.questID;
                log.questName = q.questName;
                log.questDetail = q.questDetail;
                log.reward = q.reward;
                log.completed = q.completed;

                StartCoroutine(q.DelayReset(2f));

                completedQuestLists.Add(log);
                acceptedQuestLists.Remove(q);

                questUICompletion.SetActive(true);
                questUICompletion.GetComponentInChildren<Text>().text = "Quest Completed ! \n" + "Rewarded " + log.reward + " Gold";

                StartCoroutine(closeUI(2f));
            }
        }
    }

    void tradeUIPrompt(NPCManager.Trader t)
    {
        PlayerMovement.canMove = false;
        tradeUI.SetActive(true);
        Title = GameObject.FindGameObjectWithTag("TradeTitle").GetComponent<Text>();
        Detail = GameObject.FindGameObjectWithTag("TradeDetail").GetComponent<Text>();
        Title.text = t.traderName;
        Detail.text = t.tradeDetail;
        t.readyToTrade = true;

        tradeStatusCheck(t);
    }

    void tradeStatusCheck(NPCManager.Trader t)
    {
        int count = 0;
        if (t != null)
        {
            foreach (NPCManager.TradeItem ti in t.requestedItems)
            {
                if (ti.playerOwned >= ti.itemCount)
                {
                    count++;
                }
            }
            if (count == t.itemsRequired)
            {
                if (Accepted)
                {
                    TradeLog log = new TradeLog();
                    log.tradeID = t.tradeID;
                    log.tradeNPC = t.traderName;
                    log.tradeDetail = t.tradeDetail;
                    tradeLog.Add(log);

                    StartCoroutine(t.DelayReset(2f));

                    Accepted = false;
                    currentTrader = null;
                    traderInfo = null;
                    tradeUI.SetActive(false);
                    PlayerMovement.canMove = true;
                }
            }
            else if(count != t.itemsRequired)
            {
                Accepted = false;
            }
        }
    }

    void getTraderInfo(GameObject go)
    {
        NPCManager.Trader t = NPCManager.returnTraderInfo(go);
        if (t != null)
        {
            currentTrader = t;
            traderInfo = t;
        }
        else
        {
            return;
        }
    }

    IEnumerator closeUI(float time)
    {        
        yield return new WaitForSeconds(time);
        questUICompletion.GetComponentInChildren<Text>().text = "";
        questUICompletion.SetActive(false);
    }
}
