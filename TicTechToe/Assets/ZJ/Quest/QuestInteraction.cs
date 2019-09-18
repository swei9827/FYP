using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInteraction : MonoBehaviour
{
    public List<Quests> acceptedQuestLists;
    public List<Quests> completedQuestLists;

    [System.Serializable]
    public class Quests
    {
        public string questName;
        public int questID;        
        public string requirement;
        public int amount;
        public int reward;
        public int collected;
        public bool accepted;
        public bool completed;

        public bool questStatusCheck()
        {
            if(collected >= amount)
            {
                completed = true;
                QuestManager.returnQuestCompleted(questID);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("NPC"))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Quests aq = new Quests();
                if (!QuestManager.returnQuestStatus(collision.gameObject))
                {
                    aq.questName = QuestManager.returnQuestName(collision.gameObject);
                    aq.questID = QuestManager.returnQuestID(collision.gameObject);
                    aq.requirement = QuestManager.returnRequirement(collision.gameObject);
                    aq.amount = QuestManager.returnAmount(collision.gameObject);
                    aq.reward = QuestManager.returnReward(collision.gameObject);
                    aq.accepted = true;
                    QuestManager.returnQuestAccepted(collision.gameObject);
                    acceptedQuestLists.Add(aq);
                }                
            }
        }
    }

    void Update()
    {
        foreach(Quests q in acceptedQuestLists)
        {
            if (q.questStatusCheck())
            {
                completedQuestLists.Add(q);
                acceptedQuestLists.Remove(q);
            }
        }
    }
}
