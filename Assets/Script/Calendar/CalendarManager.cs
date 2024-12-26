using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour
{
    public CSVReader csvReader;
    public GameObject calendarGrid;
    public GameObject dayPrefab;

    void Start()
    {
        List<CalendarEvent> events = csvReader.ReadCSVFiles();

        foreach (CalendarEvent calendarEvent in events)
        {
            GameObject day = Instantiate(dayPrefab, calendarGrid.transform);
            day.GetComponentInChildren<Text>().text = $"{calendarEvent.Date.ToShortDateString()}\n{calendarEvent.Event}";
        }
    }
}
