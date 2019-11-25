using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    public static bool interactable;

    NPCManager npcmanager;

    TextMeshProUGUI Title;
    TextMeshProUGUI Detail;
    TextMeshProUGUI Reward;
    bool Accepted;

    // Quest system
    [Header("Quest System")]
    public List<NPCManager.QuestInfo> acceptedQuestLists;
    public List<QuestLog> completedQuestLists;
    public GameObject questIndicatorContentPrefab;
    public GameObject questIndicatorContent;

    GameObject questUI;
    GameObject questUICompletion;
    GameObject questIndicator;
    NPCManager.QuestInfo currentQuest;
    public List<GameObject> spawnedIndicator;

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

    [System.Serializable]
    public class Indicator
    {
        public int questID;
        public string questName;
        public string questDetail;
        public bool spawned;
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
        questIndicator = GameObject.FindGameObjectWithTag("QuestIndicator");
    }

    void Start()
    {
        questUI.SetActive(false);
        questUICompletion.SetActive(false);
        tradeUI.SetActive(false);
        questIndicator.SetActive(false);
        npcmanager = GameObject.FindGameObjectWithTag("NPCManager").GetComponent<NPCManager>();
    }

    void Update()
    {
        if (currentQuest != null)
        {
            questUIPrompt(currentQuest);
        }

        if (currentTrader != null)
        {
            tradeUIPrompt(currentTrader);
        }

        if (interactable && Input.GetKeyDown(KeyCode.Mouse0) && npcmanager.currentNpc != null)
        {
            resetIndicator();

            if (npcmanager.returnNPCType(npcmanager.currentNpc.NPC, 0))
            {
                currentQuest = (NPCManager.QuestInfo)npcmanager.returnNPCData(npcmanager.currentNpc.NPC, 0);
                questUIPrompt(currentQuest);
            }

            else if (npcmanager.returnNPCType(npcmanager.currentNpc.NPC, 1))
            {
                questStatusCheck((NPCManager.QuestInfo)npcmanager.returnNPCData(npcmanager.currentNpc.NPC, 1));
            }

            else if (npcmanager.returnNPCType(npcmanager.currentNpc.NPC, 2))
            {
                currentTrader = (NPCManager.Trader)npcmanager.returnNPCData(npcmanager.currentNpc.NPC, 2);
                traderInfo = currentTrader;
                tradeUIPrompt(currentTrader);
            }
        }

        showQuestIndicator();
    }

    void showQuestIndicator()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !questIndicator.activeSelf && !interactable){
            questIndicator.SetActive(true);
            PlayerMovement.canMove = false;
            foreach(NPCManager.QuestInfo q in acceptedQuestLists)
            {
                GameObject temp = Instantiate(questIndicatorContentPrefab, questIndicatorContent.transform.position, Quaternion.identity);
                temp.transform.SetParent(questIndicatorContent.transform);
                temp.GetComponent<TextMeshProUGUI>().text = q.name;
                temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = q.detail;
                spawnedIndicator.Add(temp);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Tab) && questIndicator.activeSelf)
        {
            questIndicator.SetActive(false);
            foreach(GameObject go in spawnedIndicator)
            {
                Destroy(go);
            }
            spawnedIndicator.Clear();
            PlayerMovement.canMove = true;
        }
    }

    public void accept()
    {
        Accepted = true;
    }

    public void decline()
    {
        if(currentTrader != null)
        {
            currentTrader.accepted = false;
        }
        Accepted = false;
        questUI.SetActive(false);
        tradeUI.SetActive(false);
        currentQuest = null;
        currentTrader = null;
        traderInfo = null;
        PlayerMovement.canMove = true;
    }

    public void questItemCheck(Item item)
    {
        foreach (NPCManager.QuestInfo q in acceptedQuestLists)
        {
            foreach (NPCManager.NPCItem i in q.requirement)
            {
                if (item.itemName == i.objectName)
                {
                    i.collected++;
                }
            }
        }
    }

    void resetIndicator()
    {
        if (questIndicator.activeSelf)
        {
            questIndicator.SetActive(false);
            foreach (GameObject go in spawnedIndicator)
            {
                Destroy(go);
            }
            spawnedIndicator.Clear();
            PlayerMovement.canMove = true;
        }
    }

    void questUIPrompt(NPCManager.QuestInfo info)
    {
        info.completed = false;
        NPCManager.QuestInfo q = new NPCManager.QuestInfo();

        PlayerMovement.canMove = false;
        questUI.SetActive(true);

        Title = GameObject.FindGameObjectWithTag("QuestTitle").GetComponent<TextMeshProUGUI>();
        Detail = GameObject.FindGameObjectWithTag("QuestDetail").GetComponent<TextMeshProUGUI>();
        Reward = GameObject.FindGameObjectWithTag("QuestReward").GetComponent<TextMeshProUGUI>();

        q = info;
        Title.text = q.name;
        Detail.text = q.detail;
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

    void questStatusCheck(NPCManager.QuestInfo q)
    {
        int count = 0;
        if (q != null)
        {
            foreach (NPCManager.NPCItem i in q.requirement)
            {
                if (i.collected >= i.amount)
                {
                    count++;
                }
            }
            if (count == q.requirementCount)
            {
                resetIndicator();
                QuestLog log = new QuestLog();
                q.completed = true;
                log.questID = q.id;
                log.questName = q.name;
                log.questDetail = q.detail;
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
        Title = GameObject.FindGameObjectWithTag("TradeTitle").GetComponent<TextMeshProUGUI>();
        Detail = GameObject.FindGameObjectWithTag("TradeDetail").GetComponent<TextMeshProUGUI>();
        Title.text = t.name;
        Detail.text = t.detail;
        t.accepted = true;

        tradeStatusCheck(t);
    }

    void tradeStatusCheck(NPCManager.Trader t)
    {
        int count = 0;
        if (t != null)
        {
            foreach (NPCManager.NPCItem i in t.requirement)
            {
                if (i.collected >= t.requirementCount)
                {
                    count++;
                }
            }
            if (count == t.requirementCount)
            {
                if (Accepted)
                {
                    TradeLog log = new TradeLog();
                    log.tradeID = t.id;
                    log.tradeNPC = t.name;
                    log.tradeDetail = t.detail;
                    tradeLog.Add(log);

                    StartCoroutine(t.DelayReset(2f));

                    Accepted = false;
                    currentTrader = null;
                    traderInfo = null;
                    tradeUI.SetActive(false);
                    PlayerMovement.canMove = true;
                }
            }
            else if (count != t.requirementCount)
            {
                Accepted = false;
            }
        }
    }

    IEnumerator closeUI(float time)
    {
        yield return new WaitForSeconds(time);
        questUICompletion.GetComponentInChildren<Text>().text = "";
        questUICompletion.SetActive(false);
    }
}

