using System;
using System.Collections.Generic;

public static class ArduinoInputs
{
    private static Dictionary<string, bool> buttons = new Dictionary<string, bool>
    {
        {"Fire", false },
        {"Jump", false}
    };

    public static void SetButton(string setValue, bool value)
    {
        if (buttons.ContainsKey(setValue))
        {
            buttons[setValue] = value;
        }
        else
        {
            throw new Exception("This key does not exist");
        }
    }

    public static bool GetButton(string key)
    {
        if (buttons.ContainsKey(key))
        {
            return buttons[key];
        }
        else
        {
            throw new Exception("This key does not exist");
        }
    }
}
