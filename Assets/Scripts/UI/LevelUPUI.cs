using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelUPUI : MonoBehaviour
{
    public List<BuffInfo> allBuffs;

    private BuffInfo[] currentShowBuff = new BuffInfo[3];

    public Button[] buttons;
    public Text[] buffDescription; //����
    public Text[] buffState; //ȿ��

    private void Start()
    {
        Setting();
    }

    public void Setting()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = true;
        }

        if (allBuffs.Count <= 3)
        {
            Debug.LogError("���� ���� ����");
            return;
        }

        currentShowBuff = new BuffInfo[3];
        List<BuffInfo> copyBuff = allBuffs.ToList();

        for(int i=0; i<currentShowBuff.Length; i++)
        {
            int buffIndex = Random.Range(0, copyBuff.Count);
            currentShowBuff[i] = copyBuff[buffIndex];
            copyBuff.RemoveAt(buffIndex);
        }

        if(!(buffDescription.Length == buffState.Length))
        {
            Debug.LogError("�ؽ�Ʈ ���� �ٸ�");
            return;
        }
        
        for(int i=0; i<buffState.Length; i++)
        {
            BuffInfo buff = currentShowBuff[i];

            string info = string.Empty;

            if (buff.attackStrength != 0)
                info += $"���ݷ� {(buff.attackStrength >= 0 ? "+" : "")}{buff.attackStrength}\n";

            if (buff.armorStrength != 0)
                info += $"���� {(buff.armorStrength >= 0 ? "+" : "")}{buff.armorStrength}\n";

            if (buff.criticalChance != 0)
                info += $"ġ��Ÿ Ȯ�� {(buff.criticalChance >= 0 ? "+" : "")}{buff.criticalChance}%\n";

            if (buff.criticalDamage != 0)
                info += $"ġ��Ÿ ���� {(buff.criticalDamage >= 0 ? "+" : "")}{buff.criticalDamage}%\n";

            if(buff.constraints != string.Empty)
                info += $"\n�ΰ� ȿ��: {buff.constraints}";

            buffDescription[i].text = buff.buffDescription;
            buffState[i].text = info;
        }
    }

    public void OnClickSelectBtn(int index)
    {
        BuffManager.instance.buffs.Add(currentShowBuff[index]);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = false;
            
        }
        buttons[index].GetComponent<Animator>().SetTrigger("BtnClick");
        //��Ȱ��ȭ
    }
}
