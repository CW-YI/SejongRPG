using System.Collections.Generic;
using UnityEngine;

public class lr_PointController : MonoBehaviour
{
    [SerializeField] public List<Transform> connectedPoints; // ����� ����Ʈ��
    [SerializeField] private GameObject linePrefab;          // Line Renderer ������
    [SerializeField] private int incomingCount = 0;          // ���� ���� ��
    [SerializeField] public bool isCompleted;               // ���� �Ϸ� ����
    [SerializeField] public bool isActivatable;            // Ȱ��ȭ ���� ����
    [SerializeField] private string subjectName;         // ������
    [SerializeField] private string id;                  // ������(����)
    [SerializeField] private string category;            // ���� ����
    [SerializeField] private int credit;               // ����

    private List<GameObject> lines = new List<GameObject>(); // ������ Line Renderer ����
    private SpriteRenderer spriteRenderer;                   // ����Ʈ�� ��������Ʈ ������
    
    private void Start()
    {
        // ��������Ʈ ������ �������� (�ʿ� �� ���)
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ���� ����Ʈ ���� ������Ʈ
        UpdatePointColor();

        // ����� �����鿡 ���� Line ����
        foreach (var pointTransform in connectedPoints)
        {
            lr_PointController point = pointTransform.GetComponent<lr_PointController>();
            if (point != null)
            {
                CreateLine(pointTransform); // Transform�� Line ������ ���
                point.IncrementIncomingCount(); // PointController �޼��� ȣ��
            }
        }

        if (incomingCount == 0) isActivatable = true;
    }
    public void SetPointData(string id, string name, string category, int credit)
    {
        this.id = id;
        this.subjectName = name;
        this.category = category;
        this.credit = credit;
    }
    public void IncrementIncomingCount()
    {
        incomingCount++;
    }

    private void CreateLine(Transform targetPoint)
    {
        // Line Renderer ������ ����
        GameObject lineObj = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, transform);
        LineRenderer line = lineObj.GetComponent<LineRenderer>();

        // Line ����
        line.positionCount = 2;
        line.SetPosition(0, transform.position);    // ������: ���� ���
        line.SetPosition(1, targetPoint.position); // ����: ����� ���

        // Line Renderer ���� ����
        Color lineColor = isCompleted ? Color.white : Color.gray;
        line.startColor = lineColor;
        line.endColor = lineColor;

        lines.Add(lineObj); // ������ Line Renderer�� ����Ʈ�� ����
    }

    public void UpdatePointColor()
    {
        // ����Ʈ�� ���� ���� (��������Ʈ�� ���� ���)
        if (spriteRenderer != null)
        {
            if (isCompleted)
                spriteRenderer.color = Color.yellow;
            else if (isActivatable)
                spriteRenderer.color = Color.white;
            else
                spriteRenderer.color = Color.gray; 
        }

        // ����� Line Renderer ���� ������Ʈ
        foreach (var lineObj in lines)
        {
            LineRenderer line = lineObj.GetComponent<LineRenderer>();
            Color lineColor = isCompleted ? Color.white : Color.gray;
            line.startColor = lineColor;
            line.endColor = lineColor;
        }
    }

    public void SetCompletionStatus(bool completed)
    {
        isCompleted = completed;
        SetActiveStatus();
        UpdatePointColor();
    }

    void SetActiveStatus()
    {
        // ����� ����Ʈ�鵵 ���¸� ������Ʈ (�ɼ�)
        foreach (var point in connectedPoints)
        {
            lr_PointController connectedPoint = point.GetComponent<lr_PointController>();
            if (connectedPoint != null && !connectedPoint.isCompleted)
            {
                connectedPoint.isActivatable = true;
                connectedPoint.UpdatePointColor();
                UpdatePointColor();
            }
        }
    }

    private void OnMouseDown()
    {
        // ����Ʈ Ŭ�� �� ���� ����
        if (isActivatable && !isCompleted)
        {
            SetCompletionStatus(true);
        }
    }
    private void OnMouseEnter()
    {
        // Point ������ �˾� ǥ��
        string message = $"Name : {id}\nCategory : {category}\nCredit : {credit}";
        PopupManager.Instance.ShowPopup(message, transform.position);
    }

    private void OnMouseExit()
    {
        // �˾� �����
        PopupManager.Instance.HidePopup();
    }
}