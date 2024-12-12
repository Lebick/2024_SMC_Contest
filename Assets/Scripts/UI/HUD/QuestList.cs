using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestList : MonoBehaviour
{
    public Transform questParent;
    public GameObject questUI;

    public List<GameObject> quests;

    private void Update()
    {
        transform.GetChild(0).gameObject.SetActive(quests.Count > 0);

        UpdateQuestCount();

        UpdateQuestInfo();
    }
    
    private void UpdateQuestCount()
    {
        if (quests.Count == QuestManager.instance.currentQuests.Count) return;

        //UI���� �����ϸ�
        if(quests.Count < QuestManager.instance.currentQuests.Count)
            quests.Add(Instantiate(questUI, questParent));
        else //UI���� �� ���ٸ�
            for(int i=0; i< quests.Count - QuestManager.instance.currentQuests.Count; i++)
            {
                Destroy(quests[-i]); //������Ʈ Ǯ�� ������
                quests.RemoveAt(-i);
            }
    }

    private void UpdateQuestInfo()
    {
        foreach (GameObject quest in quests)
        {
            Quest currentQuest = QuestManager.instance.currentQuests[quests.IndexOf(quest)];
            Text description = quest.transform.GetChild(0).GetComponent<Text>();
            Text count = quest.transform.GetChild(1).GetComponent<Text>();

            description.text = currentQuest.questDescription;
            Color color = currentQuest.questCurrentCount >= currentQuest.questTargetCount ? Color.green : Color.red;
            string colorText = ColorUtility.ToHtmlStringRGB(color);
            count.text = $"<color=#{colorText}>{currentQuest.questCurrentCount}</color> / {currentQuest.questTargetCount}";
        }
    }
}
