using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLine : MonoBehaviour
{
    private Vector3 warningCenter;
    private Vector3 warningSize;
    private float warningTime;

    public void Setting(int poolingIndex, Vector3 warningCenter, Vector3 warningSize, float warningTime)
    {
        this.warningCenter = warningCenter;
        this.warningSize = warningSize;
        this.warningTime = warningTime;

        StartCoroutine(SetWarning());
    }

    private IEnumerator SetWarning()
    {
        yield return null;
    }
}
