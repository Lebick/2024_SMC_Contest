using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricOrb : MonoBehaviour
{
    private int getPoolingIndex;
    private int myPoolingIndex;

    private Transform player;

    private void Start()
    {
        player = UsefulObjectManager.instance.player.transform;
        Setting(3, Vector3.zero, 69);
    }

    public void Setting(int poolingIndex, Vector3 startPos, int childObjectPoolingIndex)
    {
        transform.position = startPos;
        this.getPoolingIndex = poolingIndex;
        this.myPoolingIndex = childObjectPoolingIndex;

        StartCoroutine(FollowingPlayer());
    }

    private IEnumerator FollowingPlayer()
    {
        float progress = 0f;

        Vector3 p1 = transform.position;
        Vector3 p3 = player.position;
        Vector3 p2 = p1 + Quaternion.Euler(0, 0, Random.Range(-110f, 110f)) * (p3 - p1);

        Vector3 lastDir = new();

        while (progress <= 0.9f)
        {
            if (GameManager.instance.isPause)
            {
                yield return null;
                continue;
            }

            progress += Time.deltaTime;
            Vector3 p4 = Vector3.Lerp(p1, p2, progress);
            Vector3 p5 = Vector3.Lerp(p2, p3, progress);
            transform.position = Vector3.Lerp(p4, p5, progress);

            lastDir = p5 - p4;
            yield return null;
        }

        p1 = transform.position;

        Vector3[] p6 = { transform.position + lastDir.normalized * 8f ,
        Quaternion.Euler(0, 0, Random.Range(10f, 30f)) * (transform.position + lastDir.normalized * 8f),
        Quaternion.Euler(0, 0, Random.Range(-30f, -10f)) * (transform.position + lastDir.normalized * 8f)};

        for(int i=0; i<3; i++)
        {
            ObjectPooling.instance.GetObject(myPoolingIndex).GetComponent<ChildElectricOrb>().Setting(myPoolingIndex, transform.position, p6[i]);
        }

        yield return null;
    }
}
