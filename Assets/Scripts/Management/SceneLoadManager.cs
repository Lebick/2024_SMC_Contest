using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    public GameObject fadeCanvas;

    public void ChangeScene(string sceneName)
    {
        Instantiate(fadeCanvas).GetComponent<SceneLoad>().LoadScene(sceneName);
    }
}
