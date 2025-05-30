using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class PlayerAttack : MonoBehaviour
{
    // ------------------풀링------------------
    public GameObject attackPrefab;
    private int myPoolingIndex;

    // ------------------설정------------------

    public  AudioClip[] attackClips;
    public  Transform player;

    public  float   attackRange;

    public LayerMask enemyMask;

    public  float   stopPosRange;
    private float   acceleration;

    public  int     damage;

    public  int     attackMaxCount;
    public int     attackCurrentCount;
    public  float   attackCD;
    public float   attackTimer;

    public float criticalChance;
    public float criticalDamage = 100f;

    // ----------------------------------------

    private void Start()
    {
        if (player == null)
            Destroy(gameObject);

        myPoolingIndex = ObjectPooling.instance.RegisterObject(attackPrefab);
        InputValueManager.instance.attackAction.AddListener(() => DefaultAttack());
    }

    #region FixedUpdate문
    private void FixedUpdate()
    {
        if (GameManager.instance.isPause) return;

        GameObject nearestEnemy = GetNearestEnemy();

        if (nearestEnemy == null)
            FollowPlayer();
        else
            FollowEnemy(nearestEnemy);

        if (attackCurrentCount < attackMaxCount)
            RecoveryAttackOrb();
    }

    private void FollowPlayer()
    {
        if (stopPosRange > Vector3.Distance(transform.position, player.position)) //현재 구슬이 범위 내에 있다면
            acceleration = Mathf.Lerp(acceleration, 0f, 0.02f);
        else
            acceleration = Vector3.Distance(transform.position, player.position) * 0.01f;

        transform.position = Vector3.Lerp(transform.position, player.position, acceleration);
    }

    private void FollowEnemy(GameObject nearestEnemy)
    {
        Vector3 dir = (nearestEnemy.transform.position - transform.position).normalized;

        Vector3 targetPos = player.position + dir * stopPosRange * 0.8f;

        if (0.1f > Vector3.Distance(transform.position, targetPos))
            acceleration = Mathf.Lerp(acceleration, 0f, 0.02f);
        else
            acceleration = Vector3.Distance(transform.position, targetPos) * 0.03f;

        transform.position = Vector3.Lerp(transform.position, targetPos, acceleration);
    }

    private void RecoveryAttackOrb()
    {
        attackTimer += 1 * Time.fixedDeltaTime;
        if(attackTimer >= attackCD)
        {
            attackTimer -= attackCD;
            attackCurrentCount++;
        }
    }

    #endregion

    private void DefaultAttack()
    {
        if(player.TryGetComponent(out PlayerController controller) && controller.nearestInteraction != null)
        {
            controller.nearestInteraction.Interaction();
            return;
        }

        GameObject attackTarget = GetNearestEnemy();

        if (attackTarget == null || attackCurrentCount <= 0) return;

        attackCurrentCount--;

        SoundManager.instance.PlaySFX(attackClips[Random.Range(0, attackClips.Length)], 0.1f);

        bool isCritical = Random.Range(0f, 100f) <= criticalChance;

        float fianlDamage = damage * (isCritical ? (criticalDamage / 100f) : 1f);

        AttackOrb orb = ObjectPooling.instance.GetObject(myPoolingIndex).GetComponent<AttackOrb>();
        orb.Setting(myPoolingIndex, transform.position, attackTarget.transform, fianlDamage, isCritical);
    }

    private GameObject GetNearestEnemy()
    {
        Collider2D[] nearEnemys = Physics2D.OverlapCircleAll(player.position, attackRange, enemyMask);

        if (nearEnemys.Length <= 0)
            return null;

        return nearEnemys.OrderBy(a => Vector3.Distance(player.position, a.transform.position)).First().gameObject;
    }

    private void OnDrawGizmos()
    {
        if (player == null) return;

        Gizmos.color = Color.blue;  
        Gizmos.DrawWireSphere(player.position, attackRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(player.position, stopPosRange);
    }
}
