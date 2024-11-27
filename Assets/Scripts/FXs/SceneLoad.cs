using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    public RectTransform fadeImage;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Fading(sceneName));
    }

    private IEnumerator Fading(string sceneName)
    {
        AsyncOperation sc = SceneManager.LoadSceneAsync(sceneName);
        sc.allowSceneActivation = false;

        float progress = 0f;
        float width = fadeImage.rect.width;

        while (progress <= 1.0f)
        {
            progress += Time.deltaTime;
            fadeImage.anchoredPosition = Vector3.Lerp(new(width, 0), new(-width, 0), progress);

            if(!sc.isDone && progress >= 0.5f)
            {
                fadeImage.anchoredPosition = Vector3.Lerp(new(width, 0), new(-width, 0), 0.5f);
                float waitTimer = 0f;

                while(sc.progress < 0.9f && waitTimer <= 1f)
                {
                    waitTimer += Time.deltaTime;
                    yield return null;
                }

                sc.allowSceneActivation = true;
            }

            yield return null;
        }

        yield return null;

        Destroy(gameObject);
    }
}
