using System.Collections.Generic;
using UnityEngine;

public class AndroidTouch : MonoBehaviour
{
    private List<Touch> touch;
    private Vector3 originalPosition;

    private bool isTouch;
    private Touch movePadTouch;

    //------------------------------------------------------------------------------------------------------

    public RectTransform joystickCenter;
    public RectTransform joystickMovePad;

    public float touchAbleRange; //해당 값 이내에 터치가 감지되었다면, 패드 움직임을 시작합니다.
    public float correctionRange; //해당 값을 넘어간 움직임이 보일 경우, 패드의 중앙 위치를 보정합니다.
    public float deadZoneRange; //해당 값 이상의 움직임을 보였을 때에만 입력값을 리턴합니다.

    //------------------------------------------------------------------------------------------------------

    public Vector3 moveDir;

    private void Start()
    {
        if (!InputManager.instance.isAndroid) Destroy(gameObject);

        originalPosition = joystickCenter.anchoredPosition;
    }

    private void Update()
    {
        touch = InputManager.instance.touch;

        CheckTouch();

        if (isTouch)
            MoveJoystickCenter();
        else
            JoystickPosReset();

        GetMoveDir();
    }


    private void CheckTouch()
    {
        foreach (Touch pos in touch)
        {
            bool touchState = isTouch;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos.position);
            float distance = Vector2.Distance(worldPos, joystickCenter.position);

            if (distance < touchAbleRange)
            {
                isTouch = true;
                movePadTouch = pos;
                if (isTouch != touchState)
                {
                    Vector3 stickCenter = worldPos;
                    stickCenter.z = joystickCenter.position.z;
                    joystickCenter.position = stickCenter;
                    joystickMovePad.position = stickCenter;
                }
                return;
            }
        }
        isTouch = false;
    }

    private void MoveJoystickCenter()
    {
        Vector3 movePadPos = Camera.main.ScreenToWorldPoint(movePadTouch.position);
        movePadPos.z = joystickMovePad.position.z;
        joystickMovePad.position = movePadPos;

        //보정
        float distance = Vector2.Distance(joystickMovePad.position, joystickCenter.position);
        if (distance > correctionRange)
        {
            Vector3 dir = (joystickCenter.position - joystickMovePad.position).normalized;
            Vector3 centerPos = dir * (correctionRange - distance);
            centerPos.z = 0;
            joystickCenter.position = joystickCenter.position + centerPos;
        }
    }

    private void JoystickPosReset()
    {
        joystickCenter.anchoredPosition = Vector3.Lerp(joystickCenter.anchoredPosition, originalPosition, 50 * Time.deltaTime);
        joystickMovePad.anchoredPosition = joystickCenter.anchoredPosition;
    }

    private void GetMoveDir()
    {
        float distance = Vector2.Distance(joystickMovePad.position, joystickCenter.position);

        if (distance > deadZoneRange)
            moveDir = (joystickMovePad.position - joystickCenter.position).normalized;
        else
            moveDir = Vector3.zero;

        InputValueManager.instance.moveDir = moveDir;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(joystickCenter.position, touchAbleRange);

        Gizmos.DrawWireSphere(joystickCenter.position, deadZoneRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(joystickCenter.position, correctionRange);
    }
}
