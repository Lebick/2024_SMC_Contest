using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class PlayerAttack : MonoBehaviour
{
    public Transform player;

    public float attackRange;

    public LayerMask enemyMask;

    public float stopPosRange;
    private float acceleration;

    private void Start()
    {
        if (player == null)
            Destroy(gameObject);

        InputValueManager.instance.attackAction.AddListener(() => DefaultAttack());
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (stopPosRange > Vector3.Distance(transform.position, player.position))
        {
            acceleration = Mathf.Lerp(acceleration, 0f, 0.02f);
        }
        else
        {
            acceleration = Vector3.Distance(transform.position, player.position) * 0.01f;
        }

        transform.position = Vector3.Lerp(transform.position, player.position, acceleration);
    }

    private void DefaultAttack()
    {
        GameObject attackTarget = GetNearestEnemy();

        if (attackTarget == null) return;

        
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
