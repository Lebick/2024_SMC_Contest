using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMark : MonoBehaviour
{
    public Transform warningFill;

    private float warningTime;
    private int getPullingIndex;
    private int myPullingIndex;

    public void Setting(int pullingIndex, Vector2 summonPos, float warningTime, GameObject warningAfterSummonObj)
    {
        transform.position = summonPos;

        getPullingIndex = pullingIndex;
        myPullingIndex = ObjectPulling.instance.RegisterObject(warningAfterSummonObj);

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
            progress += Time.deltaTime / warningTime;
            warningFill.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, progress * (1 / 0.8f));
            yield return null;
        }

        ObjectPulling.instance.GetObject(myPullingIndex).GetComponent<IPullingObj>().Setting(myPullingIndex, transform.position);

        originalColor.a = 0f;
        mySpriteRenderer.color = originalColor;

        originalColor.a = 1f;
        while (progress < 1f)
        {
            progress += Time.deltaTime / warningTime;
            originalColor.a = Mathf.Lerp(1f, 0f, 5 * (progress - 0.8f));
            fillSpriteRenderer.color = originalColor;
            yield return null;
        }

        ObjectPulling.instance.SetReadyObject(gameObject, getPullingIndex);
        gameObject.SetActive(false);
    }
}
