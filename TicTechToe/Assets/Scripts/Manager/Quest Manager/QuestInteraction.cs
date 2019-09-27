using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestInteraction : MonoBehaviour
{
    GameObject questUI;
    GameObject questUICompletion;
    QuestManager.QuestInfo currentQuest;
    Text questTitle;
    Text questDetail;
    Text questReward;
    bool questAccepted;    

    public List<QuestManager.QuestInfo> acceptedQuestLists;
    public List<QuestLog> completedQuestLists;
    public static bool interactable;

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

    void Awake()
    {
        questUI = GameObject.FindGameObjectWithTag("QuestUI");
        questUICompletion = GameObject.FindGameObjectWithTag("QuestUICompletion");
        questTitle = GameObject.FindGameObjectWithTag("QuestTitle").GetComponent<Text>();
        questDetail = GameObject.FindGameObjectWithTag("QuestDetail").GetComponent<Text>();
        questReward = GameObject.FindGameObjectWithTag("QuestReward").GetComponent<Text>();
    }

    void Start()
    {
        questUI.SetActive(false);
        questUICompletion.SetActive(false);
    }

    public void acceptQuest()
    {
        questAccepted = true;
    }

    public void declineQuest()
    {
        questAccepted = false;
        questUI.SetActive(false);
        currentQuest = null;
        PlayerMovement.canMove = true;
    }

    void questUIAccepted(QuestManager.QuestInfo info)
    {
        PlayerMovement.canMove = false;

        QuestManager.QuestInfo q = new QuestManager.QuestInfo();
        q = info;
        questUI.SetActive(true);
        questTitle.text = q.questName;
        questDetail.text = q.questDetail;
        questReward.text = "Reward " + q.reward + " gold";

        if (questAccepted)
        {
            q.accepted = true;
            acceptedQuestLists.Add(q);
            questAccepted = false;
            currentQuest = null;
            questUI.SetActive(false);
            PlayerMovement.canMove = true;
        }
    }   

    public void questStatusCheck(QuestManager.QuestInfo q)
    {
        int count = 0;
        if (q != null)
        {
            foreach (QuestManager.Requirement r in q.requirement)
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

    void Update()
    {
        if (currentQuest != null)
        {
            questUIAccepted(currentQuest);
         }
        if (interactable && Input.GetKeyDown(KeyCode.Q) && !QuestManager.returnQuestStatusProvider(NPCManager.currentNpc.NPC))
        {
            currentQuest = QuestManager.returnQuestInfoProvider(NPCManager.currentNpc.NPC);
        }
        else if(interactable && Input.GetKeyDown(KeyCode.Q))
        {
            foreach (QuestManager.QuestInfo q in acceptedQuestLists)
            {
                if (q.questCompleter == NPCManager.currentNpc.NPC)
                {
                    questStatusCheck(q);
                }
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
