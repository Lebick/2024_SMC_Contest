using System.Collections;
using System.Collections.Generic;
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
        foreach(KeyCode key in attackKey)
        {
            if (Input.GetKeyDown(key))
            {
                InputValueManager.instance.attackActions?.Invoke();
                break;
            }
        }
    }
}
