using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Buff", menuName = "BuffData", order = 2)]
public class BuffInfo : ScriptableObject
{
    [TextArea]
    public string buffDescription;

    public float attackStrength;
    public float armorStrength;
    public float criticalChance;
    public float criticalDamage;
    public float maxHP;

    public string constraints;
    public UnityEvent buffAction;

    public bool isCantKeep;

    public void Initialize(string description, float attack, float armor, float criticalChange, float criticalDamage, float maxHP)
    {
        this.buffDescription = description;
        this.attackStrength = attack;
        this.armorStrength = armor; 
        this.criticalChance = criticalChange;
        this.criticalDamage = criticalDamage;
        this.maxHP = maxHP;
    }
}