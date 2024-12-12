using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public Text text;

    private PlayerController player;

    private void Start()
    {
        player = UsefulObjectManager.instance.player;
    }

    private void Update()
    {
        if (player.nearestInteraction != null)
        {
            string name = player.nearestInteraction.GetName();
            text.text = $"<color=#FFA600>{name}</color>{KoreanPostposition.GetPostposition(name, "과", "와")} 상호작용";
        }
        else
            text.text = string.Empty;
    }
}
