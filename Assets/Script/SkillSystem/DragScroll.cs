using UnityEngine;
using UnityEngine.UI; // UI Scrollbar�� ����Ϸ��� �ʿ�

public class DragScroll : MonoBehaviour
{
    public float dragSpeed = 0.5f;   // �巡�� �ӵ� ����
    public float minY = -25f;       // ī�޶� Y�� �ּҰ�
    public float maxY = 0f;         // ī�޶� Y�� �ִ밪
    public Scrollbar scrollBar;     // ������ UI Scrollbar
    private Vector2 startPos;       // ��ġ �Ǵ� ���콺 ���� ��ġ
    private bool isDragging = false; // �巡�� ���� Ȯ��

    void Start()
    {
        // ��ũ�ѹ� �ʱ�ȭ
        if (scrollBar != null)
        {
            scrollBar.onValueChanged.AddListener(OnScrollBarValueChanged);
            UpdateScrollBarValue(); // �ʱⰪ ����ȭ
        }
    }

    void Update()
    {
        if (ButtonManager.instance.isDragAvailable)
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
    }

    // �巡�� ó�� �� ī�޶� ��ġ ����
    private void HandleDrag(Vector2 currentPos)
    {
        Vector2 delta = currentPos - startPos;
        float moveY = -delta.y * dragSpeed * Time.deltaTime;

        // ī�޶� Y�� �̵� ��� (minY ~ maxY ����)
        float newY = Mathf.Clamp(transform.position.y + moveY, minY, maxY);

        // ī�޶� ��ġ ������Ʈ
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // ��ũ�ѹ� �� ������Ʈ
        UpdateScrollBarValue();
    }

    // ��ũ�ѹ� �� ������Ʈ (ī�޶� ��ġ �� ��ũ�ѹ� ��)
    private void UpdateScrollBarValue()
    {
        if (scrollBar != null)
        {
            // ī�޶��� Y ��ġ�� 0~1�� ����ȭ�� ������ ��ȯ
            float normalizedY = Mathf.InverseLerp(minY, maxY, transform.position.y);
            scrollBar.value = normalizedY;
        }
    }

    // ��ũ�ѹ� ���� ����Ǿ��� �� (��ũ�ѹ� �� �� ī�޶� ��ġ)
    public void OnScrollBarValueChanged(float value)
    {
        if (scrollBar != null)
        {
            // ��ũ�ѹ� ���� ī�޶� Y ��ġ�� ��ȯ
            float newY = Mathf.Lerp(minY, maxY, value);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
