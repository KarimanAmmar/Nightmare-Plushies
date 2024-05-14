using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Logging
{
    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void Log(object message)
    {
        UnityEngine.Debug.Log(message);
    }
}
