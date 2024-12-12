using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapInteraction : MonoBehaviour, IInteractableObj
{
    public string myName;

    public WorldMap map;

    public string GetName()
    {
        return myName;
    }

    public void Interaction()
    {
        GameManager.instance.isWorldMapPause = true;

        map.gameObject.SetActive(true);
        map.animator.SetTrigger("Open");
        map.isActivate = true;
    }
}
