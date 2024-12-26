using System.IO;
using UnityEngine;
using System;

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

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue; // �� �� �ǳʶٱ�

            // �޸�(,)�� �� ����
            string[] cells = line.Split(',');
            Debug.Log(string.Join(" | ", cells)); // �� ������ ���


        }
    }
}