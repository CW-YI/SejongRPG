using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchManager : MonoBehaviour
{
    public TMP_InputField searchInputField; // �Է� �ʵ�
    public TextMeshProUGUI resultText; // ����� ����� �ؽ�Ʈ UI
    ClassTotalInfo totalInfo => ClassTotalInfo.instance;

    void Start()
    {
        // InputField�� OnValueChanged �̺�Ʈ ���
        searchInputField.onValueChanged.AddListener(SearchClasses);
    }

    // �˻� �޼���
    void SearchClasses(string searchText)
    {
        // �˻� �ؽ�Ʈ�� ��� ������ �ʱ�ȭ
        if (string.IsNullOrWhiteSpace(searchText))
        {
            resultText.text = "�˻� ����� �����ϴ�.";
            return;
        }

        // ����Ʈ���� �˻�
        List<Class> searchResults = totalInfo.totalClasses.FindAll(c => c.subjectName.Contains(searchText));

        // ��� ���
        if (searchResults.Count > 0)
        {
            resultText.text = "�˻� ���:\n";
            foreach (var cls in searchResults)
            {
                resultText.text += $"- {cls.subjectName} ({cls.id})\n";
            }
        }
        else
        {
            resultText.text = "�˻� ����� �����ϴ�.";
        }
    }
}
