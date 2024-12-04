using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    public GameObject fadeCanvas;

    public int currentSceneIndex;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(SceneNames sceneName)
    {
        Instantiate(fadeCanvas).GetComponent<SceneLoad>().LoadScene(sceneName);
        currentSceneIndex = (int)sceneName;
    }
}
