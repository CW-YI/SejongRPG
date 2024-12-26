using System.IO;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class ReadFromExcel : MonoBehaviour
{
    ClassTotalInfo classInfo => ClassTotalInfo.instance;
    public TextAsset csvFile; // .xlsx ������ �巡�� �� ���
    void Start()
    {
        if (csvFile == null)
        {
            Debug.LogError("CSV ������ ������� �ʾҽ��ϴ�.");
            return;
        }

        ReadCSV(csvFile.text);
    }

    void ReadCSV(string csvData)
    {
        // �� ������ ������
        string[] lines = csvData.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        var dataLines = lines.Skip(1); // LINQ�� Skip ���

        foreach (string line in dataLines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue; // �� �� �ǳʶٱ�

            // �޸�(,)�� �� ����
            string[] cells = line.Split(',');
            //Debug.Log(string.Join(" | ", cells)); // �� ������ ���

            string major = cells[0];
            string id = cells[1];
            string subjectName = cells[2];
            string category = cells[3];
            Debug.Log(cells[4]);
            int grade = int.Parse(cells[4]);
            int credit = (int)(float.Parse(cells[5]));
            bool isCompleted = false;
            List<string> otherPoints = new List<string>();

            Class newClass = new Class(major, id, subjectName, category, grade, credit, isCompleted, otherPoints);
            classInfo.totalClasses.Add(newClass);
        }
    }
}