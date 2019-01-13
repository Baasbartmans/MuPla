using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStart : MonoBehaviour
{

    // Use this to set up all classes that need to run when the game starts
    void Start()
    {
    #if ARDUINO
        new WebManager().Init(this);
    #endif

    }
}
