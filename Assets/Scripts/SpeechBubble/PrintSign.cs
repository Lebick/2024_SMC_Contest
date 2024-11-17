using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PrintSign : MonoBehaviour
{
    [TextArea]
    public string myText;

    public AudioClip textSFX;

    public Text bubbleText;

    public float printRange;

    public float printSpeed; //어느 시간동안 텍스트를 출력하는지를 결정합니다.
    private float printProgress;

    private bool isJoin;
    private bool isCanPrint;

    private int charIndex = 0;

    private void Update()
    {
        FindPlayer();

        CheckEmpty();

        if (!isCanPrint) return;
        SetText(!isJoin);
    }

    private void FindPlayer()
    {
        Collider2D[] objs = Physics2D.OverlapCircleAll(transform.position, printRange);

        foreach (Collider2D obj in objs)
        {
            if (obj.CompareTag("Player"))
            {
                isJoin = true;
                return;
            }
        }

        isJoin = false;
    }

    private void CheckEmpty()
    {
        if(!isJoin && charIndex == 0)
        {
            bubbleText.text = ". . .";
            isCanPrint = false;
        }
        else
        {
            isCanPrint = true;
        }
    }

    private void SetText(bool isReverse)
    {
        printProgress += Time.deltaTime / printSpeed * (isReverse ? -1 : 1);
        printProgress = Mathf.Clamp01(printProgress);

        charIndex = (int)Mathf.Lerp(0, myText.Length, printProgress);

        string currentText = bubbleText.text;

        bubbleText.text = myText[..charIndex];

        if (!currentText.Equals(bubbleText.text) && !isReverse)
            SoundManager.instance.PlaySFX(textSFX);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, printRange);
    }
}
