using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricOrb : MonoBehaviour, IPullingObj
{
    private int getPullingIndex;
    private int myPullingIndex;

    private Transform player;

    public void Setting(int pullingIndex, Vector3 startPos)
    {
        player = GameManager.instance.player.transform;

        transform.position = startPos;
        this.getPullingIndex = pullingIndex;
    }

    private IEnumerator FollowingPlayer()
    {
        float progress = 0f;

        Vector3 p1 = transform.position;
        Vector3 p3 = player.position;
        Vector3 p2 = p1 + Quaternion.Euler(0, 0, Random.Range(-110f, 110f)) * (p3 - p1);

        while (progress <= 0.8f)
        {
            if (GameManager.instance.isPause)
            {
                yield return null;
                continue;
            }

            progress += Time.deltaTime;
            p3 = player.position;
            Vector3 p4 = Vector3.Lerp(p1, p2, progress);
            Vector3 p5 = Vector3.Lerp(p2, p3, progress);
            transform.position = Vector3.Lerp(p4, p5, progress);


        }

        yield return null;
    }
}
