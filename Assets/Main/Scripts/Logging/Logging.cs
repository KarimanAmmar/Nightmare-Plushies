using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Logging
{
    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void Log(object message) => UnityEngine.Debug.Log(message);
    public static void Warning(object message) => UnityEngine.Debug.LogWarning(message);
    public static void Error(object message) => UnityEngine.Debug.LogError(message);
}