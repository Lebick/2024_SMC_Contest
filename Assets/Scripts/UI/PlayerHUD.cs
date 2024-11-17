using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Image hpFill;
    public Image hpRedFill;
    private float currentHP;
    private float hpDecreaseTimer;

    public Image skill1Fill;
    public Image skill2Fill;
    public Image skill3Fill;

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

        hpFill.fillAmount = hpAmount;

        if(hpDecreaseTimer > 1f)
            hpRedFill.fillAmount = Mathf.Lerp(hpRedFill.fillAmount, hpAmount, Time.deltaTime * 10f);
        else
            hpDecreaseTimer += Time.deltaTime;
    }
}
