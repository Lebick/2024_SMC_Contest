using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public bool isWorldMapPause;    //월드맵 일시정지
    public bool isForcePause;       //강제 일시정지
    public bool isCutScenePause;    //컷씬 일시정지
    public bool isDialoguePause;    //대화 일시정지
    public bool isEscapePause;      //Esc 일시정지

    public bool isPause;            //위 사항 중 하나라도 포함 시 일시정지

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
