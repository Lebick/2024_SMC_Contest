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

    public GameObject playerDeathUI;

    public float currentScenePlayTime;
    public int enemyKillCount;
    public int visitRoomCount;

    protected override void Awake()
    {
        //DontDestroyOnLoad(gameObject);
            
        base.Awake();
    }

    private void FixedUpdate()
    {
        isPause = isWorldMapPause || isForcePause || isCutScenePause || isEscapePause || isDialoguePause;

        if (!isPause)
        {
            currentScenePlayTime += Time.deltaTime;
        }
    }

    public void OnPlayerDeath()
    {
        isForcePause = true;
        playerDeathUI.SetActive(true);
    }
}
