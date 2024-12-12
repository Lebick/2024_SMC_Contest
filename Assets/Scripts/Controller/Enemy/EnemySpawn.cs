using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private GameObject enemy;
    private Transform parent;

    public float waitTime;

    private DungeonEnemySpawn room;

    private void Start()
    {
        Invoke(nameof(Spawn), waitTime);
    }

    public void Setting(GameObject enemy, Transform parent, DungeonEnemySpawn room)
    {
        this.enemy = enemy;
        this.parent = parent;
        this.room = room;
    }

    private void Spawn()
    {
        GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity, parent);
        room.summonedEnemys.Add(newEnemy);
    }
}
