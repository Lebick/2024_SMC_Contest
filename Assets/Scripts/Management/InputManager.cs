using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public List<Touch> touch = new();

    private void Update()
    {
        GetTouchPos();
    }

    private void GetTouchPos()
    {
        touch.Clear();

        for(int i=0; i<Input.touchCount; i++)
            touch.Add(Input.GetTouch(i));
    }
}
