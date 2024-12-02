using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AquaBoss : Controller
{
    private CircleCollider2D circleCollider;
    public SpriteRenderer sharkSprite;

    private float[] bossPatternCD = { };
    private float[] bossPatternTimer = { };

    private IEnumerator currentPattern;
    private bool isPattern;

    public float facingAngle;
    private Vector2 facingVector;

    public Transform sharkMouthTransform;

    public ParticleSystem stunEffect;

    public GameObject warningMark;
    private int warningPullingIndex;

    #region 패턴2
    [Space(10)]

    public bool     pattern2GizmosShow;

    public float    pattern2Range;
    public float    pattern2Angle;

    public ParticleSystem pattern2Effect;
    #endregion

    #region 패턴9
    [Space(10)]
    public GameObject whirlpool;
    public Transform[] whirlpoolSummonPos;
    public ParticleSystem pattern9Effect;
    #endregion

    //---------------------------------------

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();

        warningPullingIndex = ObjectPulling.instance.RegisterObject(warningMark);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Y))
            StartCoroutine(Pattern9());

        if (Input.GetKeyDown(KeyCode.U))
            StartCoroutine(Pattern2());

        if (Input.GetKeyDown(KeyCode.I))
            StartCoroutine(Pattern8());

        SetSprite();
    }

    private void SetSprite()
    {
        facingVector.x = (facingAngle >= 315 || facingAngle < 45) ? 1 : (facingAngle >= 135 && facingAngle <= 255) ? -1 : 0;
        facingVector.y = (facingAngle > 255 && facingAngle < 315) ? 1 : (facingAngle >= 45 && facingAngle < 135) ? -1 : 0;

        transform.localScale = new(facingVector.x != 0 ? facingVector.x : transform.localScale.x, 1f);
    }

    private void FixedUpdate()
    {
        DecreasePatternTimer();
    }

    private void DecreasePatternTimer()
    {
        for(int i=0; i< bossPatternTimer.Length; i++)
        {
            if (bossPatternTimer[i] > 0)
            bossPatternTimer[i] -= Time.fixedDeltaTime;
        }
    }

    #region 패턴들

    //팰라 가능 공격 시전 시, 보스 몸을 잠깐 빨갛게 만들든 뭘하든 해서 팰라 가능하다는걸 알려야함.

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

        pattern2Effect.transform.position = sharkMouthTransform.position;
        pattern2Effect.transform.localEulerAngles = new(0, facingAngle, 0);
        pattern2Effect.Play();
        animator.SetInteger("currentPattern", 2);
        animator.SetTrigger("setPattern");
        yield return new WaitForSeconds(1.5f);

        float timer = 0f;
        //앞으로 발사하며 뒤로 밀려남
        while (true)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp01(timer);

            float acceleration = Mathf.Pow(timer, 3f);

            Vector3 nextPos = transform.position + Quaternion.Euler(0, 0, -facingAngle) * Vector3.left * acceleration * Time.deltaTime * 50f;

            if (nextPos != MovePosClamp(nextPos))
            {
                transform.position = MovePosClamp(nextPos);
                break;
            }

            transform.position = MovePosClamp(nextPos);

            yield return null;
        }

        pattern2Effect.Stop();

        animator.SetTrigger("endPattern");

        Vector2 pos = transform.position;
        Vector2 stunPos = transform.position + Quaternion.Euler(0, 0, -facingAngle) * Vector3.right * 2f;
        Vector3 center = pos + (Vector2)(Quaternion.Euler(0, 0, 60f * Mathf.Cos(Mathf.PI / 180f * facingAngle)) * (stunPos - pos));

        //튀어나옴
        float progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime * 2f;
            Vector3 p1 = Vector3.Lerp(pos, center, progress);
            Vector3 p2 = Vector3.Lerp(center, stunPos, progress);
            transform.position = Vector3.Lerp(p1, p2, progress);
            yield return null;
        }

        stunEffect.Play();
        yield return new WaitForSeconds(1f); //1초 스턴

        animator.SetTrigger("endPattern");

        //앞쪽으로 회전
        pos = transform.position;
        stunPos = transform.position + Quaternion.Euler(0, 0, -facingAngle) * Vector3.right * 2f;
        center = pos + (Vector2)(Quaternion.Euler(0, 0, 60f * Mathf.Cos(Mathf.PI / 180f * facingAngle)) * (stunPos - pos));

        progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime * 2f;
            Vector3 p1 = Vector3.Lerp(pos, center, progress);
            Vector3 p2 = Vector3.Lerp(center, stunPos, progress);
            transform.position = Vector3.Lerp(p1, p2, progress);
            yield return null;
        }
        animator.SetTrigger("endPattern");

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

        yield return new WaitForSeconds(1.0f); //1초간 준비동작

        float x = Mathf.Cos(facingAngle * Mathf.Deg2Rad);
        float y = Mathf.Sin(facingAngle * Mathf.Deg2Rad);

        x = (Mathf.Abs(x) > 0 ? 1f : -1f);
        y = (Mathf.Abs(y) > 0 ? 1f : -1f);

        Vector3 moveDir = new Vector3(x, y).normalized;

        int collisionCount = 0;

        while(collisionCount <= 5)
        {
            Vector3 nextPos = transform.position + moveDir * Time.deltaTime * 5f;

            if (nextPos != MovePosClamp(nextPos))
            {
                collisionCount--;
                Vector3 dir = (nextPos - transform.position).normalized; //방향값

                Vector3 detectOffset = dir * circleCollider.radius * 2.0f;

                Vector3 center = transform.position + (Vector3)circleCollider.offset;
                Vector3 fixPos = nextPos + detectOffset + (Vector3)circleCollider.offset;

                RaycastHit2D hit = Physics2D.Linecast(center, fixPos, LayerMask.GetMask("Wall"));

                moveDir = Vector3.Reflect(moveDir, hit.normal);
            }

            transform.position = MovePosClamp(nextPos);
            yield return null;
        }

        yield return null;
    }

    private IEnumerator Pattern9()
    {
        //n초간 랜덤한 위치에 소용돌이 소환 / 회피가능, 패링불가 / 소환 전 경고표시 필수.
        List<Transform> pos = whirlpoolSummonPos.ToList();

        animator.SetInteger("currentPattern", 9);
        animator.SetTrigger("setPattern");

        pattern9Effect.Play();
        CameraController.instance.CameraShake(0.1f, 2f);

        for (int i=0; i<10; i++)
        {
            int randomIndex = Random.Range(0, pos.Count);
            Vector2 spawnPos = pos[randomIndex].position;
            pos.RemoveAt(randomIndex);

            ObjectPulling.instance.GetObject(warningPullingIndex).GetComponent<WarningMark>().Setting(warningPullingIndex, spawnPos, 1f, whirlpool);

            yield return new WaitForSeconds(0.2f);
        }
        animator.SetTrigger("endPattern");
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

    #endregion

    private Vector3 MovePosClamp(Vector3 nextPosition)
    {
        Vector3 dir = (nextPosition - transform.position).normalized; //방향값

        Vector3 detectOffset = dir * circleCollider.radius * 2.0f;

        Vector3 center = transform.position + (Vector3)circleCollider.offset;
        Vector3 fixPos = nextPosition + detectOffset + (Vector3)circleCollider.offset;

        Debug.DrawLine(center, fixPos, Color.red);
        RaycastHit2D hit = Physics2D.Linecast(center, fixPos, LayerMask.GetMask("Wall"));

        if (hit.collider == null)
            return nextPosition;
        else
            return (Vector3)hit.point - detectOffset - (Vector3)circleCollider.offset;
    }

    public override void GetDamage(float damage, Vector3 hitObjectPos = default, float knockback = 0)
    {
        base.GetDamage(damage, hitObjectPos, knockback);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
    }

    #region 기즈모
    private void OnDrawGizmos()
    {
        if(pattern2GizmosShow)
            Pattern2Gizmos();
    }

    private void Pattern2Gizmos()
    {
        Gizmos.color = Color.red;
        Vector2 targetPos1 = sharkMouthTransform.position + Quaternion.Euler(0, 0, pattern2Angle - facingAngle) * new Vector2(pattern2Range, 0);
        Vector2 targetPos2 = sharkMouthTransform.position + Quaternion.Euler(0, 0, -pattern2Angle - facingAngle) * new Vector2(pattern2Range, 0);

        Gizmos.DrawLine(sharkMouthTransform.position, targetPos1);
        Gizmos.DrawLine(sharkMouthTransform.position, targetPos2);
        Gizmos.DrawWireSphere(sharkMouthTransform.position, pattern2Range);

        //range만큼의 길이, angle만큼의 각도 + 쳐다보고있는 각도
    }

    #endregion
}
