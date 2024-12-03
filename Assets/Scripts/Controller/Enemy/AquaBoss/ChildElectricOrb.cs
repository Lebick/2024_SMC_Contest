 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildElectricOrb : MonoBehaviour
{
    private Vector3 p1, p2;

    public void Setting(int poolingIndex, Vector3 startPos, Vector3 finalPos)
    {
        p1 = startPos;
        p2 = finalPos;
        transform.position = startPos;
    }

    private IEnumerator Move()
    {
        float progress = 0;

        while (progress <= 1.0f)
        {
            if (GameManager.instance.isPause)
            {
                yield return null;
                continue;
            }

            progress += Time.deltaTime;
            Vector3 p4 = Vector3.Lerp(p1, p2, progress);
            Vector3 p5 = Vector3.Lerp(p2, p1, progress);
            transform.position = Vector3.Lerp(p4, p5, progress);

            yield return null;
        }
    }
}
