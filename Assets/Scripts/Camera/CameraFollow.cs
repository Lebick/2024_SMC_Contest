using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;

    public float followingSpeed;
    public Vector3 mapCenter;
    public Vector3 mapSize;

    private Vector2 screenSize;

    private void Start()
    {
        screenSize.y = Camera.main.orthographicSize;
        screenSize.x = screenSize.y * Screen.width / Screen.height;
    }

    private void FixedUpdate()
    {
        FindPlayer();

        if (player != null)
            FollowPlayer();
    }

    private void FindPlayer()
    {
        if (player == null && GameManager.instance != null)
            player = GameManager.instance.player.transform;
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = player.position;
        targetPosition.z = transform.position.z;

        float lx = mapSize.x - screenSize.x;
        targetPosition.x = Mathf.Clamp(targetPosition.x, -lx + mapCenter.x, lx + mapCenter.x);

        float ly = mapSize.y - screenSize.y;
        targetPosition.y = Mathf.Clamp(targetPosition.y, -ly + mapCenter.y, ly + mapCenter.y);

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.02f * followingSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(mapCenter, mapSize * 2);
    }
}
