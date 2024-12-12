using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateUI : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerAttack playerAttack;

    public Image hpFill;
    public Text hpText;

    public Text attackStrengthText;
    public Text armorStrengthText;
    public Text criticalChangeText;
    public Text criticalDamageText;

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        hpFill.fillAmount = playerController.hp / playerController.maxHP;
        hpText.text = $"HP | {playerController.hp / playerController.maxHP * playerController.maxHP} / {playerController.hp / playerController.maxHP}";

        attackStrengthText.text = $"공격력\n{playerAttack.damage}";
        armorStrengthText.text  = $"방어력\n{playerController.armorStrength}";
        criticalChangeText.text = $"치명타 확률\n{playerAttack.criticalChance}";
        criticalChangeText.text = $"치명타 피해\n{playerAttack.criticalDamage}";
    }
}
