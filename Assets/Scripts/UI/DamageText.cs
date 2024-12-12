using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    private RectTransform rect;
    private Text text;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        text = GetComponent<Text>();

        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        float progress = 0f;

        Color startColor = text.color;

        Color finalColor = startColor;
        finalColor.a = 0;

        while(progress <= 1f)
        {
            if (GameManager.instance.isPause)
            {
                yield return null;
                continue;
            }

            progress += Time.deltaTime * 2f;

            rect.anchoredPosition += Vector2.up * Time.deltaTime;
            text.color = Color.Lerp(startColor, finalColor, progress);
            yield return null;
        }

        yield return null;

        Destroy(gameObject);
    }
}
