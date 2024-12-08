using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller
{
    public ParticleSystem playerFind;

    public float playerDetectRange;
    protected Transform player;

    public int enemyIndex;

    protected override void Update()
    {
        base.Update();

        if(player == null)
            FindPlayer();
    }

    private void FindPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, playerDetectRange, LayerMask.GetMask("Player"));


        if (player != null)
        {
            this.player = player.transform;
            playerFind.Play();
        }
    }

    protected override void OnDeath()
    {
        QuestManager.instance.AddQuestCount(QuestType.EnemyKill, enemyIndex);

        gameObject.layer = 0;

        base.OnDeath();
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectRange);
    }
}
