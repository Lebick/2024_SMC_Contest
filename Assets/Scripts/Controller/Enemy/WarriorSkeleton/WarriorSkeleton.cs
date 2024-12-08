using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class WarriorSkeleton : EnemyController
{
    public float skillCD;
    public float skillUseRange;
    private float skillTimer;

    public float skillDistance;

    private bool isUsingSkill;

    public ParticleSystem skillEffect;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        bool isNearbyPlayer = Vector3.Distance(transform.position, player.position) <= skillUseRange;

        if (skillTimer > 0)
        {
            skillTimer -= Time.fixedDeltaTime;
        }

        animator.SetBool("isWalk", false);

        if (isNearbyPlayer)
        {
            if (skillTimer <= 0)
            {
                skillTimer = skillCD;
                isUsingSkill = true;
                StartCoroutine(UseSkill());
            }

            agent.isStopped = true;
        }
        else if (!isUsingSkill)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);

            animator.SetBool("isWalk", true);
        }

        transform.localScale = new Vector2(Mathf.Sign(player.position.x - transform.position.x), 1);
    }

    private IEnumerator UseSkill()
    {
        yield return null;
        animator.SetTrigger("Skill");
        yield return new WaitForSeconds(25f / 60f);

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + (player.position - transform.position).normalized * skillDistance;

        float progress = 0f;

        skillEffect.Play();

        CircleCollider2D circle = GetComponent<CircleCollider2D>();

        Vector2 lastestPos = startPos;

        while (progress <= 1f)
        {
            progress += Time.deltaTime * 2f;
            float acceleration = Mathf.Sqrt(1 - Mathf.Pow(progress - 1, 2));

            transform.position = Vector3.Lerp(startPos, targetPos, acceleration);

            NavMeshHit hit;
            bool isOnNavMesh = NavMesh.SamplePosition(transform.position, out hit, 0.1f, NavMesh.AllAreas);

            if (!isOnNavMesh) //이동 가능한 영역에서 벗어났으면
            {
                transform.position = lastestPos; //최근 이동한 위치로 이동
                break; //탈출
            }

            lastestPos = transform.position;

            yield return null;
        }
        skillEffect.Stop();

        yield return new WaitForSeconds((20f / 60f) - (1f - progress) / 2f);

        isUsingSkill = false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, skillUseRange);
    }
}
