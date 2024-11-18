using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputValueManager : Singleton<InputValueManager>
{
    public Vector3 moveDir;

    public UnityEvent attackActions;
}
