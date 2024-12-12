using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneNames
{
    Test = -100,
    Title = -1,
    Village = 0,
    AquaBoss = 1,
    DefaultDungeon = 2
}

public class SceneLoad : MonoBehaviour
{
    public RectTransform fadeImage;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(SceneNames sceneName)
    {
        StartCoroutine(Fading(sceneName.ToString()));
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
                    if (GameManager.instance.isPause)
                    {
                        yield return null;
                        continue;
                    }

                    waitTimer += Time.deltaTime;
                    yield return null;
                }

                sc.allowSceneActivation = true;
            }

            yield return null;
        }

        yield return null;

        GameManager.instance.isWorldMapPause = false;
        GameManager.instance.isCutScenePause = false;

        Destroy(gameObject);
    }
}
