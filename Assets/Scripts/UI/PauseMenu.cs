using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.SetActive(!pauseUI.activeSelf);

            GameManager.instance.isEscapePause = pauseUI.activeSelf;
        }
    }

    private void OnClickResumeBtn()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);

        GameManager.instance.isEscapePause = pauseUI.activeSelf;
    }

    private void OnClickSurrenderBtn()
    {
        PlayerController player = UsefulObjectManager.instance.player;
        player.GetDamage(player.maxHP, player.transform.position, 0);
    }

}
