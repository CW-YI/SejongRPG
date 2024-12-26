using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ClassManager : MonoBehaviour
{
    public List<Class> points; // 모든 Point 정보를 담은 리스트

    // JSON 파일에서 데이터를 로드하는 함수
    public void LoadPointsFromJson(string jsonString)
    {
        points = JsonUtility.FromJson<ClassListWrapper>(jsonString).points;
    }

    // 특정 Point의 정보를 가져오는 함수 (예시)
    public Class GetPointByName(string subjectName)
    {
        return points.Find(point => point.subjectName == subjectName);
    }
}

// JSON 데이터를 파싱하기 위한 래퍼 클래스
[System.Serializable]
public class ClassListWrapper
{
    public List<Class> points;
}
