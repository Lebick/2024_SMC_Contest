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
        
    }

    private void DefaultAttack()
    {
        //아페잇는저글때린다
    }
}
