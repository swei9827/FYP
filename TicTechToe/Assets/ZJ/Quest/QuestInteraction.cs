using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInteraction : MonoBehaviour
{
    public List<QuestManager.QuestInfo> acceptedQuestLists;
    public List<QuestLog> completedQuestLists;

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
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("NPC"))
        {
            if (Input.GetKeyDown(KeyCode.Q) && !QuestManager.returnQuestStatusProvider(collision.gameObject))
            {
                QuestManager.QuestInfo q = new QuestManager.QuestInfo();
                q = QuestManager.returnQuestInfoProvider(collision.gameObject);
                q.accepted = true;
                acceptedQuestLists.Add(q);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                foreach(QuestManager.QuestInfo q in acceptedQuestLists)
                {
                    if(q.questCompleter == collision.gameObject)
                    {
                        questStatusCheck(q);
                    }
                }
            }
        }
    }
}
