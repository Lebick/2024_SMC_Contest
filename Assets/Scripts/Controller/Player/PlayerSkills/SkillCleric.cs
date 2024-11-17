using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCleric : PlayerAttack
{
    public override void Attack()
    {
        base.Attack();
        //대충 있어보이는 원거리공격
    }

    public override void Skill1()
    {
        base.Skill1();
        //특정 거리 안에 있는 플레이어 회복 (게이지형식, 즉시회복x)
    }

    public override void Skill2()
    {
        base.Skill2();
        //특정 거리 안에 있는 플레이어에게 보호막 부여
    }

    public override void Skill3()
    {
        base.Skill3();
        //특정 거리 안에 있는 플레이어에게 데미지 증가 버프 부여
    }
}
