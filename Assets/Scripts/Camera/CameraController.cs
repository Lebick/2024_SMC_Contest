using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraController : Singleton<CameraController>
{
    private Transform player;

    public float followingSpeed;
    public Vector3 mapCenter;
    public Vector3 mapSize;

    private Vector2 screenSize;

    private Vector3 currentPosition;

    private IEnumerator shakeCoroutine;
    private Vector3 shakePosition;

    private void Start()
    {
        screenSize.y = Camera.main.orthographicSize;
        screenSize.x = screenSize.y * Screen.width / Screen.height;

        currentPosition = transform.position;
    }

    private void Update()
    {
        //GetComponent<PixelPerfectCamera>().refResolutionX = Screen.width;
        //GetComponent<PixelPerfectCamera>().refResolutionY = Screen.height;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.isPause) return;

        FindPlayer();

        if (player != null)
            FollowPlayer();
    }

    private void FindPlayer()
    {
        if (player == null && GameManager.instance != null)
            player = UsefulObjectManager.instance.player.transform;
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = player.position;
        targetPosition.z = transform.position.z;

        float lx = mapSize.x - screenSize.x;
        targetPosition.x = Mathf.Clamp(targetPosition.x, -lx + mapCenter.x, lx + mapCenter.x);

        float ly = mapSize.y - screenSize.y;
        targetPosition.y = Mathf.Clamp(targetPosition.y, -ly + mapCenter.y, ly + mapCenter.y);

        currentPosition = Vector3.Lerp(currentPosition, targetPosition, 0.02f * followingSpeed);
        transform.position = currentPosition + shakePosition;
    }

    public void CameraShake(float strength, float time)
    {
        if(shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = ShakeCoroutine(strength, time);
        StartCoroutine(shakeCoroutine);
    }

    private IEnumerator ShakeCoroutine(float strength, float time)
    {
        float progress = 0;

        while (progress <= 1f)
        {
            if (GameManager.instance.isPause)
            {
                yield return null;
                continue;
            }

            shakePosition = Random.insideUnitCircle * strength;
            progress += Time.deltaTime / time;
            yield return null;
        }

        shakePosition = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(mapCenter, mapSize * 2);
    }
}
