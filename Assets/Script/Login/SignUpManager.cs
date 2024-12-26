using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SignUpManager : MonoBehaviour
{
    public InputField nameInputField;
    public InputField idInputField;
    public InputField passwordInputField;
    public InputField passwordIdentifyInputField;
    public InputField userGradeInputField;
    public InputField creditSumInputField;
    public Dropdown majorDropdown; // Dropdown �߰�
    public Toggle majorTransferToggle;
    public Button signUpButton;
    public Button backButton;
    public Text mismatchText;
    public Text passwordMismatchText; // ���ο� �ؽ�Ʈ

    private string signUpUrl = "http://sejongrpg.duckdns.org:3000/users"; // �鿣�� ������ ȸ������ ��������Ʈ URL

    // Start is called before the first frame update
    void Start()
    {
        signUpButton.onClick.AddListener(OnSignUpButtonClick);
        backButton.onClick.AddListener(OnBackButtonClick); // Back ��ư Ŭ�� �̺�Ʈ �߰�
        mismatchText.gameObject.SetActive(false);
        passwordMismatchText.gameObject.SetActive(false); // ���ο� �ؽ�Ʈ �ʱ�ȭ
    }

    private void OnSignUpButtonClick()
    {
        if (passwordInputField.text != passwordIdentifyInputField.text)
        {
            passwordMismatchText.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.\n��й�ȣ�� �����ϼ���.";
            passwordMismatchText.gameObject.SetActive(true);
            return;
        }
        passwordMismatchText.gameObject.SetActive(false);

        string name = nameInputField.text;
        string id = idInputField.text;
        string password = passwordInputField.text;
        int userGrade = int.Parse(userGradeInputField.text);
        int creditSum = int.Parse(creditSumInputField.text);
        string major = majorDropdown.options[majorDropdown.value].text; // Dropdown���� ���õ� �� ��������
        bool majorTransfer = majorTransferToggle.isOn;

        User user = new User
        {
            name = name,
            id = id,
            passwd = password,
            userGrade = userGrade,
            creditSum = creditSum,
            major = major,
            majorTransfer = majorTransfer
        };

        SendUserData(user);
    }

    public void SendUserData(User user)
    {
        StartCoroutine(SendUserDataCoroutine(user));
    }
    private void OnBackButtonClick()
    {
        SceneManager.LoadScene("Login"); // �α��� ������ ��ȯ
    }
    private IEnumerator SendUserDataCoroutine(User user)
    {
        string jsonData = JsonUtility.ToJson(user);
        Debug.Log("JSON Data: " + jsonData);

        UnityWebRequest request = new UnityWebRequest(signUpUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Response: " + request.downloadHandler.text);
            ProcessServerResponse(request.downloadHandler.text);
        }
    }

    private void ProcessServerResponse(string jsonResponse)
    {
        try
        {
            User user = JsonUtility.FromJson<User>(jsonResponse);
            Debug.Log("User data received:");
            Debug.Log("Name: " + user.name);
            Debug.Log("ID: " + user.id);
            Debug.Log("User Grade: " + user.userGrade);
            Debug.Log("Credit Sum: " + user.creditSum);
            Debug.Log("Major: " + user.major);
            Debug.Log("Major Transfer: " + user.majorTransfer);
            // ȸ�������� ���������� �Ϸ�Ǹ� �α��� ������ ��ȯ
            SceneManager.LoadScene("Login");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("JSON Parsing Error: " + ex.Message);
        }
    }
}
