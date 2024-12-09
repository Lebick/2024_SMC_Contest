using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherArrow : MonoBehaviour
{
    private Vector3 dir;

    public float speed;

    private void Start()
    {
        dir = (GameManager.instance.player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.localEulerAngles = new(0, 0, angle);

        Destroy(gameObject, 2f);
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.isPause) return;

        transform.position += dir * speed * Time.fixedDeltaTime;
    }
}
