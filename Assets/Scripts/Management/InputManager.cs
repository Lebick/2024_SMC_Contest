using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public bool isAndroid;

    public List<Touch> touch = new();

    public KeyCode[] leftKey      = { KeyCode.A, KeyCode.None };
    public KeyCode[] downKey      = { KeyCode.S, KeyCode.None };
    public KeyCode[] rightKey     = { KeyCode.D, KeyCode.None };
    public KeyCode[] upKey        = { KeyCode.W, KeyCode.None };
    public KeyCode[] attackKey    = { KeyCode.Mouse0, KeyCode.None };
    public KeyCode[] dodgeKey     = { KeyCode.Space, KeyCode.None };
    public KeyCode[] parryingKey  = { KeyCode.Mouse1, KeyCode.None };

    private float horizontal;
    private float vertical;

    private void Update()
    {
        GetTouchPos();

        if (isAndroid) return;

        SetValue();
        SetInputAction();

    }

    private void GetTouchPos()
    {
        touch.Clear();

        for(int i=0; i<Input.touchCount; i++)
            touch.Add(Input.GetTouch(i));
    }

    private void SetValue()
    {
        horizontal = (Input.GetKey(leftKey[0]) || Input.GetKey(leftKey[1]) ? -1 : 0) + (Input.GetKey(rightKey[0]) || Input.GetKey(rightKey[1]) ? 1 : 0);
        vertical = (Input.GetKey(downKey[0]) || Input.GetKey(downKey[1]) ? -1 : 0) + (Input.GetKey(upKey[0]) || Input.GetKey(upKey[1]) ? 1 : 0);
        InputValueManager.instance.moveDir = new Vector3(horizontal, vertical).normalized;
    }

    private void SetInputAction()
    {
        KeyCode[] inputKeys = attackKey.Concat(dodgeKey).Concat(parryingKey).ToArray();

        foreach (KeyCode key in inputKeys)
        {
            if (Input.GetKeyDown(key))
            {
                if (attackKey.Contains(key))
                {
                    InputValueManager.instance.attackAction?.Invoke();
                    break;
                }

                if (dodgeKey.Contains(key))
                {
                    InputValueManager.instance.dodgeAction?.Invoke();
                    break;
                }

                if (parryingKey.Contains(key))
                {
                    InputValueManager.instance.parryingAction?.Invoke();
                    break;
                }
            }
        }
    }
}
