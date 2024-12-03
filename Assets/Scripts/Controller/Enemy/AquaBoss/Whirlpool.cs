using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlpool : MonoBehaviour, IPoolingObj
{
    private int myPoolingIndex;

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

    public void Setting(int poolingIndex, Vector3 startPos)
    {
        isCheckTrigger = true;

        myPoolingIndex = poolingIndex;
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
        ObjectPooling.instance.SetReadyObject(gameObject, myPoolingIndex);
        gameObject.SetActive(false);
    }

    public void ForceStop()
    {
        isCheckTrigger = false;

        if(IsInvoking(nameof(StopCheck)))
            CancelInvoke(nameof(StopCheck));

        particle.Stop();
    }
}
