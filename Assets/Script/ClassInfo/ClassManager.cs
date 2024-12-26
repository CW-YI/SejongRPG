using System.Collections.Generic;
using UnityEngine;

public class ClassManager : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab; // Point 프리팹
    [SerializeField] private TextAsset jsonData;     // JSON 데이터를 담을 TextAsset
    private float yOffset = 6f;    // Y축 간격
    private float startX = 0f;     // 시작 X 위치
    private float startY = 0f;     // 시작 Y 위치

    private Dictionary<string, lr_PointController> pointControllers = new Dictionary<string, lr_PointController>();

    void Start()
    {
        LoadPointsFromJson(jsonData.text);

        // 이미 완료된 과목 처리
        ActivateInitialPoints();
    }

    public void LoadPointsFromJson(string jsonString)
    {
        // JSON 데이터를 파싱하여 Class 리스트로 변환
        ClassesData data = JsonUtility.FromJson<ClassesData>(jsonString);

        float currentX = startX;
        float currentY = startY;

        // 모든 Class 데이터를 바탕으로 PointController 생성 및 배치
        foreach (var classData in data.classes)
        {
            CreatePoint(classData, ref currentX, ref currentY);
        }

        // Point 간의 연결 설정
        foreach (var classData in data.classes)
        {
            if (!pointControllers.TryGetValue(classData.id, out lr_PointController controller))
                continue;

            foreach (var connectedId in classData.otherPoints)
            {
                if (pointControllers.TryGetValue(connectedId, out lr_PointController connectedController))
                {
                    controller.connectedPoints.Add(connectedController.transform);
                }
            }
        }
    }

    private void CreatePoint(Class classData, ref float currentX, ref float currentY)
    {
        // 프리팹 생성 및 위치 설정
        Vector3 position = new Vector3(currentX, currentY, 0f);
        GameObject pointObj = Instantiate(pointPrefab, position, Quaternion.identity);
        lr_PointController controller = pointObj.GetComponent<lr_PointController>();

        // Point 데이터 설정
        controller.name = classData.id; // Unity 오브젝트 이름 설정
        controller.isCompleted = classData.isCompleted;
        controller.SetPointData(classData.id, classData.subjectName, classData.category, classData.credit); // 추가 데이터 설정

        // 생성된 PointController를 딕셔너리에 추가
        pointControllers[classData.id] = controller;

        // 아래로 일직선 배치
        currentY -= yOffset; // Y축으로만 간격을 둠
    }

    private void ActivateInitialPoints()
    {
        // 이미 완료된 과목의 연결된 과목 활성화
        foreach (var controller in pointControllers.Values)
        {
            if (controller.isCompleted)
            {
                foreach (var connectedTransform in controller.connectedPoints)
                {
                    lr_PointController connectedController = connectedTransform.GetComponent<lr_PointController>();
                    if (connectedController != null && !connectedController.isCompleted)
                    {
                        connectedController.isActivatable = true;
                    }
                }
            }
        }
    }
}

[System.Serializable]
public class ClassesData
{
    public List<Class> classes; // JSON 데이터의 Class 리스트
}
