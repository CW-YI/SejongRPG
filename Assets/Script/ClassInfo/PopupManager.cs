using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance; // �̱��� ����

    [SerializeField] private GameObject popupPanel; // �˾� �г�
    [SerializeField] private TextMeshProUGUI popupText;        // �˾� �ؽ�Ʈ
    [SerializeField] private Vector2 offset = new Vector2(10f, -10f); // ��ġ ������ (�г��� ����Ʈ �Ʒ��� ��ġ�ϵ��� ����)

    private RectTransform popupRectTransform;
    private Canvas canvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Panel�� Canvas�� RectTransform ��������
        popupRectTransform = popupPanel.GetComponent<RectTransform>();
        canvas = popupPanel.GetComponentInParent<Canvas>();
    }

    public void ShowPopup(string message, Vector3 worldPosition)
    {
        // �ؽ�Ʈ ����
        popupText.text = message;

        // ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // ��ũ�� ��ǥ�� Canvas�� Local Position���� ��ȯ
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(),
                screenPosition,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
                out Vector2 localPosition))
        {
            // Panel ��ġ ���� (������ ����)
            popupRectTransform.localPosition = localPosition + offset;

            // �˾� �г� Ȱ��ȭ
            popupPanel.SetActive(true);
        }
    }

    public void HidePopup()
    {
        // �˾� ��Ȱ��ȭ
        popupPanel.SetActive(false);
    }
}
