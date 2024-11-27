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

    //패링 가능 공격 시전 시, 보스 몸을 잠깐 빨갛게 만들든 뭘하든 해서 패링 가능하다는걸 알려야함.

    private IEnumerator Pattern0()
    {
        // 앞발로 평타 / 패링 가능 / 패링 성공시 아주 잠깐 보스 스턴
        yield return null;
    }

    private IEnumerator Pattern1()
    {
        // 뒤쪽 꼬리치기 / 패링 가능
        yield return null;
    }

    private IEnumerator Pattern2()
    {
        // 전방에 거품으로 공격 & 뒤로 밀림, 벽만나면 잠깐 스턴 / 패링 불가능
        yield return null;
    }

    private IEnumerator Pattern3()
    {
        // 전기구슬 n회 발사, 발사 0.5초 전까지 플레이어를 봄 / 패링 불가능
        yield return null;
    }

    private IEnumerator Pattern4()
    {
        // 점프하면서 돌아다니면서 지랄 (마우루그 점프 지랄패턴) / 위에서 온갖거 떨어질텐데 그거 패링 가능
        yield return null;
    }

    private IEnumerator Pattern5()
    {
        // 자신 주변 반경 내 안전지대, 이후 자신 주변에 유저가 있을 시 잡기 / 잡기 : 패링&회피 불가 / 안전지대 : 패링 가능
        yield return null;
    }

    private IEnumerator Pattern6()
    {
        // 위쪽으로 수영해서 올라감, 이후 맵 전체 범위공격(전기공격) / 패링 가능 / 패링 실패 시 데미지 + 스턴
        yield return null;
    }

    private IEnumerator Pattern7()
    {
        //플레이어 방향으로 뛰어서 공격 / 패링 가능 / 패링 시 보스 잠깐 스턴
        yield return null;
    }

    private IEnumerator Pattern8()
    {
        //(스프라이트 만들 수 있으면) n초간 회전, 벽돌깨기 게임처럼 벽에 부딪힐 시 반사됨.
        //플레이어 닿을 시 플레이어 잡음, 플레이어에게 일정 데미지 주고 날려보냄
        //회피불가, 패링불가
        yield return null;
    }

    private IEnumerator Pattern9()
    {
        //n초간 랜덤한 위치에 소용돌이 소환 / 회피가능, 패링불가 / 소환 전 경고표시 필수.
        yield return null;
    }

    private IEnumerator Pattern10()
    {
        //보호막 생성, 보스의 근접공격 패링 시 보호막 해제.
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
