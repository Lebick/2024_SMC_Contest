using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillArcher : PlayerAttack
{
    public override void Attack()
    {
        base.Attack();
        //활로 쏨.
    }

    public override void Skill1()
    {
        base.Skill1();
        //기본공격을 퍼펙트존 있는 적룡포같은 스킬로 변경 (기본공격 사용 이후 다시 원래대로)
    }

    public override void Skill2()
    {
        base.Skill2();
        //은신 (n초간 무적)
    }

    public override void Skill3()
    {
        base.Skill3();
        //뭐하지?
    }
}
