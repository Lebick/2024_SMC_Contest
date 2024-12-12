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
        if (BuffManager.instance.buffs.Count > uiList.Count) //UI 수 적음
        {
            if (unactivateUIs.Count <= 0) //재사용가능한거 없음
            {
                GameObject newUI = Instantiate(buffUIPrefab, transform); //생성
                uiList.Add(newUI);
            }
            else //재사용됨
            {
                GameObject newUI = unactivateUIs[0];
                uiList.Add(newUI); //재사용
                unactivateUIs.RemoveAt(0);
            }
        }
        else if (BuffManager.instance.buffs.Count < uiList.Count) // UI수 더 많음
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

            value.text = $"공격력 +{buff.attackStrength}\t\t방어력+{buff.armorStrength}\n" +
                        $"치명타 확률 +{buff.criticalChance}%\t\t치명타 피해 +{buff.criticalDamage}%\n" +
                        $"{(buff.constraints != string.Empty ? "제약 조건 : {buff.constraints}" : string.Empty)}";
        }
    }
}
