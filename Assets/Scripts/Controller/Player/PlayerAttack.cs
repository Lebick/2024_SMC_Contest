using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        if (player == null && GameManager.instance != null)
            player = GameManager.instance.player.transform;

        if (player == null)
            Destroy(gameObject);

        InputValueManager.instance.attackActions.AddListener(() => DefaultAttack());
    }

    private void Update()
    {
        //if()
    }

    private void DefaultAttack()
    {
        print($"Å×½ºÆ® : {name}");
    }
}
