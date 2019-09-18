using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public QuestInfo[] QuestsLists;

    [System.Serializable]
    public class QuestInfo
    {
        public int questID;
        public string questName;
        public GameObject QuestProvider;
        public string questType;
        public string questDetail;
        public string objectName;
        public int amount;
        public int reward;
        public bool accepted;
        public bool completed;
    }

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
    }

    public static int returnQuestID(GameObject go)
    {
        QuestInfo q = Array.Find(instance.QuestsLists, QuestInfo => QuestInfo.QuestProvider == go);
        if(q == null)
        {
            Debug.Log("Invalid NPC");
            return -1;
        }
        else
        {
            return q.questID;
        }
    }

    public static string returnRequirement(GameObject go)
    {
        QuestInfo q = Array.Find(instance.QuestsLists, QuestInfo => QuestInfo.QuestProvider == go);
        if(q == null)
        {
            Debug.Log("Invalid NPC");
            return "null";
        }
        else
        {
            return q.objectName;
        }
    }

    public static int returnAmount(GameObject go)
    {
        QuestInfo q = Array.Find(instance.QuestsLists, QuestInfo => QuestInfo.QuestProvider == go);
        if (q == null)
        {
            Debug.Log("Invalid NPC");
            return -1;
        }
        else
        {
            return q.amount;
        }
    }

    public static int returnReward(GameObject go)
    {
        QuestInfo q = Array.Find(instance.QuestsLists, QuestInfo => QuestInfo.QuestProvider == go);
        if (q == null)
        {
            Debug.Log("Invalid NPC");
            return -1;
        }
        else
        {
            return q.reward;
        }
    }

    public static string returnQuestName(GameObject go)
    {
        QuestInfo q = Array.Find(instance.QuestsLists, QuestInfo => QuestInfo.QuestProvider == go);
        if (q == null)
        {
            Debug.Log("Invalid NPC");
            return "null";
        }
        else
        {
            return q.questName;
        }
    }

    public static string returnQuestType(GameObject go)
    {
        QuestInfo q = Array.Find(instance.QuestsLists, QuestInfo => QuestInfo.QuestProvider == go);
        if (q == null)
        {
            Debug.Log("Invalid NPC");
            return "null";
        }
        else
        {
            return q.questType;
        }
    }

    public static bool returnQuestStatus(GameObject go)
    {
        QuestInfo q = Array.Find(instance.QuestsLists, QuestInfo => QuestInfo.QuestProvider == go);
        if (q == null)
        {
            Debug.Log("Invalid NPC");
            return true;
        }
        else
        {
            return q.accepted;
        }
    }

    public static void returnQuestAccepted(GameObject go)
    {
        QuestInfo q = Array.Find(instance.QuestsLists, QuestInfo => QuestInfo.QuestProvider == go);
        if (q == null)
        {
            Debug.Log("Invalid NPC");
        }
        else
        {
            q.accepted = true;
        }
    }

    public static void returnQuestCompleted(int id)
    {
        QuestInfo q = Array.Find(instance.QuestsLists, QuestInfo => QuestInfo.questID == id);
        if (q == null)
        {
            Debug.Log("Invalid ID");
        }
        else
        {
            q.completed = true;
        }
    }    
}
