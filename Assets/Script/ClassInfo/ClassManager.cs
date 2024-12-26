using System.Collections.Generic;
using UnityEngine;

public class ClassManager : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab; // Point ������
    [SerializeField] private TextAsset jsonData;     // JSON �����͸� ���� TextAsset
    private float yOffset = 6f;    // Y�� ����
    private float startX = 0f;     // ���� X ��ġ
    private float startY = 0f;     // ���� Y ��ġ

    private Dictionary<string, lr_PointController> pointControllers = new Dictionary<string, lr_PointController>();

    void Start()
    {
        LoadPointsFromJson(jsonData.text);

        // �̹� �Ϸ�� ���� ó��
        ActivateInitialPoints();
    }

    public void LoadPointsFromJson(string jsonString)
    {
        // JSON �����͸� �Ľ��Ͽ� Class ����Ʈ�� ��ȯ
        ClassesData data = JsonUtility.FromJson<ClassesData>(jsonString);

        float currentX = startX;
        float currentY = startY;

        // ��� Class �����͸� �������� PointController ���� �� ��ġ
        foreach (var classData in data.classes)
        {
            CreatePoint(classData, ref currentX, ref currentY);
        }

        // Point ���� ���� ����
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
        // ������ ���� �� ��ġ ����
        Vector3 position = new Vector3(currentX, currentY, 0f);
        GameObject pointObj = Instantiate(pointPrefab, position, Quaternion.identity);
        lr_PointController controller = pointObj.GetComponent<lr_PointController>();

        // Point ������ ����
        controller.name = classData.id; // Unity ������Ʈ �̸� ����
        controller.isCompleted = classData.isCompleted;
        controller.SetPointData(classData.id, classData.subjectName, classData.category, classData.credit); // �߰� ������ ����

        // ������ PointController�� ��ųʸ��� �߰�
        pointControllers[classData.id] = controller;

        // �Ʒ��� ������ ��ġ
        currentY -= yOffset; // Y�����θ� ������ ��
    }

    private void ActivateInitialPoints()
    {
        // �̹� �Ϸ�� ������ ����� ���� Ȱ��ȭ
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
    public List<Class> classes; // JSON �������� Class ����Ʈ
}
