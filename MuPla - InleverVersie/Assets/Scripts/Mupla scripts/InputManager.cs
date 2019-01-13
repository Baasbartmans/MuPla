using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#region Manager 

public class InputManager
{
    public static IInput CreateInput()
    {
        #if ARDUINO
            return new ArduinoInput();
        #else
            return new UnityInput();
        #endif
    }
}

#endregion

#region Interface

public interface IInput
{
    bool GetButton(string key);
}

#endregion

#region Unity Input

public class UnityInput : IInput

{
    public bool GetButton(string key)
    {
        return Input.GetButton(key);
    }
}

#endregion

#region Arduino Input
public class ArduinoInput : IInput
{
    public bool GetButton(string key)
    {
        return ArduinoInputs.GetButton(key);
    }
}
#endregion


