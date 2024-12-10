using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DungeonEnemySpawn : MonoBehaviour
{
    public GameObject enemySpawnEffect;
    public List<GameObject> summonEnemyList;

    public Transform enemyParent;

    public GameObject exit;

    public Vector3 mapCenter;
    public Vector3 mapSize;

    private bool isSummon;
    private bool isAllEnemySpawn;
    private List<GameObject> summonedEnemys = new();

    private void Update()
    {
        if(Physics2D.OverlapBox(transform.position + mapCenter, mapSize, 0, LayerMask.GetMask("Player")) && !isSummon)
        {
            isSummon = true;
            exit.SetActive(true);
            StartCoroutine(SummonEnemy());
        }

        for(int i=0; i<summonedEnemys.Count; i++)
        {
            if (summonedEnemys[i] == null)
            {
                summonedEnemys.RemoveAt(i);
                i--;
            }
        }

        if (isAllEnemySpawn && summonedEnemys.Count <= 0)
        {
            exit.SetActive(false);
        }
    }

    private IEnumerator SummonEnemy()
    {
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < summonEnemyList.Count; i++)
        {
            float lx = (mapSize.x / 2f) + mapCenter.x;
            float ly = (mapSize.y / 2f) + mapCenter.y;
            Vector3 randomPos = new Vector2(Random.Range(-lx, lx), Random.Range(-ly, ly));

            NavMeshHit hit;
            bool isOnNavMesh = NavMesh.SamplePosition(transform.position + randomPos, out hit, 0.5f, NavMesh.AllAreas);
            int count = 0;
            while (!isOnNavMesh)
            {
                randomPos = new Vector2(Random.Range(-lx, lx), Random.Range(-ly, ly));
                isOnNavMesh = NavMesh.SamplePosition(transform.position + randomPos, out hit, 0.5f, NavMesh.AllAreas);
                 
                count++;
                if (count > 100)
                    break;
            }

            GameObject enemy = Instantiate(enemySpawnEffect, transform.position + randomPos, Quaternion.identity, enemyParent);
            enemy.GetComponent<EnemySpawn>().Setting(summonEnemyList[i], enemyParent);
            summonedEnemys.Add(enemy);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        }
        yield return null;

        isAllEnemySpawn = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + mapCenter, mapSize);
    }
}
