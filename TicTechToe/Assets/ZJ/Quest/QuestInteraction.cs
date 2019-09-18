using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInteraction : MonoBehaviour
{
    public List<AcceptedQuests> acceptedQuestLists;

    [System.Serializable]
    public class AcceptedQuests
    {
        public string questName;
        public int questID;        
        public string requirement;
        public int amount;
        public int reward;
        public bool accepted;
        public bool completed;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("NPC"))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                AcceptedQuests aq = new AcceptedQuests();
                if (!QuestManager.returnQuestStatus(collision.gameObject))
                {
                    aq.questName = QuestManager.returnQuestName(collision.gameObject);
                    aq.questID = QuestManager.returnQuestID(collision.gameObject);
                    aq.requirement = QuestManager.returnRequirement(collision.gameObject);
                    aq.amount = QuestManager.returnAmount(collision.gameObject);
                    aq.reward = QuestManager.returnReward(collision.gameObject);
                    aq.accepted = true;
                    QuestManager.questStatusChange(collision.gameObject);
                    acceptedQuestLists.Add(aq);
                }                
            }
        }
    }
}
