using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TimeCounterWindow : EditorWindow
{
    [MenuItem("Window/Project Time Counter")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<TimeCounterWindow>("Project Time Counter");
    }

    void OnGUI()
    {
        GUILayout.Label("Total project time (without current session): " + TimeCounter.GetTotalProjectTime());
        GUILayout.Label("Current session time: " + TimeCounter.GetCurrentSessionTime());
    }
}
