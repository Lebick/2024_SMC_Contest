using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "QuestData", order = 2)]
public class Quest : ScriptableObject
{
    public int questIndex; //저장 시 어느 퀘스트를 완료했는지 기록하기 위함

    [TextArea]
    public string[] questStory; //퀘스트를 받을 때 NPC가 출력할 내용

    [TextArea]
    public string[] questCompleteStory; //퀘스트를 완료할 때 NPC가 출력할 내용

    [TextArea]
    public string questDescription; //퀘스트 목표 요약

    public QuestType questType; //퀘스트 종류
    public int questTargetIndex; //목표하는 적/보스/아이템의 인덱스

    public int questCurrentCount;
    public int questTargetCount; //목표하는 수

    public GameObject reward; //보상
}
