using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public List<string> fileNames; // ���� ���� ���� �̸��� �����ϴ� ����Ʈ

    public List<CalendarEvent> ReadCSVFiles()
    {
        List<CalendarEvent> events = new List<CalendarEvent>();

        foreach (string fileName in fileNames)
        {
            TextAsset csvFile = Resources.Load<TextAsset>(fileName);
            if (csvFile != null)
            {
                string[] lines = csvFile.text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    string[] fields = line.Split(',');
                    DateTime date = DateTime.Parse(fields[0]);
                    string eventDescription = fields[1];
                    events.Add(new CalendarEvent { Date = date, Event = eventDescription });
                }
            }
            else
            {
                Debug.LogError("File not found: " + fileName);
            }
        }

        return events;
    }
}