using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffActions : MonoBehaviour
{
    //��� ���� ����. ��� ���� +[������ ����2]
    public void AllBuffDestroy()
    {
        float value = BuffManager.instance.buffs.Count * 2;
        BuffManager.instance.buffs.Clear();

        BuffInfo buff = ScriptableObject.CreateInstance<BuffInfo>();
        buff.Initialize("<color=#F84600>Ž��</color>", value, value, value, value, value);
        BuffManager.instance.buffs.Add(buff);
    }
}
