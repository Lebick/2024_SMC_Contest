using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Text hpText;
    public Image hpFill;
    public Image hpRedFill;

    public Text xpText;
    public Image xpFill;

    private float currentHP;
    private float hpDecreaseTimer;

    public void UpdateHUD(PlayerController player)
    {
        UpdateHPValue(player);
        UpdateXPValue(player);
    }

    private void UpdateHPValue(PlayerController player)
    {
        if(currentHP != player.hp)
        {
            currentHP = player.hp;
            hpDecreaseTimer = 0;
        }

        float hpAmount = (float)player.hp / player.maxHP;
        hpText.text = $"HP | {player.hp} / {player.maxHP}";
        hpFill.fillAmount = Mathf.Lerp(hpFill.fillAmount, hpAmount, Time.deltaTime * 10f);

        if(hpDecreaseTimer > 0.5f)
            hpRedFill.fillAmount = Mathf.Lerp(hpRedFill.fillAmount, hpAmount, Time.deltaTime * 10f);
        else
            hpDecreaseTimer += Time.deltaTime;

        if (player.hp <= 0)
        {
            hpRedFill.fillAmount = 0;
            hpFill.fillAmount = 0;
        }
    }

    private void UpdateXPValue(PlayerController player)
    {
        float xpAmount = player.xp == 0 ? 0 : player.xp / player.maxXP;
        xpText.text = $"XP | {(int)player.xp} / {(int)player.maxXP}";
        xpFill.fillAmount = Mathf.Lerp(xpFill.fillAmount, xpAmount, Time.deltaTime * 10f);
    }
}
