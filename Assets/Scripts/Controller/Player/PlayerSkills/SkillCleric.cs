using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCleric : PlayerAttack
{
    public override void Attack()
    {
        base.Attack();
        //���� �־�̴� ���Ÿ�����
    }

    public override void Skill1()
    {
        base.Skill1();
        //Ư�� �Ÿ� �ȿ� �ִ� �÷��̾� ȸ�� (����������, ���ȸ��x)
    }

    public override void Skill2()
    {
        base.Skill2();
        //Ư�� �Ÿ� �ȿ� �ִ� �÷��̾�� ��ȣ�� �ο�
    }

    public override void Skill3()
    {
        base.Skill3();
        //Ư�� �Ÿ� �ȿ� �ִ� �÷��̾�� ������ ���� ���� �ο�
    }
}
