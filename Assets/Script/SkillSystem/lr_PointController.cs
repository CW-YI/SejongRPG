using System.Collections.Generic;
using UnityEngine;

public class lr_PointController : MonoBehaviour
{
    [SerializeField] public List<Transform> connectedPoints; // 연결된 포인트들
    [SerializeField] private GameObject linePrefab;          // Line Renderer 프리팹
    [SerializeField] private int incomingCount = 0;          // 진입 간선 수
    [SerializeField] public bool isCompleted;               // 수업 완료 여부
    [SerializeField] public bool isActivatable;            // 활성화 가능 여부
    [SerializeField] private string subjectName;         // 교과명
    [SerializeField] private string id;                  // 교과명(영어)
    [SerializeField] private string category;            // 과목 구분
    [SerializeField] private int credit;               // 학점

    private List<GameObject> lines = new List<GameObject>(); // 생성된 Line Renderer 관리
    private SpriteRenderer spriteRenderer;                   // 포인트의 스프라이트 렌더러
    
    private void Start()
    {
        // 스프라이트 렌더러 가져오기 (필요 시 사용)
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 현재 포인트 색상 업데이트
        UpdatePointColor();

        // 연결된 정점들에 대해 Line 생성
        foreach (var pointTransform in connectedPoints)
        {
            lr_PointController point = pointTransform.GetComponent<lr_PointController>();
            if (point != null)
            {
                CreateLine(pointTransform); // Transform을 Line 생성에 사용
                point.IncrementIncomingCount(); // PointController 메서드 호출
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
        // Line Renderer 프리팹 생성
        GameObject lineObj = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, transform);
        LineRenderer line = lineObj.GetComponent<LineRenderer>();

        // Line 설정
        line.positionCount = 2;
        line.SetPosition(0, transform.position);    // 시작점: 현재 노드
        line.SetPosition(1, targetPoint.position); // 끝점: 연결된 노드

        // Line Renderer 색상 변경
        Color lineColor = isCompleted ? Color.white : Color.gray;
        line.startColor = lineColor;
        line.endColor = lineColor;

        lines.Add(lineObj); // 생성된 Line Renderer를 리스트에 저장
    }

    public void UpdatePointColor()
    {
        // 포인트의 색상 변경 (스프라이트가 있을 경우)
        if (spriteRenderer != null)
        {
            if (isCompleted)
                spriteRenderer.color = Color.yellow;
            else if (isActivatable)
                spriteRenderer.color = Color.white;
            else
                spriteRenderer.color = Color.gray; 
        }

        // 연결된 Line Renderer 색상 업데이트
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
        // 연결된 포인트들도 상태를 업데이트 (옵션)
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
        // 포인트 클릭 시 상태 변경
        if (isActivatable && !isCompleted)
        {
            SetCompletionStatus(true);
        }
    }
    private void OnMouseEnter()
    {
        // Point 정보로 팝업 표시
        string message = $"Name : {id}\nCategory : {category}\nCredit : {credit}";
        PopupManager.Instance.ShowPopup(message, transform.position);
    }

    private void OnMouseExit()
    {
        // 팝업 숨기기
        PopupManager.Instance.HidePopup();
    }
}