using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class TimeCounter
{
    private static DateTime startTime;
    static TimeCounter()
    {
        startTime = DateTime.Now;
        UnityEngine.Debug.Log("Current time " + startTime);

        EditorApplication.update += Update;
        EditorApplication.wantsToQuit += OnQuit;
    }

    public static void Update()
    {
        Debug.Log("Apdejtuje");
    }

    public static bool OnQuit()
    {
        DateTime nowTime = DateTime.Now;
        TimeSpan sessionTime = (nowTime - startTime);
        Debug.Log("Unity quiting: " + sessionTime);
        return true;
    }


}
