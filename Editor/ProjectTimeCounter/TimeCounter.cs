using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[InitializeOnLoad]
public class TimeCounter
{
    private static DateTime startTime;
    private static List<TimeSpan> sessions = new List<TimeSpan>();
    private static string saveFilePath = Application.dataPath + "/Editor/ProjectTimeCounter/total_project_time.dat";

    static TimeCounter()
    {
        startTime = DateTime.Now;

        if (File.Exists(saveFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream reader = new FileStream(saveFilePath, FileMode.Open))
            {
                sessions = (List<TimeSpan>) bf.Deserialize(reader);
            }
        }

        Debug.Log("Total project time: " + GetFormattedTime(GetTotalProjectTime()));

        EditorApplication.wantsToQuit += OnQuit;
    }

    public static TimeSpan GetTotalProjectTime()
    {
        TimeSpan totalTime;
        foreach (TimeSpan session in sessions)
        {
            totalTime += session;
        }

        return totalTime;
    }

    public static TimeSpan GetCurrentSessionTime()
    {
        DateTime nowTime = DateTime.Now;
        TimeSpan sessionTime = (nowTime - startTime);
        return sessionTime;
    }

    public static string GetFormattedTime(TimeSpan time)
    {
        return string.Format("{0}h/{1}m/{2}s", time.Hours, time.Minutes, time.Seconds);
    }

    public static bool OnQuit()
    {
        TimeSpan sessionTime = GetCurrentSessionTime();
        sessions.Add(sessionTime);

        bool serialized = false;

        BinaryFormatter bf = new BinaryFormatter();

        using (FileStream writer = new FileStream(saveFilePath, FileMode.Create))
        {
            bf.Serialize(writer, sessions);
            serialized = true;
        }

        return serialized;
    }


}
