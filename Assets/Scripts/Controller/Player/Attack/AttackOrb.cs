using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackOrb : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private Transform targetTransform;

    public GameObject hitEffect;

    private float damage;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Setting(Transform target, float damage)
    {
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
            //if (GamePlayManager.instance.isCutScene || GamePlayManager.instance.isPause)
            //{
            //    yield return null;
            //    continue;
            //}


            progress += Time.deltaTime;
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

        gameObject.SetActive(false);
    }
}
