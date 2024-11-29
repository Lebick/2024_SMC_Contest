using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackOrb : MonoBehaviour
{
    private TrailRenderer trailRenderer;

    private int pullingIndex;
    private Transform targetTransform;

    public float moveTime = 0.2f; // 해당 값만큼의 시간동안 적에게 이동
    public GameObject hitEffect;

    private float damage;

    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.time = moveTime;
    }

    public void Setting(int pullingIndex, Vector3 startPos, Transform target, float damage)
    {
        this.pullingIndex = pullingIndex;
        transform.position = startPos;
        targetTransform = target;
        this.damage = damage;

        StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget()
    {
        Vector3 p1 = transform.position;
        Vector3 p3 = targetTransform.position;
        Vector3 p2 = p1 + Quaternion.Euler(0, 0, Random.Range(-110f, 110f)) * (p3 - p1);

        float progress = 0f;
        while (progress < 1f)
        {
            if (GameManager.instance.isPause)
            {
                yield return null;
                continue;
            }

            progress += Time.deltaTime / moveTime;
            p3 = targetTransform.position;
            Vector3 p4 = Vector3.Lerp(p1, p2, progress);
            Vector3 p5 = Vector3.Lerp(p2, p3, progress);
            transform.position = Vector3.Lerp(p4, p5, progress);
            yield return null;
        }

        if(targetTransform.TryGetComponent<Controller>(out Controller enemy) && !enemy.isInvincibility)
        {
            enemy.GetDamage(damage);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(trailRenderer.time);

        ObjectPulling.instance.SetReadyObject(gameObject, pullingIndex);
        gameObject.SetActive(false);
    }
}
