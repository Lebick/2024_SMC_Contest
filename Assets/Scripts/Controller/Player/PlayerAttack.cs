using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAttack : MonoBehaviour
{
    public Transform player;

    public float attackRange;

    public LayerMask enemyMask;

    private void Start()
    {
        if (player == null)
            Destroy(gameObject);

        InputValueManager.instance.attackAction.AddListener(() => DefaultAttack());
    }

    private void Update()
    {
        
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
    }
}
