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

    #region ����2
    [Space(10)]

    public bool     pattern2GizmosShow;

    public float    pattern2Range;
    public float    pattern2Angle;

    public ParticleSystem pattern2Effect;
    #endregion

    #region ����9
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

    #region ���ϵ�

    //�Ӷ� ���� ���� ���� ��, ���� ���� ��� ������ ����� ���ϵ� �ؼ� �Ӷ� �����ϴٴ°� �˷�����.

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

        pattern2Effect.transform.position = sharkMouthTransform.position;
        pattern2Effect.transform.localEulerAngles = new(0, facingAngle, 0);
        pattern2Effect.Play();
        animator.SetInteger("currentPattern", 2);
        animator.SetTrigger("setPattern");
        yield return new WaitForSeconds(1.5f);

        float timer = 0f;
        //������ �߻��ϸ� �ڷ� �з���
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

        //Ƣ���
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
        yield return new WaitForSeconds(1f); //1�� ����

        animator.SetTrigger("endPattern");

        //�������� ȸ��
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

        yield return new WaitForSeconds(1.0f); //1�ʰ� �غ���

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
                Vector3 dir = (nextPos - transform.position).normalized; //���Ⱚ

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
        //n�ʰ� ������ ��ġ�� �ҿ뵹�� ��ȯ / ȸ�ǰ���, �и��Ұ� / ��ȯ �� ���ǥ�� �ʼ�.
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
        //��ȣ�� ����, ������ �������� �и� �� ��ȣ�� ����.
        yield return null;
    }

    private IEnumerator Pattern11()
    {
        yield return null;
    }

    #endregion

    private Vector3 MovePosClamp(Vector3 nextPosition)
    {
        Vector3 dir = (nextPosition - transform.position).normalized; //���Ⱚ

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

    #region �����
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

        //range��ŭ�� ����, angle��ŭ�� ���� + �Ĵٺ����ִ� ����
    }

    #endregion
}
