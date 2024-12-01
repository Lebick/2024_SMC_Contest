using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlpool : MonoBehaviour, IPullingObj
{
    private int myPullingIndex;

    private bool isCheckTrigger;
    public float damage;

    private ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        Invoke(nameof(StopCheck), particle.main.duration);
    }

    private void StopCheck()
    {
        isCheckTrigger = false;
    }

    public void Setting(int pullingIndex, Vector3 startPos)
    {
        isCheckTrigger = true;

        myPullingIndex = pullingIndex;
        transform.position = startPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCheckTrigger) return;
        if (collision.CompareTag("Player") && TryGetComponent<Controller>(out Controller controller))
        {
            controller.GetDamage(damage, transform.position, 1f);
        }
    }

    private void OnParticleSystemStopped()
    {
        ObjectPulling.instance.SetReadyObject(gameObject, myPullingIndex);
        gameObject.SetActive(false);
    }
}
