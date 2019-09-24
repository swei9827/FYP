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

    void Start()
    {
        foreach(QuestInfo q in QuestsLists)
        {
            q.requirementCount = q.requirement.Count;
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
}
