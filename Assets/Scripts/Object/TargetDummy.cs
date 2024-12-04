using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : Controller
{
    public bool isAttackDummy;
    public float playerDetectRange;
    public ParticleSystem attackEffect;

    public float attackCD;
    private float attackTimer;

    private bool isNearbyPlayer;
    private bool isAttackState;

    private Transform player;

    private void Start()
    {
        player = GameManager.instance.player.transform;
    }

    public override void GetDamage(float damage, Vector3 hitObjectPos = default, float knockback = 0)
    {
        base.GetDamage(damage, hitObjectPos, knockback);

        if(!isAttackState)
            animator.SetTrigger("Hit");
    }

    protected override void Update()
    {
        if (GameManager.instance.isPause) return;

        base.Update();

        //자동회복
        if (hp < maxHP)
            hp += (int)(100 * Time.deltaTime);

        if(isAttackDummy)
            Attack();
    }

    private void Attack()
    {
        isNearbyPlayer = Vector3.Distance(transform.position, player.position) < playerDetectRange;

        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        if(isNearbyPlayer && attackTimer <= 0)
        {
            attackTimer = attackCD;
            isAttackState = true;
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        attackEffect.Play();
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(2f);

        if (isNearbyPlayer)
        {
            player.GetComponent<PlayerController>().GetDamage(0, transform.position, 0);
        }

        yield return new WaitForSeconds(0.683f);
        isAttackState = false;

        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectRange);
    }
}
