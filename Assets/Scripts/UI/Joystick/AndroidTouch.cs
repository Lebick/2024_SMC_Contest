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

    public float touchAbleRange; //�ش� �� �̳��� ��ġ�� �����Ǿ��ٸ�, �е� �������� �����մϴ�.
    public float correctionRange; //�ش� ���� �Ѿ �������� ���� ���, �е��� �߾� ��ġ�� �����մϴ�.
    public float deadZoneRange; //�ش� �� �̻��� �������� ������ ������ �Է°��� �����մϴ�.

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

        //����
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
