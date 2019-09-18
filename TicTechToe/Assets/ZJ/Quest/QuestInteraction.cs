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
        public string questType;
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
                Quests q = new Quests();
                if (!QuestManager.returnQuestStatus(collision.gameObject))
                {
                    q.questName = QuestManager.returnQuestName(collision.gameObject);
                    q.questID = QuestManager.returnQuestID(collision.gameObject);
                    q.questType = QuestManager.returnQuestType(collision.gameObject);
                    q.requirement = QuestManager.returnRequirement(collision.gameObject);
                    q.amount = QuestManager.returnAmount(collision.gameObject);
                    q.reward = QuestManager.returnReward(collision.gameObject);
                    QuestManager.returnQuestAccepted(collision.gameObject);
                    q.accepted = true;                    
                    acceptedQuestLists.Add(q);
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
