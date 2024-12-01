using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour
{
    private int pullingIndex;

    public void Setting(int pullingIndex, Vector3 summonPos)
    {
        this.pullingIndex = pullingIndex;
        transform.position = summonPos;
    }

    private void OnParticleSystemStopped()
    {
        ObjectPulling.instance.SetReadyObject(gameObject, pullingIndex);
        gameObject.SetActive(false);
    }
}
