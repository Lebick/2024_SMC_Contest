using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMark : MonoBehaviour
{
    public Transform warningFill;

    private float warningTime;
    private int getPoolingIndex;
    private int myPoolingIndex;

    public void Setting(int poolingIndex, Vector2 summonPos, float warningTime, GameObject warningAfterSummonObj = null)
    {
        transform.position = summonPos;

        getPoolingIndex = poolingIndex;

        if (warningAfterSummonObj != null)
            myPoolingIndex = ObjectPooling.instance.RegisterObject(warningAfterSummonObj);
        else
            myPoolingIndex = -1;

        this.warningTime = warningTime;

        StartCoroutine(SetWarning());
    }

    private IEnumerator SetWarning()
    {
        SpriteRenderer mySpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer fillSpriteRenderer = warningFill.GetComponent<SpriteRenderer>();

        Color originalColor = mySpriteRenderer.color;

        originalColor.a = 0.25f;
        mySpriteRenderer.color = originalColor;

        originalColor.a = 1f;
        fillSpriteRenderer.color = originalColor;


        float progress = 0f;

        while(progress < 0.8f)
        {
            if (GameManager.instance.isPause)
            {
                yield return null;
                continue;
            }

            progress += Time.deltaTime / warningTime;
            warningFill.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, progress * (1 / 0.8f));
            yield return null;
        }

        if(myPoolingIndex != -1)
            ObjectPooling.instance.GetObject(myPoolingIndex).GetComponent<IPoolingObj>().Setting(myPoolingIndex, transform.position);

        originalColor.a = 0f;
        mySpriteRenderer.color = originalColor;

        originalColor.a = 1f;
        while (progress < 1f)
        {
            if (GameManager.instance.isPause)
            {
                yield return null;
                continue;
            }

            progress += Time.deltaTime / warningTime;
            originalColor.a = Mathf.Lerp(1f, 0f, 5 * (progress - 0.8f));
            fillSpriteRenderer.color = originalColor;
            yield return null;
        }

        ObjectPooling.instance.SetReadyObject(gameObject, getPoolingIndex);
        gameObject.SetActive(false);
    }
}
