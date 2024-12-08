using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "QuestData", order = 2)]
public class Quest : ScriptableObject
{
    public int questIndex; //���� �� ��� ����Ʈ�� �Ϸ��ߴ��� ����ϱ� ����

    [TextArea]
    public string[] questStory; //����Ʈ�� ���� �� NPC�� ����� ����

    [TextArea]
    public string[] questCompleteStory; //����Ʈ�� �Ϸ��� �� NPC�� ����� ����

    [TextArea]
    public string questDescription; //����Ʈ ��ǥ ���

    public QuestType questType; //����Ʈ ����
    public int questTargetIndex; //��ǥ�ϴ� ��/����/�������� �ε���

    public int questCurrentCount;
    public int questTargetCount; //��ǥ�ϴ� ��

    public GameObject reward; //����
}
