using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml.Serialization;

[InitializeOnLoad]
public class TimeCounter
{
    private static DateTime startTime;
    private static List<TimeSpan> sessions = new List<TimeSpan>();
    private static string saveFilePath = @"D:\sessions.xml";

    static TimeCounter()
    {
        startTime = DateTime.Now;
        UnityEngine.Debug.Log("Current time " + startTime);

        if (File.Exists(saveFilePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<TimeSpan>));
            using (StreamReader reader = new StreamReader(File.OpenRead(saveFilePath)))
            {
                sessions = (List<TimeSpan>) serializer.Deserialize(reader);
            }
        }

        foreach (TimeSpan session in sessions)
        {
            Debug.Log("Session: " + session);
        }

        EditorApplication.update += Update;
        EditorApplication.wantsToQuit += OnQuit;
    }

    public static void Update()
    {
        
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
        XmlSerializer serializer = new XmlSerializer(typeof(List<TimeSpan>));
        using (FileStream fileStream = new FileStream(saveFilePath, FileMode.Create))
        {
            serializer.Serialize(fileStream, sessions);
            serialized = true;
        }

        foreach (TimeSpan session in sessions)
        {
            Debug.Log("Sessions at end: " + session);
        }

        return false;
    }


}
