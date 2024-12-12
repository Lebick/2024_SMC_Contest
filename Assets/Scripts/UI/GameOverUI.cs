using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public Text enemyKillCount;
    public Text playTime;
    public Text findRoomCount;

    public Transform buffButtonParent;
    public GameObject buffButtonPrefab;

    public List<Text> buffCheck;

    public int currentCheckBuff = -1;

    private void Start()
    {
        float time = GameManager.instance.currentScenePlayTime;
        int hh = (int)(time / 3600);
        int mm = (int)(time % 3600 / 60);
        int ss = (int)(time % 60);

        playTime.text = $"{hh:D2} : {mm:D2} : {ss:D2}";

        enemyKillCount.text = $"{GameManager.instance.enemyKillCount:N0}";

        findRoomCount.text = $"{GameManager.instance.visitRoomCount:N0}";

        for(int i=0; i<BuffManager.instance.buffs.Count; i++)
        {
            BuffInfo currentBuff = BuffManager.instance.buffs[i];

            if (currentBuff.isCantKeep) continue;

            GameObject btn = Instantiate(buffButtonPrefab, buffButtonParent).transform.Find("SelectButton").gameObject;
            btn.GetComponent<Button>().onClick.AddListener(() => OnClickBuffCheck(btn));
            buffCheck.Add(btn.transform.GetChild(0).GetComponent<Text>());
        }
    }

    public void OnClickBuffCheck(GameObject btn)
    {
        for (int i = 0; i < buffCheck.Count; i++)
            buffCheck[i].text = string.Empty;

        btn.transform.GetChild(0).GetComponent<Text>().text = "\u2713";

        currentCheckBuff = buffCheck.IndexOf(btn.transform.GetChild(0).GetComponent<Text>());
    }

    public void OnClickBackVillage()
    {

    }
}
