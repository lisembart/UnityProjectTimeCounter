using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[InitializeOnLoad]
public class TimeCounter
{
    private static DateTime startTime;
    private static List<TimeSpan> sessions = new List<TimeSpan>();
    private static string saveFilePath = @"D:\sessions.dat";

    static TimeCounter()
    {
        startTime = DateTime.Now;
        UnityEngine.Debug.Log("Current time " + startTime);

        if (File.Exists(saveFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream reader = new FileStream(saveFilePath, FileMode.Open))
            {
                sessions = (List<TimeSpan>) bf.Deserialize(reader);
            }
        }

        Debug.Log("Total project time: " + GetTotalProjectTime());

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

    public static bool OnQuit()
    {
        DateTime nowTime = DateTime.Now;
        TimeSpan sessionTime = (nowTime - startTime);
        sessions.Add(sessionTime);
        Debug.Log("Unity quiting, session time: " + sessionTime);
        TimeSpan projectTIme;
        foreach (TimeSpan session in sessions)
        {
            projectTIme += session;
        }
        Debug.Log("Total time: " + projectTIme);

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
