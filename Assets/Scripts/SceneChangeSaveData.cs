using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeSaveData : Singleton<SceneChangeSaveData>
{
    public List<BuffInfo> buffs = new();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
