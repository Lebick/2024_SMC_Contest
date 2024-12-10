using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private GameObject enemy;
    private Transform parent;

    public float waitTime;

    private void Start()
    {
        Invoke(nameof(Spawn), waitTime);
    }

    public void Setting(GameObject enemy, Transform parent)
    {
        this.enemy = enemy;
        this.parent = parent;
    }

    private void Spawn()
    {
        Instantiate(enemy, transform.position, Quaternion.identity, parent);
    }
}
