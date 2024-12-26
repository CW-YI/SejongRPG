using System.IO;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class ReadFromExcel : MonoBehaviour
{
    ClassTotalInfo classInfo => ClassTotalInfo.instance;
    public TextAsset csvFile; // .xlsx 파일을 드래그 앤 드롭
    void Start()
    {
        if (csvFile == null)
        {
            Debug.LogError("CSV 파일이 연결되지 않았습니다.");
            return;
        }

        ReadCSV(csvFile.text);
    }

    void ReadCSV(string csvData)
    {
        // 줄 단위로 나누기
        string[] lines = csvData.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        var dataLines = lines.Skip(1); // LINQ의 Skip 사용

        foreach (string line in dataLines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue; // 빈 줄 건너뛰기

            // 콤마(,)로 셀 구분
            string[] cells = line.Split(',');
            //Debug.Log(string.Join(" | ", cells)); // 셀 내용을 출력

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