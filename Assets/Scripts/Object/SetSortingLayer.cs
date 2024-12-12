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
            if(player.position.y < transform.position.y) //�÷��̾ �ڽź��� �Ʒ��� ����
                spriteRenderer.sortingLayerName = "PlayerBack"; //�÷��̾� �켱
            else
                spriteRenderer.sortingLayerName = "PlayerFront"; //�ڽ� �켱
        }

            
    }
}
