using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MonoBehaviour
{
    public Animator animator;

    public bool isActivate;

    public RectTransform vilageBtn;
    public RectTransform testBtn;
    public RectTransform aquaBtn;

    public RectTransform currentPositionSign;
    public Vector2 currentPositionOffset;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 signPos = new();

        switch ((SceneNames)SceneLoadManager.instance.currentSceneIndex)
        {
            case SceneNames.Test:
                signPos = testBtn.anchoredPosition;
                break;

            case SceneNames.Vilage:
                signPos = vilageBtn.anchoredPosition;
                break;

            case SceneNames.AquaBoss:
                signPos = aquaBtn.anchoredPosition;
                break;
        }

        currentPositionSign.anchoredPosition = signPos + currentPositionOffset;
            


        if(Input.GetKeyDown(KeyCode.Escape))
            OnClickExitBtn();

        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Equals("Null"))
        {
            GameManager.instance.isWorldMapPause = false;
            gameObject.SetActive(false);
        }
    }

    public void OnClickMapBtn(int stage)
    {
        if (!isActivate) return;
        if (stage == SceneLoadManager.instance.currentSceneIndex) return;

        SceneLoadManager.instance.ChangeScene((SceneNames)stage);
    }

    public void OnClickExitBtn()
    {
        if (isActivate)
        {
            isActivate = false;
            animator.SetTrigger("Close");
        }
    }
}
