using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSortingLayer : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player == null && GameManager.instance != null)
            player = UsefulObjectManager.instance.player.transform;

        if(player != null)
        {
            if(player.position.y < transform.position.y) //플레이어가 자신보다 아래에 있음
                spriteRenderer.sortingLayerName = "PlayerBack"; //플레이어 우선
            else
                spriteRenderer.sortingLayerName = "PlayerFront"; //자신 우선
        }

            
    }
}
