using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller
{
    public ParticleSystem warningEffect;

    public float playerDetectRange;
    protected Transform player;

    public int enemyIndex;
    public float damage;

    protected override void Update()
    {
        base.Update();

        if(player == null)
            FindPlayer();
    }

    private void FindPlayer()
    {
        this.player = UsefulObjectManager.instance.player.transform;
        Collider2D player = Physics2D.OverlapCircle(transform.position, playerDetectRange, LayerMask.GetMask("Player"));

        

        if (player != null)
            this.player = player.transform;
    }

    public override void GetDamage(float damage, Vector3 hitObjectPos = default, float knockback = 0, bool isCritical = false)
    {
        if (isDeath) return;
        base.GetDamage(damage, hitObjectPos, knockback);
    }

    protected override void OnDeath()
    {
        QuestManager.instance.AddQuestCount(QuestType.EnemyKill, enemyIndex);
        print("ÀÌÈ÷Èþ");
        gameObject.layer = 0;

        base.OnDeath();
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectRange);
    }
}
