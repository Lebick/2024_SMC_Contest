using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaBoss : Controller
{
    private float[] bossPatternCD = { };
    private float[] bossPatternTimer = { };

    private IEnumerator currentPattern;
    private bool isPattern;

    protected override void Update()
    {
        base.Update();

        DecreasePatternTimer();


    }

    private void DecreasePatternTimer()
    {
        for(int i=0; i< bossPatternTimer.Length; i++)
        {
            if (bossPatternTimer[i] > 0)
            bossPatternTimer[i] -= Time.deltaTime;
        }
    }

    //�и� ���� ���� ���� ��, ���� ���� ��� ������ ����� ���ϵ� �ؼ� �и� �����ϴٴ°� �˷�����.

    private IEnumerator Pattern0()
    {
        // �չ߷� ��Ÿ / �и� ���� / �и� ������ ���� ��� ���� ����
        yield return null;
    }

    private IEnumerator Pattern1()
    {
        // ���� ����ġ�� / �и� ����
        yield return null;
    }

    private IEnumerator Pattern2()
    {
        // ���濡 ��ǰ���� ���� & �ڷ� �и�, �������� ��� ���� / �и� �Ұ���
        yield return null;
    }

    private IEnumerator Pattern3()
    {
        // ���ⱸ�� nȸ �߻�, �߻� 0.5�� ������ �÷��̾ �� / �и� �Ұ���
        yield return null;
    }

    private IEnumerator Pattern4()
    {
        // �����ϸ鼭 ���ƴٴϸ鼭 ���� (������ ���� ��������) / ������ �°��� �������ٵ� �װ� �и� ����
        yield return null;
    }

    private IEnumerator Pattern5()
    {
        // �ڽ� �ֺ� �ݰ� �� ��������, ���� �ڽ� �ֺ��� ������ ���� �� ��� / ��� : �и�&ȸ�� �Ұ� / �������� : �и� ����
        yield return null;
    }

    private IEnumerator Pattern6()
    {
        // �������� �����ؼ� �ö�, ���� �� ��ü ��������(�������) / �и� ���� / �и� ���� �� ������ + ����
        yield return null;
    }

    private IEnumerator Pattern7()
    {
        //�÷��̾� �������� �پ ���� / �и� ���� / �и� �� ���� ��� ����
        yield return null;
    }

    private IEnumerator Pattern8()
    {
        //(��������Ʈ ���� �� ������) n�ʰ� ȸ��, �������� ����ó�� ���� �ε��� �� �ݻ��.
        //�÷��̾� ���� �� �÷��̾� ����, �÷��̾�� ���� ������ �ְ� ��������
        //ȸ�ǺҰ�, �и��Ұ�
        yield return null;
    }

    private IEnumerator Pattern9()
    {
        //n�ʰ� ������ ��ġ�� �ҿ뵹�� ��ȯ / ȸ�ǰ���, �и��Ұ� / ��ȯ �� ���ǥ�� �ʼ�.
        yield return null;
    }

    private IEnumerator Pattern10()
    {
        //��ȣ�� ����, ������ �������� �и� �� ��ȣ�� ����.
        yield return null;
    }

    private IEnumerator Pattern11()
    {
        yield return null;
    }

    public override void GetDamage(float damage, Vector3 hitObjectPos = default, float knockback = 0)
    {
        base.GetDamage(damage, hitObjectPos, knockback);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
    }
}
