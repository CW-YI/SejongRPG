using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance; // 싱글톤 접근

    [SerializeField] private GameObject popupPanel; // 팝업 패널
    [SerializeField] private TextMeshProUGUI popupText;        // 팝업 텍스트
    [SerializeField] private Vector2 offset = new Vector2(10f, -10f); // 위치 오프셋 (패널이 포인트 아래에 위치하도록 설정)

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

        // Panel과 Canvas의 RectTransform 가져오기
        popupRectTransform = popupPanel.GetComponent<RectTransform>();
        canvas = popupPanel.GetComponentInParent<Canvas>();
    }

    public void ShowPopup(string message, Vector3 worldPosition)
    {
        // 텍스트 설정
        popupText.text = message;

        // 월드 좌표를 스크린 좌표로 변환
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // 스크린 좌표를 Canvas의 Local Position으로 변환
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(),
                screenPosition,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
                out Vector2 localPosition))
        {
            // Panel 위치 설정 (오프셋 적용)
            popupRectTransform.localPosition = localPosition + offset;

            // 팝업 패널 활성화
            popupPanel.SetActive(true);
        }
    }

    public void HidePopup()
    {
        // 팝업 비활성화
        popupPanel.SetActive(false);
    }
}
