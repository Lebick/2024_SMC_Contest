using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageShard : MonoBehaviour
{
    public float speed;

    public float readyTime;
    private float readyTimer = 0;

    private Transform player;

    private Vector2 dir;

    private float damage;

    public void Setting(float damage)
    {
        this.damage = damage;

        player = UsefulObjectManager.instance.player.transform;
        Destroy(gameObject, readyTime + 2f);
    }

    private void FixedUpdate()
    {
        readyTimer += Time.fixedDeltaTime;

        if (readyTimer >= readyTime)
        {
            transform.position += (Vector3)dir * Time.fixedDeltaTime * speed;
        }
        else
        {
            dir = (player.position - transform.position).normalized;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.localEulerAngles = new(0, 0, angle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerController player))
        {
            player.GetDamage(damage, player.transform.position - (Vector3)dir, 3f);
        }
    }
}
