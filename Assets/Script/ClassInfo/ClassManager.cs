using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ClassManager : MonoBehaviour
{
    public List<Class> points; // ��� Point ������ ���� ����Ʈ

    // JSON ���Ͽ��� �����͸� �ε��ϴ� �Լ�
    public void LoadPointsFromJson(string jsonString)
    {
        points = JsonUtility.FromJson<ClassListWrapper>(jsonString).points;
    }

    // Ư�� Point�� ������ �������� �Լ� (����)
    public Class GetPointByName(string subjectName)
    {
        return points.Find(point => point.subjectName == subjectName);
    }
}

// JSON �����͸� �Ľ��ϱ� ���� ���� Ŭ����
[System.Serializable]
public class ClassListWrapper
{
    public List<Class> points;
}
