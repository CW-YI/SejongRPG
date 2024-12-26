using UnityEngine;
using UnityEngine.UI; // UI Scrollbar를 사용하려면 필요

public class DragScroll : MonoBehaviour
{
    public float dragSpeed = 0.5f;   // 드래그 속도 조정
    public float minY = -25f;       // 카메라 Y축 최소값
    public float maxY = 0f;         // 카메라 Y축 최대값
    public Scrollbar scrollBar;     // 연결할 UI Scrollbar
    private Vector2 startPos;       // 터치 또는 마우스 시작 위치
    private bool isDragging = false; // 드래그 상태 확인

    void Start()
    {
        // 스크롤바 초기화
        if (scrollBar != null)
        {
            scrollBar.onValueChanged.AddListener(OnScrollBarValueChanged);
            UpdateScrollBarValue(); // 초기값 동기화
        }
    }

    void Update()
    {
        if (ButtonManager.instance.isDragAvailable)
        {
            // 터치 입력 처리
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPos = touch.position;
                        isDragging = true;
                        break;

                    case TouchPhase.Moved:
                        if (isDragging)
                        {
                            HandleDrag(touch.position);
                            startPos = touch.position;
                        }
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        isDragging = false;
                        break;
                }
            }

            // 마우스 입력 처리
            if (Input.GetMouseButtonDown(0)) // 마우스 클릭 시작
            {
                startPos = Input.mousePosition;
                isDragging = true;
            }
            else if (Input.GetMouseButton(0) && isDragging) // 마우스 드래그 중
            {
                HandleDrag(Input.mousePosition);
                startPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0)) // 마우스 클릭 종료
            {
                isDragging = false;
            }
        }
    }

    // 드래그 처리 및 카메라 위치 제한
    private void HandleDrag(Vector2 currentPos)
    {
        Vector2 delta = currentPos - startPos;
        float moveY = -delta.y * dragSpeed * Time.deltaTime;

        // 카메라 Y축 이동 계산 (minY ~ maxY 제한)
        float newY = Mathf.Clamp(transform.position.y + moveY, minY, maxY);

        // 카메라 위치 업데이트
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // 스크롤바 값 업데이트
        UpdateScrollBarValue();
    }

    // 스크롤바 값 업데이트 (카메라 위치 → 스크롤바 값)
    private void UpdateScrollBarValue()
    {
        if (scrollBar != null)
        {
            // 카메라의 Y 위치를 0~1의 정규화된 값으로 변환
            float normalizedY = Mathf.InverseLerp(minY, maxY, transform.position.y);
            scrollBar.value = normalizedY;
        }
    }

    // 스크롤바 값이 변경되었을 때 (스크롤바 값 → 카메라 위치)
    public void OnScrollBarValueChanged(float value)
    {
        if (scrollBar != null)
        {
            // 스크롤바 값을 카메라 Y 위치로 변환
            float newY = Mathf.Lerp(minY, maxY, value);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
