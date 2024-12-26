using System.IO;
using UnityEngine;
using System;

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

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue; // 빈 줄 건너뛰기

            // 콤마(,)로 셀 구분
            string[] cells = line.Split(',');
            Debug.Log(string.Join(" | ", cells)); // 셀 내용을 출력


        }
    }
}