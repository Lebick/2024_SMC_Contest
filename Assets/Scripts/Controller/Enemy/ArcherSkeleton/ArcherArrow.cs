using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherArrow : MonoBehaviour
{
    private Vector3 dir;

    public float speed;

    private float damage;

    public void Setting(float damage)
    {
        this.damage = damage;

        dir = (UsefulObjectManager.instance.player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.localEulerAngles = new(0, 0, angle);

        Destroy(gameObject, 2f);
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.isPause) return;

        transform.position += dir * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            player.GetDamage(damage, player.transform.position - dir, 3f);
        }
    }
}
