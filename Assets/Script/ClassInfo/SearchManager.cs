using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchManager : MonoBehaviour
{
    public TMP_InputField searchInputField; // 입력 필드
    public TextMeshProUGUI resultText; // 결과를 출력할 텍스트 UI
    ClassTotalInfo totalInfo => ClassTotalInfo.instance;

    void Start()
    {
        // InputField에 OnValueChanged 이벤트 등록
        searchInputField.onValueChanged.AddListener(SearchClasses);
    }

    // 검색 메서드
    void SearchClasses(string searchText)
    {
        // 검색 텍스트가 비어 있으면 초기화
        if (string.IsNullOrWhiteSpace(searchText))
        {
            resultText.text = "검색 결과가 없습니다.";
            return;
        }

        // 리스트에서 검색
        List<Class> searchResults = totalInfo.totalClasses.FindAll(c => c.subjectName.Contains(searchText));

        // 결과 출력
        if (searchResults.Count > 0)
        {
            resultText.text = "검색 결과:\n";
            foreach (var cls in searchResults)
            {
                resultText.text += $"- {cls.subjectName} ({cls.id})\n";
            }
        }
        else
        {
            resultText.text = "검색 결과가 없습니다.";
        }
    }
}
