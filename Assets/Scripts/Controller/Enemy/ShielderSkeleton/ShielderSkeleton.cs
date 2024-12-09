using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ShielderSkeleton : EnemyController
{
    public float skillCD;
    public float skillUseRange;
    private float skillTimer;

    private bool isUsingSkill;

    public ParticleSystem skillEffect;

    private NavMeshAgent agent;

    public Transform canvas;
    public Image hpBar;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected override void Update()
    {
        base.Update();

        canvas.transform.localScale = new(transform.localScale.x, 1, 1);
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, hp / maxHP, Time.deltaTime * 10f);
    }

    private void FixedUpdate()
    {
        if ((GameManager.instance.isPause || isDeath) && !agent.isStopped)
            agent.isStopped = true;

        if (player == null || isDeath || GameManager.instance.isPause) return;

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

        yield return new WaitForSeconds(60f / 60f);

        skillEffect.Play();

        if(Physics2D.OverlapCircle(transform.position, skillEffect.main.startSize.constant, LayerMask.GetMask("Player")).TryGetComponent(out Controller controller))
        {
            controller.GetDamage(damage, transform.position, 5f);
            print("맞어");
        }

        //n범위 안 적 공격

        yield return new WaitForSeconds(40f / 60f);

        isUsingSkill = false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, skillUseRange);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        animator.SetTrigger("Death");
        Invoke(nameof(DestroyObj), (18f / 12f));
    }

    private void DestroyObj()
    {
        Destroy(gameObject);
    }
}
