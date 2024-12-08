using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageSkeleton : EnemyController
{
    public float skillCD;
    public float skillUseRange;
    private float skillTimer;

    private bool isUsingSkill;

    public GameObject shardPrefab;
    public Transform[] shardPos;

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
        yield return new WaitForSeconds(30f / 60f);

        for(int i=0; i<shardPos.Length; i++)
        {
            Instantiate(shardPrefab, shardPos[i].position, Quaternion.identity);
            yield return new WaitForSeconds(24f / shardPos.Length / 60f); 
        }

        yield return new WaitForSeconds(26f / 60f);

        isUsingSkill = false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, skillUseRange);
    }
}
