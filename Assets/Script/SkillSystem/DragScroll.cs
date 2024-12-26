using UnityEngine;

public class DragScroll : MonoBehaviour
{
    public float dragSpeed = 0.5f; // 드래그 속도 조정
    private float minY = -25f;       // 카메라 Y축 최소값
    private float maxY = 0f;      // 카메라 Y축 최대값
    private Vector2 startPos;     // 터치 또는 마우스 시작 위치
    private bool isDragging = false; // 드래그 상태 확인

    void Update()
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

    // 드래그 처리 및 카메라 위치 제한
    private void HandleDrag(Vector2 currentPos)
    {
        Vector2 delta = currentPos - startPos;
        float moveY = -delta.y * dragSpeed * Time.deltaTime;

        // 현재 카메라 위치 계산
        float newY = Mathf.Clamp(transform.position.y + moveY, minY, maxY);

        // 새로운 위치로 이동
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
