using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPulling : Singleton<ObjectPulling>
{
    public Transform pullingObjParent;

    public List<PullingObj> pullingObj = new List<PullingObj>();

    public int RegisterObject(GameObject prefab)
    {
        foreach(PullingObj obj in pullingObj)
            if (obj.prefab == prefab)
                return obj.pullingIndex;

        pullingObj.Add(new PullingObj(prefab, pullingObj.Count));

        return pullingObj.Count - 1;
    }

    public GameObject GetObject(int myIndex)
    {
        Transform parent = pullingObjParent.Find(pullingObj[myIndex].prefab.name);
        GameObject obj;

        if (parent == null)
        {
            parent = new GameObject(pullingObj[myIndex].prefab.name).transform;
            parent.SetParent(pullingObjParent, false);
        }

        if (pullingObj[myIndex].activateList.Count > 0)
        {
            obj = pullingObj[myIndex].activateList[0];
            pullingObj[myIndex].activateList.RemoveAt(0);
        }

        else
            obj = Instantiate(pullingObj[myIndex].prefab, parent);

        obj.SetActive(true);

        return obj;
    }

    public void SetReadyObject(GameObject obj, int myIndex)
    {
        pullingObj[myIndex].activateList.Add(obj);
    }
}

public class PullingObj
{
    public GameObject parent;
    public GameObject prefab;
    public int pullingIndex;
    public List<GameObject> activateList = new List<GameObject>();

    public PullingObj(GameObject prefab, int pullingIndex)
    {
        this.prefab = prefab;
        this.pullingIndex = pullingIndex;
    }
}