using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuffUIList : MonoBehaviour
{
    public GameObject buffUIPrefab;

    private List<GameObject> uiList = new();
    private List<GameObject> unactivateUIs = new();

    private void Update()
    {
        ChangeUICount();

        SetUIContent();
    }

    private void ChangeUICount()
    {
        if (BuffManager.instance.buffs.Count > uiList.Count) //UI �� ����
        {
            if (unactivateUIs.Count <= 0) //���밡���Ѱ� ����
            {
                GameObject newUI = Instantiate(buffUIPrefab, transform); //����
                uiList.Add(newUI);
            }
            else //�����
            {
                GameObject newUI = unactivateUIs[0];
                uiList.Add(newUI); //����
                unactivateUIs.RemoveAt(0);
            }
        }
        else if (BuffManager.instance.buffs.Count < uiList.Count) // UI�� �� ����
        {
            for (int i = 0; i < uiList.Count - BuffManager.instance.buffs.Count; i++)
            {
                uiList[0].SetActive(false);
                unactivateUIs.Add(uiList[0]);
                uiList.RemoveAt(0);
            }
        }
    }

    private void SetUIContent()
    {
        for (int i = 0; i < uiList.Count; i++)
        {
            Text description    = uiList[i].transform.GetChild(0).GetComponent<Text>();
            Text value          = uiList[i].transform.GetChild(1).GetComponent<Text>();

            BuffInfo buff       = BuffManager.instance.buffs[i];

            description.text    = buff.buffDescription;

            value.text = $"���ݷ� +{buff.attackStrength}\t\t����+{buff.armorStrength}\n" +
                        $"ġ��Ÿ Ȯ�� +{buff.criticalChance}%\t\tġ��Ÿ ���� +{buff.criticalDamage}%\n" +
                        $"{(buff.constraints != string.Empty ? "���� ���� : {buff.constraints}" : string.Empty)}";
        }
    }
}
