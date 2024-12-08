using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    EnemyKill,
    BossKill,
    CollectItem,
}

public class QuestManager : Singleton<QuestManager>
{
    public List<Quest> currentQuests; //현재 받고있는 퀘스트

    public List<Quest> completeQuests; //완료한 퀘스트들

    public void AddQuestCount(QuestType type, int index)
    {
        foreach(Quest quest in currentQuests)
            if(quest.questType == type && quest.questTargetIndex == index)
                quest.questCurrentCount++;
    }
}