using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnterInteraction : MonoBehaviour, IInteractableObj
{
    public string myName;

    public string GetName()
    {
        return myName;
    }

    public void Interaction()
    {
        GameManager.instance.isCutScenePause = true;
        SceneLoadManager.instance.ChangeScene(SceneNames.DefaultDungeon);
    }
}
