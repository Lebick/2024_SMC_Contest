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
    public List<Quest> currentQuests; //���� �ް��ִ� ����Ʈ

    public List<Quest> completeQuests; //�Ϸ��� ����Ʈ��

    public void AddQuestCount(QuestType type, int index)
    {
        foreach(Quest quest in currentQuests)
            if(quest.questType == type && quest.questTargetIndex == index)
                quest.questCurrentCount++;
    }
}