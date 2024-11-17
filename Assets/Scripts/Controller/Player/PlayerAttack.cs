using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    protected float[] skillCD = new float[3];
    protected float[] skillTimer = new float[3];

    protected virtual void Update()
    {
        UpdateSkillCD();
    }

    protected virtual void UpdateSkillCD()
    {
        for(int i=0; i<skillTimer.Length; i++)
        {
            if (skillTimer[i] > 0)
                skillTimer[i] -= Time.deltaTime;
        }
    }

    public virtual void Attack() { }

    public virtual void Skill1()
    {
        skillTimer[0] = skillCD[0];
    }

    public virtual void Skill2()
    {
        skillTimer[1] = skillCD[1];
    }

    public virtual void Skill3()
    {
        skillTimer[2] = skillCD[2];
    }
}
