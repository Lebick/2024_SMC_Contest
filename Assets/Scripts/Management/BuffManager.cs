using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffManager : Singleton<BuffManager>
{
    public List<BuffInfo> buffs = new();

    private int lastestBuffCount = 0;

    public float attackStrength;
    public float armorStrength;
    public float criticalChange;
    public float criticalDamage;

    private void Start()
    {
        if(SceneChangeSaveData.instance.buffs != null)
            buffs = SceneChangeSaveData.instance.buffs.ToList();
    }

    private void Update()
    {
        if(lastestBuffCount != buffs.Count)
        {
            lastestBuffCount = buffs.Count;
            GetBuffState();
        }
    }

    private void GetBuffState()
    {
        attackStrength = 0;
        armorStrength = 0;
        criticalChange = 0;
        criticalDamage = 0;

        foreach (BuffInfo buff in buffs)
        {
            if (buff.buffAction != null)
                buff.buffAction?.Invoke();

            attackStrength += buff.attackStrength;
            armorStrength += buff.armorStrength;
            criticalChange += buff.criticalChance; 
            criticalDamage += buff.criticalDamage;
        }
    }
}
