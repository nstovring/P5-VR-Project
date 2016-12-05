using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class StringLogger : MonoBehaviour {

    static StringLogger Instance;
    string message;
    string loadMessage = "Yeah working";
    string data;
    FileInfo f;
    string fileName = "TimeStamps.txt";
    void Start()
    {
        Instance = this;
        if (File.Exists(Application.persistentDataPath + "\\" + fileName))
        {
            Debug.Log(fileName + " already exists.");
            return;
        }
        var sr = File.CreateText(Application.persistentDataPath + "\\" + fileName);
        sr.Close();
    }


    public static void AddTimeStamp(string when)
    {
        var sr = File.AppendText(Application.persistentDataPath + "\\" + "TimeStamps.txt");
        sr.WriteLine( "\n" + "Current Time: " + DateTimeToUnixTimestamp(DateTime.Now) +" "+ when + "\n");
        sr.Close();
    }

    public static double DateTimeToUnixTimestamp(DateTime dateTime)
    {
        return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
               new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
    }

    void CreateFile()
    {
        f = new FileInfo(Application.persistentDataPath + "\\" + "myFile.txt");
    }
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, 500, 500));
        GUILayout.Label(message + " " + data);
        if (GUILayout.Button("Save"))
        {

            if (!f.Exists)
            {
                message = "Creating File";
                Save();
            }
            else
            {
                message = "Saving";
                Save();
            }
        }
        if (GUILayout.Button("Load"))
        {
            if (f.Exists)
            {
                Load();
            }
            else
            {
                message = "No File found";
            }
        }
        GUILayout.EndArea();
    }

    void Save()
    {
        StreamWriter w;
        if (!f.Exists)
        {
            w = f.CreateText();
        }
        else
        {
            f.Delete();
            w = f.CreateText();
        }
        w.WriteLine(loadMessage);
        w.Close();
    }

    void Load()
    {
        StreamReader r = File.OpenText(Application.dataPath + "\\" + "myFile.txt");
        string info = r.ReadToEnd();
        r.Close();
        data = info;
    }
}
