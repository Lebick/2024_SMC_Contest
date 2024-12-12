using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ArcherSkeleton : EnemyController
{
    public float skillCD;
    public float skillUseRange;
    private float skillTimer;

    private bool isUsingSkill;

    private NavMeshAgent agent;

    public GameObject arrow;
    public Transform arrowSpawnPos;

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
        warningEffect.Play();
        animator.SetTrigger("Skill");

        float timer = 0f;

        while(timer >= (7f / 3f))
        {
            if (GameManager.instance.isPause)
            {
                yield return null;
                continue;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(70f / 30f);

        GameObject arrow = Instantiate(this.arrow, arrowSpawnPos.position, arrowSpawnPos.rotation);
        arrow.GetComponent<ArcherArrow>().Setting(damage);

        yield return new WaitForSeconds(35f / 30f);

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
