using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    public Transform poolingObjParent;

    public List<PoolingObj> poolingObj = new List<PoolingObj>();

    public int RegisterObject(GameObject prefab)
    {
        foreach(PoolingObj obj in poolingObj)
            if (obj.prefab == prefab)
                return obj.poolingIndex;

        poolingObj.Add(new PoolingObj(prefab, poolingObj.Count));

        return poolingObj.Count - 1;
    }

    public GameObject GetObject(int myIndex)
    {
        Transform parent = poolingObjParent.Find(poolingObj[myIndex].prefab.name);
        GameObject obj;

        if (parent == null)
        {
            parent = new GameObject(poolingObj[myIndex].prefab.name).transform;
            parent.SetParent(poolingObjParent, false);
        }

        if (poolingObj[myIndex].activateList.Count > 0)
        {
            obj = poolingObj[myIndex].activateList[0];
            poolingObj[myIndex].activateList.RemoveAt(0);
        }

        else
            obj = Instantiate(poolingObj[myIndex].prefab, parent);

        obj.SetActive(true);

        return obj;
    }

    public void SetReadyObject(GameObject obj, int myIndex)
    {
        poolingObj[myIndex].activateList.Add(obj);
    }
}

public class PoolingObj
{
    public GameObject parent;
    public GameObject prefab;
    public int poolingIndex;
    public List<GameObject> activateList = new List<GameObject>();

    public PoolingObj(GameObject prefab, int poolingIndex)
    {
        this.prefab = prefab;
        this.poolingIndex = poolingIndex;
    }
}

