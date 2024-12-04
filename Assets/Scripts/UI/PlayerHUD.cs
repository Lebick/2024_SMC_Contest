using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Text hpText;
    public Image hpFill;
    public Image hpRedFill;
    private float currentHP;
    private float hpDecreaseTimer;

    public void UpdateHUD(PlayerController player)
    {
        UpdateHPValue(player);
    }

    private void UpdateHPValue(PlayerController player)
    {
        if(currentHP != player.hp)
        {
            currentHP = player.hp;
            hpDecreaseTimer = 0;
        }

        float hpAmount = (float)player.hp / player.maxHP;
        hpText.text = $"HP | {hpAmount * player.maxHP} / {player.maxHP}";
        hpFill.fillAmount = Mathf.Lerp(hpFill.fillAmount, hpAmount, Time.deltaTime * 10f);

        if(hpDecreaseTimer > 0.5f)
            hpRedFill.fillAmount = Mathf.Lerp(hpRedFill.fillAmount, hpAmount, Time.deltaTime * 10f);
        else
            hpDecreaseTimer += Time.deltaTime;
    }
}
