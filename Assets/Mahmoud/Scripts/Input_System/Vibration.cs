using UnityEngine;
using System.Runtime.InteropServices;

public class Vibration
{
#if UNITY_ANDROID && !UNITY_EDITOR
    private static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    private static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    private static AndroidJavaClass vibrationPlugin = new AndroidJavaClass("com.yourcompany.vibrationplugin.VibrationPlugin");

    public static void Vibrate(long milliseconds)
    {
        vibrationPlugin.CallStatic("Vibrate", milliseconds);
    }

    public static void VibratePattern(long[] pattern, int repeat)
    {
        vibrationPlugin.CallStatic("VibratePattern", pattern, repeat);
    }
#else
	public static void Vibrate(long milliseconds) { }
	public static void VibratePattern(long[] pattern, int repeat) { }
#endif
}
