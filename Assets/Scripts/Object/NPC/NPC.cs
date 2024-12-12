using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractableObj
{
    public string myName;

    public bool isExistQuest;
    public bool isSuccessQuest;

    public List<Quest> myQuests;

    public GameObject speechBubble;
    public Text speechBubbleText;

    private void Update()
    {
        List<Quest> copyQuest = myQuests.ToList();

        //완료한 퀘스트 있으면 제거
        foreach (Quest quest in copyQuest)
            if (QuestManager.instance.completeQuests.Contains(quest))
                myQuests.Remove(quest);

        if (!GameManager.instance.isPause)
        {
            speechBubble.SetActive(isExistQuest || isSuccessQuest);
            speechBubbleText.text = " ! ";
        }

        if (myQuests.Count <= 0)
        {
            isExistQuest = false;
            isSuccessQuest = false;
            return;
        }

        //지금 줄 퀘스트가 진행중이라면 퀘스트 안줌
        isExistQuest = !QuestManager.instance.currentQuests.Contains(myQuests[0]);

        //퀘스트 완료여부
        isSuccessQuest = !isExistQuest && myQuests[0].questCurrentCount >= myQuests[0].questTargetCount;
    }

    public void Interaction()
    {
        if ((!isExistQuest && !isSuccessQuest) || myQuests.Count <= 0) return;

        StartCoroutine(StartDialogue(myQuests[0], isSuccessQuest));
    }

    private IEnumerator StartDialogue(Quest currentQuest, bool success)
    {
        GameManager.instance.isDialoguePause = true;

        string[] questText;

        if (!success)
            questText = currentQuest.questStory;
        else
            questText = currentQuest.questCompleteStory;

        for (int i = 0; i < questText.Length; i++)
        {
            for(int j = 0; j <= questText[i].Length; j++)
            {
                speechBubbleText.text = questText[i][..j];
                yield return new WaitForSeconds(0.1f);
            }

            while (!Input.anyKeyDown)
                yield return null;

            yield return null;
        }

        if (!success)
        {
            QuestManager.instance.currentQuests.Add(currentQuest);
        }
        else
        {
            QuestManager.instance.currentQuests.Remove(currentQuest);
            QuestManager.instance.completeQuests.Add(currentQuest);
        }
        GameManager.instance.isDialoguePause = false;

        yield return null;
    }

    public string GetName()
    {
        return myName;
    }
}
