using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour
{
    private int poolingIndex;

    public void Setting(int poolingIndex, Vector3 summonPos)
    {
        this.poolingIndex = poolingIndex;
        transform.position = summonPos;
    }

    private void OnParticleSystemStopped()
    {
        ObjectPooling.instance.SetReadyObject(gameObject, poolingIndex);
        gameObject.SetActive(false);
    }
}
