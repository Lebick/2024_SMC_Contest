using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public bool isWorldMapPause;    //����� �Ͻ�����
    public bool isForcePause;       //���� �Ͻ�����
    public bool isCutScenePause;    //�ƾ� �Ͻ�����
    public bool isDialoguePause;    //��ȭ �Ͻ�����
    public bool isEscapePause;      //Esc �Ͻ�����

    public bool isPause;            //�� ���� �� �ϳ��� ���� �� �Ͻ�����

    protected override void Awake()
    {
        DontDestroyOnLoad(gameObject);
            
        base.Awake();
    }

    private void FixedUpdate()
    {
        isPause = isWorldMapPause || isForcePause || isCutScenePause || isEscapePause || isDialoguePause;
    }
}
