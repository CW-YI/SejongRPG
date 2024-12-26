using UnityEngine;

public class DragScroll : MonoBehaviour
{
    public float dragSpeed = 0.5f; // �巡�� �ӵ� ����
    private float minY = -25f;       // ī�޶� Y�� �ּҰ�
    private float maxY = 0f;      // ī�޶� Y�� �ִ밪
    private Vector2 startPos;     // ��ġ �Ǵ� ���콺 ���� ��ġ
    private bool isDragging = false; // �巡�� ���� Ȯ��

    void Update()
    {
        // ��ġ �Է� ó��
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

        // ���콺 �Է� ó��
        if (Input.GetMouseButtonDown(0)) // ���콺 Ŭ�� ����
        {
            startPos = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButton(0) && isDragging) // ���콺 �巡�� ��
        {
            HandleDrag(Input.mousePosition);
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0)) // ���콺 Ŭ�� ����
        {
            isDragging = false;
        }
    }

    // �巡�� ó�� �� ī�޶� ��ġ ����
    private void HandleDrag(Vector2 currentPos)
    {
        Vector2 delta = currentPos - startPos;
        float moveY = -delta.y * dragSpeed * Time.deltaTime;

        // ���� ī�޶� ��ġ ���
        float newY = Mathf.Clamp(transform.position.y + moveY, minY, maxY);

        // ���ο� ��ġ�� �̵�
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
