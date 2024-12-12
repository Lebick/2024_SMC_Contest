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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void AddQuestCount(QuestType type, int index)
    {
        foreach(Quest quest in currentQuests)
            if(quest.questType == type && (quest.questTargetIndex == index || quest.questTargetIndex == -1))
                quest.questCurrentCount++;
    }
}